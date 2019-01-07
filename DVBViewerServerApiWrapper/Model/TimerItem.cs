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
    /// Stellt ein Timer im DMS dar. Dieser Timer ist kein System-Timer.
    /// Represents a timer in the DMS. This timer is not a system timer.
    /// </summary>
    [XmlRoot(ElementName = "Timer")]
    public class TimerItem
    {
        /// <summary>
        /// Der Typ des Timers.
        /// The Type of the Timer.
        /// </summary>
        [XmlAttribute(AttributeName = "Type")]
        public int Type { get; set; }

        /// <summary>
        /// GUID des Timers.
        /// GUID of the timer.
        /// </summary>
        [XmlAttribute(AttributeName = "ID")]
        public Guid Guid { get; set; }

        /// <summary>
        /// Der Timer ist aktiviert = -1, und deaktiviert = 0.
        /// The timer is activated = -1, and disabled = 0.
        /// </summary>
        [XmlAttribute(AttributeName = "Enabled")]
        public int Enabled { get; set; }

        /// <summary>
        /// Priorität in Prozent. 50 meint 50%, sollten 2 Timer existieren wird der Timer zu erst genommen, dessen Priorität höher ist.
        /// Priority in percent. 50 means 50%, if there are 2 timers, the timer whose priority is higher is taken first.
        /// </summary>
        [XmlAttribute(AttributeName = "Priority")]
        public int Priority { get; set; }

        /// <summary>
        /// 255 meint UTF-8.
        /// 255 means UTF-8.
        /// </summary>
        [XmlAttribute(AttributeName = "Charset")]
        public int Charset { get; set; }

        /// <summary>
        /// Datum des Timers.
        /// Date of the timer.
        /// </summary>
        [XmlAttribute(AttributeName = "Date")]
        public string Date { get; set; }

        /// <summary>
        /// Startzeit des Timer als HH:mm:ss.
        /// Start time of timer as HH:mm:ss.
        /// </summary>
        [XmlAttribute(AttributeName = "Start")]
        public string Start { get; set; }

        /// <summary>
        /// Die Dauer der Aufnahme in Minuten.
        /// The duration of the recording in minutes.
        /// </summary>
        [XmlAttribute(AttributeName = "Dur")]
        public int Duration { get; set; }

        /// <summary>
        /// Die Endzeit des Timers (der Aufnahme) in HH:mm:ss.
        /// The end time of the timer (recording) in HH:mm:ss.
        /// </summary>
        [XmlAttribute(AttributeName = "End")]
        public string End { get; set; }

        /// <summary>
        /// Die Vorlaufzeit des Timers in Minuten.
        /// The lead time of the timer in minutes.
        /// </summary>
        [XmlAttribute(AttributeName = "PreEPG")]
        public int PreEpg { get; set; }

        /// <summary>
        /// Die Nachlaufzeit des Timer in Minuten.
        /// The follow-up time of the timer in minutes.
        /// </summary>
        [XmlAttribute(AttributeName = "PostEPG")]
        public int PostEpg { get; set; }

        /// <summary>
        /// Unklar.
        /// Unknown.
        /// </summary>
        [XmlAttribute(AttributeName = "Action")]
        public int Action { get; set; }

        /// <summary>
        /// Die EPGEventID aus dem EPG.
        /// The EPGEventID from the EPG.
        /// </summary>
        [XmlAttribute(AttributeName = "EPGEventID")]
        public int EpgEventID { get; set; }

        /// <summary>
        /// Die Beschreibung des Timer oder besser dessen Name.
        /// The description of the timer or rather its name.
        /// </summary>
        [XmlElement(ElementName = "Descr")]
        public string Description { get; set; }

        /// <summary>
        /// Die Optionen des Timers.
        /// The options of the timer.
        /// </summary>
        [XmlElement(ElementName = "Options", Type = typeof(TimerOptions))]
        public TimerOptions TimerOptions { get; set; }

        /// <summary>
        /// Das Format des Timers: 0 = Audio Only (*.mp2, *.ac3, *.aac), 1 = Video/Audio PS (*.mpg), 2 = Video/Audio TS (*.ts).
        /// The format of the timer: 0 = Audio Only (* .mp2, * .ac3, * .aac), 1 = Video/Audio PS (* .mpg), 2 = Video/Audio TS (* .ts).
        /// </summary>
        [XmlElement(ElementName = "Format")]
        public int Format { get; set; }

        /// <summary>
        /// Das Aufnahmeverzeichnis: Auto für die automatische Wahl des Verzeichnisses.
        /// The recording directory: Auto for the automatic selection of the directory.
        /// </summary>
        [XmlElement(ElementName = "Folder")]
        public string Folder { get; set; }

        /// <summary>
        /// Das Namensschema nachdem der TimerName erstellt wurde.
        /// The naming scheme which the TimerName is created.
        /// </summary>
        [XmlElement(ElementName = "NameScheme")]
        public string NameScheme { get; set; }

        /// <summary>
        /// Die Serie in der die Aufnahme erstellt wird.
        /// The series in which the recording is made.
        /// </summary>
        [XmlElement(ElementName = "Series", Type = typeof(RecordingSeries))]
        public RecordingSeries Series { get; set; }

        /// <summary>
        /// Die Quelle des Timers: Search: Suchbegriff oder Webinterface etc.
        /// The source of the timer: Search: Search term or webinterface etc.
        /// </summary>
        [XmlElement(ElementName = "Source")]
        public string Source { get; set; }

        private ChannelItem channelItem;

        /// <summary>
        /// Der Kanal als ChannelItem. Sollte kein brauchbares ChannelItem zurück gegeben werden, verwende <seealso cref="ChannelListAsync"/> oder <seealso cref="GetChannelListAsync"/>.
        /// The channel as ChannelItem. If no usable ChannelItem is returned, use <seealso cref = "ChannelListAsync" /> or <seealso cref = "GetChannelListAsync" />.
        /// </summary>
        /// <remarks></remarks>
        [XmlElement(ElementName = "Channel", Type = typeof(ChannelItem))]
        public ChannelItem ChannelItem { get { return ChannelList.GetChannelList(channelItem)?.TopGroups[0]?.Groups[0]?.Items[0]; } set { channelItem = value; } }

        /// <summary>
        /// Gibt eine Kanalliste mit einem Kanal zurück, welche diesem Timer zugeordnet ist.
        /// Returns a ChannelList with one channel, which is referenced to this timer.
        /// </summary>
        /// <returns></returns>
        public Task<ChannelList> ChannelListAsync { get { return ChannelList.GetChannelListAsync(channelItem); } }

        /// <summary>
        /// Gibt eine Kanalliste mit einem Kanal zurück, welche diesem Timer zugeordnet ist.
        /// Returns a ChannelList with one channel, which is referenced to this timer.
        /// </summary>
        /// <returns></returns>
        public ChannelList ChannelList { get { return ChannelListAsync.Result; } }

        /// <summary>
        /// Wird die Aufnahme ausgeführt oder sind Konflikte bekannt. -1 = Aufnahme wird ausgeführt. 0 = Timer ist deaktiviert oder ein Problem wurde erkannt.
        /// Is the recording performed or conflicts are known? -1 = recording is being carried out. 0 = Timer is disabled or a problem has been detected.
        /// </summary>
        [XmlElement(ElementName = "Executable")]
        public int Executable { get; set; }

        /// <summary>
        /// Wenn der Timer aktiv ist. Das heißt eine Aufnahme findet genau jetzt statt.
        /// When the timer is active. That means a recording takes place right now.
        /// </summary>
        [XmlElement(ElementName = "Recording")]
        public int Recording { get; set; }

        /// <summary>
        /// Die ID des Timers.
        /// The ID of the timer.
        /// </summary>
        [XmlElement(ElementName = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// Zeigt ob der Timer nach der Aufnahme eine weitere Aktion durchführt: null = nichts, 1 = Herunterfahren, 2 = Energie sparen, 3 = Ruhemodus.
        /// Indicates whether the timer performs another action after recording: zero = nothing, 1 = shutdown, 2 = energy saving, 3 = sleep mode.
        /// </summary>
        [XmlElement(ElementName = "Shutdown")]
        public int Shutdown { get; set; }

        /// <summary>
        /// Welcher Prozess wird nach der Aufnahme gestartet.
        /// Which process is started after the recording?
        /// </summary>
        [XmlElement(ElementName = "AfterProcess")]
        public string AfterProcess { get; set; }

        /// <summary>
        /// Gibt den aktuellen EPG Eintrag zurück.
        /// Returns the current EPG entry.
        /// </summary>
        /// <returns></returns>
        public Task<EpgList> EpgListAsync { get { return GetEpgListAsync(); } }

        /// <summary>
        /// Gibt den aktuellen EPG Eintrag zurück.
        /// Returns the current EPG entry.
        /// </summary>
        /// <returns></returns>
        public EpgList EpgList { get { return GetEpgList(); } }

        internal TimerItem() { }

        /// <summary>
        /// Gibt den aktuellen EPG Eintrag zurück.
        /// Returns the current EPG entry.
        /// </summary>
        /// <returns></returns>
        public async Task<EpgList> GetEpgListAsync()
        {
            //Channel holen
            var channel = await ChannelList.GetChannelListAsync(ChannelItem).ConfigureAwait(false);
            var channelItem = channel.TopGroups[0].Groups[0].Items[0];
            //EPG Eintrag holen und zurückgeben.
            return await EpgList.GetEpgListAsync(channelItem, EpgEventID).ConfigureAwait(false);
        }

        /// <summary>
        /// Gibt den aktuellen EPG Eintrag zurück.
        /// Returns the current EPG entry.
        /// </summary>
        /// <returns></returns>
        public EpgList GetEpgList()
        {
            return GetEpgListAsync().Result;
        }

        /// <summary>
        /// Gibt eine Kanalliste mit einem Kanal zurück, welche diesem Timer zugeordnet ist.
        /// Returns a ChannelList with one channel, which is referenced to this timer.
        /// </summary>
        /// <returns></returns>
        public Task<ChannelList> GetChannelListAsync()
        {
            return ChannelList.GetChannelListAsync(ChannelItem);
        }

        /// <summary>
        /// Gibt eine Kanalliste mit einem Kanal zurück, welche diesem Timer zugeordnet ist.
        /// Returns a ChannelList with one channel, which is referenced to this timer.
        /// </summary>
        /// <returns></returns>
        public ChannelList GetChannelList()
        {
            return GetChannelListAsync().Result;
        }
    }
}