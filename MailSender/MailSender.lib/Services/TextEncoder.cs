using System.Linq;

namespace MailSender.lib.Services
{
    /// <summary> Шифрование методом Цезаря </summary>
    public class TextEncoder
    {
        public static string Encode(string str, int key = 1)
        {
            return new string(str.Select(c => (char) (c + key)).ToArray());
        }
        public static string Decode(string str, int key = 1)
        {
            return new string(str.Select(c => (char)(c - key)).ToArray());
        }
    }
    /// <summary> Функции-расширения для строк </summary>
    public static class ExtendedTextEncoder
    {
        /// <summary> Функция-расширение кодирование текста методом Цезаря </summary>
        public static string Encode(this string Source, int key = 1) => TextEncoder.Encode(Source, key);
        /// <summary> Функция-расширение декодирование зашифрованного текста методом Цезаря </summary>
        public static string Decode(this string Source, int key = 1) => TextEncoder.Decode(Source, key);
    }
}
