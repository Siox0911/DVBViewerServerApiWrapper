﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVBViewerServerApiWrapper.Helper
{
    /// <summary>
    /// Stellt eine Dateigröße bereit
    /// </summary>
    public class FileSize
    {
        /// <summary>
        /// Die Größe in Bytes o. KB o. MB oder GB
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// Die Gewichtung also Byte, KB, MB oder GB
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Die Größe in Bytes
        /// </summary>
        public long ByteSize { get; set; }

        /// <summary>
        /// Berechnet die Dateigröße gibt ein FileSize Objekt zurück.
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static FileSize GetFileSize(long Bytes)
        {
            var t = new FileSize
            {
                ByteSize = Bytes,
                Weight = "Byte"
            };
            var nBytes = (double)Bytes;
            if ((Bytes / 1024) > 1)
            {
                nBytes /= 1024;
                t.Weight = "KB";
                if ((nBytes / 1024) > 1)
                {
                    nBytes /= 1024;
                    t.Weight = "MB";
                    if ((nBytes / 1024) > 1)
                    {
                        nBytes /= 1024;
                        t.Weight = "GB";
                        if ((nBytes / 1024) > 1)
                        {
                            nBytes /= 1024;
                            t.Weight = "TB";
                        }
                    }
                }
            }
            t.Size = Math.Round(nBytes, 2);
            return t;
        }

        public override string ToString()
        {
            return $"{Size} {Weight}";
        }
    }
}
