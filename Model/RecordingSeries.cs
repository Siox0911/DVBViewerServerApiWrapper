using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Ein Aufnahmeserie. A recording series
    /// </summary>
    [XmlRoot(ElementName = "series")]
    public class RecordingSeries : IEquatable<RecordingSeries>
    {
        /// <summary>
        /// Die Bezeichnung der Serie.
        /// The name of the series
        /// </summary>
        [XmlText(Type = typeof(string))]
        public string Name { get; set; }

        /// <summary>
        /// <para>
        /// Die Serie kann ein Freitext sein. Dafür muss der Konstruktor public sein, damit eine neue Serie erstellt werden, die noch nicht existiert.
        /// </para>
        /// <para>
        /// The series can be a free text. To do this, the constructor must be public to create a new series that does not yet exist.
        /// </para>
        /// </summary>
        public RecordingSeries() { }

        /// <summary>
        /// Vergleicht beide Serien und gibt True zurück, wenn beide gleich sind.
        /// Compare both series and return true if both are the same
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RecordingSeries other)
        {
            if (other is null) return false;
            if (Name == null && other.Name == null) return true;
            if (Name.Equals(other.Name)) return true;
            else return false;
        }

        /// <summary>
        /// Gibt den Hashcode der Serie zurück
        /// Returns the hash code of the series
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
        /// Returns a string of the instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Gibt eine Liste der Serien zurück. Die Anzahl der Serien ist abhängig von den aktuellen Aufnahmen.
        /// Returns a list of series. The number of series depends on the current recordings
        /// </summary>
        /// <returns></returns>
        public static async Task<List<RecordingSeries>> GetSeriesAsync()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var recs = await RecordingList.GetRecordingsAsync().ConfigureAwait(false);
                return (from f in recs.Items where f.Series?.Name != null orderby f.Series.Name select f.Series).Distinct().ToList();
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Liste der Serien zurück. Die Anzahl der Serien ist abhängig von den aktuellen Aufnahmen.
        /// Returns a list of series. The number of series depends on the current recordings
        /// </summary>
        /// <returns></returns>
        public static List<RecordingSeries> GetSeries()
        {
            return GetSeriesAsync().Result;
        }
    }
}
