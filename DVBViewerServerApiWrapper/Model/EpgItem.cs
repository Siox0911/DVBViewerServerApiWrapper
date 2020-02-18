using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Ein einzelner EPG Eintrag.
    /// A single EPG entry.
    /// </summary>
    [XmlRoot(ElementName = "programme")]
    public class EpgItem
    {
        /// <summary>
        /// Die Startzeit als yyyyMMddHHmmss.
        /// The start time as yyyyMMddHHmmss.
        /// </summary>
        [XmlAttribute(AttributeName = "start")]
        public long Start { get; set; }

        /// <summary>
        /// Die Zeit des Endes als  yyyyMMddHHmmss.
        /// The time of the end as yyyyMMddHHmmmm.
        /// </summary>
        [XmlAttribute(AttributeName = "stop")]
        public long Stop { get; set; }

        /// <summary>
        /// Die Startzeit als DateTime.
        /// The start time as DateTime.
        /// </summary>
        public DateTime StartDate => Start != 0 ? DateTime.Parse(Start.ToString("0000-00-00 00:00:00")) : default;

        /// <summary>
        /// Die Zeit des Endes als DateTime.
        /// The time of the end as DateTime.
        /// </summary>
        public DateTime StopDate => Stop != 0 ? DateTime.Parse(Stop.ToString("0000-00-00 00:00:00")) : default;

        /// <summary>
        /// Die EPG-Kanal ID.
        /// The EPG channel ID.
        /// </summary>
        [XmlAttribute(AttributeName = "channel")]
        public long EpgChannelID { get; set; }

        /// <summary>
        /// Die EPG Event ID.
        /// The EPG Event ID.
        /// </summary>
        [XmlElement(ElementName = "eventid")]
        public string EpgEventID { get; set; }

        /// <summary>
        /// 255 bedeutet, der Text ist in UTF-8.
        /// 255 means the text is in UTF-8.
        /// http://www.dvbviewer.tv/forum/topic/37211-recording-service-api/?tab=comments#comment-272680
        /// </summary>
        [XmlElement(ElementName = "charset")]
        public int Charset { get; set; }

        /// <summary>
        /// Der Titel des EPG-Eintrags.
        /// The title of the EPG entry.
        /// </summary>
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Die Beschreibung des Titels.
        /// The description of the title.
        /// </summary>
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// PDC (Program Delivery Control) des EPG Eintrags.
        /// PDC (Program Delivery Control) of the EPG entry.
        /// </summary>
        [XmlElement(ElementName = "pdc")]
        public int PDC { get; set; }

        /// <summary>
        /// Content: Wahrscheinlich Spielfilm etc. 
        /// </summary>
        [XmlElement(ElementName = "content")]
        public int Content { get; set; }

        /// <summary>
        /// Der Event, könnte fast die Info sein.
        /// </summary>
        [XmlElement(ElementName = "event")]
        public string Event { get; set; }

        internal EpgItem() { }
    }
}