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
    /// Gruppe der Server Tasks
    /// </summary>
    [XmlRoot(ElementName = "group")]
    public class ServerTaskGroup
    {
        /// <summary>
        /// Der Name der Gruppe
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Behinhaltet alle Tasks
        /// </summary>
        [XmlElement(ElementName = "task", Type = typeof(ServerTaskItem))]
        public List<ServerTaskItem> TaskItems { get; set; }
    }
}
