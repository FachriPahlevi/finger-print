using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint4
{
    internal class DatabaseConnection
    {
        public static String connectionString = 
            @"Server=localhost;
            Database=FingerPrintDB;
            Trusted_Connection=True;";
        private static readonly string EncryptionKey = "AgatosAveva";

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Ubah string menjadi byte
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Hash password
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Ubah byte menjadi string hex
                StringBuilder builder = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);

            using (Aes aes = Aes.Create())
            {
                // Membuat key 32 byte
                var pdb = new Rfc2898DeriveBytes(
                    EncryptionKey,
                    Encoding.UTF8.GetBytes("12345678")
                );

                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(
                        ms,
                        aes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
        public static string Decrypt(string cipherText)
        {
            byte[] cipherBytes =
                Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(
                    EncryptionKey,
                    Encoding.UTF8.GetBytes("12345678")
                );

                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(
                        ms,
                        aes.CreateDecryptor(),
                        CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}
