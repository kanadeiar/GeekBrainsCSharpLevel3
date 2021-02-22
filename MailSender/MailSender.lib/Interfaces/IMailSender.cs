namespace MailSender.lib.Interfaces
{
    /// <summary> Рассыльщик почты </summary>
    public interface IMailSender
    {
        void Send(string from, string to, string subject, string message);
    }
}
