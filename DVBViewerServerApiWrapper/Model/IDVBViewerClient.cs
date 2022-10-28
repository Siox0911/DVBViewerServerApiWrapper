using System.Net;
using System.Threading.Tasks;

namespace DVBViewerServerApiWrapper.Model
{
    public interface IDVBViewerClient
    {
        string Name { get; set; }

        HttpStatusCode PlayChannel(ChannelItem channelItem);
        HttpStatusCode PlayChannel(ChannelSubItem channelSubItem);
        Task<HttpStatusCode> PlayChannelAsync(ChannelItem channelItem);
        Task<HttpStatusCode> PlayChannelAsync(ChannelSubItem channelSubItem);
        HttpStatusCode PlayRecording(RecordingItem recordingItem);
        Task<HttpStatusCode> PlayRecordingAsync(RecordingItem recordingItem);
        HttpStatusCode PlayVideo(VideoFileItem videoFileItem);
        Task<HttpStatusCode> PlayVideoAsync(VideoFileItem videoFileItem);
        HttpStatusCode SendXCommand(Enums.DVBViewerXCommand dVBViewerCommand);
        Task<HttpStatusCode> SendXCommandAsync(Enums.DVBViewerXCommand dVBViewerCommand);
    }
}