using System;
using System.Collections.Generic;

namespace MailSender.lib.Interfaces
{
    /// <summary> Отправщик почты по времени </summary>
    public interface ISchedulerMailSender
    {
        void AddTask(DateTime DateTimeSend, string from, IEnumerable<string> tos, string title, string text);
        void Stop();
        event EventHandler EmailSended;
    }
}
