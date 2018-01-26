﻿using System;
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
    /// Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
    /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.
    /// </summary>
    [XmlRoot(ElementName = "table")]
    public class RecordedList
    {
        /// <summary>
        /// Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
        /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.
        /// </summary>
        [XmlElement(ElementName = "row", Type = typeof(RecordedItem))]
        public List<RecordedItem> RecordedItems { get; set; }

        internal RecordedList() { }

        internal static RecordedList CreateRecordedList(XDocument xDocument)
        {
            return Deserializer.Deserialize<RecordedList>(xDocument, new Type[] { typeof(RecordedItem) });
        }

        /// <summary>
        /// Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
        /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.
        /// </summary>
        internal static RecordedList GetRecordedList()
        {
            var dvbApi = DVBViewerServerApi.GetCurrentInstance();
            if (dvbApi != null)
            {
                var xmldata = dvbApi.GetDataAsync("sql",
                    new List<UriParameter> {
                    SUriParams.Rec1,
                    SUriParams.Query("Select * from recordedlist")
                    }).Result;

                if (xmldata != null)
                {
                    return CreateRecordedList(xmldata);
                }
            }
            return null;
        }
    }
}
