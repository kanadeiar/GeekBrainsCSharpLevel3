
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MailSender.Data;
using MailSender.Infrastructure.Commands;
using MailSender.lib.Models;
using MailSender.ViewModels.Base;

namespace MailSender.ViewModels
{
    /// <summary> Вьюмодель главного окна приложения </summary>
    class WpfMailSenderViewModel : ViewModel
    {
        private static string __DataFileName = "test.xml";

        public WpfMailSenderViewModel()
        {
            _timer = new Timer
            {
                Interval = 100,
                AutoReset = true,
                Enabled = true,
            };
            _timer.Elapsed += OnTimerElapsed;
        }

        #region Свойства

        private string _title = "Geekbrains. Домашнее задание №3. MVVM.";
        /// <summary> Заголовок главного окна </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        private string _description = "Geekbrains. Домашнее задание №3. Разработка WPF-приложений с использованием шаблона MVVM на примере MVVM Light Toolkit";
        /// <summary> Описание приложения </summary>
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private ObservableCollection<Server> _servers;
        /// <summary> Почтовые сервера с которых отправляется почта </summary>
        public ObservableCollection<Server> Servers
        {
            get => _servers;
            set => Set(ref _servers, value);
        }
        private Server _selectedServer;
        /// <summary> Выбранный сервер </summary>
        public Server SelectedServer
        {
            get => _selectedServer;
            set => Set(ref _selectedServer, value);
        }

        private ObservableCollection<Sender> _senders;
        /// <summary> Отправители в почтовом сообщении </summary>
        public ObservableCollection<Sender> Senders
        {
            get => _senders;
            set => Set(ref _senders, value);
        }
        private Sender _selectedSender;
        /// <summary> Выбранный отправитель </summary>
        public Sender SelectedSender
        {
            get => _selectedSender;
            set => Set(ref _selectedSender, value);
        }

        private ObservableCollection<Recipient> _recipients;
        /// <summary> Получатели почтового сообщения </summary>
        public ObservableCollection<Recipient> Recipients
        {
            get => _recipients;
            set => Set(ref _recipients, value);
        }
        private Recipient _selectedRecipient;
        /// <summary> Выбранный получатель </summary>
        public Recipient SelectedRecipient
        {
            get => _selectedRecipient;
            set => Set(ref _selectedRecipient, value);
        }

        private ObservableCollection<Message> _messages;
        /// <summary> Сообщения электронной почты </summary>
        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set => Set(ref _messages, value);
        }
        private Message _selectedMessage;
        /// <summary> Выбранное сообщение </summary>
        public Message SelectedMessage
        {
            get => _selectedMessage;
            set => Set(ref _selectedMessage, value);
        }


        #region Текущее время

        private readonly Timer _timer;
        public DateTime CurrentTime => DateTime.Now;
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }

        #endregion        

        #endregion

        #region Команды

        #region Команды работы с тестовыми данными

        private ICommand _loadDataCommand;
        /// <summary> Команда загрузки тестовых данных </summary>
        public ICommand LoadDataCommand => _loadDataCommand ??= new LambdaCommand(OnLoadDataCommandExecute);
        private void OnLoadDataCommandExecute(object p)
        {
            var data = TestData.CreateInstance(10,10,10,100);
            Servers = new ObservableCollection<Server>(data.Servers);
            Senders = new ObservableCollection<Sender>(data.Senders);
            Recipients = new ObservableCollection<Recipient>(data.Recipients);
            Messages = new ObservableCollection<Message>(data.Messages);
        }
        private ICommand _loadDataFileCommand;
        /// <summary> Команда загрузки данных из файла </summary>
        public ICommand LoadDataFileCommand => _loadDataFileCommand ??= new LambdaCommand(OnLoadDataFileCommandExecute);
        private void OnLoadDataFileCommandExecute(object p)
        {
            var data = File.Exists(__DataFileName)
                ? TestData.LoadFromXml(__DataFileName)
                : TestData.CreateInstance(10,10,10,100);
            Servers = new ObservableCollection<Server>(data.Servers);
            Senders = new ObservableCollection<Sender>(data.Senders);
            Recipients = new ObservableCollection<Recipient>(data.Recipients);
            Messages = new ObservableCollection<Message>(data.Messages);
        }
        private ICommand _saveDataFileCommand;
        /// <summary> Команда сохранения данных в файл </summary>
        public ICommand SaveDataFileCommand => _saveDataFileCommand ??= new LambdaCommand(OnSaveDataFIleCommandExecute);
        private void OnSaveDataFIleCommandExecute(object p)
        {
            var data = TestData.CreateInstance();
            data.Servers = Servers.ToList();
            data.Senders = Senders.ToList();
            data.Recipients = Recipients.ToList();
            data.Messages = Messages.ToList();
            data.SaveToXml(__DataFileName);
        }

        #endregion


        private ICommand _showDialogCommand;
        /// <summary> Команда показа простого диалогового окна приложения </summary>
        public ICommand ShowDialogCommand => _showDialogCommand ??= new LambdaCommand(OnShowDialogCommandExecute);
        private void OnShowDialogCommandExecute(object p)
        {
            var message = p as string ?? "Привет, Мир!";
            MessageBox.Show(message, "Сообщение приложения", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private ICommand _toTab;
        /// <summary> Команда перехода на закладку главного окна приложения </summary>
        public ICommand ToTab => _toTab ??= new LambdaCommand(OnToTab);
        private void OnToTab(object p)
        {
            if (!(p is TabItem tabItem)) return;
            tabItem.IsSelected = true;
        }

        #endregion
    }
}
