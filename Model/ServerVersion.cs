using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    [XmlRoot(ElementName = "version")]
    public class ServerVersion
    {
        /// <summary>
        /// Internal Version
        /// </summary>
        [XmlAttribute(AttributeName = "iver")]
        public string IVer { get; set; }
        /// <summary>
        /// Die Serverversion des DVBViewerServers
        /// </summary>
        [XmlText(Type = typeof(string))]
        public string Version { get; set; }

        internal ServerVersion() { }

        internal static ServerVersion CreateServerVersion(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<ServerVersion>(xDocument);
        }

        internal static ServerVersion GetServerVersion()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = dvbApi.GetDataAsync("version").Result;

                if (xmldata != null)
                {
                    return CreateServerVersion(xmldata);
                }
            }
            return null;
        }
    }
}
