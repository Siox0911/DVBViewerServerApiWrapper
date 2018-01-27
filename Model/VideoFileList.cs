using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Eine Liste mit Videos im Media Server
    /// </summary>
    [XmlRoot(ElementName = "table")]
    public class VideoFileList
    {
        /// <summary>
        /// Die Videos als Liste
        /// </summary>
        [XmlElement(ElementName = "row", Type = typeof(VideoFileItem))]
        public List<VideoFileItem> Items { get; set; }

        internal static VideoFileList CreateMediaFileList(XDocument xDocument)
        {
            return Helper.Deserializer.Deserialize<VideoFileList>(xDocument, new Type[] { typeof(VideoFileItem) });
        }

        /// <summary>
        /// Eine Liste mit Videos, welche bis zur letzten Aktualisierung der Datenbank vorhanden waren.
        /// </summary>
        /// <returns></returns>
        internal static VideoFileList GetMediaFileList()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                const string query = "Select objects.Object_ID, object_details.Details_ID, " +
                                        "paths.idPath, objects.Filename, paths.Path, " +
                                        "object_details.channel, object_details.Added, " +
                                        "object_details.Description, object_details.Duration, " +
                                        "object_details.Info, object_details.Lastplayed, " +
                                        "object_details.Time, object_details.Titel, " +
                                        "object_details.filesize " +
                                        "from paths inner join objects " +
                                        "on paths.idPath = objects.PathID " +
                                        "inner join object_details " +
                                        "on objects.Detail_ID = object_details.Details_ID " +
                                        "where Type=3";

                var xmldata = dvbApi.GetDataAsync("sql",
                    new List<Helper.UriParameter> {
                    Helper.UriParam.Query(query)
                    }).Result;

                if (xmldata != null)
                {
                    return CreateMediaFileList(xmldata);
                }
            }
            return null;
        }
    }
}
