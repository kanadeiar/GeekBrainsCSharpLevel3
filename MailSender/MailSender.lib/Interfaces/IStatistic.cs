using System;

namespace MailSender.lib.Interfaces
{
    public interface IStatistic
    {
        int SendedMailsCount { get; }
        event EventHandler SendedMailsCountChanged;
        TimeSpan UpTime { get; }
        void MailSended();
    }
}
