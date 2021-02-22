namespace MailSender.lib.Interfaces
{
    /// <summary> Сервис рассылки почты </summary>
    public interface IMailService
    {
        IMailSender GetSender(string address, int port, bool useSsl, string login, string password);
    }
}
