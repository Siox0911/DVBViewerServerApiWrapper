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
    /// A server task that can be executed directly.
    /// </summary>
    [XmlRoot(ElementName = "task")]
    public class ServerTaskItem
    {
        /// <summary>
        /// Der Tasktyp 0 oder 2.
        /// The task type 0 or 2
        /// </summary>
        [XmlAttribute(AttributeName = "type")]
        public int Tasktype { get; set; }

        /// <summary>
        /// Der Name des Tasks
        /// The name of the task
        /// </summary>
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Die Aktion welche ausgeführt wird.
        /// he action which is executed
        /// </summary>
        [XmlElement(ElementName = "action")]
        public string Action { get; set; }

        internal ServerTaskItem() { }

        /// <summary>
        /// Startet den Task auf dem Server.
        /// Starts the task on the server
        /// </summary>
        /// <returns></returns>
        public HttpStatusCode RunTask()
        {
            return RunTaskAsync().Result;
        }

        /// <summary>
        /// Startet den Task auf dem Server.
        /// Starts the task on the server
        /// </summary>
        /// <returns></returns>
        public Task<HttpStatusCode> RunTaskAsync()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            return dvbApi.SendApiDataAsync("tasks", new List<Helper.UriParameter>
            {
                new Helper.UriParameter("task", Action)
            });
        }
    }
}
