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
    /// Repräsentiert einen Pfad
    /// </summary>
    [XmlRoot(ElementName = "table")]
    public class VideoFilePath
    {
        [XmlElement(ElementName = "row", Type = typeof(VideoFilePathItem))]
        public List<VideoFilePathItem> Items { get; set; }

        internal VideoFilePath() { }

        internal static VideoFilePath CreateVideoFilePath(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<VideoFilePath>(xDocument, new Type[] { typeof(VideoFilePath), typeof(VideoFilePathItem) });
        }

        /// <summary>
        /// Eine Liste mit Pfaden
        /// </summary>
        /// <returns></returns>
        public static VideoFilePath GetVideoFilePath()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                const string query = "Select paths.idPath, paths.Folder, paths.Path, objects.Object_ID, objects.Parent_ID from objects inner join paths on objects.PathID = paths.idPath where objects.Type < 3 Order by paths.Path";

                var xmldata = dvbApi.GetDataAsync("sql", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Video1,
                    Helper.UriParam.Query(query)
                }).Result;

                if (xmldata != null)
                {
                    return CreateVideoFilePath(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt die untergeordneten Verzeichnisse zurück.
        /// </summary>
        /// <param name="objectID"></param>
        /// <returns></returns>
        internal static VideoFilePath GetVideoFilePathChilds(int objectID)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                string query = $"Select paths.idPath, paths.Folder, paths.Path, objects.Object_ID, objects.Parent_ID from objects inner join paths on objects.PathID = paths.idPath where objects.Type < 3 and Parent_ID = {objectID} Order by paths.Path";

                var xmldata = dvbApi.GetDataAsync("sql", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Video1,
                    Helper.UriParam.Query(query)
                }).Result;

                if (xmldata != null)
                {
                    return CreateVideoFilePath(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt das Elternverzeichnis zurück, oder wenn man die objectID nimmt das aktuelle Verzeichnis
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        internal static VideoFilePath GetVideoFilePathParents(int parentID)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                string query = $"Select paths.idPath, paths.Folder, paths.Path, objects.Object_ID, objects.Parent_ID from objects inner join paths on objects.PathID = paths.idPath where objects.Type < 3 and Object_ID = {parentID} Order by paths.Path";

                var xmldata = dvbApi.GetDataAsync("sql", new List<Helper.UriParameter>
                {
                    Helper.UriParam.Video1,
                    Helper.UriParam.Query(query)
                }).Result;

                if (xmldata != null)
                {
                    return CreateVideoFilePath(xmldata);
                }
            }
            return null;
        }

    }
}
