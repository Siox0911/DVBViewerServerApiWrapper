using System;
using System.Collections.Generic;
using System.IO;
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
        //TODO: Ich bin noch nicht zufrieden damit. Es wäre schön wenn die Verschlüsselung des Serverpassworts schon automatisch auf den Benutzer zugeschnitten ist.

        /// <summary>
        /// Verschlüsselt ein Password als AES Verschlüsselung. Zurückgegeben wird ein Base64 encodiertes und AES verschlüsseltes Passwort. Sowie der geheime Schlüssel <paramref name="IV"/> und der öffentliche Schlüssel <paramref name="Key"/>.
        /// </summary>
        /// <param name="plainText">Das Passwort als Klartext</param>
        /// <param name="Key">Der öffentliche Schlüssel für die Verschlüsselung und Entschlüsselung</param>
        /// <param name="IV">Der geheime Schlüssel</param>
        /// <returns></returns>
        public static string GenerateEnrcyptedPassword(string plainText, out byte[] Key, out byte[] IV)
        {
            byte[] encrypted;
            using (AesCryptoServiceProvider myAes = new AesCryptoServiceProvider())
            {
                // Encrypt the string to an array of bytes.
                encrypted = EncryptStringToBytesAes(plainText, myAes.Key, myAes.IV);

                Key = myAes.Key;
                IV = myAes.IV;
            }

            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Entschlüsselt ein Password <paramref name="cipherText"/> mit den AES Daten. Der öffentliche Schlüssel ist <paramref name="Key"/> und der geheime ist <paramref name="IV"/>
        /// </summary>
        /// <param name="cipherText">Das verschlüsselte Passwort</param>
        /// <param name="Key">Der öffentliche Schlüssel für die Verschlüsselung und Entschlüsselung</param>
        /// <param name="IV">Der geheime Schlüssel</param>
        /// <returns></returns>
        public static string GenerateUnEnrcyptedPassword(string cipherText, byte[] Key, byte[] IV)
        {
            return DecryptStringFromBytesAes(Convert.FromBase64String(cipherText), Key, IV);
        }

        private static byte[] EncryptStringToBytesAes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException(nameof(Key));
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));
            byte[] encrypted;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        /// <summary>
        /// Entschlüsselt den cipherText.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="Key">Der öffentliche Schlüssel für die Verschlüsselung und Entschlüsselung</param>
        /// <param name="IV">Der geheime Schlüssel</param>
        /// <returns></returns>
        private static string DecryptStringFromBytesAes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException(nameof(Key));
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
