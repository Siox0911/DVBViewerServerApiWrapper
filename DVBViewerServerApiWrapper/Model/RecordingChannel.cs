using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Represents a Channel. Präsentiert einen Kanal
    /// </summary>
    [XmlRoot(ElementName = "channel")]
    public class RecordingChannel : IEquatable<RecordingChannel>
    {
        /// <summary>
        /// Der Name des Senders (Kanal)
        /// Name of Channel
        /// </summary>
        [XmlText(Type = typeof(string))]
        public string Name { get; set; }

        /// <summary>
        /// <para>
        /// Der Konstruktor wird als public benötigt.
        /// Der Sender kann als Freitext belegt werden. Damit dies möglich ist kann ein Sender erstellt werden.
        /// </para>
        /// <para>
        /// The constructor is needed as public.
        /// The channel name can be a free text. To make this possible, a channel can be created.
        /// </para>
        /// </summary>
        public RecordingChannel() { }

        /// <summary>
        /// Vergleicht beide Sender und gibt True zurück, wenn beide gleich sind
        /// Compare both channels and return true if both are the same
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RecordingChannel other)
        {
            if (other is null) return false;
            if (Name == null && other.Name == null) return true;
            return Name.Equals(other.Name);
        }

        /// <summary>
        /// Gibt den HashCode des Senders zurück
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (Name == null)
                return 0;

            return Name.GetHashCode();
        }

        /// <summary>
        /// Gibt eine Zeichenfolge der Instanz zurück.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Gibt eine Liste aller Sender zurück.
        /// Returns a list of all channels.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<RecordingChannel>> GetChannelsAsync()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var recs = await RecordingList.GetRecordingsAsync().ConfigureAwait(false);
                return (from f in recs.Items where f.Channel?.Name != null orderby f.Channel.Name select f.Channel).Distinct().ToList();
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Liste aller Sender zurück.
        /// Returns a list of all channels.
        /// </summary>
        /// <returns></returns>
        public static List<RecordingChannel> GetChannels()
        {
            return GetChannelsAsync().Result;
        }
    }
}
