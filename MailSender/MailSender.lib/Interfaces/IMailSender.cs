using System.Collections.Generic;

namespace MailSender.lib.Interfaces
{
    /// <summary> Рассыльщик почты </summary>
    public interface IMailSender
    {
        void Send(string from, string to, string subject, string message);
        void Send(string from, IEnumerable<string> tos, string subject, string message);
    }
}
