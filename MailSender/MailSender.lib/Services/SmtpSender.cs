using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using MailSender.lib.Interfaces;

namespace MailSender.lib.Services
{
    /// <summary> Боевой сервис отправки почты с шифрованием пароля по Цезарю </summary>
    public class SmtpSender : IMailSender
    {
        private readonly IStatistic _statistic;
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
        public SmtpSender(string address, int port, bool useSsl, string login, string password, IStatistic statistic)
        {
            _address = address;
            _port = port;
            _useSsl = useSsl;
            _login = login;
            _password = password;
            _statistic = statistic;
        }
        /// <summary> Отправка реального письма по протоколу smtp </summary>
        /// <param name="From">отправитель</param>
        /// <param name="To">получатель</param>
        /// <param name="Subject">заголовок</param>
        /// <param name="Message">сообщение</param>
        public void Send(string from, string to, string subject, string message)
        {
            var tMessage = new MailMessage(from, to)
            {
                Subject = subject,
                Body = message,
            };
            var client = new SmtpClient(_address, _port)
            {
                EnableSsl = _useSsl,
                Credentials = new NetworkCredential(_login, TextEncoder.Decode(_password,9)),
            };
            try
            {
                client.Send(tMessage);
                _statistic.MailSended();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
        /// <summary> Групповая отправка реальных писем по smtp </summary>
        /// <param name="from">отправитель</param>
        /// <param name="tos">получатели</param>
        /// <param name="subject">заголовок</param>
        /// <param name="message">сообщение</param>
        public void Send(string from, IEnumerable<string> tos, string subject, string message)
        {
            foreach (var to in tos)
            {
                Send(from, to, subject, message);
                _statistic.MailSended();
            }
        }
    }
}
