using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Eine Liste mit Videos im Media Server.
    /// A list of videos in the Media Server
    /// </summary>
    [XmlRoot(ElementName = "table")]
    public class VideoFileList : IDisposable
    {
        private static VideoFileList videoFileList;

        /// <summary>
        /// Die Videos als Liste.
        /// The videos as a list
        /// </summary>
        [XmlElement(ElementName = "row", Type = typeof(VideoFileItem))]
        public List<VideoFileItem> Items { get; set; }

        internal VideoFileList() => videoFileList = this;

        internal static VideoFileList GetInstance() => videoFileList;

        internal static Task<VideoFileList> GetVideoFileListAsync(List<Helper.UriParameter> uriParameters)
        {
            return Helper.Lists.GetListAsync<VideoFileList>("sql", uriParameters, new Type[] { typeof(VideoFileItem) });
        }

        private const string baseSQL = "SELECT objects.Object_ID, objects.Enabled, " +
                                        "objects.found, object_details.Details_ID, " +
                                        "objects.Ext, paths.idPath, objects.Filename, paths.Path, " +
                                        "object_details.channel, object_details.Added, " +
                                        "object_details.Description, object_details.Duration, " +
                                        "object_details.Info, object_details.Lastplayed, " +
                                        "object_details.Time, object_details.Titel, " +
                                        "object_details.filesize " +
                                            "FROM paths INNER JOIN objects " +
                                            "ON paths.idPath = objects.PathID " +
                                            "INNER JOIN object_details " +
                                            "ON objects.Detail_ID = object_details.Details_ID " +
                                        "WHERE found = 1";

        /// <summary>
        /// Eine Liste mit Videos, welche bis zur letzten Aktualisierung der Datenbank vorhanden waren.
        /// A list of videos that existed until the last time the database was updated.
        /// </summary>
        /// <returns></returns>
        public static Task<VideoFileList> GetVideoFileListAsync()
        {
            string query = $"{baseSQL} AND Type = 3";

            return GetVideoFileListAsync(new List<Helper.UriParameter>
            {
                Helper.UriParam.Video1,
                Helper.UriParam.Query(query)
            });
        }

        /// <summary>
        /// Eine Liste mit Videos, welche bis zur letzten Aktualisierung der Datenbank vorhanden waren.
        /// A list of videos that existed until the last time the database was updated.
        /// </summary>
        /// <returns></returns>
        public static VideoFileList GetVideoFileList()
        {
            return GetVideoFileListAsync().Result;
        }

        /// <summary>
        /// Eine Liste mit Videos, welche bis zur letzten Aktualisierung der Datenbank vorhanden waren. Filtert nach einem Teil des Namens.
        /// A list of videos that existed until the last time the database was updated. Filters for a part of the name.
        /// </summary>
        /// <param name="partOfTitle"></param>
        /// <returns></returns>
        public static Task<VideoFileList> GetVideoFileListAsync(string partOfTitle)
        {
            //very simple SQL-Injektion prevention. Not perfekt, but the database is readonly...
            partOfTitle = partOfTitle.Replace("'", "''");

            string query = $"{baseSQL} AND Type = 3 AND object_details.Titel like '%" + partOfTitle + "%'";

            return GetVideoFileListAsync(new List<Helper.UriParameter>
            {
                Helper.UriParam.Video1,
                Helper.UriParam.Query(query)
            });
        }

        /// <summary>
        /// Eine Liste mit Videos, welche bis zur letzten Aktualisierung der Datenbank vorhanden waren. Filtert nach einem Teil des Namens.
        /// A list of videos that existed until the last time the database was updated. Filters for a part of the name.
        /// </summary>
        /// <param name="partOfTitle"></param>
        /// <returns></returns>
        public static VideoFileList GetVideoFileList(string partOfTitle)
        {
            return GetVideoFileListAsync(partOfTitle).Result;
        }

        /// <summary>
        /// Eine Liste mit Videos, welche bis zur letzten Aktualisierung der Datenbank vorhanden waren. Filtert nach einem Teil des Pfades.
        /// A list of videos that existed until the last time the database was updated. Filters for part of the path.
        /// </summary>
        /// <param name="partOfPath"></param>
        /// <returns></returns>
        public static Task<VideoFileList> GetVideoFileListByPathAsync(string partOfPath)
        {
            //very simple SQL-Injektion prevention. Not perfekt, but the database is readonly...
            partOfPath = partOfPath.Replace("'", "''");

            string query = $"{baseSQL} AND Type = 3 AND paths.Path like '%" + partOfPath + "%'";

            return GetVideoFileListAsync(new List<Helper.UriParameter>
            {
                Helper.UriParam.Video1,
                Helper.UriParam.Query(query)
            });
        }

        /// <summary>
        /// Eine Liste mit Videos, welche bis zur letzten Aktualisierung der Datenbank vorhanden waren. Filtert nach einem Teil des Pfades.
        /// A list of videos that existed until the last time the database was updated. Filters for part of the path.
        /// </summary>
        /// <param name="partOfPath"></param>
        /// <returns></returns>
        public static VideoFileList GetVideoFileListByPath(string partOfPath)
        {
            return GetVideoFileListByPathAsync(partOfPath).Result;
        }

        /// <summary>
        /// Gibt alle Videos im aktuellen Verzeichnis und den Unterverzeichnissen zurück. Leider dauert das eine Weile.
        /// Returns all videos in the current directory and subdirectories. Unfortunately, that takes a while.
        /// </summary>
        /// <param name="pathObjectID"></param>
        /// <returns></returns>
        internal static async Task<VideoFileList> GetVideoFileListFromFolderRecursiveAsync(int pathObjectID)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null && pathObjectID > 0)
            {
                var currentDir = await VideoFilePath.GetVideoFilePathParentsAsync(pathObjectID).ConfigureAwait(false);
                //Im aktuellen Verzeichnis die Videos holen
                var videos = await GetVideoFileListFromFolderAsync(pathObjectID).ConfigureAwait(false);
                foreach (var item in currentDir.Items)
                {
                    var childpath = await item.ChildPathsAsync;
                    //videos.Items.AddRange(GetVideoFileList(item.ObjectID).Items);
                    if (childpath.Items.Count > 0)
                    {
                        foreach (var item2 in childpath.Items)
                        {
                            var recListe = await GetVideoFileListFromFolderRecursiveAsync(item2.ObjectID).ConfigureAwait(false);
                            videos.Items.AddRange(recListe.Items);
                        }
                    }
                }

                return videos;
            }
            return null;
        }

        /// <summary>
        /// Gibt alle Videos im aktuellen Verzeichnis und den Unterverzeichnissen zurück. Leider dauert das eine Weile.
        /// Returns all videos in the current directory and subdirectories. Unfortunately, that takes a while.
        /// </summary>
        /// <param name="pathObjectID"></param>
        /// <returns></returns>
        internal static VideoFileList GetVideoFileListFromFolderRecursive(int pathObjectID)
        {
            return GetVideoFileListFromFolderRecursiveAsync(pathObjectID).Result;
        }

        /// <summary>
        /// Gibt eine Liste von Videos im angegebenen Verzeichnis zurück.
        /// Returns a list of videos in the specified directory.
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        internal static Task<VideoFileList> GetVideoFileListFromFolderAsync(int parentID)
        {
            string query = $"{baseSQL} AND Type = 3 AND parent_ID = " + parentID;

            return GetVideoFileListAsync(new List<Helper.UriParameter>
            {
                Helper.UriParam.Video1,
                Helper.UriParam.Query(query)
            });
        }

        /// <summary>
        /// Gibt eine Liste von Videos im angegebenen Verzeichnis zurück.
        /// Returns a list of videos in the specified directory.
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        internal static VideoFileList GetVideoFileListFromFolder(int parentID)
        {
            return GetVideoFileListFromFolderAsync(parentID).Result;
        }

        /// <summary>
        /// Erneuert die Videodatenbank. Dazu werden alle Informationen in der DB gelöscht und wieder neu erstellt. 
        /// Renews the video database. All information in the DB is deleted and recreated.
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpStatusCode> ReCreateVideoDatabaseAsync()
        {
            var tasks = await ServerTaskList.GetServerTaskListAsync().ConfigureAwait(false);
            if (tasks != null)
            {
                var task = (from f in tasks.Groups.Where(p => p.TaskItems.Any(x => x.Action.IndexOf("RebuildVideoDB", StringComparison.OrdinalIgnoreCase) != -1)) select f.TaskItems).FirstOrDefault()?.FirstOrDefault();
                return await task?.RunTaskAsync();
            }
            return 0;
        }

        /// <summary>
        /// Erneuert die Videodatenbank. Dazu werden alle Informationen in der DB gelöscht und wieder neu erstellt. 
        /// Renews the video database. All information in the DB is deleted and recreated.
        /// </summary>
        /// <returns></returns>
        public static HttpStatusCode ReCreateVideoDatabase()
        {
            return ReCreateVideoDatabaseAsync().Result;
        }

        /// <summary>
        /// Aktualisiert die Videodatenbank. Dazu werden neue Inhalte aufgenommmen.
        /// Updates the video database. For this new content is added.
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpStatusCode> UpdateVideoDatabaseAsync()
        {
            var tasks = await ServerTaskList.GetServerTaskListAsync().ConfigureAwait(false);
            if (tasks != null)
            {
                var task = (from f in tasks.Groups.Where(p => p.TaskItems.Any(x => x.Action.IndexOf("UpdateVideoDB", StringComparison.OrdinalIgnoreCase) != -1)) select f.TaskItems).FirstOrDefault()?.FirstOrDefault();
                return await task?.RunTaskAsync();
            }
            return 0;
        }

        /// <summary>
        /// Aktualisiert die Videodatenbank. Dazu werden neue Inhalte aufgenommmen.
        /// Updates the video database. For this new content is added.
        /// </summary>
        /// <returns></returns>
        public static HttpStatusCode UpdateVideoDatabase()
        {
            return UpdateVideoDatabaseAsync().Result;
        }

        /// <summary>
        /// Bereinigt die Videodatenbank. Dazu werden Einträge welche nicht mehr existieren entfernt.
        /// Cleans up the video database. For this purpose, entries that no longer exist are removed.
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpStatusCode> CleanUpVideoDatabaseAsync()
        {
            var tasks = await ServerTaskList.GetServerTaskListAsync().ConfigureAwait(false);
            if (tasks != null)
            {
                var task = (from f in tasks.Groups.Where(p => p.TaskItems.Any(x => x.Action.IndexOf("CleanUpVideoDB", StringComparison.OrdinalIgnoreCase) != -1)) select f.TaskItems).FirstOrDefault()?.FirstOrDefault();
                return await task?.RunTaskAsync();
            }
            return 0;
        }

        /// <summary>
        /// Bereinigt die Videodatenbank. Dazu werden Einträge welche nicht mehr existieren entfernt.
        /// Cleans up the video database. For this purpose, entries that no longer exist are removed.
        /// </summary>
        /// <returns></returns>
        public static HttpStatusCode CleanUpVideoDatabase()
        {
            return CleanUpVideoDatabaseAsync().Result;
        }

        /// <summary>
        /// Löscht die Wiedergabe-Statistik der Videodatenbank.
        /// Clears the playback statistics of the video database.
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpStatusCode> ClearVideoStatsAsync()
        {
            var tasks = await ServerTaskList.GetServerTaskListAsync().ConfigureAwait(false);
            if (tasks != null)
            {
                var task = (from f in tasks.Groups.Where(p => p.TaskItems.Any(x => x.Action.IndexOf("ClearVideoStats", StringComparison.OrdinalIgnoreCase) != -1)) select f.TaskItems).FirstOrDefault()?.FirstOrDefault();
                return await task?.RunTaskAsync();
            }
            return 0;
        }

        /// <summary>
        /// Löscht die Wiedergabe-Statistik der Videodatenbank.
        /// Clears the playback statistics of the video database.
        /// </summary>
        /// <returns></returns>
        public static HttpStatusCode ClearVideoStats()
        {
            return ClearVideoStatsAsync().Result;
        }

        /// <summary>
        /// Erzeugt aus der Liste der Videos eine M3U Datei. Die Datei befindet sich normalerweise im Tempverzeichnis.
        /// Generates an M3U file from the list of videos. The file is usually located in the Temp directory
        /// </summary>
        /// <returns>Ein Pfad zur m3u Datei</returns>
        public string CreateM3UFile()
        {
            if (Items.Count > 0)
            {
                var tPath = Path.GetTempPath();
                var fName = $"{Items[0].Title}.m3u";
                var cPathName = tPath + fName;
                using (var fStream = new FileStream(cPathName, FileMode.Create))
                {
                    using (var sw = new StreamWriter(fStream))
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

        /// <summary>
        /// Führt anwendungsspezifische Aufgaben durch, die mit der Freigabe, der Zurückgabe oder dem Zurücksetzen von nicht verwalteten Ressourcen zusammenhängen.
        ///</summary>
        public void Dispose()
        {
            Items = null;
        }
    }
}
