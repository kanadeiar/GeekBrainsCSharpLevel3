using System;
using System.Timers;
using System.Windows.Input;
using MailSender.ViewModels.Base;
using WpfMailSender.lib.Commands;
using WpfMailSender.lib.Interfaces;

namespace MailSender.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        private readonly IStatistic _statistic;
        public int SendedMailsCount => _statistic.SendedMailsCount;
        public int SendersCount => _statistic.SendersCount;
        public int RecipientsCount => _statistic.RecipientsCount;
        public TimeSpan UpTime => _statistic.UpTime;
        public StatisticViewModel(IStatistic statistic)
        {
            _statistic = statistic;
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
            OnPropertyChanged(nameof(SendedMailsCount));
            OnPropertyChanged(nameof(SendersCount));
            OnPropertyChanged(nameof(RecipientsCount));
        }
    }
}
