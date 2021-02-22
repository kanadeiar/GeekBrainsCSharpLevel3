using System.Collections.Generic;
using System.Diagnostics;
using MailSender.lib.Interfaces;

namespace MailSender.lib.Services
{
    /// <summary> Сервис бурной имитации отправки почты для целей отладки </summary>
    public class DebugMailService : IMailService
    {
        public IMailSender GetSender(string address, int port, bool useSsl, string login, string password) 
            => new DebugMailSender(address, port, useSsl, login, password);
    }
    public class DebugMailSender : IMailSender
    {
        private readonly string _address;
        private readonly int _port;
        private readonly bool _useSsl;
        private readonly string _login;
        private readonly string _password;
        public DebugMailSender(string address, int port, bool useSsl, string login, string password)
        {
            _address = address;
            _port = port;
            _useSsl = useSsl;
            _login = login;
            _password = password;
        }
        public void Send(string from, string to, string subject, string message)
        {
            Debug.WriteLine($"Почтовый сервер {_address}:{_port} ssl:{(_useSsl?"да":"нет")} (Логин:{_login} Пароль:{TextEncoder.Decode(_password, 9)})");
            Debug.WriteLine($"Отправка письма от {from} к {to} с заголовком: {subject} и тестом: {message}");
        }
        public void Send(string @from, IEnumerable<string> tos, string subject, string message)
        {
            Debug.WriteLine($"Почтовый сервер {_address}:{_port} ssl:{(_useSsl?"да":"нет")} (Логин:{_login} Пароль:{TextEncoder.Decode(_password, 9)})");
            foreach (var to in tos)
            {
                Debug.WriteLine($"Отправка группового письма от {from} к {to} с заголовком: {subject} и тестом: {message}");
            }
        }
    }
}
