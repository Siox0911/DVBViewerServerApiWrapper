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
    /// Ein Pool mit allen existierenden Aufnahmen. Dies ist eine Basisklasse, welche selbst zusätzliche Informationen und zudem die Liste der Aufnahmen enthält.
    /// </summary>
    [XmlRoot(ElementName = "recordings")]
    public class RecordingList
    {
        /// <summary>
        /// Version der Aufnahmetabelle
        /// </summary>
        [XmlAttribute(AttributeName = "Ver")]
        public int Version { get; set; }
        /// <summary>
        /// Revision der Aufnahmetabelle
        /// </summary>
        [XmlElement(ElementName = "rev", Type = typeof(int))]
        public int Revision { get; set; }
        /// <summary>
        /// Die Basisserver URL. Um daraus einen Stream aufzubauen: ServerURL + ID + ".ts"
        /// </summary>
        [XmlElement(ElementName = "serverURL")]
        public string ServerURL { get; set; }
        /// <summary>
        /// Die Basis URL für Bilder. Bildurl = ImageURL + Record.Image (falls thumbnails aktiviert sind)
        /// </summary>
        [XmlElement(ElementName = "imageURL")]
        public string ImageURL { get; set; }
        /// <summary>
        /// Die komplette Liste der Aufnahmen
        /// </summary>
        [XmlElement(ElementName = "recording", Type = typeof(RecordingItem))]
        public List<RecordingItem> Items { get; set; }

        internal RecordingList() { }

        /// <summary>
        /// Erzeugt ein Recording
        /// </summary>
        /// <param name="xDocument"></param>
        /// <returns></returns>
        internal static RecordingList CreateRecording(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<RecordingList>(xDocument, new Type[] {
                typeof(RecordingItem),
                typeof(RecordingSeries),
                typeof(RecordingChannel)
            });
        }

        /// <summary>
        /// Gibt alle Aufnahmen zurück.
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        internal static RecordingList GetRecordings()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = dvbApi.GetDataAsync("recordings", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Utf81,
                    Helper.UriParam.EventID1,
                    Helper.UriParam.Images1
                }).Result;

                if (xmldata != null)
                {
                    return CreateRecording(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt alle Aufnahmen vom Service zurück. Es fehlen darin Dateinamen und die lange Beschreibung.
        /// </summary>
        /// <returns></returns>
        internal static RecordingList GetRecordingsShort()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = dvbApi.GetDataAsync("recordings", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Utf81 ,
                    Helper.UriParam.Images1,
                    Helper.UriParam.NoDesc1,
                    Helper.UriParam.NoFileName1,
                    Helper.UriParam.EventID1
                }).Result;

                if (xmldata != null)
                {
                    return CreateRecording(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Aufnahme anhand der AufnahmeID zurück.
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        public static RecordingList GetRecording(int recordID)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = dvbApi.GetDataAsync("recordings", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Utf81,
                    Helper.UriParam.Images1,
                    Helper.UriParam.EventID1,
                    new Helper.UriParameter("id", recordID.ToString())
                }).Result;

                if (xmldata != null)
                {
                    return CreateRecording(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche den Titel enthalten
        /// </summary>
        /// <param name="partOfName">Teil des Namens</param>
        /// <returns></returns>
        public static RecordingList GetRecordings(string partOfName)
        {
            var result = GetRecordings();
            if (result != null)
            {
                result.Items = (from f in result.Items where f.Title.IndexOf(partOfName, StringComparison.OrdinalIgnoreCase) != -1 select f).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche zu dieser Serie aufgenommen wurden.
        /// </summary>
        /// <param name="recordingSeries"></param>
        /// <returns></returns>
        public static RecordingList GetRecordings(RecordingSeries recordingSeries)
        {
            var result = GetRecordings();
            if(result != null)
            {
                result.Items = (from f in result.Items where f.Series?.Name.Equals(recordingSeries.Name) == true select f).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche Text als Teil in der Beschreibung haben.
        /// </summary>
        /// <param name="partOfDesc">Ein Teil der Beschreibung</param>
        /// <returns></returns>
        public static RecordingList GetRecordingsByDesc(string partOfDesc)
        {
            var result = GetRecordings();
            if (result != null)
            {
                result.Items = (from f in result.Items where f.Description?.IndexOf(partOfDesc, StringComparison.OrdinalIgnoreCase) != -1 select f).ToList();
            }
            return result;
        }



        /// <summary>
        /// Erzeugt aus der Liste der Videos eine M3U Datei. Die Datei befindet sich normalerweise im Tempverzeichnis
        /// </summary>
        /// <returns>Ein Pfad zur m3u Datei</returns>
        public string CreateM3UFile()
        {
            if (Items.Count > 0)
            {
                var tPath = System.IO.Path.GetTempPath();
                var fName = $"{Items[0].ID}.m3u";
                var cPathName = tPath + fName;
                using (var fStream = new System.IO.FileStream(cPathName, System.IO.FileMode.OpenOrCreate))
                {
                    using (var sw = new System.IO.StreamWriter(fStream))
                    {
                        for (int i = 0; i < Items.Count; i++)
                        {
                            var oldTitle = Items[i].Title;
                            Items[i].Title = $"[{i + 1} / {Items.Count}] {oldTitle}";
                            sw.WriteLine(Items[i].GetM3uPrefString());
                            Items[i].Title = oldTitle;
                            sw.WriteLine(Items[i].GetUPnPUriString());
                        }
                    }
                }

                return cPathName;
            }
            return null;
        }

    }
}
