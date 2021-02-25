using System.Collections.Generic;
using System.Diagnostics;
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
    public class DebugMailSender : IMailSender
    {
        private readonly IStatistic _statistic;
        private readonly string _address;
        private readonly int _port;
        private readonly bool _useSsl;
        private readonly string _login;
        private readonly string _password;
        public DebugMailSender(string address, int port, bool useSsl, string login, string password, IStatistic statistic)
        {
            _address = address;
            _port = port;
            _useSsl = useSsl;
            _login = login;
            _password = password;
            _statistic = statistic;
        }
        public void Send(string from, string to, string subject, string message)
        {
            Debug.WriteLine($"Почтовый сервер {_address}:{_port} ssl:{(_useSsl?"да":"нет")} (Логин:{_login} Пароль:{_password.Rfc2898Decode()})");
            Debug.WriteLine($"Отправка письма от {from} к {to} с заголовком: {subject} и тестом: {message}");
            _statistic.MailSended();
        }
        public void Send(string @from, IEnumerable<string> tos, string subject, string message)
        {
            Debug.WriteLine($"Почтовый сервер {_address}:{_port} ssl:{(_useSsl?"да":"нет")} (Логин:{_login} Пароль:{_password.Rfc2898Decode()})");
            foreach (var to in tos)
            {
                Debug.WriteLine($"Отправка группового письма от {from} к {to} с заголовком: {subject} и тестом: {message}");
                _statistic.MailSended();
            }
        }
    }
}
