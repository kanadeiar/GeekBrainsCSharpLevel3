using System;
using System.Diagnostics;
using MailSender.lib.Interfaces;

namespace MailSender.lib.Services
{
    public class MemoryStatisticService : IStatistic
    {
        private int _sendedMailCount;
        public int SendedMailsCount => _sendedMailCount;
        public event EventHandler SendedMailsCountChanged;

        public void MailSended()
        { 
            _sendedMailCount++;
            SendedMailsCountChanged?.Invoke(this, EventArgs.Empty);
        }
        private readonly Stopwatch _stopeatchTimer = Stopwatch.StartNew();
        public TimeSpan UpTime => _stopeatchTimer.Elapsed;
        public MemoryStatisticService()
        {
        }
    }
}
