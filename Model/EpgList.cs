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
        public static Task<EpgList> GetEpgListAsync(long epgChannelID)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("channel", $"{epgChannelID}")
            });
        }

        /// <summary>
        /// Gibt das komplette verfügbare EPG eines Senders zurück.
        /// Returns the complete available EPG of a channel.
        /// </summary>
        /// <param name="epgChannelID"></param>
        /// <returns></returns>
        public static EpgList GetEpgList(long epgChannelID)
        {
            return GetEpgListAsync(epgChannelID).Result;
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
        public static Task<EpgList> GetEpgListAsync(long epgChannelID, DateTime currentTime)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("channel", $"{epgChannelID}"),
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
        public static EpgList GetEpgList(long epgChannelID, DateTime currentTime)
        {
            return GetEpgListAsync(epgChannelID, currentTime).Result;
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
        public static Task<EpgList> GetEpgListAsync(long epgChannelID, DateTime fromDate, DateTime toDate)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("channel", $"{epgChannelID}"),
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
        public static EpgList GetEpgList(long epgChannelID, DateTime fromDate, DateTime toDate)
        {
            return GetEpgListAsync(epgChannelID, fromDate, toDate).Result;
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
        public static Task<EpgList> GetEpgListAsync(string searchText, long epgChannelID, Enums.EpgSearchOptions searchOptions = Enums.EpgSearchOptions.T | Enums.EpgSearchOptions.S)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("channel", $"{epgChannelID}"),
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
        public static EpgList GetEpgList(string searchText, long epgChannelID, Enums.EpgSearchOptions searchOptions = Enums.EpgSearchOptions.T | Enums.EpgSearchOptions.S)
        {
            return GetEpgListAsync(searchText, epgChannelID, searchOptions).Result;
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
        public static Task<EpgList> GetEpgListAsync(string searchText, long epgChannelID, DateTime fromDate, DateTime toDate, Enums.EpgSearchOptions searchOptions = Enums.EpgSearchOptions.T | Enums.EpgSearchOptions.S)
        {
            return GetEpgListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("channel", $"{epgChannelID}"),
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
        public static EpgList GetEpgList(string searchText, long epgChannelID, DateTime fromDate, DateTime toDate, Enums.EpgSearchOptions searchOptions = Enums.EpgSearchOptions.T | Enums.EpgSearchOptions.S)
        {
            return GetEpgListAsync(searchText, epgChannelID, fromDate, toDate, searchOptions).Result;
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
    }
}