using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DVBViewerServerApiWrapper.Helper;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Eine Liste mit allen existierenden Aufnahmen
    /// </summary>
    [XmlRoot(ElementName = "recordings")]
    public class Recording
    {
        /// <summary>
        /// Version der Aufnahmetabelle
        /// </summary>
        [XmlAttribute(AttributeName = "Ver")]
        public int Version { get; set; }
        /// <summary>
        /// Revision der Aufnahmetabelle
        /// </summary>
        [XmlElement(ElementName = "rev", Type = typeof(int))]
        public int Revision { get; set; }
        /// <summary>
        /// Die Basisserver URL. Um daraus einen Stream aufzubauen: ServerURL + ID + ".ts"
        /// </summary>
        [XmlElement(ElementName = "serverURL")]
        public string ServerURL { get; set; }
        /// <summary>
        /// Die Basis URL für Bilder. Bildurl = ImageURL + Record.Image (falls thumbnails aktiviert sind)
        /// </summary>
        [XmlElement(ElementName = "imageURL")]
        public string ImageURL { get; set; }
        /// <summary>
        /// Die komplette Liste der Aufnahmen
        /// </summary>
        [XmlElement(ElementName = "recording", Type = typeof(Record))]
        public List<Record> Records { get; set; }

        internal Recording() { }

        /// <summary>
        /// Erzeugt ein Recording
        /// </summary>
        /// <param name="xDocument"></param>
        /// <returns></returns>
        internal static Recording CreateRecording(XDocument xDocument)
        {
            return Deserializer.Deserialize<Recording>(xDocument, new Type[] { typeof(Record) });
        }

        /// <summary>
        /// Gibt alle Aufnahmen zurück.
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        internal static Recording GetRecordings()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = dvbApi.GetDataAsync("recordings",
                new List<UriParameter> {
                    new UriParameter("utf8", "1"),
                    new UriParameter("images", "1")
                }).Result;

                if (xmldata != null)
                {
                    return CreateRecording(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt alle Aufnahmen vom Service zurück. Es fehlen darin Dateinamen und die lange Beschreibung.
        /// </summary>
        /// <returns></returns>
        internal static Recording GetRecordingsShort()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = dvbApi.GetDataAsync("recordings",
                        new List<UriParameter> {
                            new UriParameter("utf8", "1"),
                            new UriParameter("images", "1"),
                            new UriParameter("nodesc", "1"),
                            new UriParameter("nofilename", "1")
                        }).Result;

                if (xmldata != null)
                {
                    return CreateRecording(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Aufnahme anhand der AufnahmeID zurück.
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        internal static Recording GetRecording(int recordID)
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = dvbApi.GetDataAsync("recordings",
                new List<UriParameter> {
                    new UriParameter("utf8", "1"),
                    new UriParameter("images", "1"),
                    new UriParameter("id", recordID.ToString())
                }).Result;

                if (xmldata != null)
                {
                    return CreateRecording(xmldata);
                }
            }
            return null;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche den Titel enthalten
        /// </summary>
        /// <param name="partOfName">Teil des Namens</param>
        /// <returns></returns>
        internal static Recording GetRecordings(string partOfName)
        {
            var result = GetRecordings();
            if (result != null)
            {
                result.Records = (from f in result.Records where f.Title.IndexOf(partOfName, StringComparison.OrdinalIgnoreCase) != -1 select f).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gibt eine Liste mit Aufnahmen zurück, welche Text als Teil in der Beschreibung haben.
        /// </summary>
        /// <param name="partOfDesc">Ein Teil der Beschreibung</param>
        /// <returns></returns>
        internal static Recording GetRecordingsByDesc(string partOfDesc)
        {
            var result = GetRecordings();
            if(result != null)
            {
                result.Records = (from f in result.Records where f.Description.IndexOf(partOfDesc, StringComparison.OrdinalIgnoreCase) != -1 select f).ToList();
            }
            return result;
        }
    }
}
