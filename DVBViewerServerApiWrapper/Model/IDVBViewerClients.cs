using System.Collections.Generic;

namespace DVBViewerServerApiWrapper.Model
{
    public interface IDVBViewerClients
    {
        List<DVBViewerClient> Items { get; set; }

        void SendXCommand(Enums.DVBViewerXCommand dVBViewerXCommand);
        void SendXCommandAsync(Enums.DVBViewerXCommand dVBViewerXCommand);
    }
}