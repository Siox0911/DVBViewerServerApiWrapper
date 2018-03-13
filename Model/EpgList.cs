using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Die EPG Liste vom DMS. The epg list of the DMS.
    /// </summary>
    [XmlRoot(ElementName = "epg")]
    public class EpgList
    {
        /// <summary>
        /// Versionsnummer.
        /// Version number.
        /// </summary>
        [XmlAttribute(AttributeName = "Ver")]
        public string Version { get; set; }

        /// <summary>
        /// Die EPG Items.
        /// 
        /// </summary>
        [XmlElement(ElementName = "programme", Type = typeof(EpgItem))]
        public List<EpgItem> Items { get; set; }

        /// <summary>
        /// Internal Constructor
        /// </summary>
        internal EpgList() { }

        /// <summary>
        /// Internal function to get the data from the server and derialize it
        /// </summary>
        /// <param name="uriParameters"></param>
        /// <returns></returns>
        internal static Task<EpgList> GetEpgListAsync(List<Helper.UriParameter> uriParameters)
        {
            return Helper.Lists.GetListAsync<EpgList>("epg", uriParameters, new Type[] { typeof(EpgItem) });
        }

        /// <summary>
        /// Gibt das komplette EPG vom Server zurück. Die Menge der Daten können enorm sein. 
        /// Returns the complete EPG from the server. The amount of data can be enormous.
        /// </summary>
        /// <returns></returns>
        public static Task<EpgList> GetEpgListAsync()
        {
            return GetEpgListAsync(uriParameters: null);
        }

        /// <summary>
        /// Gibt das komplette EPG vom Server zurück. Die Menge der Daten können enorm sein. 
        /// Returns the complete EPG from the server. The amount of data can be enormous.
        /// </summary>
        /// <returns></returns>
        public static EpgList GetEpgList()
        {
            return GetEpgListAsync().Result;
        }

        /// <summary>
        /// Gibt das komplette verfügbare EPG eines Senders zurück.
        /// Returns the complete available EPG of a channel.
        /// </summary>
        /// <param name="epgChannelID"></param>
        /// <returns></returns>
        public static Task<EpgList> GetEpgListAsync(ChannelItem channelItem)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("channel", $"{channelItem.EpgChannelID}")
            });
        }

        /// <summary>
        /// Gibt das komplette verfügbare EPG eines Senders zurück.
        /// Returns the complete available EPG of a channel.
        /// </summary>
        /// <param name="epgChannelID"></param>
        /// <returns></returns>
        public static EpgList GetEpgList(ChannelItem channelItem)
        {
            return GetEpgListAsync(channelItem).Result;
        }

        /// <summary>
        /// Gibt einen einzigen EPG Eintrag zurück, welcher der EPGEventID entspricht. Dies funktioniert nur, wenn die EPGEventID nicht geändert wurde.
        /// Returns a single EPG entry corresponding to the EPGEventID. This only works if the EPGEventID has not been changed.
        /// </summary>
        /// <param name="channelItem"></param>
        /// <param name="epgEventID"></param>
        /// <returns></returns>
        public static Task<EpgList> GetEpgListAsync(ChannelItem channelItem, int epgEventID)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("channel", channelItem.EpgChannelID.ToString()),
                new Helper.UriParameter("eventid", epgEventID.ToString())
            });
        }

        /// <summary>
        /// Gibt das EPG ab einem bestimmten Zeitpunkt zurück.
        /// Returns the EPG at a certain point in time.
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public static Task<EpgList> GetEpgListAsync(DateTime currentTime)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("start", currentTime.ToOADate().ToString())
            });
        }

        /// <summary>
        /// Gibt das EPG ab einem bestimmten Zeitpunkt zurück.
        /// Returns the EPG at a certain point in time.
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public static EpgList GetEpgList(DateTime currentTime)
        {
            return GetEpgListAsync(currentTime).Result;
        }

        /// <summary>
        /// Gibt das EPG ab einem bestimmten Zeitpunkt eines Kanals zurück.
        /// Returns the EPG at a specific time of a channel.
        /// </summary>
        /// <param name="epgChannelID"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public static Task<EpgList> GetEpgListAsync(ChannelItem channelItem, DateTime currentTime)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("channel", $"{channelItem.EpgChannelID}"),
                new Helper.UriParameter("start", currentTime.ToOADate().ToString())
            });
        }

        /// <summary>
        /// Gibt das EPG ab einem bestimmten Zeitpunkt eines Kanals zurück.
        /// Returns the EPG at a specific time of a channel.
        /// </summary>
        /// <param name="epgChannelID"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public static EpgList GetEpgList(ChannelItem channelItem, DateTime currentTime)
        {
            return GetEpgListAsync(channelItem, currentTime).Result;
        }

        /// <summary>
        /// Gibt das komplette EPG eines bestimmten Zeitraumes zurück.
        /// Returns the complete EPG of a given period.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static Task<EpgList> GetEpgListAsync(DateTime fromDate, DateTime toDate)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("start", fromDate.ToOADate().ToString()),
                new Helper.UriParameter("end", toDate.ToOADate().ToString())
            });
        }

        /// <summary>
        /// Gibt das komplette EPG eines bestimmten Zeitraumes zurück.
        /// Returns the complete EPG of a given period.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static EpgList GetEpgList(DateTime fromDate, DateTime toDate)
        {
            return GetEpgListAsync(fromDate, toDate).Result;
        }

        /// <summary>
        /// Gibt das komplette EPG eines Kanals in einem bestimmten Zeitraum zurück.
        /// Returns the complete EPG of a channel in a given time period.
        /// </summary>
        /// <param name="epgChannelID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static Task<EpgList> GetEpgListAsync(ChannelItem channelItem, DateTime fromDate, DateTime toDate)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("channel", $"{channelItem.EpgChannelID}"),
                new Helper.UriParameter("start", fromDate.ToOADate().ToString()),
                new Helper.UriParameter("end", toDate.ToOADate().ToString())
            });
        }

        /// <summary>
        /// Gibt das komplette EPG eines Kanals in einem bestimmten Zeitraum zurück.
        /// Returns the complete EPG of a channel in a given time period.
        /// </summary>
        /// <param name="epgChannelID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static EpgList GetEpgList(ChannelItem channelItem, DateTime fromDate, DateTime toDate)
        {
            return GetEpgListAsync(channelItem, fromDate, toDate).Result;
        }

        /// <summary>
        /// Gibt eine Liste mit EPG-Einträge zurück, welche den Suchtext enthalten.
        /// Returns a list of EPG entries containing the search text.
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="searchOptions">Optional: Default is T (Title) and S (Subtitle)</param>
        /// <returns></returns>
        public static Task<EpgList> GetEpgListAsync(string searchText, Enums.EpgSearchOptions searchOptions = Enums.EpgSearchOptions.T | Enums.EpgSearchOptions.S)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("search", searchText),
                new Helper.UriParameter("options", searchOptions.ToString("F").Replace(",", "").Replace(" ", "").Trim())
            });
        }

        /// <summary>
        /// Gibt eine Liste mit EPG-Einträge zurück, welche den Suchtext enthalten.
        /// Returns a list of EPG entries containing the search text.
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="searchOptions">Optional: Default is T (Title) and S (Subtitle)</param>
        /// <returns></returns>
        public static EpgList GetEpgList(string searchText, Enums.EpgSearchOptions searchOptions = Enums.EpgSearchOptions.T | Enums.EpgSearchOptions.S)
        {
            return GetEpgListAsync(searchText, searchOptions).Result;
        }

        /// <summary>
        /// Gibt eine Liste mit EPG-Einträge zurück, welche den Suchtext enthalten.
        /// Returns a list of EPG entries containing the search text.
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="epgChannelID"></param>
        /// <param name="searchOptions">Optional: Default is T (Title) and S (Subtitle)</param>
        /// <returns></returns>
        public static Task<EpgList> GetEpgListAsync(string searchText, ChannelItem channelItem, Enums.EpgSearchOptions searchOptions = Enums.EpgSearchOptions.T | Enums.EpgSearchOptions.S)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("channel", $"{channelItem.EpgChannelID}"),
                new Helper.UriParameter("search", searchText),
                new Helper.UriParameter("options", searchOptions.ToString("F").Replace(",", "").Replace(" ", "").Trim())
            });
        }

        /// <summary>
        /// Gibt eine Liste mit EPG-Einträge zurück, welche den Suchtext enthalten.
        /// Returns a list of EPG entries containing the search text.
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="epgChannelID"></param>
        /// <param name="searchOptions">Optional: Default is T (Title) and S (Subtitle)</param>
        /// <returns></returns>
        public static EpgList GetEpgList(string searchText, ChannelItem channelItem, Enums.EpgSearchOptions searchOptions = Enums.EpgSearchOptions.T | Enums.EpgSearchOptions.S)
        {
            return GetEpgListAsync(searchText, channelItem, searchOptions).Result;
        }

        /// <summary>
        /// Gibt eine Liste mit EPG-Einträge zurück, welche den Suchtext enthalten.
        /// Returns a list of EPG entries containing the search text.
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="epgChannelID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="searchOptions">Optional: Default is T (Title) and S (Subtitle)</param>
        /// <returns></returns>
        public static Task<EpgList> GetEpgListAsync(string searchText, ChannelItem channelItem, DateTime fromDate, DateTime toDate, Enums.EpgSearchOptions searchOptions = Enums.EpgSearchOptions.T | Enums.EpgSearchOptions.S)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("channel", $"{channelItem.EpgChannelID}"),
                new Helper.UriParameter("start", fromDate.ToOADate().ToString()),
                new Helper.UriParameter("end", toDate.ToOADate().ToString()),
                new Helper.UriParameter("search", searchText),
                new Helper.UriParameter("options", searchOptions.ToString("F").Replace(",", "").Replace(" ", "").Trim())
            });
        }

        /// <summary>
        /// Gibt eine Liste mit EPG-Einträge zurück, welche den Suchtext enthalten.
        /// Returns a list of EPG entries containing the search text.
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="epgChannelID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="searchOptions">Optional: Default is T (Title) and S (Subtitle)</param>
        /// <returns></returns>
        public static EpgList GetEpgList(string searchText, ChannelItem channelItem, DateTime fromDate, DateTime toDate, Enums.EpgSearchOptions searchOptions = Enums.EpgSearchOptions.T | Enums.EpgSearchOptions.S)
        {
            return GetEpgListAsync(searchText, channelItem, fromDate, toDate, searchOptions).Result;
        }

        /// <summary>
        /// Gibt eine EPGListe zurück, welche alle Sender zur aktuellen Zeit aussenden.
        /// Returns an EPG list, which send all channels at the current time.
        /// </summary>
        /// <returns></returns>
        public static Task<EpgList> GetEpgListNowAsync()
        {
            return GetEpgListAsync(DateTime.Now, DateTime.Now + TimeSpan.FromMinutes(1.0));
        }

        /// <summary>
        /// Gibt eine EPGListe zurück, welche alle Sender zur aktuellen Zeit aussenden.
        /// Returns an EPG list, which send all channels at the current time.
        /// </summary>
        /// <returns></returns>
        public static EpgList GetEpgListNow()
        {
            return GetEpgListNowAsync().Result;
        }

        /// <summary>
        /// Gibt eine EPGListe zurück, welche der Sender zur aktuellen Zeit aussendet. Dies beinhaltet nur 1 EPG Item.
        /// Returns an EPG list, which send the channel at the current time. This inherits 1 EPG Item.
        /// </summary>
        /// <param name="epgChannelID"></param>
        /// <returns></returns>
        public static Task<EpgList> GetEpgListNowAsync(ChannelItem channelItem)
        {
            return GetEpgListAsync(channelItem, DateTime.Now, DateTime.Now + TimeSpan.FromMinutes(1.0));
        }

        /// <summary>
        /// Gibt eine EPGListe zurück, welche der Sender zur aktuellen Zeit aussendet. Dies beinhaltet nur 1 EPG Item.
        /// Returns an EPG list, which send the channel at the current time. This inherits 1 EPG Item.
        /// </summary>
        /// <param name="epgChannelID"></param>
        /// <returns></returns>
        public static EpgList GetEpgListNow(ChannelItem channelItem)
        {
            return GetEpgListNowAsync(channelItem).Result;
        }

        /// <summary>
        /// Löscht das EPG. 
        /// Clears the EPG.
        /// </summary>
        /// <param name="epgClear">If is not specified the default is all EPG data (currently 7)</param>
        /// <returns></returns>
        public static Task<HttpStatusCode> DeleteEpgAsync(Enums.EpgClearSources epgClear = Enums.EpgClearSources.DVB | Enums.EpgClearSources.MHW | Enums.EpgClearSources.External)
        {
            var api = DVBViewerServerApi.GetCurrentInstance();
            return api?.SendApiDataAsync("egpclear", new List<Helper.UriParameter>
            {
                new Helper.UriParameter("source", $"{(int)epgClear}")
            });
        }
    }
}