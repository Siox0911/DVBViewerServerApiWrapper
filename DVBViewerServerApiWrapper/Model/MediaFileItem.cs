using DVBViewerServerApiWrapper.Helper;
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
    /// EIn Item in der MediaFileList
    /// </summary>
    [XmlRoot(ElementName = "file")]
    public class MediaFileItem
    {
        /// <summary>
        /// Der Name des Videos, bzw. dessen Dateiname
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Die interne ObjectID
        /// </summary>
        [XmlAttribute(AttributeName = "objid")]
        public int ObjectID { get; set; }

        /// <summary>
        /// Der Titel des Videos
        /// </summary>
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Die Länge des Videos
        /// </summary>
        [XmlAttribute(AttributeName = "dur")]
        public int Duration { get; set; }

        /// <summary>
        /// Die Horizontale Auflösung des Videos
        /// </summary>
        [XmlAttribute(AttributeName = "hres")]
        public int HResolution { get; set; }

        /// <summary>
        /// Die Vertikale Auflösung des Videos
        /// </summary>
        [XmlAttribute(AttributeName = "vres")]
        public int VResolution { get; set; }

        /// <summary>
        /// Die Videovorschaudatei
        /// </summary>
        [XmlElement(ElementName = "thumb")]
        public string Image { get; set; }

        internal MediaFileItem() { }
    }
}