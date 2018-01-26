# DVBViewerServerApiWrapper

Der DVBViewerServerApiWrapper ist eine Bibliothek, welche die webbasierte API in die .Net Welt holt.
Sie wird unter der Verwendung des MediaServers von DVBViewer entwickelt.

DVBViewerServerApiWrapper is a lib that brings the web-based API into the .Net world. 
It is developed using DVBViewer's MediaServer.

www.dvbviewer.com

### Current state (Aktueller Status)

Retrieve data
- parse the service API into .Net objects (readonly)


Daten abrufen
- umwandeln der Daten von der Service API in .Net Objekte (nur lesen)

Symbols: <img src="images/ToDo_Ready_256.png" width="22"/>Ready, 
<img src="images/ToDo_Current_256.png" width="22"/> Work, 
<img src="images/ToDo_Add_256.png" width="22"/> ToDo, 
<img src="images/ToDo_Abort_256.png" width="22"/> Abort, Canceled

<br/><img src="images/ToDo_Ready_256.png" width="22"/> status2.html
<br/><img src="images/ToDo_Current_256.png" width="22"/> dvbcommand.html
<br/><img src="images/ToDo_Current_256.png" width="22"/> recordings.html - Add eventID to uri
<br/><img src="images/ToDo_Add_256.png" width="22"/> getconfigfile.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> version.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> setting.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> getchannelsxml.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> mediafiles.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> recdelete.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> epg.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> epgclear.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> timerlist.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> timeradd.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> timeredit.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> timerdelete.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> tasks.html
<br/><img src="images/ToDo_Current_256.png" width="22"/><img src="images/ToDo_Abort_256.png" width="22"/> sql.html - some changes in a newer media server version

### Using the lib

```C#
    class Program
    {
        static void Main(string[] args)
        {
            var dvbServ = new DVBViewerServerApi
            {
                IpAddress = "Name-of-PC or IpAddress",
                Password = "password for guest or admin",
                User = "username for guest or admin",
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