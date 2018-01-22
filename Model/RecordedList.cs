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
    /// Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
    /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.
    /// </summary>
    [XmlRoot(ElementName = "table")]
    public class RecordedList
    {
        /// <summary>
        /// Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
        /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.
        /// </summary>
        [XmlElement(ElementName = "row", Type = typeof(RecordedItem))]
        public List<RecordedItem> RecordedItems { get; set; }

        internal RecordedList() { }

        internal static RecordedList CreateRecordedList(XDocument xDocument)
        {
            var t = new XmlSerializer(typeof(RecordedList), new Type[] { typeof(RecordedItem) });
            return (RecordedList)t.Deserialize(xDocument.CreateReader());
        }

        /// <summary>
        /// Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
        /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.
        /// </summary>
        internal static RecordedList GetRecordedList()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = dvbApi.GetDataAsync("sql",
                    new List<UriParameter> {
                    new UriParameter("rec", "1"),
                    new UriParameter("query", "Select * from recordedlist")
                    }).Result;

                if (xmldata != null)
                {
                    return CreateRecordedList(xmldata);
                }
            }
            return null;
        }
    }
}
