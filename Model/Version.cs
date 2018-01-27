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
    public class Version
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
        public string ServerVersion { get; set; }

        internal Version() { }

        internal static Version CreateVersion(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<Version>(xDocument);
        }

        internal static Version GetVersion()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = dvbApi.GetDataAsync("version").Result;

                if (xmldata != null)
                {
                    return CreateVersion(xmldata);
                }
            }
            return null;
        }
    }
}
