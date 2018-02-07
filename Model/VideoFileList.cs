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
    public class VideoFileList
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

        /// <summary>
        /// Eine Liste mit Videos, welche bis zur letzten Aktualisierung der Datenbank vorhanden waren.
        /// A list of videos that existed until the last time the database was updated.
        /// </summary>
        /// <returns></returns>
        public static async Task<VideoFileList> GetVideoFileList()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                const string query = "Select objects.Object_ID, objects.Enabled, objects.found, object_details.Details_ID, " +
                                        "objects.Ext, paths.idPath, objects.Filename, paths.Path, " +
                                        "object_details.channel, object_details.Added, " +
                                        "object_details.Description, object_details.Duration, " +
                                        "object_details.Info, object_details.Lastplayed, " +
                                        "object_details.Time, object_details.Titel, " +
                                        "object_details.filesize " +
                                   "from paths inner join objects " +
                                        "on paths.idPath = objects.PathID " +
                                        "inner join object_details " +
                                        "on objects.Detail_ID = object_details.Details_ID " +
                                        "where Type=3";

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
        public static async Task<VideoFileList> GetVideoFileList(string partOfTitle)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                //very simple SQL-Injektion prevention. Not perfekt, but the database is readonly...
                partOfTitle = partOfTitle.Replace("'", "''");

                string query = "Select objects.Object_ID, objects.Enabled, objects.found, object_details.Details_ID, " +
                                    "objects.Ext, paths.idPath, objects.Filename, paths.Path, " +
                                    "object_details.channel, object_details.Added, " +
                                    "object_details.Description, object_details.Duration, " +
                                    "object_details.Info, object_details.Lastplayed, " +
                                    "object_details.Time, object_details.Titel, " +
                                    "object_details.filesize " +
                               "from paths inner join objects " +
                                    "on paths.idPath = objects.PathID " +
                                    "inner join object_details " +
                                    "on objects.Detail_ID = object_details.Details_ID " +
                                    "where Type=3 AND object_details.Titel like '%" + partOfTitle + "%'";

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
        public static async Task<VideoFileList> GetVideoFileListByPath(string partOfPath)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                //very simple SQL-Injektion prevention. Not perfekt, but the database is readonly...
                partOfPath = partOfPath.Replace("'", "''");

                string query = "Select objects.Object_ID, objects.Enabled, objects.found, object_details.Details_ID, " +
                                    "objects.Ext, paths.idPath, objects.Filename, paths.Path, " +
                                    "object_details.channel, object_details.Added, " +
                                    "object_details.Description, object_details.Duration, " +
                                    "object_details.Info, object_details.Lastplayed, " +
                                    "object_details.Time, object_details.Titel, " +
                                    "object_details.filesize " +
                               "from paths inner join objects " +
                                    "on paths.idPath = objects.PathID " +
                                    "inner join object_details " +
                                    "on objects.Detail_ID = object_details.Details_ID " +
                                    "where Type=3 AND paths.Path like '%" + partOfPath + "%'";

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

        internal static async Task< VideoFileList> GetVideoFileListRecursive(int pathObjectID)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null && pathObjectID > 0)
            {
                var currentDir = await VideoFilePath.GetVideoFilePathParents(pathObjectID).ConfigureAwait(false);
                //Im aktuellen Verzeichnis die Videos holen
                var videos = await GetVideoFileList(pathObjectID).ConfigureAwait(false);
                foreach (var item in currentDir.Items)
                {
                    var childpath = await item.ChildPaths;
                    //videos.Items.AddRange(GetVideoFileList(item.ObjectID).Items);
                    if (childpath.Items.Count > 0)
                    {
                        foreach (var item2 in childpath.Items)
                        {
                            var recListe = await GetVideoFileListRecursive(item2.ObjectID).ConfigureAwait(false);
                            videos.Items.AddRange(recListe.Items);
                        }
                    }
                }

                return videos;
            }
            return null;
        }

        internal static async Task<VideoFileList> GetVideoFileList(int parentID)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                string query = "Select objects.Object_ID, objects.Enabled, objects.found, object_details.Details_ID, " +
                                    "objects.Ext, paths.idPath, objects.Filename, paths.Path, " +
                                    "object_details.channel, object_details.Added, " +
                                    "object_details.Description, object_details.Duration, " +
                                    "object_details.Info, object_details.Lastplayed, " +
                                    "object_details.Time, object_details.Titel, " +
                                    "object_details.filesize " +
                               "from paths inner join objects " +
                                    "on paths.idPath = objects.PathID " +
                                    "inner join object_details " +
                                    "on objects.Detail_ID = object_details.Details_ID " +
                                    "where Type=3 AND parent_ID = " + parentID;

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
        /// Erneuert die Videodatenbank. Dazu werden alle Informationen in der DB gelöscht und wieder neu erstellt. 
        /// Renews the video database. All information in the DB is deleted and recreated.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpStatusCode> ReCreateVideoDatabase()
        {
            //Instanz besorgen
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            //all groups
            var allgroups = await dvbApi?.ServerTasks;
            //Gruppe ermitteln
            var taskgroup = (from f in allgroups.Groups where f.Name.IndexOf("Mediadateien", StringComparison.OrdinalIgnoreCase) != -1 select f).FirstOrDefault();
            //Task ermitteln
            var task = (from f in taskgroup?.TaskItems where f.Action.IndexOf("RebuildVideoDB", StringComparison.OrdinalIgnoreCase) != -1 select f).FirstOrDefault();
            //Starten
            return await task?.RunTask();
        }

        /// <summary>
        /// Bereinigt die Videodatenbank. 
        /// Cleans up the video database.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpStatusCode> CleanUpVideoDatabase()
        {
            //Instanz besorgen
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            //all groups
            var allgroups = await dvbApi?.ServerTasks;
            //Gruppe ermitteln
            var taskgroup = (from f in allgroups.Groups where f.Name.IndexOf("Mediadateien", StringComparison.OrdinalIgnoreCase) != -1 select f).FirstOrDefault();
            //Task ermitteln
            var task = (from f in taskgroup?.TaskItems where f.Action.IndexOf("CleanUpVideoDB", StringComparison.OrdinalIgnoreCase) != -1 select f).FirstOrDefault();
            //Starten
            return await task?.RunTask();
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
    }
}
