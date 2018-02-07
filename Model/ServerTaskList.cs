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
    /// Liste mit Aufgaben des DMC, betrifft Aufnahmen, Medien und Ruhezustand oder Standby.
    /// List of tasks of the DMC, affects recordings, media and hibernation or standby
    /// </summary>
    [XmlRoot(ElementName = "tasklist")]
    public class ServerTaskList
    {
        /// <summary>
        /// DMC Aufgabengruppen.
        /// DMC task groups
        /// </summary>
        [XmlElement(ElementName = "group", Type = typeof(ServerTaskGroup))]
        public List<ServerTaskGroup> Groups { get; set; }

        internal ServerTaskList() { }

        internal static ServerTaskList CreateServerTaskList(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<ServerTaskList>(xDocument, new Type[] { typeof(ServerTaskGroup), typeof(ServerTaskItem) });
        }

        /// <summary>
        /// Gibt alle Aufgaben des Servers zurück. Returns all tasks run on the DMS.
        /// </summary>
        /// <returns></returns>
        public static async Task<ServerTaskList> GetServerTaskList()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = await dvbApi.GetDataAsync("tasks").ConfigureAwait(false);

                if (xmldata != null)
                {
                    return CreateServerTaskList(xmldata);
                }
            }
            return null;
        }
    }
}
