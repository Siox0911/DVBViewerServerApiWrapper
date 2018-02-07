using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Gibt eine Liste mit den DVBViewer Clienten des Service zurück.
    /// Returns a list of service DVBViewer clients.
    /// </summary>
    [XmlRoot(ElementName = "targets")]
    public class DVBViewerClients
    {
        /// <summary>
        /// Liste mit den Namen der Clienten als Netbios PC-Name
        /// List with the names of Clients as Netbios PC-Name
        /// </summary>
        [XmlElement(ElementName = "target", Type = typeof(DVBViewerClient))]
        public List<DVBViewerClient> Items { get; set; }

        internal DVBViewerClients() { }

        internal static DVBViewerClients CreateDvbViewerClients(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<DVBViewerClients>(xDocument);
        }

        /// <summary>
        /// Gibt alle verbundenen DVBViewer zurück.
        /// Gives back all connected DVBViewer Clients
        /// </summary>
        /// <returns></returns>
        public static async Task<DVBViewerClients> GetDvbViewerClients()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = await dvbApi.GetDataAsync("dvbcommand").ConfigureAwait(false);

                if (xmldata != null)
                {
                   return CreateDvbViewerClients(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Sendet an jeden Clienten ein DVBCommand. Da es sich um jeden Clienten handelt, gibt es keinen Rückgabewert
        /// Send a DVCommand to each client. Because it is every client, there is no return value
        /// </summary>
        /// <param name="dVBViewerXCommand"></param>
        /// <returns></returns>
        public async void SendXCommandAsync(Enums.DVBViewerXCommand dVBViewerXCommand)
        {
            try
            {
                foreach (var client in Items)
                {
                   await client.SendXCommandAsync(dVBViewerXCommand).ConfigureAwait(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
