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
        /// Die RTSP Url GrundURL für die Sender. 
        /// The RTSP basis url. 
        /// </summary>
        [XmlElement(ElementName = "rtspURL")]
        public string RtspUrl { get; set; }

        /// <summary>
        /// Die UPnP Grund Url für den Sender. 
        /// The UPnP basis url for all channels.
        /// </summary>
        [XmlElement(ElementName = "upnpURL")]
        public string UpnpUrl { get; set; }

        /// <summary>
        /// Die Obergruppen der Kanäle, enthält Satteliten usw. 
        /// The top groups of the channels, inherits the satellites etc.
        /// </summary>
        [XmlElement(ElementName = "root", Type = typeof(ChannelTopGroup))]
        public List<ChannelTopGroup> TopGroups { get; set; }

        internal ChannelList() => chList = this;

        internal static ChannelList GetInstance() => chList;

        internal static Task<ChannelList> GetChannelListAsync(List<Helper.UriParameter> uriParameters)
        {
            return Helper.Lists.GetListAsync<ChannelList>("getchannelsxml", uriParameters, new Type[]
            {
                typeof(ChannelTopGroup),
                typeof(ChannelGroup),
                typeof(ChannelItem),
                typeof(ChannelSubItem),
                typeof(ChannelTuner)
            });
        }

        /// <summary>
        /// Gibt die komplette Senderliste des DMS zurück. Darin sind die Favoriten enthalten. 
        /// Returns the complete station list of the DMS. This contains the favorites.
        /// </summary>
        /// <returns></returns>
        public static Task<ChannelList> GetChannelListAsync()
        {
            return GetChannelListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("logo", "1"),
                new Helper.UriParameter("rtsp", "1"),
                new Helper.UriParameter("upnp", "1"),
                new Helper.UriParameter("subchannels", "1"),
                new Helper.UriParameter("fav", "1"),
                new Helper.UriParameter("tuner", "1")
            });
        }

        /// <summary>
        /// Gibt die komplette Senderliste des DMS zurück. Darin sind die Favoriten enthalten. 
        /// Returns the complete station list of the DMS. This contains the favorites.
        /// </summary>
        /// <returns></returns>
        public static ChannelList GetChannelList()
        {
            return GetChannelListAsync().Result;
        }

        /// <summary>
        /// Gibt die Liste mit einem einzigen Kanal zurück, welcher der ChannelID entspricht.
        /// Gives back a list with only one channel. This is the channel with the channelID.
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public static Task<ChannelList> GetChannelListAsync(long channelID)
        {
            return GetChannelListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("logo", "1"),
                new Helper.UriParameter("rtsp", "1"),
                new Helper.UriParameter("upnp", "1"),
                new Helper.UriParameter("subchannels", "1"),
                new Helper.UriParameter("fav", "1"),
                new Helper.UriParameter("tuner", "1"),
                new Helper.UriParameter("id", channelID.ToString())
            });
        }

        /// <summary>
        /// Gibt die Liste mit einem einzigen Kanal zurück, welcher der EPG ID entspricht.
        /// Gives back a list with only one channel. This is the channel with the EPG ID.
        /// </summary>
        /// <param name="epgChannelID"></param>
        /// <returns></returns>
        public static Task<ChannelList> GetChannelListAsync(string epgChannelID)
        {
            return GetChannelListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("logo", "1"),
                new Helper.UriParameter("rtsp", "1"),
                new Helper.UriParameter("upnp", "1"),
                new Helper.UriParameter("subchannels", "1"),
                new Helper.UriParameter("fav", "1"),
                new Helper.UriParameter("tuner", "1"),
                new Helper.UriParameter("epgid", epgChannelID)
            });
        }

        /// <summary>
        /// Gibt die Liste mit einem einzigen Kanal zurück, welcher der ChannelID entspricht.
        /// Gives back a list with only one channel. This is the channel with the channelID.
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public static ChannelList GetChannelList(long channelID)
        {
            return GetChannelListAsync(channelID).Result;
        }

        /// <summary>
        /// Gibt eine Liste reine Favouritenliste der Kanäle zurück.
        /// </summary>
        /// <returns></returns>
        public static Task<ChannelList> GetChannelListFavOnlyAsync()
        {
            return GetChannelListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("logo", "1"),
                new Helper.UriParameter("rtsp", "1"),
                new Helper.UriParameter("upnp", "1"),
                new Helper.UriParameter("subchannels", "1"),
                new Helper.UriParameter("tuner", "1"),
                new Helper.UriParameter("favonly", "1")
            });

        }

        /// <summary>
        /// Gibt eine Liste reine Favouritenliste der Kanäle zurück.
        /// </summary>
        /// <returns></returns>
        public static ChannelList GetChannelListFavOnly()
        {
            return GetChannelListFavOnlyAsync().Result;
        }

        /// <summary>
        /// Gibt nur TV Kanäle zurück.
        /// </summary>
        /// <returns></returns>
        public static Task<ChannelList> GetChannelTVOnlyAsync()
        {
            return GetChannelListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("logo", "1"),
                new Helper.UriParameter("rtsp", "1"),
                new Helper.UriParameter("upnp", "1"),
                new Helper.UriParameter("subchannels", "1"),
                new Helper.UriParameter("tuner", "1"),
                new Helper.UriParameter("fav", "1"),
                new Helper.UriParameter("tvonly", "1")
            });
        }

        /// <summary>
        /// Gibt nur TV Kanäle zurück.
        /// </summary>
        /// <returns></returns>
        public static ChannelList GetChannelTVOnly()
        {
            return GetChannelTVOnlyAsync().Result;
        }

        /// <summary>
        /// Gibt nur Radio Kanäle zurück.
        /// </summary>
        /// <returns></returns>
        public static Task<ChannelList> GetChannelRadioOnlyAsync()
        {
            return GetChannelListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("logo", "1"),
                new Helper.UriParameter("rtsp", "1"),
                new Helper.UriParameter("upnp", "1"),
                new Helper.UriParameter("subchannels", "1"),
                new Helper.UriParameter("tuner", "1"),
                new Helper.UriParameter("fav", "1"),
                new Helper.UriParameter("radioonly", "1")
            });
        }

        /// <summary>
        /// Gibt nur Radio Kanäle zurück.
        /// </summary>
        /// <returns></returns>
        public static ChannelList GetChannelRadioOnly()
        {
            return GetChannelTVOnlyAsync().Result;
        }
    }
}
