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
        /// <summary>
        /// Die Videos als Liste.
        /// The videos as a list
        /// </summary>
        [XmlElement(ElementName = "row", Type = typeof(VideoFileItem))]
        public List<VideoFileItem> Items { get; set; }

        internal VideoFileList() { }

        internal static VideoFileList CreateVideoFileList(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<VideoFileList>(xDocument, new Type[] { typeof(VideoFileItem) });
        }

        private static string baseSQL = "SELECT objects.Object_ID, objects.Enabled, " +
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
        public static async Task<VideoFileList> GetVideoFileListAsync()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                string query = $"{baseSQL} AND Type = 3";

                var xmldata = await dvbApi.GetDataAsync("sql", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Video1,
                    Helper.UriParam.Query(query)
                }).ConfigureAwait(false);

                if (xmldata != null)
                {
                    return CreateVideoFileList(xmldata);
                }
            }
            return null;
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
        public static async Task<VideoFileList> GetVideoFileListAsync(string partOfTitle)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                //very simple SQL-Injektion prevention. Not perfekt, but the database is readonly...
                partOfTitle = partOfTitle.Replace("'", "''");

                string query = $"{baseSQL} AND Type = 3 AND object_details.Titel like '%" + partOfTitle + "%'";

                var xmldata = await dvbApi.GetDataAsync("sql", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Video1,
                    Helper.UriParam.Query(query)
                }).ConfigureAwait(false);

                if (xmldata != null)
                {
                    return CreateVideoFileList(xmldata);
                }
            }
            return null;
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
        public static async Task<VideoFileList> GetVideoFileListByPathAsync(string partOfPath)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                //very simple SQL-Injektion prevention. Not perfekt, but the database is readonly...
                partOfPath = partOfPath.Replace("'", "''");

                string query = $"{baseSQL} AND Type = 3 AND paths.Path like '%" + partOfPath + "%'";

                var xmldata = await dvbApi.GetDataAsync("sql", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Video1,
                    Helper.UriParam.Query(query)
                }).ConfigureAwait(false);

                if (xmldata != null)
                {
                    return CreateVideoFileList(xmldata);
                }
            }
            return null;
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
        internal static async Task<VideoFileList> GetVideoFileListFromFolderAsync(int parentID)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                string query = $"{baseSQL} AND Type = 3 AND parent_ID = " + parentID;

                var xmldata = await dvbApi.GetDataAsync("sql", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Video1,
                    Helper.UriParam.Query(query)
                }).ConfigureAwait(false);

                if (xmldata != null)
                {
                    return CreateVideoFileList(xmldata);
                }
            }
            return null;
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
        public async Task<HttpStatusCode> ReCreateVideoDatabaseAsync()
        {
            //Instanz besorgen
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            //all groups
            var allgroups = await dvbApi?.ServerTasksAsync;
            //Gruppe ermitteln
            var taskgroup = (from f in allgroups.Groups where f.Name.IndexOf("Mediadateien", StringComparison.OrdinalIgnoreCase) != -1 select f).FirstOrDefault();
            //Task ermitteln
            var task = (from f in taskgroup?.TaskItems where f.Action.IndexOf("RebuildVideoDB", StringComparison.OrdinalIgnoreCase) != -1 select f).FirstOrDefault();
            //Starten
            return await task?.RunTaskAsync();
        }

        /// <summary>
        /// Erneuert die Videodatenbank. Dazu werden alle Informationen in der DB gelöscht und wieder neu erstellt. 
        /// Renews the video database. All information in the DB is deleted and recreated.
        /// </summary>
        /// <returns></returns>
        public HttpStatusCode ReCreateVideoDatabase()
        {
            return ReCreateVideoDatabaseAsync().Result;
        }

        /// <summary>
        /// Bereinigt die Videodatenbank. 
        /// Cleans up the video database.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpStatusCode> CleanUpVideoDatabaseAsync()
        {
            //Instanz besorgen
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            //all groups
            var allgroups = await dvbApi?.ServerTasksAsync;
            //Gruppe ermitteln
            var taskgroup = (from f in allgroups.Groups where f.Name.IndexOf("Mediadateien", StringComparison.OrdinalIgnoreCase) != -1 select f).FirstOrDefault();
            //Task ermitteln
            var task = (from f in taskgroup?.TaskItems where f.Action.IndexOf("CleanUpVideoDB", StringComparison.OrdinalIgnoreCase) != -1 select f).FirstOrDefault();
            //Starten
            return await task?.RunTaskAsync();
        }

        /// <summary>
        /// Bereinigt die Videodatenbank. 
        /// Cleans up the video database.
        /// </summary>
        /// <returns></returns>
        public HttpStatusCode CleanUpVideoDatabase()
        {
            return CleanUpVideoDatabaseAsync().Result;
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
                using (var fStream = new FileStream(cPathName, FileMode.OpenOrCreate))
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
