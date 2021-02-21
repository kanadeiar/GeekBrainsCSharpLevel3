using System.Net;
using System.Net.Mail;

namespace MailSender.lib.Services
{
    /// <summary> Боевой сервис отправки почты с шифрованием пароля по Цезарю </summary>
    public class SmtpSenderSerivce
    {
        private readonly string _address;
        private readonly int _port;
        private readonly bool _useSsl;
        private readonly string _login;
        private readonly string _password;
        /// <summary> Конструктор боевого сервиса отправки по smtp</summary>
        /// <param name="address">адрес</param>
        /// <param name="port">порт</param>
        /// <param name="useSsl">шифрование</param>
        /// <param name="login">логин</param>
        /// <param name="password">пароль</param>
        public SmtpSenderSerivce(string address, int port, bool useSsl, string login, string password)
        {
            _address = address;
            _port = port;
            _useSsl = useSsl;
            _login = login;
            _password = password;
        }
        /// <summary> Отправка реального письма по протоколу smtp </summary>
        /// <param name="From">отправитель</param>
        /// <param name="To">получатель</param>
        /// <param name="Subject">заголовок</param>
        /// <param name="Message">сообщение</param>
        public void SendMessage(string From, string To, string Subject, string Message)
        {
            var message = new MailMessage(From, To)
            {
                Subject = Subject,
                Body = Message,
            };
            var client = new SmtpClient(_address, _port)
            {
                EnableSsl = _useSsl,
                Credentials = new NetworkCredential(_login, TextEncoder.Decode(_password,9)),
            };
            client.Send(message);
        }
    }
}
