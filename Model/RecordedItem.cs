using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Eine Aufnahme welche irgendwann aufgenommen wurde. Diese muss nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
    /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.
    /// </summary>
    [XmlRoot(ElementName = "row")]
    public class RecordedItem
    {
        /// <summary>
        /// Eine fortlaufende Nummer. Diese hat nichts mit der AufnahmeID gemein.
        /// </summary>
        [XmlElement(ElementName = "IDRECORD")]
        public int ID { get; set; }
        /// <summary>
        /// Der Kanal auf dem die Aufnahme gestartet wurde
        /// </summary>
        [XmlElement(ElementName = "CHANNEL")]
        public string Channel { get; set; }
        /// <summary>
        /// Das Datum an dem die Aufnahme gestartet wurde. Das Format ist nicht klar.
        /// Zeit in Millisekunden seit blub
        /// </summary>
        [XmlElement(ElementName = "DATEADDED")]
        public string DateAdded { get; set; }
        /// <summary>
        /// Die Dauer der Aufnahme. Das Format ist nicht klar.
        /// </summary>
        [XmlElement(ElementName = "DURATION")]
        public string Duration { get; set; }
        /// <summary>
        /// Der Titel der Aufnahme
        /// </summary>
        [XmlElement(ElementName = "TITLE")]
        public string Title { get; set; }
        /// <summary>
        /// Der Untertitel der Aufnahme oder die Kurzbeschreibung.
        /// </summary>
        [XmlElement(ElementName = "INFO")]
        public string Info { get; set; }

        internal RecordedItem() { }
    }
}
