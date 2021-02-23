using System;
using System.Diagnostics;
using MailSender.lib.Interfaces;

namespace MailSender.lib.Services
{
    public class MemoryStatisticService : IStatistic
    {
        private readonly ISenderStorage _senderStorage;
        private readonly IRecipientStorage _recipientStorage;
        private int _sendedMailCount;
        public int SendedMailsCount => _sendedMailCount;
        public event EventHandler SendedMailsCountChanged;
        public void MailSended()
        { 
            _sendedMailCount++;
            SendedMailsCountChanged?.Invoke(this, EventArgs.Empty);
        }
        public int SendersCount => (int)(_senderStorage.Items?.Count ?? 0);
        public int RecipientsCount => (int)(_recipientStorage.Items?.Count ?? 0);

        private readonly Stopwatch _stopeatchTimer = Stopwatch.StartNew();
        public TimeSpan UpTime => _stopeatchTimer.Elapsed;
        public MemoryStatisticService(ISenderStorage senderStorage, IRecipientStorage recipientStorage)
        {
            _senderStorage = senderStorage;
            _recipientStorage = recipientStorage;
        }
    }
}
