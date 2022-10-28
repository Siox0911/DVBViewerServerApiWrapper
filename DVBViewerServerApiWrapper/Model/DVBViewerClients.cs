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
    public class DVBViewerClients : IDVBViewerClients
    {
        /// <summary>
        /// Liste mit den Namen der Clienten als Netbios PC-Name
        /// List with the names of Clients as Netbios PC-Name
        /// </summary>
        [XmlElement(ElementName = "target", Type = typeof(DVBViewerClient))]
        public List<DVBViewerClient> Items { get; set; }

        internal DVBViewerClients() { }

        internal static Task<DVBViewerClients> GetDVBViewerClientsAsync(List<Helper.UriParameter> uriParameters)
        {
            return Helper.Lists.GetListAsync<DVBViewerClients>("dvbcommand", uriParameters);
        }

        /// <summary>
        /// Gibt alle verbundenen DVBViewer zurück.
        /// Gives back all connected DVBViewer Clients
        /// </summary>
        /// <returns></returns>
        public static Task<DVBViewerClients> GetDVBViewerClientsAsync()
        {
            return GetDVBViewerClientsAsync(null);
        }

        /// <summary>
        /// Gibt alle verbundenen DVBViewer zurück.
        /// Gives back all connected DVBViewer Clients
        /// </summary>
        /// <returns></returns>
        public static DVBViewerClients GetDVBViewerClients()
        {
            return GetDVBViewerClientsAsync().Result;
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

        /// <summary>
        /// Sendet an jeden Clienten ein DVBCommand. Da es sich um jeden Clienten handelt, gibt es keinen Rückgabewert
        /// Send a DVCommand to each client. Because it is every client, there is no return value
        /// </summary>
        /// <param name="dVBViewerXCommand"></param>
        /// <returns></returns>
        public void SendXCommand(Enums.DVBViewerXCommand dVBViewerXCommand)
        {
            try
            {
                SendXCommandAsync(dVBViewerXCommand);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
