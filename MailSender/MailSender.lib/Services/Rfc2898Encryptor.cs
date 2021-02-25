using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MailSender.lib.Interfaces;

namespace MailSender.lib.Services
{
    /// <summary> Шифрование и расшифровывание </summary>
    public class Rfc2898Encryptor : IEncryptService
    {
        private static readonly byte[] Salt =
        {
            0x19, 0x88, 0x1a, 0x99,
            0x55, 0x11, 0x77, 0x9f,
            0x88, 0x89, 0x1a, 0x99,
            0x01, 0x20, 0x12, 0x21,
        };
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        /// <summary> Шифрование текста </summary>
        /// <param name="text">оригинальный текст</param>
        /// <param name="password">ключ</param>
        /// <returns>зашифрованный текст</returns>
        public string Encrypt(string text, string password)
        {
            var encoding = Encoding ?? Encoding.UTF8;
            var bytes = encoding.GetBytes(text);
            var encryptedBytes = Encrypt(bytes, password);
            return Convert.ToBase64String(encryptedBytes);
        }
        /// <summary> Расшифрование текста </summary>
        /// <param name="text">зашифрованный текст</param>
        /// <param name="password">ключ</param>
        /// <returns>оригинальный текст</returns>
        public string Decrypt(string text, string password)
        {
            var encryptedBytes = Convert.FromBase64String(text);
            var bytes = Decrypt(encryptedBytes, password);
            var encoding = Encoding ?? Encoding.UTF8;
            return encoding.GetString(bytes);
        }

        public byte[] Encrypt(byte[] data, string password)
        {
            var algorithm = getAlgorithmCryptoTransform(password);
            using (var stream = new MemoryStream())
            using (var cryproStream = new CryptoStream(stream, algorithm, CryptoStreamMode.Write))
            {
                cryproStream.Write(data, 0, data.Length);
                cryproStream.FlushFinalBlock();
                return stream.ToArray();
            }
        }
        public byte[] Decrypt(byte[] data, string password)
        {
            var algorithm = getInverseAlgorithmCryptoTransform(password);
            using (var stream = new MemoryStream())
            using (var cryproStream = new CryptoStream(stream, algorithm, CryptoStreamMode.Write))
            {
                cryproStream.Write(data, 0, data.Length);
                cryproStream.FlushFinalBlock();
                return stream.ToArray();
            }
        }

        private static ICryptoTransform getAlgorithmCryptoTransform(string password)
        {
            var algorithm = Algorithm(password);
            return algorithm.CreateEncryptor();
        }
        private static ICryptoTransform getInverseAlgorithmCryptoTransform(string password)
        {
            var algorithm = Algorithm(password);
            return algorithm.CreateDecryptor();
        }
        private static Rijndael Algorithm(string password)
        {
            var pdb = new Rfc2898DeriveBytes(password, Salt);
            var algorithm = Rijndael.Create();
            algorithm.Key = pdb.GetBytes(32);
            algorithm.IV = pdb.GetBytes(16);
            return algorithm;
        }
    }
    /// <summary> Функции-расширения для строк </summary>
    public static class ExtendedRfc2898Encoder
    {
        /// <summary> Функция-расширение кодирование текста методом Цезаря </summary>
        public static string Rfc2898Encode(this string Source, string password = "Geekbrains")
        {
            var encryptor = new Rfc2898Encryptor();
            return encryptor.Encrypt(Source, password);
        }
        /// <summary> Функция-расширение декодирование зашифрованного текста методом Цезаря </summary>
        public static string Rfc2898Decode(this string Source, string password = "Geekbrains")
        {
            var encryptor = new Rfc2898Encryptor();
            return encryptor.Decrypt(Source, password);
        }
    }
}
