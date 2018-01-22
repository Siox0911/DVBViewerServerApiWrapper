using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DVBViewerServerApiWrapper.Helper;


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
        [XmlElement(ElementName = "target", Type = typeof(string))]
        public List<string> Clients { get; set; }

        internal DVBViewerClients() { }

        internal static DVBViewerClients CreateDvbViewerClients(XDocument xDocument)
        {
            return Deserializer.Deserialize<DVBViewerClients>(xDocument);
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

    }
}
