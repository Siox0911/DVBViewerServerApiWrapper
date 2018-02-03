using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    [XmlRoot(ElementName = "channel")]
    public class RecordingChannel : IEquatable<RecordingChannel>
    {
        /// <summary>
        /// Der Name des Senders (Kanal)
        /// </summary>
        [XmlText(Type = typeof(string))]
        public string Name { get; set; }

        /// <summary>
        /// Vergleicht beide Sender und gibt True zurück, wenn beide gleich sind
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RecordingChannel other)
        {
            if (other is null) return false;
            if (Name.Equals(other.Name)) return true;
            else return false;
        }

        /// <summary>
        /// Gibt den HashCode des Senders zurück
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
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
        /// </summary>
        /// <returns></returns>
        public static List<RecordingChannel> GetChannels()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var recs = RecordingList.GetRecordings();
                return (from f in recs.Items where f.Channel != null orderby f.Channel.Name select f.Channel).Distinct().ToList();
            }
            return null;
        }

    }
}
