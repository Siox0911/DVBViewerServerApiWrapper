using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Ein Unterkanal der Kanals ChannelItem. A sub channel of the ChannelItem.
    /// </summary>
    [XmlRoot(ElementName = "subchannel")]
    public class ChannelSubItem
    {
        /// <summary>
        /// Der Name des Unterkanals. The name of the subchannel.
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Die ID des Unterkanals. The ID of the subchannel.
        /// </summary>
        [XmlAttribute(AttributeName = "ID")]
        public string ChannelID { get; set; }

        internal ChannelSubItem() { }

        /// <summary>
        /// Spiel den Sender auf dem DVBViewer ab. Playback the subchannel on the DVBViewer.
        /// </summary>
        /// <param name="dVBViewerClient"></param>
        /// <returns></returns>
        public Task<HttpStatusCode> PlayAsync(DVBViewerClient dVBViewerClient)
        {
            return dVBViewerClient.PlayChannelAsync(this);
        }
    }
}