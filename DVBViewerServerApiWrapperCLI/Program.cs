using DVBViewerServerApiWrapper;
using DVBViewerServerApiWrapper.Helper;
using DVBViewerServerApiWrapper.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DVBViewerServerApiWrapperCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var dvbServ = new DVBViewerServerApi
            {
                IpAddress = "192.168.2.101",
                User = "admin",
                Port = 8089
            };

            //Define new SecureString
            var spw = new SecureString();

            var pw = "integrale";
            foreach (char item in pw)
            {
                spw.AppendChar(item);
            }

            //Set the password
            dvbServ.Password = spw;
            pw = null;
            spw = null;

            try
            {
                var clients = DVBViewerClients.GetDVBViewerClientsAsync().Result;
                var client = clients.Items[0];
                var test = ChannelList.GetChannelListAsync().Result;

                //var sw = new Stopwatch();
                //sw.Start();
                //var epgList = EpgList.GetEpgList();
                //sw.Stop();
                //Console.WriteLine($"EPGList with {epgList.Items.Count} entrys. Needed to load {sw.Elapsed.TotalSeconds} seconds.");

                //Gets the Channel 3Sat HD from the server. (Satellite)
                var sat3 = ChannelList.GetChannelList($"{2359890891631983072}").TopGroups[0].Groups[0].Items[0];
                Console.WriteLine($"EPG Channel ID: {sat3.EpgChannelID}");
                Console.WriteLine($"Channelname: {sat3.Name}");

                //Current EPG Title
                var sat3EpgList = sat3.GetEpgListNow();
                //The sat3EpgList.Items.Count is only 1
                Console.WriteLine($"Current Title: {sat3EpgList.Items[0].Title}");

                //alle Timer holen
                var lTimer = TimerList.GetTimerListAsync().Result;
                //ersten Timer auswählen
                var timer0 = lTimer.Items[0];
                //Epg anzeigen
                var epgLTimer = timer0.EpgListAsync.Result;
                Console.WriteLine($"Timer 0: Channel: {timer0.ChannelItem.Name}, Timername: {timer0.Description} has the EPG Description: {epgLTimer.Items[0].Description}");
                Console.WriteLine($"Timer 0: The channel: {timer0.ChannelList.TopGroups[0].Groups[0].Items[0].Name}, currently has the EPG: {timer0.ChannelList.TopGroups[0].Groups[0].Items[0].EpgListNow.Items[0].Title} - {timer0.ChannelList.TopGroups[0].Groups[0].Items[0].EpgListNow.Items[0].Description}");


                var status = Serverstatus.GetServerstatus();

                Console.WriteLine($"Die Rechte sind: {status.Rights}");
            }
            catch (Exception ex)
            {
                if(ex.InnerException == null)
                {
                    Console.WriteLine(ex.Message);
                }
                else
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }

            Console.ReadKey();
        }
    }
}
