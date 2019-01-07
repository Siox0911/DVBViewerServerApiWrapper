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
    /// Stellt einen Kanaltuner dar, der die Tunereinstellungen zum Kanal enthält. Es sind nicht alle Eigenschaften aus dem DMS übernommen wurden.
    /// Represents a channel tuner that contains the tuner settings for the channel. Not all properties have been taken from the DMS.
    /// </summary>
    [XmlRoot(ElementName = "tuner")]
    public class ChannelTuner
    {
        /// <summary>
        /// Der Tunertyp.
        /// The Tunertype: 0 = cable, 1 = satellite, 2 = terrestrial, 3 = atsc, 4 = iptv, 5 = stream (URL, DVBViewer GE)
        /// </summary>
        [XmlAttribute(AttributeName = "tnrtype")]
        public int TnrType { get; set; }

        /// <summary>
        /// The channelgroup.
        /// 0 = Group A, 1 = Group B, 2 = Group C etc.
        /// </summary>
        [XmlAttribute(AttributeName = "group")]
        public int Group { get; set; }

        /// <summary>
        /// Die Flags des Kanals. Channel flags.
        /// The flags of the channel. Channel flags.
        /// </summary>
        [XmlAttribute(AttributeName = "flags")]
        public int Flags
        {
            get { return (int)ChannelProperties; }
            set
            {
                ChannelProperties = (Enums.ChannelProperties)value;
            }
        }

        /// <summary>
        /// Die allgemeinen Eigenschaften des Kanals. Audio, Video, RDS etc.
        /// The general Channel properties. Audio, Video, RDS etc.
        /// </summary>
        [XmlIgnore]
        public Enums.ChannelProperties ChannelProperties;

        /// <summary>
        /// Die Frequenz des Senders.
        /// The frequency of the channel.
        /// </summary>
        [XmlAttribute(AttributeName = "freq")]
        public long Frequency { get; set; }

        /// <summary>
        /// Die Symbolrate des Senders.
        /// The symbol rate of the channel.
        /// </summary>
        [XmlAttribute(AttributeName = "symb")]
        public long SymbolRate { get; set; }

        /// <summary>
        /// LOF des Senders.
        /// Lof of the channel
        /// </summary>
        [XmlAttribute(AttributeName = "LOF")]
        public int LocalOscillatorFrequency { get; set; }

        /// <summary>
        /// PMT des Senders.
        /// PMT of the channel
        /// </summary>
        [XmlAttribute(AttributeName = "PMT")]
        public int ProgramMapTable { get; set; }

        /// <summary>
        /// APID des Senders.
        /// APID of the channel
        /// </summary>
        [XmlAttribute(AttributeName = "APID")]
        public int AudioPID { get; set; }

        /// <summary>
        /// VPID des Senders.
        /// VPID of the channel
        /// </summary>
        [XmlAttribute(AttributeName = "VPID")]
        public int VideoPID { get; set; }

        /// <summary>
        /// TSID des Senders.
        /// TSID of the channel
        /// </summary>
        [XmlAttribute(AttributeName = "TSID")]
        public int TransportStreamID { get; set; }

        /// <summary>
        /// NID des Senders.
        /// NID of the channel
        /// </summary>
        [XmlAttribute(AttributeName = "NID")]
        public int NetworkID { get; set; }

        /// <summary>
        /// SID des Senders.
        /// SID of the channel
        /// </summary>
        [XmlAttribute(AttributeName = "SID")]
        public int ServiceID { get; set; }

        /// <summary>
        /// Die Sprache des Senders.
        /// The Language of the channel.
        /// </summary>
        [XmlAttribute(AttributeName = "alang")]
        public string Language { get; set; }

        internal ChannelTuner() { }
    }
}