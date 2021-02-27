using System;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;
using MailSender.Infrastructure.Commands;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.ViewModels.Base;

namespace MailSender.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        private readonly IStatistic _statistic;
        private readonly ObservableCollection<Sender> _Senders;
        private readonly ObservableCollection<Recipient> _Recipients; 
        public int SendedMailsCount => _statistic.SendedMailsCount;
        public int SendersCount => _Senders.Count;
        public int RecipientsCount => _Recipients.Count;
        public TimeSpan UpTime => _statistic.UpTime;
        public StatisticViewModel(MainWindowViewModel mainWindowViewModel, IStatistic statistic)
        {
            _statistic = statistic;
            _Senders = mainWindowViewModel.Senders;
            _Recipients = mainWindowViewModel.Recipients;
            _statistic.SendedMailsCountChanged += (_, _) => OnPropertyChanged(nameof(SendedMailsCount));
            _Senders.CollectionChanged += (_, _) => OnPropertyChanged(nameof(SendersCount));
            _Recipients.CollectionChanged += (_, _) => OnPropertyChanged(nameof(RecipientsCount));
            var timer = new Timer(100);
            timer.Elapsed += (_, _) => OnPropertyChanged(nameof(UpTime));
            timer.Start();

        }
        private ICommand _updateStatisticCommand;
        /// <summary> Команда обновления статистики </summary>
        public ICommand UpdateStatisticCommand =>
            _updateStatisticCommand ??= new LambdaCommand(OnUpdateStatisticCommandExecute);
        private void OnUpdateStatisticCommandExecute(object p)
        {
            OnPropertyChanged(nameof(SendersCount));
            OnPropertyChanged(nameof(RecipientsCount));
        }
    }
}
