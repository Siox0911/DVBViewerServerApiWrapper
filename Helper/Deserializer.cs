using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Helper
{
    public static class Deserializer
    {
        /// <summary>
        /// Deserialisiert Xml Daten in ein Object T.
        /// </summary>
        /// <typeparam name="T">Der zu erstellende Typ</typeparam>
        /// <param name="xDocument">Xml Document</param>
        /// <param name="others">Ein Array mit zusätzlichen Typen.</param>
        /// <returns></returns>
        public static T Deserialize<T>(XDocument xDocument, Type[] others = null)
        {
            try
            {
                XmlSerializer res = others == null ? new XmlSerializer(typeof(T)) : new XmlSerializer(typeof(T), others);

                return (T)res.Deserialize(xDocument.CreateReader());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
