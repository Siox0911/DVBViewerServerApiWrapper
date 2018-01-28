using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DVBViewerServerApiWrapper.Model;
using DVBViewerServerApiWrapper.Helper;
using System.Diagnostics;

namespace DVBViewerServerApiWrapper
{
    /// <summary>
    /// Bündelt alle Informationen zum RecordingService oder Mediaserver
    /// </summary>
    public class DVBViewerServerApi
    {
        private string ipAddress;

        /// <summary>
        /// aktive Instanz
        /// </summary>
        private static DVBViewerServerApi currentInstance;

        #region Public Properties
        /// <summary>
        /// Benutzername des Service
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Password für den Service
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Port zum Service: Default 8089
        /// </summary>
        public int Port { get; set; } = 8089;
        /// <summary>
        /// IpAdresse zum Service, falls bereits ein Hostname gesetzt wurde, wird die IpAdresse nicht benötigt
        /// </summary>
        public string IpAddress { get => ipAddress; set => ipAddress = value; }
        /// <summary>
        /// Hostname des Service, falls bereits eine IpAdresse gesetzt wurde, wird der Hostname nicht benötigt
        /// </summary>
        public string Hostname { get => ipAddress; set => ipAddress = value; }
        /// <summary>
        /// Gibt die DVBViewer Clienten (PC-Name) zurück, welche seit dem letzten Start des Servers verbunden waren.
        /// </summary>
        public DVBViewerClients DVBViewerClients
        {
            get
            {
                try
                {
                    return DVBViewerClients.GetDvbViewerClients();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Der aktuelle Serverstatus
        /// </summary>
        public Serverstatus Serverstatus
        {
            get
            {
                try
                {
                    return Serverstatus.CreateServerstatus(GetDataAsync().Result);
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
        }
        /// <summary>
        /// Gibt alle Aufnahmen vom Service zurück
        /// </summary>
        public Recording Recordings
        {
            get
            {
                try
                {
                    return Recording.GetRecordings();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gibt alle Aufnahmen vom Service zurück. Es fehlen darin Dateinamen und die lange Beschreibung.
        /// </summary>
        public Recording RecordingsShort
        {
            get
            {
                try
                {
                    return Recording.GetRecordingsShort();
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
        /// </summary>
        public RecordedList RecordedList
        {
            get
            {
                try
                {
                    return RecordedList.GetRecordedList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt eine Liste mit allen Videos zurück, welche im Service bekannt sind
        /// </summary>
        public VideoFileList VideoFileList
        {
            get
            {
                try
                {
                    return VideoFileList.GetVideoFileList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt die aktuelle Media Server Version zurück.
        /// </summary>
        public Model.Version ServerVersion
        {
            get
            {
                try
                {
                    return Model.Version.GetVersion();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// Gibt alle Servertasks zurück. Tasks sind z.B. Datenbanken aktualisieren etc.
        /// </summary>
        public Model.ServerTaskList ServerTasks
        {
            get
            {
                try
                {
                    return Model.ServerTaskList.GetServerTaskList();
                }
                catch (Exception)
                {
                    throw;
                }
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
        /// </summary>
        /// <returns></returns>
        public static DVBViewerServerApi GetCurrentInstance()
        {
            return currentInstance;
        }

        /// <summary>
        /// Gibt eine Aufnahme anhand der AufnahmeID zurück.
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        public Recording GetRecording(int recordID)
        {
            if (recordID > 0)
            {
                try
                {
                    return Recording.GetRecording(recordID);
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
        /// </summary>
        /// <param name="partOfName"></param>
        /// <returns></returns>
        public Recording GetRecordings(string partOfName)
        {
            if (!string.IsNullOrEmpty(partOfName))
            {
                try
                {
                    return Recording.GetRecordings(partOfName);
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
        /// </summary>
        /// <param name="partOfDescription"></param>
        /// <returns></returns>
        public Recording GetRecordingsByDescription(string partOfDescription)
        {
            if (!string.IsNullOrEmpty(partOfDescription))
            {
                try
                {
                    return Recording.GetRecordingsByDesc(partOfDescription);
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
        /// </summary>
        /// <param name="partOfName"></param>
        /// <returns></returns>
        public VideoFileList GetVideoList(string partOfName)
        {
            if (!string.IsNullOrEmpty(partOfName))
            {
                try
                {
                    return VideoFileList.GetVideoFileList(partOfName);
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
        /// Generiert aus den Daten eine URL zum Verbinden zum Service
        /// </summary>
        /// <param name="page">Die Seite welche geladen werden soll ohne html am Ende</param>
        /// <param name="uriParameters">Eine Liste mit Parametern</param>
        /// <returns>Eine Uri welche zum Connect zum Service genutzt werden kann.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Der Port konnte nicht gesetzt werden.</exception>
        /// <exception cref="ArgumentException">Das Schema konnte nicht gesetzt werden.</exception>
        /// <exception cref="MissingFieldException"><seealso cref="Hostname"/> oder <seealso cref="Port"/> wurden nicht gesetzt.</exception>
        private Uri CreateApiUri(string page, List<UriParameter> uriParameters)
        {

            if (string.IsNullOrEmpty(ipAddress) || Port == 0)
            {
                throw new MissingFieldException("Hostname oder Port wurden nicht gesetzt.");
            }

            try
            {
                var ub = new UriBuilder
                {
                    Host = ipAddress,
                    //Wird nicht benötigt: Credentials werden beim Abrufen gesetzt
                    //UserName = user,
                    //Password = password,
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
        /// Generiert aus den Daten eine URL zum Verbinden zum Service
        /// </summary>
        /// <param name="page">Die Seite welche geladen werden soll ohne html am Ende</param>
        /// <param name="uriParameters">Eine Liste mit Parametern</param>
        /// <returns>Eine Uri welche zum Connect zum Service genutzt werden kann.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Der Port konnte nicht gesetzt werden.</exception>
        /// <exception cref="ArgumentException">Das Schema konnte nicht gesetzt werden.</exception>
        /// <exception cref="MissingFieldException"><seealso cref="Hostname"/> oder <seealso cref="Port"/> wurden nicht gesetzt.</exception>
        private Uri CreateUri(string page, List<UriParameter> uriParameters)
        {

            if (string.IsNullOrEmpty(ipAddress) || Port == 0)
            {
                throw new MissingFieldException("Hostname oder Port wurden nicht gesetzt.");
            }

            try
            {
                var ub = new UriBuilder
                {
                    Host = ipAddress,
                    //Wird nicht benötigt: Credentials werden beim Abrufen gesetzt
                    //UserName = user,
                    //Password = password,
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
        /// Gibt die Daten vom Server asynchron zurück.
        /// </summary>
        /// <param name="page">Die Seite welche geladen werden soll.</param>
        /// <param name="uriParameters"></param>
        /// <exception cref="ArgumentNullException">Es wurde keine Seite angegeben oder die Seite ist null</exception>
        /// <exception cref="NullReferenceException">Benutzername und Password zum Service wurden nicht angegeben oder sind leer.</exception>
        public async Task<XDocument> GetDataAsync(string page = "status2", List<UriParameter> uriParameters = null)
        {
            if (string.IsNullOrEmpty(page))
            {
                throw new ArgumentNullException(nameof(page), "Die Seite darf nicht leer sein.");
            }

            if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
            {
                throw new NullReferenceException("Benutzer oder Password wurde nicht gesetzt.");
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
                //AuthType
                webRequest.Credentials = new NetworkCredential(User, Password);
                webRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
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
        /// Sendet Daten asynchron zum Server und gibt einen Code über den Erfolg zurück.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="uriParameters"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<HttpStatusCode> SendDataAsync(string page = "dvbcommand", List<UriParameter> uriParameters = null)
        {
            if (string.IsNullOrEmpty(page))
            {
                throw new ArgumentNullException(nameof(page), "Die Seite darf nicht leer sein.");
            }

            if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
            {
                throw new NullReferenceException("Benutzer oder Password wurde nicht gesetzt.");
            }

            try
            {
                //Uri
                var uri = CreateApiUri(page, uriParameters);

                var webRequest = WebRequest.Create(uri);
                //Falls ein Proxy im System ist, kann das helfen. So lange im IE ein Proxy eingetragen wurde.
                webRequest.Proxy = WebRequest.DefaultWebProxy;
                //AuthType
                webRequest.Credentials = new NetworkCredential(User, Password);
                webRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
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
        /// Ruft eine Seite auf und gibt die erhaltene Datei als Pfad und Dateiname zurück.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="uriParameters"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<string> GetFileAsync(string page, List<UriParameter> uriParameters)
        {
            if (string.IsNullOrEmpty(page))
            {
                throw new ArgumentNullException(nameof(page), "Die Seite darf nicht leer sein.");
            }

            if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
            {
                throw new NullReferenceException("Benutzer oder Password wurde nicht gesetzt.");
            }

            try
            {
                //Uri
                var uri = CreateUri(page, uriParameters);

                var webRequest = WebRequest.Create(uri);
                //Falls ein Proxy im System ist, kann das helfen. So lange im IE ein Proxy eingetragen wurde.
                webRequest.Proxy = WebRequest.DefaultWebProxy;
                //AuthType
                webRequest.Credentials = new NetworkCredential(User, Password);
                webRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
                //Abfragemethode
                webRequest.Method = WebRequestMethods.Http.Get;

                //Abfrage durchführen
                using (var response = (HttpWebResponse)await webRequest.GetResponseAsync().ConfigureAwait(false))
                {
                    var file = response.ResponseUri.AbsolutePath;

                    using (var stream = response.GetResponseStream())
                    {
                        //TODO: Hier muss noch die Datei gespeichert werden. Aktuell wird nur der Absolute Pfad der Datei aus dem Server zurückgegeben.
                        return response.ResponseUri.AbsolutePath;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
