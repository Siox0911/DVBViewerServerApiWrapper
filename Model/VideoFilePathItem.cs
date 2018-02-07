using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    [XmlRoot(ElementName = "row")]
    public class VideoFilePathItem
    {
        /// <summary>
        /// Die ID des Pfades
        /// </summary>
        [XmlElement(ElementName = "IDPATH")]
        public int ID { get; set; }

        /// <summary>
        /// Der Name des Pfades
        /// </summary>
        [XmlElement(ElementName = "FOLDER")]
        public string Name { get; set; }

        /// <summary>
        /// Der volle Pfad
        /// </summary>
        [XmlElement(ElementName = "PATH")]
        public string FullName { get; set; }

        /// <summary>
        /// Die Objekt ID aus der Tabelle Objects
        /// </summary>
        [XmlElement(ElementName = "OBJECT_ID")]
        public int ObjectID { get; set; }

        /// <summary>
        /// Die übergeordnete ID aus der Tabelle Objects
        /// </summary>
        [XmlElement(ElementName = "PARENT_ID")]
        public int ParentID { get; set; }

        /// <summary>
        /// Gibt eine Liste mit Pfaden zurück, welche diesem untergeordnet sind
        /// </summary>
        public Task<VideoFilePath> ChildPaths { get { return VideoFilePath.GetVideoFilePathChilds(ObjectID); } }

        /// <summary>
        /// Gibt einen Pfad zurück, welche diesem Pfad übergeordnet ist
        /// </summary>
        public Task<VideoFilePath> ParentPath { get { return VideoFilePath.GetVideoFilePathParents(ParentID); } }

        /// <summary>
        /// Eine Liste von Videos im aktuellen Verzeichnis
        /// </summary>
        public Task<VideoFileList> Videos { get { return VideoFileList.GetVideoFileList(ObjectID); } }

        /// <summary>
        /// Eine Liste mit Videos im aktuellen und allen Unterverzeichnissen
        /// </summary>
        public Task<VideoFileList> SubVideos { get { return VideoFileList.GetVideoFileListRecursive(ObjectID); } }

        internal VideoFilePathItem() { }
    }
}
