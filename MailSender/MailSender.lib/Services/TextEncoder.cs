using System.Linq;

namespace MailSender.lib.Services
{
    /// <summary> Шифрование методом Цезаря </summary>
    public class TextEncoder
    {
        /// <summary> Шифрование </summary>
        /// <param name="str">Исходная строка</param>
        /// <param name="key">ключ</param>
        /// <returns>Зашифрованная строка</returns>
        public static string Encode(string str, int key = 1)
        {
            return new string(str.Select(c => (char) (c + key)).ToArray());
        }
        /// <summary> Дешифрование </summary>
        /// <param name="str">Зашифрованная строка</param>
        /// <param name="key">ключ</param>
        /// <returns>Исходная строка</returns>
        public static string Decode(string str, int key = 1)
        {
            return new string(str.Select(c => (char)(c - key)).ToArray());
        }
    }
}
