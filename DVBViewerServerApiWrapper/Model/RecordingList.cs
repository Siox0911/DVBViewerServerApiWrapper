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
    /// Ein Pool mit allen existierenden Aufnahmen. Dies ist eine Basisklasse, welche selbst zusätzliche Informationen und zudem die Liste der Aufnahmen enthält.
    /// A pool with all existing recordings. This is a base class, which itself contains additional information as well as the list of recordings.
    /// </summary>
    [XmlRoot(ElementName = "recordings")]
    public class RecordingList
    {
        private static RecordingList rList;

        /// <summary>
        /// Version der Aufnahmetabelle
        /// Version of the recording table
        /// </summary>
        [XmlAttribute(AttributeName = "Ver")]
        public int Version { get; set; }

        /// <summary>
        /// Revision der Aufnahmetabelle
        /// Revision of the recording table
        /// </summary>
        [XmlElement(ElementName = "rev", Type = typeof(int))]
        public int Revision { get; set; }

        /// <summary>
        /// Die Basisserver URL. Um daraus einen Stream aufzubauen: ServerURL + ID + ".ts"
        /// The base server URL. To build a stream from it: ServerURL + ID + ".ts"
        /// </summary>
        [XmlElement(ElementName = "serverURL")]
        public string ServerURL { get; set; }

        /// <summary>
        /// Die Basis URL für Bilder. Bildurl = ImageURL + Record.Image (falls thumbnails aktiviert sind)
        /// The base URL for images. Imageurl = ImageURL + Record.Image (if thumbnails are enabled)
        /// </summary>
        [XmlElement(ElementName = "imageURL")]
        public string ImageURL { get; set; }

        /// <summary>
        /// Die komplette Liste der Aufnahmen
        /// The complete list of recordings
        /// </summary>
        [XmlElement(ElementName = "recording", Type = typeof(RecordingItem))]
        public List<RecordingItem> Items { get; set; }

        internal RecordingList() => rList = this;

        internal static RecordingList GetInstance() => rList;

        internal static Task<RecordingList> GetRecordingsAsync(List<Helper.UriParameter> uriParameters)
        {
            return Helper.Lists.GetListAsync<RecordingList>("recordings", uriParameters, new Type[] {
                typeof(RecordingItem),
                typeof(RecordingSeries),
                typeof(RecordingChannel)
            });
        }

        /// <summary>
        /// Gibt alle Aufnahmen zurück.
        /// Returns all recordings.
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        public static Task<RecordingList> GetRecordingsAsync()
        {
            return GetRecordingsAsync(new List<Helper.UriParameter>
            {
                Helper.UriParam.Utf81,
                Helper.UriParam.EventID1,
                Helper.UriParam.Images1
            });
        }

        /// <summary>
        /// Gibt alle Aufnahmen zurück.
        /// Returns all recordings.
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        public static RecordingList GetRecordings()
        {
            return GetRecordingsAsync().Result;
        }

        /// <summary>
        /// Gibt alle Aufnahmen vom Service zurück. Es fehlen darin Dateinamen und die lange Beschreibung.
        /// Returns all recordings of the service. It lacks file names and the long description.
        /// </summary>
        /// <returns></returns>
        public static Task<RecordingList> GetRecordingsShortAsync()
        {
            return GetRecordingsAsync(new List<Helper.UriParameter>
            {
                Helper.UriParam.Utf81 ,
                Helper.UriParam.Images1,
                Helper.UriParam.NoDesc1,
                Helper.UriParam.NoFileName1,
                Helper.UriParam.EventID1
            });
        }

        /// <summary>
        /// Gibt alle Aufnahmen vom Service zurück. Es fehlen darin Dateinamen und die lange Beschreibung.
        /// Returns all recordings of the service. It lacks file names and the long description.
        /// </summary>
        /// <returns></returns>
        public static RecordingList GetRecordingsShort()
        {
            return GetRecordingsShortAsync().Result;
        }

        /// <summary>
        /// Gibt eine Aufnahme anhand der AufnahmeID zurück.
        /// Returns a recording based on the recording ID.
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        public static Task<RecordingList> GetRecordingAsync(int recordID)
        {
            return GetRecordingsAsync(new List<Helper.UriParameter>
            {
                Helper.UriParam.Utf81,
                Helper.UriParam.Images1,
                Helper.UriParam.EventID1,
                new Helper.UriParameter("id", recordID.ToString())
            });
        }

        /// <summary>
        /// Gibt eine Aufnahme anhand der AufnahmeID zurück.
        /// Returns a recording based on the recording ID.
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        public static RecordingList GetRecording(int recordID)
        {
            return GetRecordingAsync(recordID).Result;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche den Titel enthalten
        /// Returns a list of recordings containing the title
        /// </summary>
        /// <param name="partOfName">Teil des Namens</param>
        /// <returns></returns>
        public static async Task<RecordingList> GetRecordingsAsync(string partOfName)
        {
            var result = await GetRecordingsAsync().ConfigureAwait(false);
            if (result != null)
            {
                result.Items = (from f in result.Items where f.Title.IndexOf(partOfName, StringComparison.CurrentCultureIgnoreCase) != -1 select f).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche den Titel enthalten
        /// Returns a list of recordings containing the title
        /// </summary>
        /// <param name="partOfName">Teil des Namens</param>
        /// <returns></returns>
        public static RecordingList GetRecordings(string partOfName)
        {
            return GetRecordingsAsync(partOfName).Result;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche zu dieser Serie aufgenommen wurden.
        /// Returns a list of recordings taken of this series.
        /// </summary>
        /// <param name="recordingSeries"></param>
        /// <returns></returns>
        public static async Task<RecordingList> GetRecordingsAsync(RecordingSeries recordingSeries)
        {
            var result = await GetRecordingsAsync().ConfigureAwait(false);
            if (result != null)
            {
                result.Items = (from f in result.Items where f.Series?.Name.Equals(recordingSeries.Name) == true select f).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche zu dieser Serie aufgenommen wurden.
        /// Returns a list of recordings taken of this series.
        /// </summary>
        /// <param name="recordingSeries"></param>
        /// <returns></returns>
        public static RecordingList GetRecordings(RecordingSeries recordingSeries)
        {
            return GetRecordingsAsync(recordingSeries).Result;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche Text als Teil in der Beschreibung haben.
        /// Returns a list of recordings that have text as part of the description.
        /// </summary>
        /// <param name="partOfDesc">Ein Teil der Beschreibung. Part of the description</param>
        /// <returns></returns>
        public static async Task<RecordingList> GetRecordingsByDescAsync(string partOfDesc)
        {
            var result = await GetRecordingsAsync().ConfigureAwait(false);
            if (result != null)
            {
                result.Items = (from f in result.Items where f.Description?.IndexOf(partOfDesc, StringComparison.CurrentCultureIgnoreCase) != -1 select f).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche Text als Teil in der Beschreibung haben.
        /// Returns a list of recordings that have text as part of the description.
        /// </summary>
        /// <param name="partOfDesc">Ein Teil der Beschreibung. Part of the description</param>
        /// <returns></returns>
        public static RecordingList GetRecordingsByDesc(string partOfDesc)
        {
            return GetRecordingsByDescAsync(partOfDesc).Result;
        }

        /// <summary>
        /// Aktualisiert die Aufnahmedatenbank.
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpStatusCode> RefreshDBAsync()
        {
            var tasks = await ServerTaskList.GetServerTaskListAsync().ConfigureAwait(false);
            if (tasks != null)
            {
                var task = (from f in tasks.Groups.Where(p => p.TaskItems.Any(x => x.Action.IndexOf("RefreshDB", StringComparison.OrdinalIgnoreCase) != -1)) select f.TaskItems).FirstOrDefault()?.FirstOrDefault();
                return await task?.RunTaskAsync();
            }
            return 0;
        }

        /// <summary>
        /// Aktualisiert die Aufnahmedatenbank.
        /// </summary>
        /// <returns></returns>
        public static HttpStatusCode RefreshDB()
        {
            return RefreshDBAsync().Result;
        }

        /// <summary>
        /// Bereinigt die Aufnahme Datenbank. Clean up the recording database.
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpStatusCode> CleanUpDBAsync()
        {
            var tasks = await ServerTaskList.GetServerTaskListAsync().ConfigureAwait(false);
            if (tasks != null)
            {
                var task = (from f in tasks.Groups.Where(p => p.TaskItems.Any(x => x.Action.IndexOf("CleanupDB", StringComparison.OrdinalIgnoreCase) != -1)) select f.TaskItems).FirstOrDefault()?.FirstOrDefault();
                return await task?.RunTaskAsync();
            }
            return 0;
        }

        /// <summary>
        /// Bereinigt die Aufnahme Datenbank. Clean up the recording database.
        /// </summary>
        /// <returns></returns>
        public static HttpStatusCode CleanUpDB()
        {
            return CleanUpDBAsync().Result;
        }

        /// <summary>
        /// Baut die Historie der Aufnahmedatenbank anhand der Aufnahmen wieder auf. Rebuild the recorded hostorie with the current list of the recordings.
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpStatusCode> RebuildRecordedHistoryAsync()
        {
            var tasks = await ServerTaskList.GetServerTaskListAsync().ConfigureAwait(false);
            if (tasks != null)
            {
                var task = (from f in tasks.Groups.Where(p => p.TaskItems.Any(x => x.Action.IndexOf("RebuildRecordedHistory", StringComparison.OrdinalIgnoreCase) != -1)) select f.TaskItems).FirstOrDefault()?.FirstOrDefault();
                return await task?.RunTaskAsync();
            }
            return 0;
        }

        /// <summary>
        /// Baut die Historie der Aufnahmedatenbank anhand der Aufnahmen wieder auf. Rebuild the recorded hostorie with the current list of the recordings.
        /// </summary>
        /// <returns></returns>
        public static HttpStatusCode RebuildRecordedHistory()
        {
            return RebuildRecordedHistoryAsync().Result;
        }

        /// <summary>
        /// Löscht die Aufnahme Historie. Deletes the recording history.
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpStatusCode> ClearRecordingHistoryAsync()
        {
            var tasks = await ServerTaskList.GetServerTaskListAsync().ConfigureAwait(false);
            if (tasks != null)
            {
                var task = (from f in tasks.Groups.Where(p => p.TaskItems.Any(x => x.Action.IndexOf("ClearRecordingHistory", StringComparison.OrdinalIgnoreCase) != -1)) select f.TaskItems).FirstOrDefault()?.FirstOrDefault();
                return await task?.RunTaskAsync();
            }
            return 0;
        }

        /// <summary>
        /// Löscht die Aufnahme Historie. Deletes the recording history.
        /// </summary>
        /// <returns></returns>
        public static HttpStatusCode ClearRecordingHistory()
        {
            return ClearRecordingHistoryAsync().Result;
        }

        /// <summary>
        /// Löscht die Aufnahme Statistik. Deletes the recording statistic.
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpStatusCode> ClearRecordingStatsAsync()
        {
            var tasks = await ServerTaskList.GetServerTaskListAsync().ConfigureAwait(false);
            if (tasks != null)
            {
                var task = (from f in tasks.Groups.Where(p => p.TaskItems.Any(x => x.Action.IndexOf("ClearRecordingStats", StringComparison.OrdinalIgnoreCase) != -1)) select f.TaskItems).FirstOrDefault()?.FirstOrDefault();
                return await task?.RunTaskAsync();
            }
            return 0;
        }

        /// <summary>
        /// Löscht die Aufnahme Statistik. Deletes the recording statistic.
        /// </summary>
        /// <returns></returns>
        public static HttpStatusCode ClearRecordingStats()
        {
            return ClearRecordingStatsAsync().Result;
        }

        /// <summary>
        /// Erzeugt aus der Liste der Videos eine M3U Datei. Die Datei befindet sich normalerweise im Tempverzeichnis.
        /// Generates an M3U file from the list of videos. The file is usually located in the Temp directory
        /// </summary>
        /// <returns>Ein Pfad zur m3u Datei. A path to the m3u file</returns>
        public string CreateM3UFile()
        {
            if (Items.Count > 0)
            {
                var tPath = System.IO.Path.GetTempPath();
                var fName = $"{Items[0].ID}.m3u";
                var cPathName = tPath + fName;
                using (var fStream = new System.IO.FileStream(cPathName, System.IO.FileMode.Create))
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
