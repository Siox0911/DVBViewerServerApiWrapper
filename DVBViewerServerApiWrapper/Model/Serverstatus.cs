using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Gibt den Serverstatus zurück.
    /// Returns the server status
    /// </summary>
    [XmlRoot(ElementName = "status")]
    public class Serverstatus
    {
        /// <summary>
        /// Anzahl der aktiven Timer, egal welcher Typ
        /// Number of active timers, wherever what type
        /// </summary>
        [XmlElement(ElementName = "timercount", Type = typeof(int))]
        public int TimerCount { get; set; }

        /// <summary>
        /// Anzahl der laufenden Aufnahmen
        /// number of running recordings
        /// </summary>
        [XmlElement(ElementName = "reccount", Type = typeof(int))]
        public int RecordCount { get; set; }

        /// <summary>
        /// Anzahl der Sekunden bis zum nächsten Timerstart. Egal welcher Timer. -1 meint kein Timer geplant
        /// Number of seconds until the next timer start. every timer. -1 means no timer planned
        /// </summary>
        [XmlElement(ElementName = "nexttimer", Type = typeof(int))]
        public int NextTimer { get; set; }

        /// <summary>
        /// Anzahl der Sekunden bis zur nächsten Aufnahme. -1 keine Aufnahme geplant
        /// Number of seconds until the next shot. -1 no recording planned
        /// </summary>
        [XmlElement(ElementName = "nextrec", Type = typeof(int))]
        public int NextRecording { get; set; }

        /// <summary>
        /// Anzahl der Streamclients
        /// Number of stream clients
        /// </summary>
        [XmlElement(ElementName = "streamclientcount", Type = typeof(int))]
        public int StreamClientsCount { get; set; }

        /// <summary>
        /// Anzahl der RTSP Clienten (Sat->IP). Eventuell mehrere pro DVBViewer Instanz
        /// Number of RTSP clients (Sat-> IP). Maybe several per DVBViewer instance
        /// </summary>
        [XmlElement(ElementName = "rtspclientcount", Type = typeof(int))]
        public int RtspClientsCount { get; set; }

        /// <summary>
        /// Anzahl der Unicast Clienten
        /// Number of unicast clients
        /// </summary>
        [XmlElement(ElementName = "unicastclientcount", Type = typeof(int))]
        public int UnicastClientsCount { get; set; }

        /// <summary>
        /// Zeit in Sekunden seit dem letzten Login
        /// Time in seconds since the last login
        /// </summary>
        [XmlElement(ElementName = "lastuiaccess", Type = typeof(int))]
        public int LastUIAccess { get; set; }

        /// <summary>
        /// Zeigt an ob der Service aktuelle den Standby-Modus von Windows blcokiert
        /// Indicates whether the service is currently scrolling Windows standby
        /// </summary>
        [XmlElement(ElementName = "standbyblock", Type = typeof(bool))]
        public bool StandbyBlock { get; set; }

        /// <summary>
        /// Anzahl der verwendeten Tuner
        /// Number of tuners used
        /// </summary>
        [XmlElement(ElementName = "tunercount", Type = typeof(int))]
        public int TunerCount { get; set; }

        /// <summary>
        /// Anzahl der Tuner, welche von Stream Clienten besetzt sind.
        /// Number of tuners occupied by stream clients
        /// </summary>
        [XmlElement(ElementName = "streamtunercount", Type = typeof(int))]
        public int StreamTunerCount { get; set; }

        /// <summary>
        /// Anzahl der Tuner, welche von Aufnahmen besetzt sind.
        /// Number of tuners occupied by recordings.
        /// </summary>
        [XmlElement(ElementName = "rectunercount", Type = typeof(int))]
        public int RecordingTunerCount { get; set; }

        /// <summary>
        /// Der Status des EPGs
        /// The status of the EPG
        /// </summary>
        [XmlElement(ElementName = "epgupdate", Type = typeof(Enums.EPGUpdate))]
        public Enums.EPGUpdate EPGUpdate { get; set; }

        /// <summary>
        /// Die Rechte welche vom Service gewährt wurden
        /// he rights granted by the service
        /// </summary>
        [XmlElement(ElementName = "rights", Type = typeof(string))]
        public string Rights { get; set; }

        /// <summary>
        /// Anzahl der Aufnahmen im Service
        /// Number of recordings in the service
        /// </summary>
        [XmlElement(ElementName = "recfiles", Type = typeof(int))]
        public int RecordingFiles { get; set; }

        /// <summary>
        /// Die Aufnahmeverzeichnisse im Service
        /// The recording directories in the service
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

        /// <summary>
        /// Gibt den Serverstatus zurück. Returns the server status.
        /// </summary>
        /// <returns></returns>
        public async static Task<Serverstatus> GetServerstatusAsync()
        {
            var api = DVBViewerServerApi.GetCurrentInstance();
            if(api != null)
            {
                return CreateServerstatus(await api.GetApiDataAsync().ConfigureAwait(false));
            }
            return null;
        }

        /// <summary>
        /// Gibt den Serverstatus zurück. Returns the server status.
        /// </summary>
        /// <returns></returns>
        public static Serverstatus GetServerstatus()
        {
            return GetServerstatusAsync().Result;
        }
    }
}
