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
}
