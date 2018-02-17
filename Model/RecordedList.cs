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
    /// <para>
    /// Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
    /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.</para>
    /// <para>A list of recordings that were taken sometime. These no longer need to exist as a file. There are no references to filenames.
    /// This is used to recognize already recorded pictures.</para>
    /// </summary>
    [XmlRoot(ElementName = "table")]
    public class RecordedList
    {
        /// <summary>
        /// <para>Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
        /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.</para>
        /// <para>
        /// A list of recordings that were taken sometime. These no longer need to exist as a file. There are no references to filenames.
        /// This is used to recognize already recorded pictures.
        /// </para>
        /// </summary>
        [XmlElement(ElementName = "row", Type = typeof(RecordedItem))]
        public List<RecordedItem> RecordedItems { get; set; }

        internal RecordedList() { }

        internal static RecordedList CreateRecordedList(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<RecordedList>(xDocument, new Type[] { typeof(RecordedItem) });
        }

        /// <summary>
        /// <para>Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
        /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.</para>
        /// <para>
        /// A list of recordings that were taken sometime. These no longer need to exist as a file. There are no references to filenames.
        /// This is used to recognize already recorded pictures.
        /// </para>
        /// </summary>
        public static async Task<RecordedList> GetRecordedListAsync()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = await dvbApi.GetApiDataAsync("sql", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Rec1,
                    Helper.UriParam.Query("Select * from recordedlist")
                }).ConfigureAwait(false);

                if (xmldata != null)
                {
                    return CreateRecordedList(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// <para>Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
        /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.</para>
        /// <para>
        /// A list of recordings that were taken sometime. These no longer need to exist as a file. There are no references to filenames.
        /// This is used to recognize already recorded pictures.
        /// </para>
        /// </summary>
        public static RecordedList GetRecordedList()
        {
            return GetRecordedListAsync().Result;
        }
    }
}
