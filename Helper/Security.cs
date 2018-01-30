using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVBViewerServerApiWrapper.Helper
{
    /// <summary>
    /// Klasse mit Funktion zum Password verschlüsseln und entschlüsseln
    /// </summary>
    public static class Security
    {

        /// <summary>
        /// Verschlüsselt ein Password abhängig vom Benutzeraccount in Windows
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="entropy64"></param>
        /// <returns></returns>
        public static string GenerateEnrcyptedPassword(string plainText, out string entropy64)
        {
            byte[] plainbytes = Encoding.UTF8.GetBytes(plainText);

            var entropy = new byte[20];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }
            byte[] ciphertext = ProtectedData.Protect(plainbytes, entropy, DataProtectionScope.CurrentUser);
            entropy64 = Convert.ToBase64String(entropy);
            return Convert.ToBase64String(ciphertext);
        }

        /// <summary>
        /// Entschlüsselt ein Password abhängig vom Benutzeraccount in Windows
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="entropy"></param>
        /// <returns></returns>
        public static string GenerateUnEnrcyptedPassword(string cipherText, string entropy)
        {
            return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(cipherText), Convert.FromBase64String(entropy), DataProtectionScope.CurrentUser));
        }

    }
}
