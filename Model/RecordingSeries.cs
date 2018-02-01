using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Ein Aufnahmeserie
    /// </summary>
    [XmlRoot(ElementName = "series")]
    public class RecordingSeries : IEquatable<RecordingSeries>
    {
        /// <summary>
        /// Die Bezeichnung der Serie
        /// </summary>
        [XmlText(Type = typeof(string))]
        public string Name { get; set; }

        /// <summary>
        /// Vergleicht beide Serien und gibt True 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RecordingSeries other)
        {
            if (other is null) return false;
            if (Name.Equals(other.Name)) return true;
            else return false;
        }

        /// <summary>
        /// Gibt den Hashcode der Serie zurück
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
        /// Gibt eine Liste der Serien zurück.
        /// </summary>
        /// <returns></returns>
        public static List<RecordingSeries> GetSeries()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var recs = RecordingList.GetRecordings();
                return (from f in recs.Items where f.Series != null orderby f.Series.Name select f.Series).Distinct().ToList();
            }
            return null;
        }
    }
}
