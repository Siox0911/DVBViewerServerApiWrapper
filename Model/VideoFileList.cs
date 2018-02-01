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
    /// Eine Liste mit Videos im Media Server
    /// </summary>
    [XmlRoot(ElementName = "table")]
    public class VideoFileList
    {
        /// <summary>
        /// Die Videos als Liste
        /// </summary>
        [XmlElement(ElementName = "row", Type = typeof(VideoFileItem))]
        public List<VideoFileItem> Items { get; set; }

        internal static VideoFileList CreateVideoFileList(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<VideoFileList>(xDocument, new Type[] { typeof(VideoFileItem) });
        }

        /// <summary>
        /// Eine Liste mit Videos, welche bis zur letzten Aktualisierung der Datenbank vorhanden waren.
        /// </summary>
        /// <returns></returns>
        internal static VideoFileList GetVideoFileList()
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

                var xmldata = dvbApi.GetDataAsync("sql", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Video1,
                    Helper.UriParam.Query(query)
                }).Result;

                if (xmldata != null)
                {
                    return CreateVideoFileList(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Eine Liste mit Videos, welche bis zur letzten Aktualisierung der Datenbank vorhanden waren. Filtert nach einem Teil des Namens.
        /// </summary>
        /// <param name="partOfTitle"></param>
        /// <returns></returns>
        public static VideoFileList GetVideoFileList(string partOfTitle)
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

                var xmldata = dvbApi.GetDataAsync("sql", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Video1,
                    Helper.UriParam.Query(query)
                }).Result;

                if (xmldata != null)
                {
                    return CreateVideoFileList(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Erneuert die Videodatenbank. Dazu werden alle Informationen in der DB gelöscht und wieder neu erstellt. 
        /// </summary>
        /// <returns></returns>
        public Task<HttpStatusCode> ReCreateVideoDatabase()
        {
            //Instanz besorgen
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            //Gruppe ermitteln
            var taskgroup = (from f in dvbApi?.ServerTasks.Groups where f.Name.IndexOf("Mediadateien", StringComparison.OrdinalIgnoreCase) != -1 select f).FirstOrDefault();
            //Task ermitteln
            var task = (from f in taskgroup?.TaskItems where f.Action.IndexOf("RebuildVideoDB", StringComparison.OrdinalIgnoreCase) != -1 select f).FirstOrDefault();
            //Starten
            return task?.RunTask();
        }

        /// <summary>
        /// Bereinigt die Videodatenbank. 
        /// </summary>
        /// <returns></returns>
        public Task<HttpStatusCode> CleanUpVideoDatabase()
        {
            //Instanz besorgen
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            //Gruppe ermitteln
            var taskgroup = (from f in dvbApi?.ServerTasks.Groups where f.Name.IndexOf("Mediadateien", StringComparison.OrdinalIgnoreCase) != -1 select f).FirstOrDefault();
            //Task ermitteln
            var task = (from f in taskgroup?.TaskItems where f.Action.IndexOf("CleanUpVideoDB", StringComparison.OrdinalIgnoreCase) != -1 select f).FirstOrDefault();
            //Starten
            return task?.RunTask();
        }

        /// <summary>
        /// Erzeugt aus der Liste der Videos eine M3U Datei. Die Datei befindet sich normalerweise im Tempverzeichnis
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
                            Items[i].Title = $"[{i+1} / {Items.Count}] {oldTitle}";
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
