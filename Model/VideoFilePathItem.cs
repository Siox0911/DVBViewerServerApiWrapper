using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Represents a video folder
    /// </summary>
    [XmlRoot(ElementName = "row")]
    public class VideoFilePathItem
    {
        /// <summary>
        /// Die ID des Pfades
        /// ID of the path
        /// </summary>
        [XmlElement(ElementName = "IDPATH")]
        public int ID { get; set; }

        /// <summary>
        /// Der Name des Pfades
        /// Name of the path
        /// </summary>
        [XmlElement(ElementName = "FOLDER")]
        public string Name { get; set; }

        /// <summary>
        /// Der volle Pfad
        /// The complete Path
        /// </summary>
        [XmlElement(ElementName = "PATH")]
        public string FullName { get; set; }

        /// <summary>
        /// Die Objekt ID aus der Tabelle Objects
        /// Object ID from table objects
        /// </summary>
        [XmlElement(ElementName = "OBJECT_ID")]
        public int ObjectID { get; set; }

        /// <summary>
        /// Die übergeordnete ID aus der Tabelle Objects
        /// The parent ID from the Objects table
        /// </summary>
        [XmlElement(ElementName = "PARENT_ID")]
        public int ParentID { get; set; }

        /// <summary>
        /// Gibt eine Liste mit Pfaden zurück, welche diesem untergeordnet sind
        /// Returns a list of paths that are subordinate to it
        /// </summary>
        public Task<VideoFilePath> ChildPathsAsync { get { return VideoFilePath.GetVideoFilePathChildsAsync(ObjectID); } }

        /// <summary>
        /// Gibt eine Liste mit Pfaden zurück, welche diesem untergeordnet sind
        /// Returns a list of paths that are subordinate to it
        /// </summary>
        public VideoFilePath ChildPaths { get { return VideoFilePath.GetVideoFilePathChilds(ObjectID); } }

        /// <summary>
        /// Gibt einen Pfad zurück, welche diesem Pfad übergeordnet ist
        /// Returns a path above this path
        /// </summary>
        public Task<VideoFilePath> ParentPathAsync { get { return VideoFilePath.GetVideoFilePathParentsAsync(ParentID); } }

        /// <summary>
        /// Gibt einen Pfad zurück, welche diesem Pfad übergeordnet ist
        /// Returns a path above this path
        /// </summary>
        public VideoFilePath ParentPath { get { return VideoFilePath.GetVideoFilePathParents(ParentID); } }

        /// <summary>
        /// Eine Liste von Videos im aktuellen Verzeichnis
        /// A list of videos in the current directory
        /// </summary>
        public Task<VideoFileList> VideosAsync { get { return VideoFileList.GetVideoFileListAsync(ObjectID); } }

        /// <summary>
        /// Eine Liste von Videos im aktuellen Verzeichnis
        /// A list of videos in the current directory
        /// </summary>
        public VideoFileList Videos { get { return VideoFileList.GetVideoFileList(ObjectID); } }

        /// <summary>
        /// Eine Liste mit Videos im aktuellen und allen Unterverzeichnissen
        /// A list of videos in the current and all subdirectories
        /// </summary>
        public Task<VideoFileList> SubVideosAsync { get { return VideoFileList.GetVideoFileListRecursiveAsync(ObjectID); } }

        /// <summary>
        /// Eine Liste mit Videos im aktuellen und allen Unterverzeichnissen
        /// A list of videos in the current and all subdirectories
        /// </summary>
        public VideoFileList SubVideos { get { return VideoFileList.GetVideoFileListRecursive(ObjectID); } }

        internal VideoFilePathItem() { }
    }
}
