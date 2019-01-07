using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// <para>Eine Liste mit Aufnahmen welche irgendwann aufgenommen wurden. Diese müssen nicht mehr als Datei existieren. Es existieren auch keine Verweise auf Dateinamen.
    /// Dies wird verwendet um bereits aufgenommene Aufnahmen zu erkennen.</para>
    /// <para>
    /// A list of recordings that were taken sometime. These no longer need to exist as a file. There are no references to filenames.
    /// This is used to recognize already recorded pictures.
    /// </para>
    /// </summary>
    [XmlRoot(ElementName = "row")]
    public class RecordedItem
    {
        /// <summary>
        /// Eine fortlaufende Nummer. Diese hat nichts mit der AufnahmeID gemein.
        /// A consecutive number. This has nothing in common with the recording ID.
        /// </summary>
        [XmlElement(ElementName = "IDRECORD")]
        public int ID { get; set; }

        /// <summary>
        /// Der Kanal auf dem die Aufnahme gestartet wurde.
        /// The channel on which the recording was started
        /// </summary>
        [XmlElement(ElementName = "CHANNEL")]
        public string Channel { get; set; }

        /// <summary>
        /// Das Datum an dem die Aufnahme gestartet wurde. Das Format ist nicht klar.
        /// Zeit in Millisekunden seit 1899
        /// The date on which the recording was started. The format is not clear.
        /// Time in milliseconds since 1899
        /// </summary>
        [XmlElement(ElementName = "DATEADDED")]
        public string DateAdded { get; set; }

        /// <summary>
        /// Die Dauer der Aufnahme. Das Format ist nicht klar.
        /// The duration of the recording. The format is not clear.
        /// </summary>
        [XmlElement(ElementName = "DURATION")]
        public string Duration { get; set; }

        /// <summary>
        /// Der Titel der Aufnahme
        /// The title of the recording
        /// </summary>
        [XmlElement(ElementName = "TITLE")]
        public string Title { get; set; }

        /// <summary>
        /// Der Untertitel der Aufnahme oder die Kurzbeschreibung.
        /// The subtitle of the recording or the short description.
        /// </summary>
        [XmlElement(ElementName = "INFO")]
        public string Info { get; set; }

        internal RecordedItem() { }
    }
}
