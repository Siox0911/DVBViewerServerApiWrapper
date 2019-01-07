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
        public static Task<ChannelList> GetChannelListAsync(string channelID)
        {
            //HACK: Fix für die Version 2.0.4.0 GetChannelList mit ChannelID|ChannelName funktioniert hier nicht.
            //Das funktioniert erst in einer späteren Version.
            //Also hier haben wir die Krücke, dass wir prüfen ob ein | in der ChannelID ist.
            //Wenn ja, werden wir den String splitten und nur den ersten Teil nehmen
            //Lösche das, wenn die Version des neuen Servers über der Version 2.0.4.0 online ist.
            //Aktualisiere dann auch den Hilfetext
            if (channelID.IndexOf("|", StringComparison.CurrentCultureIgnoreCase) > 0)
            {
                channelID = channelID.Split(new string[] { "|" }, StringSplitOptions.None)[0];
            }

            return GetChannelListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("logo", "1"),
                new Helper.UriParameter("rtsp", "1"),
                new Helper.UriParameter("upnp", "1"),
                new Helper.UriParameter("subchannels", "1"),
                new Helper.UriParameter("fav", "1"),
                new Helper.UriParameter("tuner", "1"),
                new Helper.UriParameter("id", channelID)
            });
        }

        /// <summary>
        /// Gibt die Liste mit einem einzigen Kanal zurück, welcher der EPG ID entspricht.
        /// Gives back a list with only one channel. This is the channel with the EPG ID.
        /// </summary>
        /// <param name="epgChannelID"></param>
        /// <returns></returns>
        public static Task<ChannelList> GetChannelListAsync(long epgChannelID)
        {
            return GetChannelListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("logo", "1"),
                new Helper.UriParameter("rtsp", "1"),
                new Helper.UriParameter("upnp", "1"),
                new Helper.UriParameter("subchannels", "1"),
                new Helper.UriParameter("fav", "1"),
                new Helper.UriParameter("tuner", "1"),
                new Helper.UriParameter("epgid", epgChannelID.ToString())
            });
        }

        /// <summary>
        /// Gibt die Liste mit einem einzigen Kanal zurück, welcher der ChannelID entspricht.
        /// Gives back a list with only one channel. This is the channel with the channelID.
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public static ChannelList GetChannelList(string channelID)
        {
            return GetChannelListAsync(channelID).Result;
        }

        /// <summary>
        /// Gibt die Liste mit einem einzigen Kanal zurück, welcher der EPG ID entspricht.
        /// Gives back a list with only one channel. This is the channel with the EPG ID.
        /// </summary>
        /// <param name="epgChannelID"></param>
        /// <returns></returns>
        public static ChannelList GetChannelList(long epgChannelID)
        {
            return GetChannelListAsync(epgChannelID).Result;
        }

        /// <summary>
        /// Gibt eine ChannelList anhand des ChannelItems zurück. Dabei wird die ChannelID verwendet. Wird verwendet um einen vollständigen Kanal aus einem Timer zu erhalten.
        /// Returns a ChannelList based on the ChannelItem. The ChannelID is used. Used to get a complete channel from a timer.
        /// </summary>
        /// <param name="channelItem"></param>
        /// <returns></returns>
        public static Task<ChannelList> GetChannelListAsync(ChannelItem channelItem)
        {
            return GetChannelListAsync(channelItem.ChannelID);
        }

        /// <summary>
        /// Gibt eine ChannelList anhand des ChannelItems zurück. Dabei wird die ChannelID verwendet. Wird verwendet um einen vollständigen Kanal aus einem Timer zu erhalten.
        /// Returns a ChannelList based on the ChannelItem. The ChannelID is used. Used to get a complete channel from a timer.
        /// </summary>
        /// <param name="channelItem"></param>
        /// <returns></returns>
        public static ChannelList GetChannelList(ChannelItem channelItem)
        {
            return GetChannelListAsync(channelItem).Result;
        }

        /// <summary>
        /// Gibt eine ChannelList zurück, welche eine Liste mit Kanälen enthält welche dem Suchtext entsprechen. Ist exact True wird der genaue Name gesucht.
        /// Returns a ChannelList containing a list of channels matching the search text. If exact True, the exact name is searched for.
        /// </summary>
        /// <param name="partOfChannelName"></param>
        /// <param name="exact"></param>
        /// <returns></returns>
        public static async Task<ChannelList> GetChannelListAsync(string partOfChannelName, bool exact)
        {
            //Alle Kanäle holen
            var channelList = await GetChannelListAsync().ConfigureAwait(false);

            //Topgruppen durchlaufen
            for (int a = 0; a < channelList.TopGroups.Count; a++)
            {
                //Gruppen durchlaufen
                for (int b = 0; b < channelList.TopGroups[a].Groups.Count; b++)
                {
                    if (exact)
                    {
                        //Exakten Namen suchen
                        channelList.TopGroups[a].Groups[b].Items = (from f in channelList.TopGroups[a].Groups[b].Items where f.Name.Equals(partOfChannelName) select f).ToList();
                    }
                    else
                    {
                        //Ähnlichen Namen suchen
                        channelList.TopGroups[a].Groups[b].Items = (from f in channelList.TopGroups[a].Groups[b].Items where f.Name.IndexOf(partOfChannelName, StringComparison.CurrentCultureIgnoreCase) != -1 select f).ToList();
                    }
                }
            }
            return channelList;
        }

        /// <summary>
        /// Gibt eine ChannelList zurück, welche eine Liste mit Kanälen enthält welche dem Suchtext entsprechen. Ist exact True wird der genaue Name gesucht.
        /// Returns a ChannelList containing a list of channels matching the search text. If exact True, the exact name is searched for.
        /// </summary>
        /// <param name="partOfChannelName"></param>
        /// <param name="exact"></param>
        /// <returns></returns>
        public static ChannelList GetChannelList(string partOfChannelName, bool exact)
        {
            return GetChannelListAsync(partOfChannelName, exact).Result;
        }

        /// <summary>
        /// Gibt eine reine Favouritenliste der Kanäle zurück.
        /// Returns a pure favorite list of channels.
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
        /// Gibt eine reine Favouritenliste der Kanäle zurück.
        /// Returns a pure favorite list of channels.
        /// </summary>
        /// <returns></returns>
        public static ChannelList GetChannelListFavOnly()
        {
            return GetChannelListFavOnlyAsync().Result;
        }

        /// <summary>
        /// Gibt nur TV Kanäle zurück.
        /// Returns only TV channels.
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
        /// Returns only TV channels.
        /// </summary>
        /// <returns></returns>
        public static ChannelList GetChannelTVOnly()
        {
            return GetChannelTVOnlyAsync().Result;
        }

        /// <summary>
        /// Gibt nur Radio Kanäle zurück.
        /// Returns only radio channels.
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
        /// Returns only radio channels.
        /// </summary>
        /// <returns></returns>
        public static ChannelList GetChannelRadioOnly()
        {
            return GetChannelRadioOnlyAsync().Result;
        }
    }
}
