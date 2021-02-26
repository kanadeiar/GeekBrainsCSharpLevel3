using System;

namespace MailSender.lib.Interfaces
{
    /// <summary> Отправщик почты по времени </summary>
    public interface ISchedulerMailSender
    {
        void AddTaskSend(DateTime DateTimeSend, string from, string to, string title, string text);
        void Stop();
        event EventHandler EmailSended;
    }
}
