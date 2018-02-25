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
        public long EpgChannelID { get; set; }

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
        public long ChannelID { get; set; }

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

        [XmlElement(ElementName = "tuner", Type = typeof(ChannelTuner))]
        public ChannelTuner Tuner { get; set; }

        internal ChannelItem() { }

        /// <summary>
        /// Gives back a complete UPnP URL to playback the channel. Gibt einen UPnP URL Text zurück der direkt verwendet werden kann.
        /// </summary>
        /// <returns></returns>
        public string GetUPnPUriString()
        {
            var chList = ChannelList.GetInstance();
            return chList?.UpnpUrl + ChannelID;
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
        /// Gibt den String zurück, der verwendet wird, bevor der UPnP String in die Datei m3u geschrieben wird.
        /// Beginnend mit #EXTINF:
        /// Returns the string used before the UPnP string is written to the m3u file.
        /// Starting with #EXTINF:
        /// </summary>
        /// <returns></returns>
        internal string GetM3uPrefString()
        {
            return $"#EXTINF:0,{Name} - {GetEpgListNow()?.Items?[0].Title}";
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
        /// Erzeugt aus dem Kanal eine M3U Datei. Die Datei befindet sich normalerweise im Tempverzeichnis.
        /// Generates an M3U file from this channel. The file is usually located in the Temp directory
        /// </summary>
        /// <returns>Ein Pfad zur m3u Datei. A path to the m3u file</returns>
        public string CreateM3UFile()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            //Create a playlist
            var tPath = System.IO.Path.GetTempPath();
            var fName = $"{ChannelID}.m3u";
            var cPathName = tPath + fName;
            using (var fStream = new System.IO.FileStream(cPathName, System.IO.FileMode.Create))
            {
                using (var sw = new System.IO.StreamWriter(fStream))
                {
                    sw.WriteLine(GetM3uPrefString());
                    sw.WriteLine(GetUPnPUriString());
                }
            }
            return cPathName;
        }

        /// <summary>
        /// Returns the complete EPG List of this channel. Gibt die komplette EPG Liste des Senders zurück.
        /// </summary>
        /// <returns></returns>
        public Task<EpgList> GetEpgListAsync()
        {
            return EpgList.GetEpgListAsync(EpgChannelID);
        }

        /// <summary>
        /// Return the complete EPG List of this channel. Gibt die komplette EPG Liste des Senders zurück.
        /// </summary>
        /// <returns></returns>
        public EpgList GetEpgList()
        {
            return GetEpgListAsync().Result;
        }

        /// <summary>
        /// Gibt eine Liste mit dem aktuellen EPG Eintrag zurück.
        /// </summary>
        /// <returns></returns>
        public Task<EpgList> GetEpgListNowAsync()
        {
            return EpgList.GetEpgListAsync(EpgChannelID, DateTime.Now, DateTime.Now + TimeSpan.FromMinutes(1.0));
        }

        /// <summary>
        /// Gibt eine Liste mit dem aktuellen EPG Eintrag zurück.
        /// </summary>
        /// <returns></returns>
        public EpgList GetEpgListNow()
        {
            return GetEpgListNowAsync().Result;
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
            return other != null && ChannelID == other.ChannelID;
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
            return 1213502048 + ChannelID.GetHashCode();
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