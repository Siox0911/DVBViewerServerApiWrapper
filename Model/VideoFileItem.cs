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
        /// ?
        /// </summary>
        [XmlElement(ElementName = "TIME")]
        public double Time { get; set; }

        /// <summary>
        /// ?
        /// </summary>
        [XmlElement(ElementName = "ADDED")]
        public double Added { get; set; }

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
        /// Der Title der Aufnahme
        /// </summary>
        [XmlElement(ElementName = "TITEL")]
        public string Title { get; set; }

        /// <summary>
        /// Die Dateigröße der Aufnahme
        /// </summary>
        [XmlElement(ElementName = "FILESIZE")]
        public long FileSize { get; set; }

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
    }
}
