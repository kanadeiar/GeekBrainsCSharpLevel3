using System;
using System.Timers;
using Task2.ViewModels.Base;

namespace Task2.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Geekbrains. Домашнее задание №5. Многопоточное программирование.";

        /// <summary> Заголовок </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private readonly Timer _timer;
        public DateTime CurrentTime => DateTime.Now;

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }

        public MainWindowViewModel()
        {
            _timer = new Timer
            {
                Interval = 100,
                AutoReset = true,
                Enabled = true,
            };
            _timer.Elapsed += OnTimerElapsed;
        }
    }
}
