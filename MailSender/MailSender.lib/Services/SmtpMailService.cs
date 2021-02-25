using MailSender.lib.Interfaces;

namespace MailSender.lib.Services
{
    /// <summary> Сервис отправки почты с шифрованием методом цезаря пароля </summary>
    public class SmtpMailService : IMailService
    {
        private readonly IStatistic _statistic;
        public SmtpMailService(IStatistic statistic) => _statistic = statistic;
        public IMailSender GetSender(string address, int port, bool useSsl, string login, string password)
        {
            return new SmtpSender(address, port, useSsl, login, password, _statistic);
        }
    }
}
