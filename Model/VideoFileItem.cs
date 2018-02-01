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
    /// Eine Videodatei im Media Server
    /// </summary>
    [XmlRoot(ElementName = "row")]
    public class VideoFileItem
    {
        /// <summary>
        /// Die Object ID des Videos. 
        /// </summary>
        [XmlElement(ElementName = "OBJECT_ID")]
        public int ObjectID { get; set; }

        /// <summary>
        /// Die Details ID des Videos. Für weitere Details wird das benötigt
        /// </summary>
        [XmlElement(ElementName = "DETAILS_ID")]
        public int ObjectDetailsID { get; set; }

        /// <summary>
        /// Die Pfad ID des Servers. Verwendung um zum Beispiel noch andere Videos im Pfad zu finden.
        /// </summary>
        [XmlElement(ElementName = "IDPATH")]
        public int PathID { get; set; }

        /// <summary>
        /// Der Dateiname des Videos
        /// </summary>
        [XmlElement(ElementName = "FILENAME")]
        public string FileName { get; set; }

        /// <summary>
        /// Der Pfad indem das Video gespeichert ist
        /// </summary>
        [XmlElement(ElementName = "PATH")]
        public string Path { get; set; }

        /// <summary>
        /// Der Kanal auf dem das Video aufgenommen wurde, falls vorhanden
        /// </summary>
        [XmlElement(ElementName = "CHANNEL")]
        public string Channel { get; set; }

        /// <summary>
        /// Datum der letzten Änderung
        /// </summary>
        public DateTime DateLastChange => DateTime.FromOADate(DDateLastChange);

        /// <summary>
        /// Das letzte Datum der Änderung als Gleitkommazahl im Delphiformat
        /// </summary>
        [XmlElement(ElementName = "TIME")]
        public double DDateLastChange { get; set; }

        /// <summary>
        /// Datum, wann die Datei in die Datenbank aufgenommen wurde.
        /// </summary>
        public DateTime DateAdded => DateTime.FromOADate(DDateAdded);

        /// <summary>
        /// Datum, wann die Datei in die Datenbank aufgenommen wurde als Gleitkommazahl im Delphiformat
        /// </summary>
        [XmlElement(ElementName = "ADDED")]
        public double DDateAdded { get; set; }

        /// <summary>
        /// Beschreibung des Videos, falls vorhanden
        /// </summary>
        [XmlElement(ElementName = "DESCRIPTION")]
        public string Description { get; set; }

        /// <summary>
        /// Abspieldauer des Videos in Sekunden
        /// </summary>
        [XmlElement(ElementName = "DURATION")]
        public int Duration { get; set; }

        /// <summary>
        /// Der Untertitel des Videos
        /// </summary>
        [XmlElement(ElementName = "INFO")]
        public string Info { get; set; }

        /// <summary>
        /// Zuletzt gespiel an...
        /// </summary>
        [XmlElement(ElementName = "LASTPLAYED")]
        public double LastPlayed { get; set; }

        /// <summary>
        /// Der Title des Videos
        /// </summary>
        [XmlElement(ElementName = "TITEL")]
        public string Title { get; set; }

        /// <summary>
        /// Die Dateigröße des Videos
        /// </summary>
        [XmlElement(ElementName = "FILESIZE")]
        public long FileSize { get; set; }

        /// <summary>
        /// Das Video ist aktiviert und wird angezeigt
        /// </summary>
        [XmlElement(ElementName = "ENABLED", Type = typeof(bool))]
        public bool Enabled { get; set; }

        /// <summary>
        /// Die Datei zum Video wurde auf dem Server gefunden
        /// </summary>
        [XmlElement(ElementName = "FOUND", Type = typeof(bool))]
        public bool Found { get; set; }

        /// <summary>
        /// Gibt die Fileextension zurück. z.B. .avi, .mpeg, .mkv etc.
        /// </summary>
        [XmlElement(ElementName = "EXT")]
        public string FileExtension { get; set; }

        internal VideoFileItem() { }

        /// <summary>
        /// Spiel das Video auf einem Clienten ab, sofern der DVBViewer auf diesem läuft.
        /// </summary>
        /// <param name="dVBViewerClient"></param>
        /// <returns></returns>
        public Task<HttpStatusCode> Play(DVBViewerClient dVBViewerClient)
        {
            return dVBViewerClient.PlayVideo(this);
        }

        /// <summary>
        /// Gibt eine URL zurück, welche das Video auf einen UPnP Gerät abspielen lässt.
        /// </summary>
        /// <returns></returns>
        public string GetUPnPUriString()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            return $"http://{dvbApi.Hostname}:8090/upnp/video/{ObjectID.ToString("D5")}{FileExtension}?d={Duration}";
        }

        /// <summary>
        /// Gibt den String zurück, der verwendet wird, bevor der UPnP String in die Datei m3u geschrieben wird.
        /// Beginnend mit #EXTINF:
        /// </summary>
        /// <returns></returns>
        internal string GetM3uPrefString()
        {
            return $"#EXTINF:{Duration},{Title}";
        }

        /// <summary>
        /// Erzeugt aus der Liste der Videos eine M3U Datei. Die Datei befindet sich normalerweise im Tempverzeichnis
        /// </summary>
        /// <returns>Ein Pfad zur m3u Datei</returns>
        public string CreateM3UFile()
        {
            var tPath = System.IO.Path.GetTempPath();
            var fName = $"{Title}.m3u";
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
