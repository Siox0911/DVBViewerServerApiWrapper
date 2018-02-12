using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DVBViewerServerApiWrapper.Helper;
using System.Diagnostics;
using System.Security;

namespace DVBViewerServerApiWrapper
{
    /// <summary>
    /// Bündelt alle Informationen zum RecordingService oder Mediaserver
    /// Bundles all information about the recording service or media server
    /// </summary>
    public class DVBViewerServerApi
    {
        private SecureString password;

        /// <summary>
        /// aktive Instanz
        /// active instance
        /// </summary>
        private static DVBViewerServerApi currentInstance;

        #region Public Properties
        /// <summary>
        /// Benutzername des Service
        /// Username of the service
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Password für den Service
        /// Password for the service
        /// </summary>
        public SecureString Password
        {
            private get { return password; }
            set
            {
                password = value;
                password?.MakeReadOnly();
            }
        }
        /// <summary>
        /// Port zum Service: Default 8089
        /// Port to Service: Default 8089
        /// </summary>
        public int Port { get; set; } = 8089;
        /// <summary>
        /// IpAdresse zum Service, falls bereits ein Hostname gesetzt wurde, wird die IpAdresse nicht benötigt
        /// IP address for service, if a host name has already been set, the IP address is not required
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// Hostname des Service, falls bereits eine IpAdresse gesetzt wurde, wird der Hostname nicht benötigt
        /// Hostname of the service, if an ipaddress has already been set, the hostname is not needed
        /// </summary>
        public string Hostname { get => IpAddress; set => IpAddress = value; }

        /// <summary>
        /// <para>
        /// Das Gerät auf dem der Wrapper ausgeführt wird, befindet sich in der Liste für Trusted Devices im DMS. Wenn dies true gesetzt wird, werden Benutzername und Passwort nicht benötigt.
        /// </para>
        /// <para>
        /// The device on which the wrapper is running is in the Trusted Devices list on the DMS. If is set to true, username and password are not required.
        /// </para>
        /// </summary>
        public bool TrustedDevice { get; set; }

        /// <summary>
        /// <para>
        /// Wenn der DMS und eine APP auf der selben Maschine laufen, kann ein Bypass gesetzt werden, welcher direkt auf die Mediendatei verweist. Das sorgt dafür, dass der Wrapper, bei Play, keine Playlisten als m3u für die UPnP streams erstellt. Es wird direkt die Mediendatei zurückgibt.
        /// </para>
        /// <para>
        /// If the DMS and an APP are running on the same machine, a bypass can be set that points directly to the media file. This ensures that the wrapper, in Play, does not create playlists as m3u for the UPnP streams. It will directly return the media file.
        /// </para>
        /// </summary>
        public bool BypassLocalhost { get; set; }

