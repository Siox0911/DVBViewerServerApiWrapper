# DVBViewerServerApiWrapper

Der DVBViewerServerApiWrapper ist eine Bibliothek, welche die webbasierte API in die .Net Welt holt.
Sie wird unter der Verwendung des MediaServers von DVBViewer entwickelt.

DVBViewerServerApiWrapper is a lib that brings the web-based API into the .Net world. 
It is developed using DVBViewer's MediaServer.

www.dvbviewer.com

### Current state (Aktueller Status)

Version 0.0.2.1

Retrieve data
- parse the service API into .Net objects (readonly)


Daten abrufen
- umwandeln der Daten von der Service API in .Net Objekte (nur lesen)

Symbols: <img src="images/ToDo_Ready_256.png" width="22"/>Ready, 
<img src="images/ToDo_Current_256.png" width="22"/> Work, 
<img src="images/ToDo_Add_256.png" width="22"/> ToDo, 
<img src="images/ToDo_Abort_256.png" width="22"/> Abort, Canceled

<br/><img src="images/ToDo_Ready_256.png" width="22"/> status2.html
<br/><img src="images/ToDo_Ready_256.png" width="22"/> version.html
<br/><img src="images/ToDo_Current_256.png" width="22"/> dvbcommand.html
<br/><img src="images/ToDo_Current_256.png" width="22"/> recordings.html
<br/><img src="images/ToDo_Current_256.png" width="22"/> mediafiles.html -> own implementation over the sql.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> getconfigfile.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> setting.html -> maybe unnecessary (double support to status2.html)
<br/><img src="images/ToDo_Add_256.png" width="22"/> getchannelsxml.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> recdelete.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> epg.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> epgclear.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> timerlist.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> timeradd.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> timeredit.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> timerdelete.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> tasks.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> sideload.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> startts.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> stopts.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> searchlist.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> searchdelete.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> searchedit.html
<br/><img src="images/ToDo_Add_256.png" width="22"/> searchadd.html
<br/><img src="images/ToDo_Abort_256.png" width="22"/> sql.html - integration in all relevant classes

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
                
               //Get the serverstatus if online
                var status = dvbServ.Serverstatus;

                Console.WriteLine($"ServerRights: {status.Rights}");
                Console.WriteLine($"EPGUpdate: {status.EPGUpdate}");
                foreach (var item in status.RecordingFolders)
                {
                    Console.WriteLine($"RecordingFolder: {item.Folder}");
                }

                var version = dvbServ.ServerVersion;
                Console.WriteLine($"Serverversion: {version.ServerVersion}");

                //All current recordings
                var recsAll = dvbServ.Recordings;
                Console.WriteLine($"Number of recordings: {recsAll.Items.Count}");

                //All current recordings in a shorter and faster way
                var recs = dvbServ.RecordingsShort;

                //All hist recordings ever since database exists (maybe recordings have been deleted, but here exist a copy of the base data)
                var histRecs = dvbServ.RecordedList;

                //All current recordings with "bank" in the name
                var recsN = dvbServ.GetRecordings("bank");
                Console.WriteLine($"Number of recordings with bank in the name: {recsN.Items.Count}");

                //All current recordings with "bank" in the description
                var recsD = dvbServ.GetRecordingsByDescription("bank");
                Console.WriteLine($"Number of recordings with bank in the description: {recsD.Items.Count}");

                //All connected Clients since media server restarts as PC-Names
                var clients = dvbServ.DVBViewerClients;
                Console.WriteLine($"Number of clients connected to MediaServer since start: {clients.Items.Count}");

                var videofiles = dvbServ.VideoFileList;
                Console.WriteLine($"Number of videofiles in MediaServer since last database update: {videofiles.Items.Count}");

                //Neuer Zufallsgenerator
                var rnd = new Random();
                //Neue Zufallszahl erzeugen
                int next = rnd.Next(videofiles.Items.Count);
                //Dem ersten Clienten ein zufälliges Video abspielen lassen
                clients.Items[0].PlayVideo(videofiles.Items[next]);
                Console.WriteLine($"Client \"{clients.Items[0].Name}\" plays video \"{videofiles.Items[next].Title}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
```

![Screenshot C L I](images/screenshotCLI.png)