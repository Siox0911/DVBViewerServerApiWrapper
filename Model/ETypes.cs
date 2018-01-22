using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVBViewerServerApiWrapper.Model
{
    /// <summary>
    /// Statische Klasse mit allen Enums
    /// </summary>
    public static class ETypes
    {
        /// <summary>
        /// Zeigt an welcher Status das EPGUpdate hat
        /// </summary>
        public enum EPGUpdate
        {
            /// <summary>
            /// Inaktiv
            /// </summary>
            inactive,
            /// <summary>
            /// Aktiv, läuft
            /// </summary>
            active,
            /// <summary>
            /// Wartet auf einen freien Tuner
            /// </summary>
            waiting
        }
    }
}
