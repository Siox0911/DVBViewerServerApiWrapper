using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Klasse welche einen Parameter einer Uri darstellt.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UriParameter
    {
        /// <summary>
        /// Schlüssel, wie sec, id, def, logo, rtsp etc.
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Der Wert des Schlüssels
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Erzeugt einen UriParameter mit Schlüssel und Wert
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public UriParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
