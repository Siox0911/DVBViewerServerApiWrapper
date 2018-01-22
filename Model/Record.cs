using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Record
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
        public long Start { get; set; }
        /// <summary>
        /// Die Länge der Aufnahme HHMMSS
        /// </summary>
        [XmlAttribute(AttributeName = "duration")]
        public long Duration { get; set; }
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
        [XmlElement(ElementName = "series")]
        public string Series { get; set; }
        /// <summary>
        /// Die Bilddatei (Thumpnail) falls erlaubt
        /// </summary>
        [XmlElement(ElementName = "image")]
        public string Image { get; set; }

        internal Record() { }

    }
}
