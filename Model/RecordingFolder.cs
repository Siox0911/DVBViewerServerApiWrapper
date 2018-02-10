using DVBViewerServerApiWrapper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Stellt ein Aufnahmeverzeichnis im Service dar.
    /// Represents a host directory in the service.
    /// </summary>
    [XmlRoot(ElementName = "recfolders")]
    public class RecordingFolder
    {
        /// <summary>
        /// Der Verzeichnispfad wie er im Service festgelegt wurde.
        /// The directory path as defined in the service
        /// </summary>
        [XmlText(Type = typeof(string))]
        public string Folder { get; set; }
        /// <summary>
        /// Die Größe der Festplatte auf dem sich das Verzeichnis befindet.
        /// The size of the hard disk on which the directory is located
        /// </summary>
        [XmlAttribute(AttributeName = "size")]
        public long Size { get; set; }
        /// <summary>
        /// Gibt die Größe der Festplatte als formatiertes Objekt zurück.
        /// Returns the size of the disk as a formatted object.
        /// </summary>
        public FileSize SizeF { get { return FileSize.GetFileSize(Size); } }
        /// <summary>
        /// Die Größe des noch freien Speichers - Minus des frei zuhaltenden Speichers
        /// The size of the free memory - Minus of the free-to-hold memory
        /// </summary>
        [XmlAttribute(AttributeName = "free")]
        public long Free { get; set; }
        /// <summary>
        /// Die Größe des freien Speichers als formatiertes Objekt.
        /// The amount of free memory as a formatted object.
        /// </summary>
        public FileSize FreeF { get { return FileSize.GetFileSize(Free); } }

        internal RecordingFolder() { }
    }

}
