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
    /// Eine Obergruppe der Kanalliste. A top group of the channel list.
    /// </summary>
    [XmlRoot(ElementName = "root")]
    public class ChannelTopGroup
    {
        /// <summary>
        /// Der Name der Obergruppe. The name of the top group.
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Eine Liste mit den Untergruppen. A list of the sub groups.
        /// </summary>
        [XmlElement(ElementName = "group", Type = typeof(ChannelGroup))]
        public List<ChannelGroup> Groups { get; set; }

        internal ChannelTopGroup() { }
    }
}