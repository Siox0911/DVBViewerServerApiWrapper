using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Eine Aufnahme aus dem Pool aller existierender Aufnahmen.
    /// </summary>
    [XmlRoot(ElementName = "recording")]
    public class RecordingItem
    {
        /// <summary>
        /// Die ID der Aufnahme
        /// </summary>
        [XmlAttribute(AttributeName = "id")]
        public int ID { get; set; }
        /// <summary>
        /// 255 bedeutet, der Text ist in UTF-8.
        /// http://www.dvbviewer.tv/forum/topic/37211-recording-service-api/?tab=comments#comment-272680
        /// </summary>
        [XmlAttribute(AttributeName = "charset")]
        public int Charset { get; set; }
        /// <summary>
        /// Habe ich noch nicht ganz gerafft
        /// </summary>
        [XmlAttribute(AttributeName = "content")]
        public int Content { get; set; }
        /// <summary>
        /// Die Start Zeit als YYMMDDHHMMSS. Wenn ein richtiges Datum genommen werden soll, nimm <seealso cref="RecDate"/>.
        /// </summary>
        [XmlAttribute(AttributeName = "start")]
        public long StartDatum { get; set; }
        /// <summary>
        /// Die Start Zeit als Datum
        /// </summary>
        public DateTime RecDate
        {
            get
            {
                if (StartDatum != 0)
                    return DateTime.Parse(StartDatum.ToString("0000-00-00 00:00:00"));

                return default(DateTime);
            }
        }
        /// <summary>
        /// Die Länge der Aufnahme HHMMSS, verwende <seealso cref="Duration2"/>.
        /// </summary>
        [XmlAttribute(AttributeName = "duration")]
        public long Duration { get; set; }
        /// <summary>
        /// Die Länge der Aufnahme als TimeSpan.
        /// </summary>
        public TimeSpan Duration2
        {
            get
            {
                return TimeSpan.Parse(Duration.ToString("00:00:00"));
            }
        }
        /// <summary>
        /// Die EventID der Aufnahme (EPG)
        /// </summary>
        [XmlAttribute(AttributeName = "eventid")]
        public int EventID { get; set; }
        /// <summary>
        /// Der Aufnahmekanal als Text
        /// </summary>
        [XmlElement(ElementName = "channel", Type = typeof(RecordingChannel))]
        public RecordingChannel Channel { get; set; }
        /// <summary>
        /// Der Dateinahme als Pfad wie er im Service festgelegt wurde.
        /// </summary>
        [XmlElement(ElementName = "file")]
        public string File { get; set; }
        /// <summary>
        /// Der Aufnahme Titel
        /// </summary>
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        /// <summary>
        /// Der Untertitel des Titels oder Kurzbeschreibung
        /// </summary>
        [XmlElement(ElementName = "info")]
        public string Info { get; set; }
        /// <summary>
        /// Beschreibung aus dem EPG
        /// </summary>
        [XmlElement(ElementName = "desc")]
        public string Description { get; set; }
        /// <summary>
        /// Die Serie falls festgelegt
        /// </summary>
        [XmlElement(ElementName = "series", Type = typeof(RecordingSeries))]
        public RecordingSeries Series { get; set; }
        /// <summary>
        /// Die Bilddatei (Thumpnail) falls erlaubt
        /// </summary>
        [XmlElement(ElementName = "image")]
        public string Image { get; set; }

        internal RecordingItem() { }

        /// <summary>
        /// Spiel diese Aufnahme auf einem Clienten (DVBViewer) ab, sofern dieser connected ist.
        /// </summary>
        /// <param name="dVBViewerClient"></param>
        /// <returns></returns>
        public Task<HttpStatusCode> Play(DVBViewerClient dVBViewerClient)
        {
            return dVBViewerClient.PlayRecording(this);
        }

        /// <summary>
        /// Führt eine Aktualisierung der Aufnahme im Media Server durch. Geändert wird:
        /// Title, Info, Series, Channel und Description
        /// </summary>
        /// <returns></returns>
        public async Task<HttpStatusCode> Update()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                return await dvbApi.SendPostDataAsync("rec_edit", new List<Helper.UriParameter>
                {
                    new Helper.UriParameter("recid", ID.ToString()),
                    new Helper.UriParameter("title", Title),
                    new Helper.UriParameter("event", Info ?? ""),
                    new Helper.UriParameter("Series", Series.Name),
                    new Helper.UriParameter("Channel", Channel.Name),
                    new Helper.UriParameter("details", Description),
                    new Helper.UriParameter("chkinfofile", 1.ToString()),
                    new Helper.UriParameter("chkfileinfo", 1.ToString()),
                    new Helper.UriParameter("btnsave", 1.ToString())
                }).ConfigureAwait(false);
            }
            return 0;
        }

        /// <summary>
        /// Löscht diese Aufnahme, gibt den Code 423 zurück, wenn eine Löschnung nicht funktioniert hat.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpStatusCode> Delete()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                return await dvbApi.SendDataAsync("recdelete", new List<Helper.UriParameter>
                {
                    new Helper.UriParameter("recid", ID.ToString()),
                    new Helper.UriParameter("delfile", 1.ToString())
                }).ConfigureAwait(false);
            }
            return 0;
        }

        /// <summary>
        /// Gibt den String zurück, der verwendet wird, bevor der UPnP String in die Datei m3u geschrieben wird.
        /// Beginnend mit #EXTINF:
        /// </summary>
        /// <returns></returns>
        internal string GetM3uPrefString()
        {
            return $"#EXTINF:{Duration2.TotalSeconds},{Title} - {Info}";
        }

        /// <summary>
        /// Gibt eine URL zurück, welche die Aufnahme auf einen UPnP Gerät abspielen lässt.
        /// </summary>
        /// <returns></returns>
        public string GetUPnPUriString()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            var extension = System.IO.Path.GetExtension(File);
            return $"http://{dvbApi.Hostname}:8090/upnp/recording/{ID}{extension}";
        }

        /// <summary>
        /// Erzeugt aus der Liste der Videos eine M3U Datei. Die Datei befindet sich normalerweise im Tempverzeichnis
        /// </summary>
        /// <returns>Ein Pfad zur m3u Datei</returns>
        public string CreateM3UFile()
        {
            var tPath = System.IO.Path.GetTempPath();

            var fName = $"{ID}.m3u";
            var cPathName = tPath + fName;
            using (var fStream = new System.IO.FileStream(cPathName, System.IO.FileMode.OpenOrCreate))
            {
                using (var sw = new System.IO.StreamWriter(fStream))
                {
                    sw.WriteLine(GetM3uPrefString());
                    sw.WriteLine(GetUPnPUriString());
                }
            }
            return cPathName;
        }

    }
}
