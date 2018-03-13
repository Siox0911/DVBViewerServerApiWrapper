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
    /// Klasse welche die Timer Optionen eines Timereintrags darstellt.
    /// Class representing the timer options of a timer entry.
    /// </summary>
    [XmlRoot(ElementName = "Options")]
    public class TimerOptions
    {
        /// <summary>
        /// Jo, ? 
        /// </summary>
        [XmlAttribute(AttributeName = "AdjustPAT")]
        public int AdjustPAT { get; set; }

        /// <summary>
        /// Werden alle Audiokanäle aufgenommen. -1 = Ja. Null = Nein.
        /// Are all audio channels recorded. -1 = Yes. null = no.
        /// </summary>
        [XmlAttribute(AttributeName = "AllAudio")]
        public int AllAudio { get; set; }

        /// <summary>
        /// Werden EIT EPG Daten verwendet. -1 = Ja. Null = Nein.
        /// If EIT EPG data is used. -1 = Yes. null = no.
        /// </summary>
        [XmlAttribute(AttributeName = "EITEPG")]
        public int EITEPG { get; set; }

        /// <summary>
        /// Wird die Zeit nach dem EPG automatisch angepasst -1 = Ja, Null = Nein.
        /// If the time after the EPG is adjusted automatically -1 = Yes, null = No.
        /// </summary>
        [XmlAttribute(AttributeName = "MonitorPDC")]
        public int MonitorPDC { get; set; }

        /// <summary>
        /// Wird die Aufnahme nach EPG automatisch gestartet und beendet. -1 = Ja, Null = Nein.
        /// If recording is automatically started and stopped trough EPG. -1 = yes, null = no.
        /// </summary>
        [XmlAttribute(AttributeName = "RunningStatusSplit")]
        public int RunningStatusSplit { get; set; }

        /// <summary>
        /// Werden die DVB Untertitel aufgenommen. -1 = Ja, Null = Nein.
        /// Will the DVB subtitles be recorded. -1 = yes, null = no.
        /// </summary>
        [XmlAttribute(AttributeName = "DVBSubs")]
        public int DVBSubs { get; set; }

        /// <summary>
        /// Wird der Teletext mit aufgenommen. -1 = Ja, Null = Nein.
        /// Will the teletext be included? -1 = yes, null = no.
        /// </summary>
        [XmlAttribute(AttributeName = "Teletext")]
        public int Teletext { get; set; }

        internal TimerOptions() {}
    }
}