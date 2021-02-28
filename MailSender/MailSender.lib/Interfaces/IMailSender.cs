using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace MailSender.lib.Interfaces
{
    /// <summary> Рассыльщик почты </summary>
    public interface IMailSender
    {
        void Send(string from, string to, string subject, string text);
        void Send(string from, IEnumerable<string> tos, string subject, string text);
        void SendParallel(string from, IEnumerable<string> tos, string subject, string text);
    }
}
