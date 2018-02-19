using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Ein Kanal im DMS. A Channel in the DMS.
    /// </summary>
    [XmlRoot(ElementName = "channel")]
    public class ChannelItem : IEquatable<ChannelItem>
    {
        /// <summary>
        /// Die Kanalnummer. The channel number.
        /// </summary>
        [XmlAttribute(AttributeName = "nr")]
        public int Number { get; set; }

        /// <summary>
        /// Der Name des Kanals. The name of the channel.
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Die EPG ID des Kanals. The EPG ID of the channel.
        /// </summary>
        [XmlAttribute(AttributeName = "EPGID")]
        public string EpgID { get; set; }

        /// <summary>
        /// Die Flags des Kanals. Channel flags.
        /// </summary>
        [XmlAttribute(AttributeName = "flags")]
        public int Flags
        {
            get { return (int)ChannelProperties; }
            set
            {
                ChannelProperties = (Enums.ChannelProperties)value;
            }
        }

        /// <summary>
        /// Die allgemeinen Eigenschaften des Kanals. Audio, Video, RDS etc.
        /// The general Channel properties. Audio, Video, RDS etc.
        /// </summary>
        [XmlIgnore]
        public Enums.ChannelProperties ChannelProperties;

        /// <summary>
        /// Die ID des Kanals. Wird verwendet für den UPnP Stream. The ID of the channel. Is used for the UPnP stream. UPnPUrl + ID
        /// </summary>
        [XmlAttribute(AttributeName = "ID")]
        public long ID { get; set; }

        /// <summary>
        /// Das Logo zum Kanals als Teil einer URL. The logo to the channel as part of an url.
        /// </summary>
        [XmlElement(ElementName = "logo")]
        public string ChannelLogo { get; set; }

        /// <summary>
        /// Der Query Teil der RTSP URL. The query part of the url.
        /// </summary>
        [XmlElement(ElementName = "rtsp")]
        public string RTSPQuery { get; set; }

        /// <summary>
        /// Eine Liste mit den Unterkanälen. Darin sind meistens andere Audiospuren. A list with the subchannels of the channel. Inherits the most time other audio tracks.
        /// </summary>
        [XmlElement(ElementName = "subchannel", Type = typeof(ChannelSubItem))]
        public List<ChannelSubItem> SubChannels { get; set; }

        internal ChannelItem() { }

        /// <summary>
        /// Gives back a complete UPnP URL to playback the channel. Gibt einen UPnP URL Text zurück der direkt verwendet werden kann.
        /// </summary>
        /// <returns></returns>
        public string GetUPnPUriString()
        {
            var chList = ChannelList.GetInstance();
            return chList?.UpnpUrl + ID;
        }

        /// <summary>
        /// Gives back a complete RTSP URL to playback the channel. Gibt eine komplette RTSP Url zum direkten Abspielen zurück.
        /// </summary>
        /// <returns></returns>
        public string GetRTSPUriString()
        {
            var chList = ChannelList.GetInstance();
            return chList?.RtspUrl + RTSPQuery;
        }

        /// <summary>
        /// Gibt die komplette URL zum Logo des Kanals zurück. Gives back the complete URL to the channel logo.
        /// </summary>
        /// <returns></returns>
        public string GetChannelLogo()
        {
            var api = DVBViewerServerApi.GetCurrentInstance();
            if (api != null)
            {
                return $"{api.Hostname}:{api.Port}/{ChannelLogo}";
            }

            return null;
        }

        /// <summary>
        /// Spiel den Kanal auf dem DVBViewer ab. Playback the Channel on the DVBViewer Client.
        /// </summary>
        /// <param name="dVBViewerClient"></param>
        /// <returns></returns>
        public Task<HttpStatusCode> PlayAsync(DVBViewerClient dVBViewerClient)
        {
            return dVBViewerClient.PlayChannelAsync(this);
        }

        /// <summary>
        /// Spiel den Kanal auf dem DVBViewer ab. Playback the Channel on the DVBViewer Client.
        /// </summary>
        /// <param name="dVBViewerClient"></param>
        /// <returns></returns>
        public HttpStatusCode Play(DVBViewerClient dVBViewerClient)
        {
            return dVBViewerClient.PlayChannel(this);
        }

        /// <summary>
        /// Bestimmt, ob das angegebene Objekt mit dem aktuellen Objekt identisch ist.
        /// Determines whether the specified object is identical to the current object.
        /// </summary>
        /// <param name="obj">
        ///  Das Objekt, das mit dem aktuellen Objekt verglichen werden soll.
        ///  The object to compare with the current object.
        /// </param>
        /// <returns>
        /// <see langword="true" />, wenn das angegebene Objekt und das aktuelle Objekt gleich sind, andernfalls <see langword="false" />.
        /// <see langword="true" />, if the specified object and the current object are the same, otherwise <see langword="false" />.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as ChannelItem);
        }

        /// <summary>
        /// Gibt an, ob das aktuelle Objekt gleich einem anderen Objekt des gleichen Typs ist.
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">
        /// Ein Objekt, das mit diesem Objekt verglichen werden soll.
        /// The object to compare with the current object.
        /// </param>
        /// <returns>
        /// <see langword="true" />, wenn das aktuelle Objekt gleich dem <paramref name="other" />-Parameter ist, andernfalls <see langword="false" />.
        /// </returns>
        public bool Equals(ChannelItem other)
        {
            return other != null && ID == other.ID;
        }

        /// <summary>
        /// Fungiert als die Standardhashfunktion.
        /// Acts as the default hash function.
        /// </summary>
        /// <returns>
        /// Ein Hashcode für das aktuelle Objekt.
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            return 1213502048 + ID.GetHashCode();
        }

        public static bool operator ==(ChannelItem item1, ChannelItem item2)
        {
            return EqualityComparer<ChannelItem>.Default.Equals(item1, item2);
        }

        public static bool operator !=(ChannelItem item1, ChannelItem item2)
        {
            return !(item1 == item2);
        }
    }
}