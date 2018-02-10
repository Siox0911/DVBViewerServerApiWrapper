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
    /// Eine Videodatei im Media Server.
    /// A video file in the media server
    /// </summary>
    [XmlRoot(ElementName = "row")]
    public class VideoFileItem
    {
        /// <summary>
        /// Die Object ID des Videos. 
        /// The object ID of the video.
        /// </summary>
        [XmlElement(ElementName = "OBJECT_ID")]
        public int ObjectID { get; set; }

        /// <summary>
        /// Die Details ID des Videos. Für weitere Details wird das benötigt.
        /// The details ID of the video. This is needed for more details
        /// </summary>
        [XmlElement(ElementName = "DETAILS_ID")]
        public int ObjectDetailsID { get; set; }

        /// <summary>
        /// Die Pfad ID des Servers. Verwendung um zum Beispiel noch andere Videos im Pfad zu finden.
        /// The path ID of the server. Use for example to find other videos in the path.
        /// </summary>
        [XmlElement(ElementName = "IDPATH")]
        public int PathID { get; set; }

        /// <summary>
        /// Der Dateiname des Videos.
        /// The filename of the video
        /// </summary>
        [XmlElement(ElementName = "FILENAME")]
        public string FileName { get; set; }

        /// <summary>
        /// Der Pfad indem das Video gespeichert ist.
        /// The path where the video is stored
        /// </summary>
        [XmlElement(ElementName = "PATH")]
        public string Path { get; set; }

        /// <summary>
        /// Der Kanal auf dem das Video aufgenommen wurde, falls vorhanden.
        /// The channel on which the video was taken, if any
        /// </summary>
        [XmlElement(ElementName = "CHANNEL")]
        public string Channel { get; set; }

        /// <summary>
        /// Datum der letzten Änderung.
        /// Date of last change
        /// </summary>
        public DateTime DateLastChange => DateTime.FromOADate(DDateLastChange);

        /// <summary>
        /// Das letzte Datum der Änderung als Gleitkommazahl im Delphiformat.
        /// The last date of the change as a floating-point number in the Delphi format
        /// </summary>
        [XmlElement(ElementName = "TIME")]
        public double DDateLastChange { get; set; }

        /// <summary>
        /// Datum, wann die Datei in die Datenbank aufgenommen wurde.
        /// Date when the file was added to the database.
        /// </summary>
        public DateTime DateAdded => DateTime.FromOADate(DDateAdded);

        /// <summary>
        /// Datum, wann die Datei in die Datenbank aufgenommen wurde als Gleitkommazahl im Delphiformat.
        /// Date when the file was added to the database as a floating-point number in the Delphi format
        /// </summary>
        [XmlElement(ElementName = "ADDED")]
        public double DDateAdded { get; set; }

        /// <summary>
        /// Beschreibung des Videos, falls vorhanden.
        /// Description of the video, if available
        /// </summary>
        [XmlElement(ElementName = "DESCRIPTION")]
        public string Description { get; set; }

        /// <summary>
        /// Abspieldauer des Videos in Sekunden
        /// Playback time of the video in seconds
        /// </summary>
        [XmlElement(ElementName = "DURATION")]
        public int Duration { get; set; }

        /// <summary>
        /// Die Abspieldauer als TimeSpan.
        /// The playing time as TimeSpan
        /// </summary>
        public TimeSpan Duration2 { get { return TimeSpan.FromSeconds(Duration); } }

        /// <summary>
        /// Der Untertitel des Videos.
        /// The subtitle of the video
        /// </summary>
        [XmlElement(ElementName = "INFO")]
        public string Info { get; set; }

        /// <summary>
        /// Zuletzt gespiel an...
        /// Last played on ...
        /// </summary>
        [XmlElement(ElementName = "LASTPLAYED")]
        public double LastPlayed { get; set; }

        /// <summary>
        /// Der Title des Videos
        /// The title of the video
        /// </summary>
        [XmlElement(ElementName = "TITEL")]
        public string Title { get; set; }

        /// <summary>
        /// Die Dateigröße des Videos
        /// The file size of the video
        /// </summary>
        [XmlElement(ElementName = "FILESIZE")]
        public long FileSize { get; set; }

        /// <summary>
        /// Die Dateigröße des Videos
        /// The file size of the video
        /// </summary>
        public Helper.FileSize FileSizeF { get { return Helper.FileSize.GetFileSize(FileSize); } }

        /// <summary>
        /// Das Video ist aktiviert und wird angezeigt.
        /// The video is activated and displayed
        /// </summary>
        [XmlElement(ElementName = "ENABLED", Type = typeof(bool))]
        public bool Enabled { get; set; }

        /// <summary>
        /// Die Datei zum Video wurde auf dem Server gefunden.
        /// The video file was found on the server
        /// </summary>
        [XmlElement(ElementName = "FOUND", Type = typeof(bool))]
        public bool Found { get; set; }

        /// <summary>
        /// Gibt die Fileextension zurück. z.B. .avi, .mpeg, .mkv etc.
        /// Returns the file extension. e.g. .avi, .mpeg, .mkv etc.
        /// </summary>
        [XmlElement(ElementName = "EXT")]
        public string FileExtension { get; set; }

        internal VideoFileItem() { }

        /// <summary>
        /// Spiel das Video auf einem Clienten ab, sofern der DVBViewer auf dem Server läuft.
        /// Play the video on a client, if the DVBViewer is connected to the server.
        /// </summary>
        /// <param name="dVBViewerClient"></param>
        /// <returns></returns>
        public Task<HttpStatusCode> PlayAsync(DVBViewerClient dVBViewerClient)
        {
            return dVBViewerClient.PlayVideoAsync(this);
        }

        /// <summary>
        /// Spiel das Video auf einem Clienten ab, sofern der DVBViewer auf dem Server läuft.
        /// Play the video on a client, if the DVBViewer is connected to the server.
        /// </summary>
        /// <param name="dVBViewerClient"></param>
        /// <returns></returns>
        public HttpStatusCode Play(DVBViewerClient dVBViewerClient)
        {
            return PlayAsync(dVBViewerClient).Result;
        }

        /// <summary>
        /// Gibt eine URL zurück, welche das Video auf einen UPnP Gerät abspielen lässt.
        /// Returns a URL that plays the video on a UPnP device.
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
        /// Returns the string used before the UPnP string is written to the m3u file.
        /// Starting with #EXTINF:
        /// </summary>
        /// <returns></returns>
        internal string GetM3uPrefString()
        {
            return $"#EXTINF:{Duration},{Title}";
        }

        /// <summary>
        /// Erzeugt aus der Liste der Videos eine M3U Datei. Die Datei befindet sich normalerweise im Tempverzeichnis.
        /// Generates an M3U file from the list of videos. The file is usually located in the Temp directory
        /// </summary>
        /// <returns>Ein Pfad zur m3u Datei. A path to the m3u file</returns>
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
