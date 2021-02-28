using System;
using System.Timers;
using System.Windows.Input;
using Task1.Commands;
using Task1.Models;
using Task1.ViewModels.Base;

namespace Task1.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private Calculations _Calculations;

        private string _Title = "Geekbrains. Домашнее задание №5. Многопоточное программирование.";
        /// <summary> Заголовок </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private long _SetValue = 20L;

        /// <summary> Заданное значение </summary>
        public long SetValue
        {
            get => _SetValue;
            set => Set(ref _SetValue, value);
        }

        private long _FactorialResultValue;

        /// <summary> Вычисленное значение факториала </summary>
        public long FactorialResultValue
        {
            get => _FactorialResultValue;
            set => Set(ref _FactorialResultValue, value);
        }

        private long _SummResultValue;

        /// <summary> Вычисленное значение суммы чисел </summary>
        public long SummResultValue
        {
            get => _SummResultValue;
            set => Set(ref _SummResultValue, value);
        }

        private readonly Timer _timer;
        public DateTime CurrentTime => DateTime.Now;

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }

        public MainWindowViewModel()
        {
            _Calculations = new Calculations();
            _timer = new Timer
            {
                Interval = 100,
                AutoReset = true,
                Enabled = true,
            };
            _timer.Elapsed += OnTimerElapsed;
        }

        private ICommand _GoCalculateResultsInDoubleThreadsCommand;

        /// <summary> Выполнить расчет результатов в два потока </summary>
        public ICommand GoCalculateResultsInDoubleThreadsCommand => _GoCalculateResultsInDoubleThreadsCommand ??=
            new LambdaCommand(OnGoCalculateResultsInDoubleThreadsCommandExecuted, CanGoCalculateResultsInDoubleThreadsCommandExecute);

        private bool CanGoCalculateResultsInDoubleThreadsCommandExecute(object p) => true;

        private void OnGoCalculateResultsInDoubleThreadsCommandExecuted(object p)
        {
            var (fact, sum) = _Calculations.CalculateFactorialAndSumParallel(SetValue);
            FactorialResultValue = fact;
            SummResultValue = sum;
        }

    }
}
