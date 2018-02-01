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
        /// Die Start Zeit als YYMMDDHHMMSS
        /// </summary>
        [XmlAttribute(AttributeName = "start")]
        public long StartDatum { get; set; }
        /// <summary>
        /// Die Start Zeit als Datum in YY:MM:DD:HH:MM:SS
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
        /// Die Länge der Aufnahme HHMMSS
        /// </summary>
        [XmlAttribute(AttributeName = "duration")]
        public long Duration { get; set; }
        /// <summary>
        /// Die Länge der Aufnahme HH:MM:SS
        /// </summary>
        public string SDuration
        {
            get
            {
                return Duration.ToString("00:00:00");
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
        [XmlElement(ElementName = "channel")]
        public string Channel { get; set; }
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
        /// Gibt den String zurück, der verwendet wird, bevor der UPnP String in die Datei m3u geschrieben wird.
        /// Beginnend mit #EXTINF:
        /// </summary>
        /// <returns></returns>
        internal string GetM3uPrefString()
        {
            var tspan = TimeSpan.Parse(SDuration);
            return $"#EXTINF:{tspan.TotalSeconds},{Title}";
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
