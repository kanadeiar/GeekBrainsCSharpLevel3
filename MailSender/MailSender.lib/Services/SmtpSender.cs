using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
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
        /// <param name="text">сообщение</param>
        public void Send(string from, string to, string subject, string text)
        {
            var tMessage = new MailMessage(from, to)
            {
                Subject = subject,
                Body = text,
            };
            var client = new SmtpClient(_address, _port)
            {
                EnableSsl = _useSsl,
                Credentials = new NetworkCredential(_login, _password.Decrypt()),
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
        /// <summary> Групповая параллельная отправка реальных писем по smtp </summary>
        /// <param name="from">отправитель</param>
        /// <param name="tos">получатели</param>
        /// <param name="subject">заголовок</param>
        /// <param name="text">сообщение</param>
        public void Send(string from, IEnumerable<string> tos, string subject, string text, IProgress<double> progress = default)
        {
            var count = tos.Count();
            var i = 0;
            foreach (var to in tos)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                { 
                    Send(from, to, subject, text);
                });
                progress?.Report((double)i++ / count);
            }
        }
        /// <summary> Асинхронная отправка почты по протоколу smtp </summary>
        /// <param name="From">отправитель</param>
        /// <param name="To">получатель</param>
        /// <param name="Subject">заголовок</param>
        /// <param name="text">сообщение</param>
        /// <param name="cancel">отмена операции</param>
        /// <exception cref="OperationCanceledException">Отмена операции</exception>
        public async Task SendAsync(string from, string to, string subject, string text, CancellationToken cancel = default)
        {
            cancel.ThrowIfCancellationRequested();
            var tMessage = new MailMessage(from, to)
            {
                Subject = subject,
                Body = text,
            };
            var client = new SmtpClient(_address, _port)
            {
                EnableSsl = _useSsl,
                Credentials = new NetworkCredential(_login, _password.Decrypt()),
            };
            cancel.ThrowIfCancellationRequested();
            try
            {
                await client.SendMailAsync(tMessage, cancel).ConfigureAwait(false);
                _statistic.MailSended();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
        /// <summary> Групповая асинхронная отправка почты по протоколу smtp </summary>
        /// <param name="from">отправитель</param>
        /// <param name="tos">получатели</param>
        /// <param name="subject">заголовок</param>
        /// <param name="text">сообщение</param>
        /// <param name="progress">прогресс отправки</param>
        /// <param name="cancel">отмена операции</param>
        /// <exception cref="OperationCanceledException">Отмена операции</exception>
        public async Task SendAsync(string from, IEnumerable<string> tos, string subject, string text, CancellationToken cancel = default, 
            IProgress<double> progress = default)
        {
            cancel.ThrowIfCancellationRequested();
            var count = tos.Count();
            var i = 0;
            foreach (var to in tos)
            {
                cancel.ThrowIfCancellationRequested();
                await SendAsync(from, to, subject, text, cancel).ConfigureAwait(false);
                progress?.Report((double)i++ / count);
            }
        }
        /// <summary> Быстрая асинхронная отправка почты по протоколу smtp </summary>
        /// <param name="from">отправитель</param>
        /// <param name="tos">получатели</param>
        /// <param name="subject">заголовок</param>
        /// <param name="text">сообщение</param>
        /// <param name="cancel">отмена операции</param>
        /// <exception cref="OperationCanceledException">Отмена операции</exception>
        public async Task SendFastAsync(string from, IEnumerable<string> tos, string subject, string text, CancellationToken cancel = default)
        {
            cancel.ThrowIfCancellationRequested();
            var tasks = tos.Select(to => SendAsync(from, to, subject, text, cancel));
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