        /// <summary>
        /// Gibt die DVBViewer Clienten (PC-Name) zurück, welche seit dem letzten Start des Servers verbunden waren.
        /// Returns the DVBViewer clients (PC name) that have been connected since the server was last started.
        /// </summary>
        public Task<Model.DVBViewerClients> DVBViewerClientsAsync
        {
            get
            {
                try
                {
                    return Model.DVBViewerClients.GetDVBViewerClientsAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt die DVBViewer Clienten (PC-Name) zurück, welche seit dem letzten Start des Servers verbunden waren.
        /// Returns the DVBViewer clients (PC name) that have been connected since the server was last started.
        /// </summary>
        public Model.DVBViewerClients DVBViewerClients
        {
            get
            {
                try
                {
                    return DVBViewerClientsAsync.Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Der aktuelle Serverstatus
        /// The current server status
        /// </summary>
        public Task<Model.Serverstatus> ServerstatusAsync
        {
            get
            {
                try
                {
                    return Model.Serverstatus.GetServerstatusAsync();
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
        }

        /// <summary>
        /// Der aktuelle Serverstatus
        /// The current server status
        /// </summary>
        public Model.Serverstatus Serverstatus
        {
            get
            {
                try
                {
                    return ServerstatusAsync.Result;
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
        }

        /// <summary>
        /// Gibt alle Aufnahmen vom Service zurück
        /// Returns all recordings of the service
        /// </summary>
        public Task<Model.RecordingList> RecordingsAsync
        {
            get
            {
                try
                {
                    return Model.RecordingList.GetRecordingsAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt alle Aufnahmen vom Service zurück
        /// Returns all recordings of the service
        /// </summary>
        public Model.RecordingList Recordings
        {
            get
            {
                try
                {
                    return RecordingsAsync.Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt alle Aufnahmen vom Service zurück. Es fehlen darin Dateinamen und die lange Beschreibung.
        /// Returns all recordings of the service. It lacks file names and the long description.
        /// </summary>
        public Task<Model.RecordingList> RecordingsShortAsync
        {
            get
            {
                try
                {
                    return Model.RecordingList.GetRecordingsShortAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt alle Aufnahmen vom Service zurück. Es fehlen darin Dateinamen und die lange Beschreibung.
        /// Returns all recordings of the service. It lacks file names and the long description.
        /// </summary>
        public Model.RecordingList RecordingsShort
        {
            get
            {
                try
                {
                    return RecordingsShortAsync.Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
        /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.
        /// A list of recordings that were taken at some point. These no longer need to exist as a file. There are no references to filenames.
        /// This is used to recognize already recorded images.
        /// </summary>
        public Task<Model.RecordedList> RecordedListAsync
        {
            get
            {
                try
                {
                    return Model.RecordedList.GetRecordedListAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
        /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.
        /// A list of recordings that were taken at some point. These no longer need to exist as a file. There are no references to filenames.
        /// This is used to recognize already recorded images.
        /// </summary>
        public Model.RecordedList RecordedList
        {
            get
            {
                try
                {
                    return RecordedListAsync.Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt eine Liste mit allen Videos zurück, welche im Service bekannt sind
        /// Returns a list of all videos known in the service
        /// </summary>
        public Task<Model.VideoFileList> VideoFileListAsync
        {
            get
            {
                try
                {
                    return Model.VideoFileList.GetVideoFileListAsync();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt eine Liste mit allen Videos zurück, welche im Service bekannt sind
        /// Returns a list of all videos known in the service
        /// </summary>
        public Model.VideoFileList VideoFileList
        {
            get
            {
                try
                {
                    return VideoFileListAsync.Result;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt die aktuelle Media Server Version zurück.
        /// Returns the current Media Server version.
        /// </summary>
        public Task<Model.ServerVersion> ServerVersionAsync
        {
            get
            {
                try
                {
                    return Model.ServerVersion.GetServerVersionAsync();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt die aktuelle Media Server Version zurück.
        /// Returns the current Media Server version.
        /// </summary>
        public Model.ServerVersion ServerVersion
        {
            get
            {
                try
                {
                    return ServerVersionAsync.Result;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt alle Servertasks zurück. Tasks sind z.B. Datenbanken aktualisieren etc.
        /// Returns all server tasks. Tasks are e.g. Update databases etc.
        /// </summary>
        public Task<Model.ServerTaskList> ServerTasksAsync
        {
            get
            {
                try
                {
                    return Model.ServerTaskList.GetServerTaskListAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt alle Servertasks zurück. Tasks sind z.B. Datenbanken aktualisieren etc.
        /// Returns all server tasks. Tasks are e.g. Update databases etc.
        /// </summary>
        public Model.ServerTaskList ServerTasks
        {
            get
            {
                try
                {
                    return ServerTasksAsync.Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt eine Liste mit allen VideoPfaden zurück.
        /// Returns a list of all video paths.
        /// </summary>
        public Task<Model.VideoFilePath> VideoPathsAsync
        {
            get
            {
                return Model.VideoFilePath.GetVideoFilePathAsync();
            }
        }

        /// <summary>
        /// Gibt eine Liste mit allen VideoPfaden zurück.
        /// Returns a list of all video paths.
        /// </summary>
        public Model.VideoFilePath VideoPaths
        {
            get
            {
                return VideoPathsAsync.Result;
            }
        }

        #endregion

        public DVBViewerServerApi()
        {
            currentInstance = this;
        }

        #region Public Getter
        /// <summary>
        /// Gibt die aktive Instanz dieser Klasse zurück.
        /// Returns the active instance of this class.
        /// </summary>
        /// <returns></returns>
        public static DVBViewerServerApi GetCurrentInstance()
        {
            return currentInstance;
        }

        /// <summary>
        /// Gibt eine Aufnahme anhand der AufnahmeID zurück.
        /// Returns a recording based on the recording ID.
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        public async Task<Model.RecordingList> GetRecordingAsync(int recordID)
        {
            if (recordID > 0)
            {
                try
                {
                    return await Model.RecordingList.GetRecordingAsync(recordID).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Aufnahme anhand der AufnahmeID zurück.
        /// Returns a recording based on the recording ID.
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        public Model.RecordingList GetRecording(int recordID)
        {
            if (recordID > 0)
            {
                try
                {
                    return GetRecordingAsync(recordID).Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche den Text als Teil im Namen haben
        /// Returns a list of recordings that have the text as part of the name
        /// </summary>
        /// <param name="partOfName"></param>
        /// <returns></returns>
        public async Task<Model.RecordingList> GetRecordingsAsync(string partOfName)
        {
            if (!string.IsNullOrEmpty(partOfName))
            {
                try
                {
                    return await Model.RecordingList.GetRecordingsAsync(partOfName).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche den Text als Teil im Namen haben
        /// Returns a list of recordings that have the text as part of the name
        /// </summary>
        /// <param name="partOfName"></param>
        /// <returns></returns>
        public Model.RecordingList GetRecordings(string partOfName)
        {
            if (!string.IsNullOrEmpty(partOfName))
            {
                try
                {
                    return GetRecordingsAsync(partOfName).Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche Text als Teil in der Beschreibung haben.
        /// Returns a list of recordings that have text as part of the description.
        /// </summary>
        /// <param name="partOfDescription"></param>
        /// <returns></returns>
        public async Task<Model.RecordingList> GetRecordingsByDescriptionAsync(string partOfDescription)
        {
            if (!string.IsNullOrEmpty(partOfDescription))
            {
                try
                {
                    return await Model.RecordingList.GetRecordingsByDescAsync(partOfDescription).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche Text als Teil in der Beschreibung haben.
        /// Returns a list of recordings that have text as part of the description.
        /// </summary>
        /// <param name="partOfDescription"></param>
        /// <returns></returns>
        public Model.RecordingList GetRecordingsByDescription(string partOfDescription)
        {
            if (!string.IsNullOrEmpty(partOfDescription))
            {
                try
                {
                    return GetRecordingsByDescriptionAsync(partOfDescription).Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Videoliste zurück, welche einen Teil des Suchtextes im Titel tragen.
        /// Returns a video list that carries part of the search text in the title.
        /// </summary>
        /// <param name="partOfName"></param>
        /// <returns></returns>
        public async Task<Model.VideoFileList> GetVideoListAsync(string partOfName)
        {
            if (!string.IsNullOrEmpty(partOfName))
            {
                try
                {
                    return await Model.VideoFileList.GetVideoFileListAsync(partOfName).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Videoliste zurück, welche einen Teil des Suchtextes im Titel tragen.
        /// Returns a video list that carries part of the search text in the title.
        /// </summary>
        /// <param name="partOfName"></param>
        /// <returns></returns>
        public Model.VideoFileList GetVideoList(string partOfName)
        {
            if (!string.IsNullOrEmpty(partOfName))
            {
                try
                {
                    return GetVideoListAsync(partOfName).Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }
        #endregion

        #region Private Methodes
        /// <summary>
        /// Generiert aus den Daten eine URL mit Api zum Verbinden zum Service
        /// </summary>
        /// <param name="page">Die Seite welche geladen werden soll ohne html am Ende</param>
        /// <param name="uriParameters">Eine Liste mit Parametern</param>
        /// <returns>Eine Uri welche zum Connect zum Service genutzt werden kann.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Der Port konnte nicht gesetzt werden.</exception>
        /// <exception cref="ArgumentException">Das Schema konnte nicht gesetzt werden.</exception>
        /// <exception cref="MissingFieldException"><seealso cref="Hostname"/> oder <seealso cref="Port"/> wurden nicht gesetzt.</exception>
        private Uri CreateApiUri(string page, List<UriParameter> uriParameters)
        {

            if (string.IsNullOrEmpty(IpAddress) || Port == 0)
            {
                throw new MissingFieldException("Hostname oder Port wurden nicht gesetzt. Hostname or Port not set.");
            }

            try
            {
                var ub = new UriBuilder
                {
                    Host = IpAddress,
                    Port = Port,
                    Scheme = "http",
                    Path = $"/api/{page.ToLower()}.html"
                };

                //URL um die Parameter erweitern, falls vorhanden
                if (uriParameters?.Count > 0)
                {
                    var uriQuery = "";
                    var first = true;
                    foreach (var item in uriParameters)
                    {
                        if (!first)
                        {
                            uriQuery += "&";
                        }
                        uriQuery += $"{item.Key}={Uri.EscapeDataString(item.Value)}";
                        first = false;
                    }
                    ub.Query = uriQuery;
                }

                return ub.Uri;
            }
            catch (ArgumentOutOfRangeException)
            {
                //Port passt nicht
                throw;
            }
            catch (ArgumentException)
            {
                //Schema passt nicht
                throw;
            }
        }

        /// <summary>
        /// Generiert aus den Daten eine URL ohne Api zum Verbinden zum Service
        /// </summary>
        /// <param name="page">Die Seite welche geladen werden soll ohne html am Ende</param>
        /// <param name="uriParameters">Eine Liste mit Parametern</param>
        /// <returns>Eine Uri welche zum Connect zum Service genutzt werden kann.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Der Port konnte nicht gesetzt werden.</exception>
        /// <exception cref="ArgumentException">Das Schema konnte nicht gesetzt werden.</exception>
        /// <exception cref="MissingFieldException"><seealso cref="Hostname"/> oder <seealso cref="Port"/> wurden nicht gesetzt.</exception>
        private Uri CreateUri(string page, List<UriParameter> uriParameters)
        {

            if (string.IsNullOrEmpty(IpAddress) || Port == 0)
            {
                throw new MissingFieldException("Hostname oder Port wurden nicht gesetzt. Hostname or Port not set.");
            }

            try
            {
                var ub = new UriBuilder
                {
                    Host = IpAddress,
                    Port = Port,
                    Scheme = "http",
                    Path = $"/{page.ToLower()}.html"
                };

                //URL um die Parameter erweitern, falls vorhanden
                if (uriParameters?.Count > 0)
                {
                    var uriQuery = "";
                    var first = true;
                    foreach (var item in uriParameters)
                    {
                        if (!first)
                        {
                            uriQuery += "&";
                        }
                        uriQuery += $"{item.Key}={Uri.EscapeDataString(item.Value)}";
                        first = false;
                    }
                    ub.Query = uriQuery;
                }

                return ub.Uri;
            }
            catch (ArgumentOutOfRangeException)
            {
                //Port passt nicht
                throw;
            }
            catch (ArgumentException)
            {
                //Schema passt nicht
                throw;
            }
        }

        /// <summary>
        /// Gibt die Api-Daten vom Server asynchron zurück.
        /// </summary>
        /// <param name="page">Die Seite welche geladen werden soll.</param>
        /// <param name="uriParameters"></param>
        /// <exception cref="ArgumentNullException">Es wurde keine Seite angegeben oder die Seite ist null</exception>
        /// <exception cref="NullReferenceException">Benutzername und Password zum Service wurden nicht angegeben oder sind leer.</exception>
        internal async Task<XDocument> GetDataAsync(string page = "status2", List<UriParameter> uriParameters = null)
        {
            if (string.IsNullOrEmpty(page))
            {
                throw new ArgumentNullException(nameof(page), "Die Seite darf nicht leer sein. Page can't be empty.");
            }

            if (!TrustedDevice && (string.IsNullOrEmpty(User) || Password == null))
            {
                throw new NullReferenceException("Benutzer oder Password wurde nicht gesetzt. User or passwort not set.");
            }

            try
            {
                //Uri
                var uri = CreateApiUri(page, uriParameters);

                //Rückgabedokument
                XDocument xmlData;

                var webRequest = WebRequest.Create(uri);
                //Falls ein Proxy im System ist, kann das helfen. So lange im IE ein Proxy eingetragen wurde.
                webRequest.Proxy = WebRequest.DefaultWebProxy;
                //AuthType, if device ist not in the trusted device List
                if (!TrustedDevice)
                {
                    webRequest.Credentials = new NetworkCredential(User, Password);
                    webRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
                }
                //Abfragemethode
                webRequest.Method = WebRequestMethods.Http.Get;

                //Abfrage durchführen
                using (var response = await webRequest.GetResponseAsync().ConfigureAwait(false))
                {
                    using (var stream = response.GetResponseStream())
                    {
                        xmlData = XDocument.Load(stream, LoadOptions.SetLineInfo);
                    }
                }

                return xmlData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sendet Daten über die API asynchron zum Server und gibt einen Code über den Erfolg zurück.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="uriParameters"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        internal async Task<HttpStatusCode> SendDataAsync(string page = "dvbcommand", List<UriParameter> uriParameters = null)
        {
            if (string.IsNullOrEmpty(page))
            {
                throw new ArgumentNullException(nameof(page), "Die Seite darf nicht leer sein. Page can't be empty.");
            }

            if (!TrustedDevice && (string.IsNullOrEmpty(User) || Password == null))
            {
                throw new NullReferenceException("Benutzer oder Password wurde nicht gesetzt. User or passwort not set.");
            }

            try
            {
                //Uri
                var uri = CreateApiUri(page, uriParameters);

                var webRequest = WebRequest.Create(uri);
                //Falls ein Proxy im System ist, kann das helfen. So lange im IE ein Proxy eingetragen wurde.
                webRequest.Proxy = WebRequest.DefaultWebProxy;
                //AuthType, if device ist not in the trusted device List
                if (!TrustedDevice)
                {
                    webRequest.Credentials = new NetworkCredential(User, Password);
                    webRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
                }
                //Abfragemethode
                webRequest.Method = WebRequestMethods.Http.Get;

                //Abfrage durchführen
                var response = (HttpWebResponse)await webRequest.GetResponseAsync().ConfigureAwait(false);
                var status = response.StatusCode;
                response.Close();
                return status;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sendet Daten über POST (ohne Api) asynchron zum Server und gibt einen Code über den Erfolg zurück.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="uriParameters"></param>
        /// <param name="webRequestMethod"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        internal async Task<HttpStatusCode> SendPostDataAsync(string page = "rec_edit", List<UriParameter> uriParameters = null)
        {
            if (string.IsNullOrEmpty(page))
            {
                throw new ArgumentNullException(nameof(page), "Die Seite darf nicht leer sein. Page can't be empty.");
            }

            if (!TrustedDevice && (string.IsNullOrEmpty(User) || Password == null))
            {
                throw new NullReferenceException("Benutzer oder Password wurde nicht gesetzt. User or passwort not set.");
            }

            try
            {
                //Uri
                var uri = new UriBuilder
                {
                    Host = IpAddress,
                    Port = Port,
                    Scheme = "http",
                    Path = $"/{page.ToLower()}.html"
                };

                var postdata = new StringBuilder();
                //URL um die Parameter erweitern, falls vorhanden
                if (uriParameters?.Count > 0)
                {
                    var first = true;
                    foreach (var item in uriParameters)
                    {
                        if (!first)
                        {
                            postdata.Append("&");
                        }
                        postdata.Append(item.Key).Append("=").Append(Uri.EscapeDataString(item.Value ?? ""));
                        first = false;
                    }
                }

                var data = Encoding.ASCII.GetBytes(postdata.ToString());

                var webRequest = WebRequest.Create(uri.Uri);
                //Falls ein Proxy im System ist, kann das helfen. So lange im IE ein Proxy eingetragen wurde.
                webRequest.Proxy = WebRequest.DefaultWebProxy;
                //AuthType, if device ist not in the trusted device List
                if (!TrustedDevice)
                {
                    webRequest.Credentials = new NetworkCredential(User, Password);
                    webRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
                }
                //Abfragemethode
                webRequest.Method = WebRequestMethods.Http.Post;
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = data.Length;

                using (var stream = webRequest.GetRequestStream())
                {
                    await stream.WriteAsync(data, 0, postdata.Length).ConfigureAwait(false);
                }

                //Abfrage durchführen
                var response = (HttpWebResponse)await webRequest.GetResponseAsync().ConfigureAwait(false);
                var status = response.StatusCode;
                response.Close();
                return status;
            }
            catch (Exception)
            {
                throw;
            }
        }

        ///// <summary>
        ///// Ruft eine Seite auf und gibt die erhaltene Datei als Pfad und Dateiname zurück.
        ///// </summary>
        ///// <param name="page"></param>
        ///// <param name="uriParameters"></param>
        ///// <returns></returns>
        ///// <exception cref="NullReferenceException"></exception>
        ///// <exception cref="ArgumentNullException"></exception>
        //public async Task<string> GetFileAsync(string page, List<UriParameter> uriParameters)
        //{
        //    if (string.IsNullOrEmpty(page))
        //    {
        //        throw new ArgumentNullException(nameof(page), "Die Seite darf nicht leer sein. Page can't be empty.");
        //    }

        //    if (string.IsNullOrEmpty(User) || Password == null)
        //    {
        //        throw new NullReferenceException("Benutzer oder Password wurde nicht gesetzt. User or passwort not set.");
        //    }

        //    try
        //    {
        //        //Uri
        //        var uri = CreateUri(page, uriParameters);

        //        var webRequest = WebRequest.Create(uri);
        //        //Falls ein Proxy im System ist, kann das helfen. So lange im IE ein Proxy eingetragen wurde.
        //        webRequest.Proxy = WebRequest.DefaultWebProxy;
        //        //AuthType
        //        webRequest.Credentials = new NetworkCredential(User, Password);
        //        webRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
        //        //Abfragemethode
        //        webRequest.Method = WebRequestMethods.Http.Get;

        //        //Abfrage durchführen
        //        using (var response = (HttpWebResponse)await webRequest.GetResponseAsync().ConfigureAwait(false))
        //        {
        //            var file = response.ResponseUri.AbsolutePath;

        //            using (var stream = response.GetResponseStream())
        //            {
        //                //TODO: Hier muss noch die Datei gespeichert werden. Aktuell wird nur der Absolute Pfad der Datei aus dem Server zurückgegeben.
        //                return response.ResponseUri.AbsolutePath;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion
    }
}
