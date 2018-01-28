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
    /// Ein Servertask, der direkt ausgeführt werden kann.
    /// </summary>
    [XmlRoot(ElementName = "task")]
    public class ServerTaskItem
    {
        /// <summary>
        /// Der Tasktyp 0 oder 2
        /// </summary>
        [XmlAttribute(AttributeName = "type")]
        public int Tasktype { get; set; }

        /// <summary>
        /// Der Name des Tasks
        /// </summary>
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Die Aktion welche ausgeführt wird
        /// </summary>
        [XmlElement(ElementName = "action")]
        public string Action { get; set; }

        /// <summary>
        /// Startet den Task auf dem Server
        /// </summary>
        /// <returns></returns>
        public Task<HttpStatusCode> RunTask()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            return dvbApi.SendDataAsync("tasks", new List<Helper.UriParameter>
            {
                new Helper.UriParameter("task", Action)
            });
        }
    }
}
