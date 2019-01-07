using DVBViewerServerApiWrapper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Nutzt die mediafiles.html für die Videos. Wird aktuell nur genutzt um ein Vorschaubild für die Eigenschaft Image in VideoFileItem zu beziehen
    /// </summary>
    [XmlRoot(ElementName = "videofiles")]
    public class MediaFileList
    {
        /// <summary>
        /// Eine Liste mit den Videos
        /// </summary>
        [XmlElement(ElementName = "file", Type = typeof(MediaFileItem))]
        public List<MediaFileItem> Items { get; set; }

        internal MediaFileList() { }

        /// <summary>
        /// Erzeugt die MediaFileItem Liste
        /// </summary>
        /// <param name="uriParameters"></param>
        /// <returns></returns>
        internal static Task<MediaFileList> GetMediaFileListAsync(List<Helper.UriParameter> uriParameters)
        {
            return Helper.Lists.GetListAsync<MediaFileList>("mediafiles", uriParameters, new Type[] {
                typeof(MediaFileItem)
            });
        }

        /// <summary>
        /// Gibt alle Videos des Verzeichnisses zurück
        /// </summary>
        /// <param name="dirId"></param>
        /// <returns></returns>
        internal static Task<MediaFileList> GetMediaFileListAsync(int dirId)
        {
            return GetMediaFileListAsync(new List<UriParameter>
            {
                new UriParameter("dirid", dirId.ToString()),
                new UriParameter("thumbs", "1")
            });
        }

        /// <summary>
        /// Gibt die Liste mit einem Video zurück, welches im Verzeichnis existieren muss.
        /// </summary>
        /// <param name="dirId"></param>
        /// <param name="videoFileName"></param>
        /// <returns></returns>
        internal static async Task<MediaFileList> GetMediaFileListAsync(int dirId, string videoFileName)
        {
            var t = await GetMediaFileListAsync(dirId).ConfigureAwait(false);
            t.Items = (from f in t.Items where f.Name.Equals(videoFileName) select f).ToList();
            return t;
        }
    }
}
