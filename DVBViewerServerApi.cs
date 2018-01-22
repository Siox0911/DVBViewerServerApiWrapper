﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DVBViewerServerApiWrapper.Model;

namespace DVBViewerServerApiWrapper
{
    /// <summary>
    /// Bündelt alle Informationen zum RecordingService oder Mediaserver
    /// </summary>
    public class DVBViewerServerApi
    {
        private string user;
        private string password;
        private int port = 8089;
        private string ipAddress;

        #region Properties
        /// <summary>
        /// Benutzername des Service
        /// </summary>
        public string User { get => user; set => user = value; }
        /// <summary>
        /// Password für den Service
        /// </summary>
        public string Password { get => password; set => password = value; }
        /// <summary>
        /// Port zum Service: Default 8089
        /// </summary>
        public int Port { get => port; set => port = value; }
        /// <summary>
        /// IpAdresse zum Service, falls bereits ein Hostname gesetzt wurde, wird die IpAdresse nicht benötigt
        /// </summary>
        public string IpAddress { get => ipAddress; set => ipAddress = value; }
        /// <summary>
        /// Hostname des Service, falls bereits eine IpAdresse gesetzt wurde, wird der Hostname nicht benötigt
        /// </summary>
        public string Hostname { get => ipAddress; set => ipAddress = value; }

        /// <summary>
        /// aktive Instanz
        /// </summary>
        private static DVBViewerServerApi currentInstance;

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
            get { return RecordedList.GetRecordedList(); }
        }
        #endregion

        public DVBViewerServerApi()
        {
            currentInstance = this;
        }

        #region Getter
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
                return Recording.GetRecording(recordID);
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
                return Recording.GetRecordings(partOfName);
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
                return Recording.GetRecordingsByDesc(partOfDescription);
            }
            return null;
        }
        #endregion

        #region PrivateMethodes
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
            //view-source:http://hostname:port/api/recordings.html?utf8=1
            //view-source:http://hostname:port/api/getconfigfile.html?file=config%5C*.*
            //view-source:http://hostname:port/api/getconfigfile.html?file=config%5Csvchardware.xml
            //view-source:http://hostname:port/api/getconfigfile.html?file=*.*
            //view-source:http://hostname:port/api/getconfigfile.html?file=config/service.xml

            if (string.IsNullOrEmpty(ipAddress) || port == 0)
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
                    Port = port,
                    Scheme = "http",
                    Path = $"/api/{page.ToLower()}.html"
                };

                //URL um die Parameter erweitern, falls vorhanden
                if (uriParameters != null)
                {
                    var uriQuery = "";
                    bool first = true;
                    foreach (var item in uriParameters)
                    {
                        if (first)
                        {
                            uriQuery += $"{item.Key}={item.Value}";
                            first = false;
                        }
                        else
                        {
                            uriQuery += $"&{item.Key}={item.Value}";
                        }
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
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<XDocument> GetDataAsync(string page = "status2", List<UriParameter> uriParameters = null)
        {
            if (string.IsNullOrEmpty(page))
            {
                throw new ArgumentNullException(nameof(page), "Die Seite darf nicht leer sein.");
            }

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException($"{nameof(User)} oder {nameof(Password)}", "Benutzer oder Password wurde nicht gesetzt.");
            }

            try
            {
                //Uri
                var uri = CreateUri(page, uriParameters);

                //Rückgabedokument
                XDocument xmlData;

                var webRequest = WebRequest.Create(uri);
                //Falls ein Proxy im System ist, kann das helfen. So lange im IE ein Proxy eingetragen wurde.
                webRequest.Proxy = WebRequest.DefaultWebProxy;
                //AuthType
                webRequest.Credentials = new NetworkCredential(user, password);
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
        #endregion
    }
}
