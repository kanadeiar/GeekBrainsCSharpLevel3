using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using MailSender.lib.Models;

namespace MailSender.lib.Interfaces
{
    /// <summary> Отправщик почты по времени </summary>
    public interface ISchedulerMailSender
    {
        void Start(Scheduler Scheduler);
        void Stop();
        event EventHandler MissionCompleted;
    }
}
