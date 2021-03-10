using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.lib.Interfaces
{
    /// <summary> Рассыльщик почты </summary>
    public interface IMailSender
    {
        /// <summary> Отправка реального письма по протоколу smtp </summary>
        /// <param name="From">отправитель</param>
        /// <param name="To">получатель</param>
        /// <param name="Subject">заголовок</param>
        /// <param name="text">сообщение</param>
        void Send(string from, string to, string subject, string text);
        /// <summary> Групповая параллельная отправка реальных писем по smtp </summary>
        /// <param name="from">отправитель</param>
        /// <param name="tos">получатели</param>
        /// <param name="subject">заголовок</param>
        /// <param name="text">сообщение</param>
        void Send(string from, IEnumerable<string> tos, string subject, string text, IProgress<double> progress = default);
        /// <summary> Асинхронная отправка почты </summary>
        /// <param name="From">отправитель</param>
        /// <param name="To">получатель</param>
        /// <param name="Subject">заголовок</param>
        /// <param name="text">сообщение</param>
        /// <param name="cancel">отмена операции</param>
        /// <exception cref="OperationCanceledException">Отмена операции</exception>
        Task SendAsync(string from, string to, string subject, string text, CancellationToken cancel = default);
        /// <summary> Групповая асинхронная отправка почты </summary>
        /// <param name="from">отправитель</param>
        /// <param name="tos">получатели</param>
        /// <param name="subject">заголовок</param>
        /// <param name="text">сообщение</param>
        /// <param name="progress">прогресс отправки</param>
        /// <param name="cancel">отмена операции</param>
        /// <exception cref="OperationCanceledException">Отмена операции</exception>
        Task SendAsync(string from, IEnumerable<string> tos, string subject, string text, CancellationToken cancel = default, IProgress<double> Progress = default);
        /// <summary> Быстрая асинхронная отправка почты </summary>
        /// <param name="from">отправитель</param>
        /// <param name="tos">получатели</param>
        /// <param name="subject">заголовок</param>
        /// <param name="text">сообщение</param>
        /// <param name="cancel">отмена операции</param>
        /// <exception cref="OperationCanceledException">Отмена операции</exception>
        Task SendFastAsync(string from, IEnumerable<string> tos, string subject, string text, CancellationToken cancel = default);
    }
}
