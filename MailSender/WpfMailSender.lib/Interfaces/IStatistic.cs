using System;

namespace WpfMailSender.lib.Interfaces
{
    public interface IStatistic
    {
        int SendedMailsCount { get; }
        int SendersCount { get; }
        int RecipientsCount { get; }
        TimeSpan UpTime { get; }
        void MailSended();
    }
}
