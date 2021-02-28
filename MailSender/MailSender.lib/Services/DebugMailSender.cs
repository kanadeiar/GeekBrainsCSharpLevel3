using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using MailSender.lib.Interfaces;

namespace MailSender.lib.Services
{
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
        public void Send(string from, string to, string subject, string text)
        {
            Debug.WriteLine($"Почтовый сервер {_address}:{_port} ssl:{(_useSsl?"да":"нет")} (Логин:{_login} Пароль:{_password.Decrypt()})");
            Debug.WriteLine($"Отправка письма от {from} к {to} с заголовком: {subject} и тестом: {text}");
            _statistic.MailSended();
        }
        public void Send(string @from, IEnumerable<string> tos, string subject, string text)
        {
            Debug.WriteLine($"Параллельная отправка:");
            foreach (var to in tos)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                { 
                    Send(from, to, subject, text);
                });
            }
        }
    }
}