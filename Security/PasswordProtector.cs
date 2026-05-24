using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FingerPrint4
{
    internal static class PasswordProtector
    {
        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);

            using (Aes aes = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(GetEncryptionKey(), GetSalt());

                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);

                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(
                    memoryStream,
                    aes.CreateEncryptor(),
                    CryptoStreamMode.Write))
                {
                    cryptoStream.Write(clearBytes, 0, clearBytes.Length);
                    cryptoStream.Close();

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(GetEncryptionKey(), GetSalt());

                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);

                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(
                    memoryStream,
                    aes.CreateDecryptor(),
                    CryptoStreamMode.Write))
                {
                    cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                    cryptoStream.Close();

                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }

        private static string GetEncryptionKey()
        {
            string encryptionKey = ConfigurationManager.AppSettings["FingerprintEncryptionKey"];

            if (string.IsNullOrWhiteSpace(encryptionKey))
            {
                throw new ConfigurationErrorsException("FingerprintEncryptionKey is not configured.");
            }

            return encryptionKey;
        }

        private static byte[] GetSalt()
        {
            string salt = ConfigurationManager.AppSettings["FingerprintEncryptionSalt"];

            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new ConfigurationErrorsException("FingerprintEncryptionSalt is not configured.");
            }

            return Encoding.UTF8.GetBytes(salt);
        }
    }
}
