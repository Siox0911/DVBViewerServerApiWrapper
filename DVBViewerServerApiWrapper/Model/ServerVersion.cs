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
    /// Die Serverversion. The server version.
    /// </summary>
    [XmlRoot(ElementName = "version")]
    public class ServerVersion
    {
        /// <summary>
        /// Internal Version
        /// </summary>
        [XmlAttribute(AttributeName = "iver")]
        public string IVer { get; set; }

        /// <summary>
        /// Die Serverversion des DVBViewerServers.
        /// The server version of the DVBViewer server
        /// </summary>
        [XmlText(Type = typeof(string))]
        public string Version { get; set; }

        internal ServerVersion() { }

        internal static ServerVersion CreateServerVersion(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<ServerVersion>(xDocument);
        }

        /// <summary>
        /// Gibt die Serverversion zurück. Returns the server version.
        /// </summary>
        /// <returns></returns>
        public static async Task<ServerVersion> GetServerVersionAsync()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = await dvbApi.GetApiDataAsync("version").ConfigureAwait(false);

                if (xmldata != null)
                {
                    return CreateServerVersion(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt die Serverversion zurück. Returns the server version.
        /// </summary>
        /// <returns></returns>
        public static ServerVersion GetServerVersion()
        {
            return GetServerVersionAsync().Result;
        }
    }
}
