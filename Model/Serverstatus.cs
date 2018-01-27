using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Gibt den Serverstatus zurück
    /// </summary>
    [XmlRoot(ElementName = "status")]
    public class Serverstatus
    {
        /// <summary>
        /// Anzahl der aktiven Timer, egal welcher Typ
        /// </summary>
        [XmlElement(ElementName = "timercount", Type = typeof(int))]
        public int TimerCount { get; set; }
        /// <summary>
        /// Anzahl der laufenden Aufnahmen
        /// </summary>
        [XmlElement(ElementName = "reccount", Type = typeof(int))]
        public int RecordCount { get; set; }
        /// <summary>
        /// Anzahl der Sekunden bis zum nächsten Timerstart. Egal welcher Timer. -1 meint kein Timer geplant
        /// </summary>
        [XmlElement(ElementName = "nexttimer", Type = typeof(int))]
        public int NextTimer { get; set; }
        /// <summary>
        /// Anzahl der Sekunden bis zur nächsten Aufnahme. -1 keine Aufnahme geplant
        /// </summary>
        [XmlElement(ElementName = "nextrec", Type = typeof(int))]
        public int NextRecording { get; set; }
        /// <summary>
        /// Anzahl der Streamclients
        /// </summary>
        [XmlElement(ElementName = "streamclientcount", Type = typeof(int))]
        public int StreamClientsCount { get; set; }
        /// <summary>
        /// Anzahl der RTSP Clienten (Sat->IP). Eventuell mehrere pro DVBViewer Instanz
        /// </summary>
        [XmlElement(ElementName = "rtspclientcount", Type = typeof(int))]
        public int RtspClientsCount { get; set; }
        /// <summary>
        /// Anzahl der Unicast Clienten
        /// </summary>
        [XmlElement(ElementName = "unicastclientcount", Type = typeof(int))]
        public int UnicastClientsCount { get; set; }
        /// <summary>
        /// Zeit in Sekunden seit dem letzten Login
        /// </summary>
        [XmlElement(ElementName = "lastuiaccess", Type = typeof(int))]
        public int LastUIAccess { get; set; }
        /// <summary>
        /// Zeigt an ob der Service aktuelle den Standby-Modus von Windows blcokiert
        /// </summary>
        [XmlElement(ElementName = "standbyblock", Type = typeof(bool))]
        public bool StandbyBlock { get; set; }
        /// <summary>
        /// Anzahl der verwendeten Tuner
        /// </summary>
        [XmlElement(ElementName = "tunercount", Type = typeof(int))]
        public int TunerCount { get; set; }
        /// <summary>
        /// Anzahl der Tuner, welche von Stream Clienten besetzt sind
        /// </summary>
        [XmlElement(ElementName = "streamtunercount", Type = typeof(int))]
        public int StreamTunerCount { get; set; }
        /// <summary>
        /// Anzahl der Tuner, welche von Aufnahmen besetzt sind.
        /// </summary>
        [XmlElement(ElementName = "rectunercount", Type = typeof(int))]
        public int RecordingTunerCount { get; set; }
        /// <summary>
        /// Der Status des EPGs
        /// </summary>
        [XmlElement(ElementName = "epgupdate", Type = typeof(Enums.EPGUpdate))]
        public Enums.EPGUpdate EPGUpdate { get; set; }
        /// <summary>
        /// Die Rechte welche vom Service gewährt wurden
        /// </summary>
        [XmlElement(ElementName = "rights", Type = typeof(string))]
        public string Rights { get; set; }
        /// <summary>
        /// Anzahl der Aufnahmen im Service
        /// </summary>
        [XmlElement(ElementName = "recfiles", Type = typeof(int))]
        public int RecordingFiles { get; set; }
        /// <summary>
        /// Die Aufnahmeverzeichnisse im Service
        /// </summary>
        [XmlArray("recfolders")]
        [XmlArrayItem(ElementName = "folder", Type = typeof(RecordingFolder))]
        public List<RecordingFolder> RecordingFolders { get; set; }

        //Keine externe Instanziierung zulässig
        internal Serverstatus() { }

        internal static Serverstatus CreateServerstatus(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<Serverstatus>(xDocument, new Type[] { typeof(RecordingFolder) });
        }
    }
}
