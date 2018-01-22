# DVBViewerServerApiWrapper

Der DVBViewerServerApiWrapper ist eine Bibliothek, welche die webbasierte API in die .Net Welt holt.
Sie wird unter der Verwendung des MediaServers von DVBViewer entwickelt.

DVBViewerServerApiWrapper is a lib that brings the web-based API into the .Net world. 
It is developed using DVBViewer's MediaServer.

www.dvbviewer.com

### Using the lib

```C#
    class Program
    {
        static void Main(string[] args)
        {
            var dvbServ = new DVBViewerServerApi
            {
                IpAddress = "Name-of-PC or IpAddress",
                Password = "password set for guest or admin",
                User = "admin",
                Port = 8089
            };

            try
            {
                
                //Gets the serverstatus if online
                var status = dvbServ.Serverstatus;

                Console.WriteLine($"ServerRights: {status.Rights}");
                Console.WriteLine($"EPGUpdate: {status.EPGUpdate}");
                foreach (var item in status.RecordingFolders)
                {
                    Console.WriteLine($"RecordingFolder: {item.Folder}");
                }

                //All current recordings
                var recsAll = dvbServ.Recordings;

                //All current recordings in a shorter and faster way
                var recs = dvbServ.RecordingsShort;

                //All hist recordings ever since database exists (maybe recordings have been deleted, but here exist a copy of the base data)
                var histRecs = dvbServ.RecordedList;

                //All current recordings with "bank" in the name
                var recsN = dvbServ.GetRecordings("bank");

                //All current recordings with "bank" in the description
                var recsD = dvbServ.GetRecordingsByDescription("bank");

                //All connected Clients since media server restarts as PC-Names
                var clients = dvbServ.DVBViewerClients;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
```