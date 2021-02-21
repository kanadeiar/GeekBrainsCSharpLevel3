using System.Linq;

namespace MailSender.lib.Services
{
    /// <summary> Шифрование методом Цезаря </summary>
    public class TextEncoder
    {
        /// <summary> Шифрование </summary>
        /// <param name="str">Исходная</param>
        /// <param name="key">ключ</param>
        /// <returns>Зашифрованная</returns>
        public string Encode(string str, int key = 1)
        {
            return new string(str.Select(c => (char) (c + key)).ToArray());
        }
        /// <summary> Дешифрование </summary>
        /// <param name="str">Зашифрованная</param>
        /// <param name="key">ключ</param>
        /// <returns>Исходная</returns>
        public string Decode(string str, int key = 1)
        {
            return new string(str.Select(c => (char)(c - key)).ToArray());
        }
    }
}
