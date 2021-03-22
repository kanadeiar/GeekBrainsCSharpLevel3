using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Timers;
using System.Windows;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.lib.Service;
using MailSender.lib.Services;
using MailSender.ViewModels.Base;

namespace MailSender.ViewModels
{
    /// <summary> Вьюмодель главного окна приложения </summary>
    partial class MainWindowViewModel : ViewModel
    {
        private readonly IMailService _MailService;
        private readonly ISchedulerMailService _SchedulerService;

        private readonly IRepository<Server> _Servers;
        private readonly IRepository<Sender> _Senders;
        private readonly IRepository<Recipient> _Recipients;
        private readonly IRepository<Message> _Messages;
        private readonly IRepository<Scheduler> _Schedulers; //задания из хранилища, неактивные


        
        #region Свойства

        #region Вспомогательные свойства

        private string _title = "Geekbrains. Домашнее задание №7. Базы данных.";

        /// <summary> Заголовок главного окна </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string _description =
            "Geekbrains. Домашнее задание №7. Базы данных.";

        /// <summary> Описание приложения </summary>
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private string _status = "Готов!";

        /// <summary> Статус работы приложения </summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion

        /// <summary> Почтовые сервера с которых отправляется почта </summary>
        public ObservableCollection<Server> Servers { get; } = new();
        private void ServersOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                _Servers.AddRange(e.NewItems?.Cast<Server>());
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                _Servers.RemoveRange(e.OldItems?.Cast<Server>());
            }
        }
        /// <summary> Отправители в почтовом сообщении </summary>
        public ObservableCollection<Sender> Senders { get; } = new();
        private void SendersOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                _Senders.AddRange(e.NewItems?.Cast<Sender>());
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                _Senders.RemoveRange(e.OldItems?.Cast<Sender>());
            }
        }
        /// <summary> Получатели почтового сообщения </summary>
        public ObservableCollection<Recipient> Recipients { get; } = new();
        private void RecipientsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                _Recipients.AddRange(e.NewItems?.Cast<Recipient>());
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                _Recipients.RemoveRange(e.OldItems?.Cast<Recipient>());
            }
        }
        /// <summary> Сообщения электронной почты </summary>
        public ObservableCollection<Message> Messages { get; } = new();
        private void MessagesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                _Messages.AddRange(e.NewItems?.Cast<Message>());
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                _Messages.RemoveRange(e.OldItems?.Cast<Message>());
            }
        }
        /// <summary> Задания на рассылку почты активные </summary>
        public ObservableCollection<SchedulerMailSender> SchedulerMailSenders { get; } = new();


        private Server _selectedServer;

        /// <summary> Выбранный сервер </summary>
        public Server SelectedServer
        {
            get => _selectedServer;
            set => Set(ref _selectedServer, value);
        }

        private Sender _selectedSender;

        /// <summary> Выбранный отправитель </summary>
        public Sender SelectedSender
        {
            get => _selectedSender;
            set => Set(ref _selectedSender, value);
        }

        private Recipient _selectedRecipient;

        /// <summary> Выбранный получатель </summary>
        public Recipient SelectedRecipient
        {
            get => _selectedRecipient;
            set => Set(ref _selectedRecipient, value);
        }

        private IList _SelectedRecipients;

        /// <summary> Выбранные получатели </summary>
        public IList SelectedRecipients
        {
            get => _SelectedRecipients;
            set => Set(ref _SelectedRecipients, value);
        }

        private Message _selectedMessage;

        /// <summary> Выбранное сообщение </summary>
        public Message SelectedMessage
        {
            get => _selectedMessage;
            set => Set(ref _selectedMessage, value);
        }

        private DateTime _selectedDate = DateTime.Now;

        /// <summary> Выбранная дата отправки сообщения </summary>
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => Set(ref _selectedDate, value);
        }

        #region Фильтр получателей сообщений

        private string _recipientsFilter;

        /// <summary> Фильтр получателей сообщений </summary>
        public string RecipientsFilter
        {
            get => _recipientsFilter;
            set
            {
                Set(ref _recipientsFilter, value);
                OnPropertyChanged(nameof(FilteredRecipients));
            }
        }

        /// <summary> Отфильтрованные получатели сообщений </summary>
        public ICollection<Recipient> FilteredRecipients
        {
            get
            {
                if (string.IsNullOrEmpty(RecipientsFilter))
                    return Recipients;
                return Recipients.Where(r => r.Name.ToLower().Contains(RecipientsFilter.ToLower()))
                    .ToList();
            }
        }

        #endregion

        #region Текущее время

        private bool _timerEnabled = true;

        public bool TimerEnabled
        {
            get => _timerEnabled;
            set
            {
                if (!Set(ref _timerEnabled, value)) return;
                _timer.Enabled = value;
            }
        }

        private readonly Timer _timer;
        public DateTime CurrentTime => DateTime.Now;

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }

        #endregion

        #endregion

        public MainWindowViewModel(IMailService mailService, IRepository<Server> Servers, IRepository<Sender> Senders,
            IRepository<Recipient> Recipients, IRepository<Message> Messages, IRepository<Scheduler> Schedulers, 
            ISchedulerMailService SchedulerService, IAddinsService AddinsService)
        {
            _Servers = Servers;
            _Senders = Senders;
            _Recipients = Recipients;
            _Messages = Messages;
            _MailService = mailService;
            _Schedulers = Schedulers;
            _SchedulerService = SchedulerService;
            _timer = new Timer
            {
                Interval = 100,
                AutoReset = true,
                Enabled = true,
            };
            this.Servers.CollectionChanged += ServersOnCollectionChanged;
            this.Senders.CollectionChanged += SendersOnCollectionChanged;
            this.Recipients.CollectionChanged += RecipientsOnCollectionChanged;
            this.Messages.CollectionChanged += MessagesOnCollectionChanged;
            _timer.Elapsed += OnTimerElapsed;

            GoAddins(AddinsService);
        }

        #region Еще вспомогательные методы

        /// <summary> Выполнение загрузки из дополнения приложения </summary>
        /// <param name="AddinsService">сервис дополнений</param>
        private void GoAddins(IAddinsService AddinsService)
        {
            var texts = AddinsService.getTextsAddin();
            if (texts is null) return;
            Title = texts.Title;
            Description = texts.Description;
            Status = texts.Status;
        }

        #endregion
    }
}