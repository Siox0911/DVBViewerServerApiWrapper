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
            return dvbApi.SendDataAsync("dvbcommand", new List<Helper.UriParameter> {
                new Helper.UriParameter("target", Name),
                new Helper.UriParameter("cmd",$"-x{(int)dVBViewerCommand}")
            });
        }

        /// <summary>
        /// Spielt das Video auf dem Clienten (DVBViewer) ab, sofern dieser connected ist.
        /// Plays the video on the client (DVBViewer), if it is connected.
        /// </summary>
        /// <param name="videoFileItem"></param>
        /// <returns></returns>
        public Task<HttpStatusCode> PlayVideo(VideoFileItem videoFileItem)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            return dvbApi.SendDataAsync("dvbcommand", new List<Helper.UriParameter> {
                new Helper.UriParameter("target", Name),
                new Helper.UriParameter("cmd", videoFileItem.Path + videoFileItem.FileName)
            });
        }

        /// <summary>
        /// Spiel die Aufnahme auf dem Clienten (DVBViewer) ab, sofern dieser connected ist.
        /// Plays the recording on the client (DVBViewer), if it is connected.
        /// </summary>
        /// <param name="recordingItem"></param>
        /// <returns></returns>
        public Task<HttpStatusCode> PlayRecording(RecordingItem recordingItem)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            return dvbApi.SendDataAsync("dvbcommand", new List<Helper.UriParameter> {
                new Helper.UriParameter("target", Name),
                new Helper.UriParameter("cmd", recordingItem.File)
            });
        }

    }
}
