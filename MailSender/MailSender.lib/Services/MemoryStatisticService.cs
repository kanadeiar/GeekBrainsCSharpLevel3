using System;
using System.Diagnostics;
using System.Linq;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;

namespace MailSender.lib.Services
{
    public class MemoryStatisticService : IStatistic
    {
        private readonly IRepository<Sender> _Senders;
        private readonly IRepository<Recipient> _Recipients;
        private int _sendedMailCount;
        public int SendedMailsCount => _sendedMailCount;
        public event EventHandler SendedMailsCountChanged;
        public void MailSended()
        { 
            _sendedMailCount++;
            SendedMailsCountChanged?.Invoke(this, EventArgs.Empty);
        }
        public int SendersCount => _Senders.GetAll().Count();
        public int RecipientsCount => _Recipients.GetAll().Count();

        private readonly Stopwatch _stopeatchTimer = Stopwatch.StartNew();
        public TimeSpan UpTime => _stopeatchTimer.Elapsed;
        public MemoryStatisticService(IRepository<Sender> Senders, IRepository<Recipient> Recipients)
        {
            _Senders = Senders;
            _Recipients = Recipients;
        }
    }
}
