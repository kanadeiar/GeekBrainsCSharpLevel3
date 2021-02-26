using MailSender.lib.Interfaces;
using MailSender.lib.Models;

namespace MailSender.lib.Services
{
    /// <summary> Сервис отправки сообщений по заданию </summary>
    public class SchedulerMailService : ISchedulerMailService
    {
        /// <summary> Получение экземпляра-отправщика сообщения по времени </summary>
        /// <param name="MailSender">Сервис-отправитель</param>
        /// <returns>Отправщик по времени</returns>
        public ISchedulerMailSender GetScheduler(IMailSender MailSender) => new SchedulerMailSender(MailSender);
    }
}
