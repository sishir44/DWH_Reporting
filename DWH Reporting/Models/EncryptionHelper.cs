using System;
using System.Security.Cryptography;
using System.Text;

namespace DWH_Reporting.Models
{
    public static class EncryptionHelper
    {
        private static readonly string Key = "YourEncryptionKey123"; // Replace with a secure key

        public static string Encrypt(string plainText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key.PadRight(32).Substring(0, 32));
                aes.IV = new byte[16];
                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    var plainBytes = Encoding.UTF8.GetBytes(plainText);
                    var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        public static string Decrypt(string encryptedText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key.PadRight(32).Substring(0, 32));
                aes.IV = new byte[16];
                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    var encryptedBytes = Convert.FromBase64String(encryptedText);
                    var plainBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    return Encoding.UTF8.GetString(plainBytes);
                }
            }
        }
    }
}
