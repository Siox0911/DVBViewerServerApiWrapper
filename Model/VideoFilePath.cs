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
    /// Repräsentiert einen Pfad.
    /// Represents a path
    /// </summary>
    [XmlRoot(ElementName = "table")]
    public class VideoFilePath
    {
        /// <summary>
        /// Die Liste der Verzeichnisse.
        /// A List of folders.
        /// </summary>
        [XmlElement(ElementName = "row", Type = typeof(VideoFilePathItem))]
        public List<VideoFilePathItem> Items { get; set; }

        internal VideoFilePath() { }

        internal static Task<VideoFilePath> GetVideoFilePathAsync(List<Helper.UriParameter> uriParameters)
        {
            return Helper.Lists.GetListAsync<VideoFilePath>("sql", uriParameters, new Type[] { typeof(VideoFilePath), typeof(VideoFilePathItem) });
        }

        private static string baseSQL = "Select paths.idPath, paths.Folder, paths.Path, objects.Object_ID, objects.Parent_ID from objects inner join paths on objects.PathID = paths.idPath where objects.Type < 3";

        /// <summary>
        /// Eine Liste mit Pfaden.
        /// A List with paths
        /// </summary>
        /// <returns></returns>
        public static Task<VideoFilePath> GetVideoFilePathAsync()
        {
            string query = $"{baseSQL} Order by paths.Path";

            return GetVideoFilePathAsync(new List<Helper.UriParameter>
            {
                Helper.UriParam.Video1,
                Helper.UriParam.Query(query)
            });
        }

        /// <summary>
        /// Gibt die untergeordneten Verzeichnisse zurück.
        /// Returns the child directories.
        /// </summary>
        /// <param name="objectID"></param>
        /// <returns></returns>
        internal static Task<VideoFilePath> GetVideoFilePathChildsAsync(int objectID)
        {
            string query = $"{baseSQL} and Parent_ID = {objectID} Order by paths.Path";

            return GetVideoFilePathAsync(new List<Helper.UriParameter>
            {
                Helper.UriParam.Video1,
                Helper.UriParam.Query(query)
            });
        }

        /// <summary>
        /// Gibt die untergeordneten Verzeichnisse zurück.
        /// Returns the child directories.
        /// </summary>
        /// <param name="objectID"></param>
        /// <returns></returns>
        internal static VideoFilePath GetVideoFilePathChilds(int objectID)
        {
            return GetVideoFilePathChildsAsync(objectID).Result;
        }

        /// <summary>
        /// Gibt das Elternverzeichnis zurück, oder wenn man die objectID nimmt das aktuelle Verzeichnis.
        /// Returns the parent directory, or if the objectID the current directory
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        internal static Task<VideoFilePath> GetVideoFilePathParentsAsync(int parentID)
        {
            string query = $"{baseSQL} and Object_ID = {parentID} Order by paths.Path";

            return GetVideoFilePathAsync(new List<Helper.UriParameter>
            {
                Helper.UriParam.Video1,
                Helper.UriParam.Query(query)
            });
        }

        /// <summary>
        /// Gibt das Elternverzeichnis zurück, oder wenn man die objectID nimmt das aktuelle Verzeichnis.
        /// Returns the parent directory, or if the objectID takes the current directory
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        internal static VideoFilePath GetVideoFilePathParents(int parentID)
        {
            return GetVideoFilePathParentsAsync(parentID).Result;
        }
    }
}
