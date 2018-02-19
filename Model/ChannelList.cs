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
    /// Stellt eine Kanalliste im DMS dar. A channellist on the DMS.
    /// </summary>
    [XmlRoot(ElementName = "channels")]
    public class ChannelList
    {
        private static ChannelList chList;

        /// <summary>
        /// Die RTSP Url GrundURL für die Sender. The RTSP basis url. 
        /// </summary>
        [XmlElement(ElementName = "rtspURL")]
        public string RtspUrl { get; set; }

        /// <summary>
        /// Die UPnP Grund Url für den Sender. The UPnP basis url for all channels.
        /// </summary>
        [XmlElement(ElementName = "upnpURL")]
        public string UpnpUrl { get; set; }

        /// <summary>
        /// Die Obergruppen der Kanäle, enthält Satteliten usw. The top groups of the channels, inherits the satellites etc.
        /// </summary>
        [XmlElement(ElementName = "root", Type = typeof(ChannelTopGroup))]
        public List<ChannelTopGroup> TopGroups { get; set; }

        internal ChannelList() => chList = this;

        internal static ChannelList GetInstance() => chList;

        internal static ChannelList CreateChannelList(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<ChannelList>(xDocument, new Type[] {
                typeof(ChannelTopGroup),
                typeof(ChannelGroup),
                typeof(ChannelItem),
                typeof(ChannelSubItem)
            });
        }

        /// <summary>
        /// Gibt die komplette Senderliste des DMS zurück. Darin sind die Favoriten enthalten. Returns the complete station list of the DMS. This contains the favorites.
        /// </summary>
        /// <returns></returns>
        public static async Task<ChannelList> GetChannelListAsync()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = await dvbApi.GetApiDataAsync("getchannelsxml", new List<Helper.UriParameter>
                {
                    new Helper.UriParameter("logo", "1"),
                    new Helper.UriParameter("rtsp", "1"),
                    new Helper.UriParameter("upnp", "1"),
                    new Helper.UriParameter("subchannels", "1"),
                    new Helper.UriParameter("fav", "1")
                }).ConfigureAwait(false);

                if (xmldata != null)
                {
                    return CreateChannelList(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt die komplette Senderliste des DMS zurück. Darin sind die Favoriten enthalten. Returns the complete station list of the DMS. This contains the favorites.
        /// </summary>
        /// <returns></returns>
        public static ChannelList GetChannelList()
        {
            return GetChannelListAsync().Result;
        }
    }
}
