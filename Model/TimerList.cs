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
    /// Stellt die TimerListe im DMS dar. 
    /// </summary>
    [XmlRoot (ElementName = "Timers")]
    public class TimerList
    {
        /// <summary>
        /// Jeder einzelne Timer des DMS.
        /// </summary>
        [XmlElement(ElementName = "Timer", Type = typeof(TimerItem))]
        public List<TimerItem> Items { get; set; }

        internal TimerList() { }

        internal static Task<TimerList> GetTimerListAsync(List<Helper.UriParameter> uriParameters)
        {
            return Helper.Lists.GetListAsync<TimerList>("timerlist", uriParameters, new Type[]
            {
                typeof(TimerItem),
                typeof(RecordingSeries)
            });
        }

        /// <summary>
        /// Gibt alle Timer des DMS zurück.
        /// Gives back all timer of the DMS.
        /// </summary>
        /// <returns></returns>
        public static Task<TimerList> GetTimerListAsync()
        {
            return GetTimerListAsync(new List<Helper.UriParameter>
            {
                new Helper.UriParameter("utf8", "2")
            });
        }
    }
}
