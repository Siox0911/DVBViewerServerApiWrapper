using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Ein Client welcher seit dem Start des Media Servers connected war.
    /// A client which was connected since the start of the Media Server.
    /// </summary>
    [XmlRoot(ElementName = "target")]
    public class DVBViewerClient
    {
        /// <summary>
        /// Der Name des Clients
        /// </summary>
        [XmlText(Type = typeof(string))]
        public string Name { get; set; }

        internal DVBViewerClient() { }

        /// <summary>
        /// Sendet einen Befehl an den Clienten und gibt einen Statuscode über die Antwort zurück.
        /// Sends a command to the client and returns a status code about the response.
        /// </summary>
        /// <param name="dVBViewerCommand"></param>
        /// <returns></returns>
        public Task<HttpStatusCode> SendXCommandAsync(Enums.DVBViewerXCommand dVBViewerCommand)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            return dvbApi?.SendApiDataAsync("dvbcommand", new List<Helper.UriParameter> {
                new Helper.UriParameter("target", Name),
                new Helper.UriParameter("cmd",$"-x{(int)dVBViewerCommand}")
            });
        }

        /// <summary>
        /// Sendet einen Befehl an den Clienten und gibt einen Statuscode über die Antwort zurück.
        /// Sends a command to the client and returns a status code about the response.
        /// </summary>
        /// <param name="dVBViewerCommand"></param>
        /// <returns></returns>
        public HttpStatusCode SendXCommand(Enums.DVBViewerXCommand dVBViewerCommand)
        {
            return SendXCommandAsync(dVBViewerCommand).Result;
        }

        /// <summary>
        /// Spielt das Video auf dem Clienten (DVBViewer) ab, sofern dieser connected ist.
        /// Plays the video on the client (DVBViewer), if it is connected.
        /// </summary>
        /// <param name="videoFileItem"></param>
        /// <returns></returns>
        public Task<HttpStatusCode> PlayVideoAsync(VideoFileItem videoFileItem)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            return dvbApi?.SendApiDataAsync("dvbcommand", new List<Helper.UriParameter> {
                new Helper.UriParameter("target", Name),
                new Helper.UriParameter("cmd", videoFileItem.Path + videoFileItem.FileName)
            });
        }

        /// <summary>
        /// Spielt das Video auf dem Clienten (DVBViewer) ab, sofern dieser connected ist.
        /// Plays the video on the client (DVBViewer), if it is connected.
        /// </summary>
        /// <param name="videoFileItem"></param>
        /// <returns></returns>
        public HttpStatusCode PlayVideo(VideoFileItem videoFileItem)
        {
            return PlayVideoAsync(videoFileItem).Result;
        }

        /// <summary>
        /// Spiel die Aufnahme auf dem Clienten (DVBViewer) ab, sofern dieser connected ist.
        /// Plays the recording on the client (DVBViewer), if it is connected.
        /// </summary>
        /// <param name="recordingItem"></param>
        /// <returns></returns>
        public Task<HttpStatusCode> PlayRecordingAsync(RecordingItem recordingItem)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            return dvbApi?.SendApiDataAsync("dvbcommand", new List<Helper.UriParameter> {
                new Helper.UriParameter("target", Name),
                new Helper.UriParameter("cmd", recordingItem.File)
            });
        }

        /// <summary>
        /// Spiel die Aufnahme auf dem Clienten (DVBViewer) ab, sofern dieser connected ist.
        /// Plays the recording on the client (DVBViewer), if it is connected.
        /// </summary>
        /// <param name="recordingItem"></param>
        /// <returns></returns>
        public HttpStatusCode PlayRecording(RecordingItem recordingItem)
        {
            return PlayRecordingAsync(recordingItem).Result;
        }

        /// <summary>
        /// Spiel einen Kanal auf dem DVBViewer ab. Plays a channel on the DVBViewer.
        /// </summary>
        /// <param name="channelItem"></param>
        /// <returns></returns>
        public Task<HttpStatusCode> PlayChannelAsync(ChannelItem channelItem)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            return dvbApi?.SendApiDataAsync("dvbcommand", new List<Helper.UriParameter> {
                new Helper.UriParameter("target", Name),
                new Helper.UriParameter("cmd", $"-c:{channelItem.ChannelID}")
            });
        }

        /// <summary>
        /// Spiel einen Kanal auf dem DVBViewer ab. Plays a channel on the DVBViewer.
        /// </summary>
        /// <param name="channelItem"></param>
        /// <returns></returns>
        public HttpStatusCode PlayChannel(ChannelItem channelItem)
        {
            return PlayChannelAsync(channelItem).Result;
        }

        /// <summary>
        /// Spiel den Sender auf dem DVBViewer ab. Play the subchannel on the DVBViewer.
        /// </summary>
        /// <param name="channelSubItem"></param>
        /// <returns></returns>
        public Task<HttpStatusCode> PlayChannelAsync(ChannelSubItem channelSubItem)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            return dvbApi?.SendApiDataAsync("dvbcommand", new List<Helper.UriParameter> {
                new Helper.UriParameter("target", Name),
                new Helper.UriParameter("cmd", $"-c:{channelSubItem.ID}")
            });
        }

        /// <summary>
        /// Spiel den Sender auf dem DVBViewer ab. Playback the subchannel on the DVBViewer.
        /// </summary>
        /// <param name="channelSubItem"></param>
        /// <returns></returns>
        public HttpStatusCode PlayChannel(ChannelSubItem channelSubItem)
        {
            return PlayChannelAsync(channelSubItem).Result;
        }
    }
}
