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
    /// </summary>
    [XmlRoot(ElementName = "targets")]
    public class DVBViewerClients
    {
        /// <summary>
        /// Liste mit den Namen der Clienten als PC-Name
        /// </summary>
        [XmlElement(ElementName = "target", Type = typeof(DVBViewerClient))]
        public List<DVBViewerClient> Items { get; set; }

        internal DVBViewerClients() { }

        internal static DVBViewerClients CreateDvbViewerClients(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<DVBViewerClients>(xDocument);
        }

        internal static DVBViewerClients GetDvbViewerClients()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = dvbApi.GetDataAsync("dvbcommand").Result;

                if (xmldata != null)
                {
                    return CreateDvbViewerClients(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Sendet an jeden Clienten ein DVBCommand. Da es sich um jeden Clienten handelt, gibt es keinen Rückgabewert
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
