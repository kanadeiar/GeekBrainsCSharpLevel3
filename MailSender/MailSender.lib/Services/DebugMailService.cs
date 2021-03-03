using MailSender.lib.Interfaces;

namespace MailSender.lib.Services
{
    /// <summary> Сервис бурной имитации отправки почты для целей отладки </summary>
    public class DebugMailService : IMailService
    {
        private readonly IStatistic _statistic;
        public DebugMailService(IStatistic statistic) => _statistic = statistic;

        public IMailSender GetSender(string address, int port, bool useSsl, string login, string password) 
            => new DebugMailSender(address, port, useSsl, login, password, _statistic);
    }
}
