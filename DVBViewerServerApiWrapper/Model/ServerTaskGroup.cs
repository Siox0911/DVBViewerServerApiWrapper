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
    /// Gruppe der Server Tasks. Group of the server tasks.
    /// </summary>
    [XmlRoot(ElementName = "group")]
    public class ServerTaskGroup
    {
        /// <summary>
        /// Der Name der Gruppe.
        /// Name of the group
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Behinhaltet alle Tasks.
        /// Contains all tasks
        /// </summary>
        [XmlElement(ElementName = "task", Type = typeof(ServerTaskItem))]
        public List<ServerTaskItem> TaskItems { get; set; }

        internal ServerTaskGroup() { }
    }
}
