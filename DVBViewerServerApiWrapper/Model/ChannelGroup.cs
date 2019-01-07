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
    /// A Channellist sub group. Die Kanalliste Untergruppe.
    /// </summary>
    [XmlRoot(ElementName = "group")]
    public class ChannelGroup
    {
        /// <summary>
        /// Der Name der Untergruppe. The name of the sub group.
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Eine Liste mit den Sendern. A list with the channels.
        /// </summary>
        [XmlElement(ElementName = "channel", Type = typeof(ChannelItem))]
        public List<ChannelItem> Items { get; set; }

        internal ChannelGroup() { }
    }
}