using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Common
{
    public static class Cryptography
    {
        public static string GetMd5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));  
                StringBuilder sb = new StringBuilder();
                foreach (byte b in data)
                {
                    sb.Append(b.ToString("x2")); 
                }
                return sb.ToString();  
            }
        }

       
        public static string AESEncryption(string clearText)
        {
            string EncryptionKey = "Voleergo@2018!1#&*5";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string AESDecryption(string cipherText)
        {
            string EncryptionKey = "Voleergo@2018!1#&*5";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string EncryptSID(string strValue)
        {
            return AESEncryption(strValue);
        }
        public static string DecryptSID(string strValue)
        {
            return AESDecryption(strValue);
        }


        const string Key = "DHJKHJKLMNOMGSTRPROKMEGISX345VLG"; // must be 32 character
        const string IV = "ZPDHJKHJKLMOPVLG"; // must be 16 character
        public static string Decryptstring(string ciphertext)
        {
            var keybytes = Encoding.UTF8.GetBytes(Key);
            var iv = Encoding.UTF8.GetBytes(IV);

            var encrypted = Convert.FromBase64String(ciphertext);
            var decriptedfromjavascript = Decryptstringfrombytes(encrypted, keybytes, iv);
            return decriptedfromjavascript;
        }
        static string Decryptstringfrombytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (Aes? rijAlg = Aes.Create("AesManaged"))
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

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
           


        public static string Encryptstring(string plaintext)
        {
            var keybytes = Encoding.UTF8.GetBytes(Key);
            var iv = Encoding.UTF8.GetBytes(IV);

            var encryofromjavascript = Encryptstringtobytes(plaintext, keybytes, iv);
            return Convert.ToBase64String(encryofromjavascript);
        }
        private static byte[] Encryptstringtobytes(string plaintext, byte[] key, byte[] iv)
        {
            // check arguments.
            if (plaintext == null || plaintext.Length <= 0)
            {
                throw new ArgumentNullException("plaintext");
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
            // create a rijndaelmanaged object
            // with the specified key and iv.
            using (Aes? rijalg = Aes.Create("AesManaged"))
            {
                rijalg.Mode = CipherMode.CBC;
                rijalg.Padding = PaddingMode.PKCS7;
                rijalg.FeedbackSize = 128;

                rijalg.Key = key;
                rijalg.IV = iv;

                // create a decrytor to perform the stream transform.
                var encryptor = rijalg.CreateEncryptor(rijalg.Key, rijalg.IV);

                // create the streams used for encryption.
                using (var msencrypt = new MemoryStream())
                {
                    using (var csencrypt = new CryptoStream(msencrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swencrypt = new StreamWriter(csencrypt))
                        {
                            //write all data to the stream.
                            swencrypt.Write(plaintext);
                        }
                        encrypted = msencrypt.ToArray();
                    }
                }
            }
            // return the encrypted bytes from the memory stream.
            return encrypted;
        }

    }
}
