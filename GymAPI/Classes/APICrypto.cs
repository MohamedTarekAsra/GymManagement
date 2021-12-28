using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GymAPI.Classes
{
    public class APICrypto
    {
        public APICrypto()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// encrypt data
        /// </summary>
        /// <param name="plaintext">text to encrypt</param>
        /// <returns>encrypted data</returns>
        public static string encrypt(string plaintext)
        {
            byte[] plaintextbytes = ASCIIEncoding.UTF8.GetBytes(plaintext);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.Key = ASCIIEncoding.ASCII.GetBytes("/45-s;gplrt5=#@%$54sd@$%vxsl,dg5");
            aes.IV = ASCIIEncoding.ASCII.GetBytes(".k/sdfg;544436%&");
            byte[] cipherText = aes.CreateEncryptor(aes.Key, aes.IV).TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
            return Convert.ToBase64String(cipherText);
        }
        /// <summary>
        /// decrypt data
        /// </summary>
        /// <param name="cipherText">text to decrypt</param>
        /// <returns>decrypted data</returns>
        public static string decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherTextbytes = Convert.FromBase64String(cipherText);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.Key = ASCIIEncoding.ASCII.GetBytes("/45-s;gplrt5=#@%$54sd@$%vxsl,dg5");
            aes.IV = ASCIIEncoding.ASCII.GetBytes(".k/sdfg;544436%&");
            byte[] plainTextbytes = aes.CreateDecryptor(aes.Key, aes.IV).TransformFinalBlock(cipherTextbytes, 0, cipherTextbytes.Length);
            return ASCIIEncoding.UTF8.GetString(plainTextbytes);
        }
        //
        public bool CheckToken(string token)
        {
            try
            {
                string str = EncryptStringAES("2018*10*12*10*12*PerHour");
                //YYYY*10*MM*10*dd*PerHour
                //2018*10*12*10*07*PerHour
                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }
                //
                string decryptedString = DecryptStringAES(token);
                if (decryptedString.Length == 24)
                {
                    string[] parts = decryptedString.Split('*');
                    if (parts.Length == 6)
                    {
                        if (int.Parse(parts[0]) == DateTime.UtcNow.Year && int.Parse(parts[2]) == DateTime.UtcNow.Month && int.Parse(parts[4]) == DateTime.UtcNow.Day && parts[5] == "PerHour")
                        {
                            return true;
                        }
                    }
                }
                //DateTime currentDate = DateTime.Parse(decryptedString);
                //if (currentDate.Date.Year == DateTime.UtcNow.Date.Year && currentDate.Date.Month == DateTime.UtcNow.Date.Month
                //    && currentDate.Date.Day == DateTime.UtcNow.Date.Day )  // && currentDate.Date.Hour == DateTime.UtcNow.Date.Hour
                //{
                //    //TimeSpan ts = DateTime.UtcNow - currentDate;
                //    //if (ts.TotalMinutes > 5)
                //    //{
                //    //    return false;
                //    //}
                //}
                //else
                //{
                //    return false;
                //}
                //
                return false;
                //
            }
            catch (Exception)
            {
                return false;
            }
        }
        //
        public  string DecryptStringAES(string cipherText)
        {

            var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
            var iv = Encoding.UTF8.GetBytes("8080808080808080");

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }
        //
        private  string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                try
                {
                    // Create the streams used for decryption.
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }
        //
        private  byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            byte[] encrypted;
            // Create a RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
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
        //
        public  string EncryptStringAES(string plainText)
        {

            var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
            var iv = Encoding.UTF8.GetBytes("8080808080808080");

            //var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = EncryptStringToBytes(plainText, keybytes, iv);
            return string.Format(Convert.ToBase64String(decriptedFromJavascript));
        }
    }
}