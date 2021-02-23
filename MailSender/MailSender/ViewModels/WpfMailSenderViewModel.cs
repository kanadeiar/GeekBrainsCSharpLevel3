
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MailSender.Infrastructure.Commands;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.ViewModels.Base;
using MailSender.Windows;

namespace MailSender.ViewModels
{
    /// <summary> Вьюмодель главного окна приложения </summary>
    class WpfMailSenderViewModel : ViewModel
    {
        private readonly IMailService _MailService;
        private readonly IServerStorage _serverStorage;
        private readonly ISenderStorage _senderStorage;
        private readonly IRecipientStorage _recipientStorage;
        private readonly IMessageStorage _messageStorage;
        public WpfMailSenderViewModel(IMailService mailService, IServerStorage serverStorage, ISenderStorage senderStorage, 
            IRecipientStorage recipientStorage, IMessageStorage messageStorage)
        {
            _MailService = mailService;
            _serverStorage = serverStorage;
            _senderStorage = senderStorage;
            _recipientStorage = recipientStorage;
            _messageStorage = messageStorage;
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
        private string _status = "Готов!";
        /// <summary> Статус работы приложения </summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
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

        private readonly Timer _timer;
        public DateTime CurrentTime => DateTime.Now;
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }

        #endregion        

        #endregion

        #region Команды

        #region Команды работы с данными

        private ICommand _loadDataFileCommand;
        /// <summary> Команда загрузки данных </summary>
        public ICommand LoadDataFileCommand => _loadDataFileCommand ??= new LambdaCommand(OnLoadDataFileCommandExecute);
        private void OnLoadDataFileCommandExecute(object p)
        {
            _serverStorage.Load();
            Servers = new ObservableCollection<Server>(_serverStorage.Items);
            Senders = new ObservableCollection<Sender>(_senderStorage.Items);
            Recipients = new ObservableCollection<Recipient>(_recipientStorage.Items);
            Messages = new ObservableCollection<Message>(_messageStorage.Items);
            OnPropertyChanged(nameof(FilteredRecipients));
        }
        private ICommand _saveDataFileCommand;
        /// <summary> Команда сохранения данных </summary>
        public ICommand SaveDataFileCommand => _saveDataFileCommand ??= new LambdaCommand(OnSaveDataFIleCommandExecute);
        private void OnSaveDataFIleCommandExecute(object p)
        {
            _serverStorage.SaveChanges();
        }

        #endregion

        #region Команды изменения данных

        private ICommand _createServerCommand;
        /// <summary> Коданда создания нового сервера </summary>
        public ICommand CreateServerCommand => _createServerCommand ??= new LambdaCommand(OnCreateServerCommandExecute);
        private void OnCreateServerCommandExecute(object p)
        {
            if (!ServerEditWindow.Create(
                out var name,
                out var address,
                out var port,
                out var ssl,
                out var description,
                out var login,
                out var password))
                return;
            int newid = default;
            if (Servers.Count != 0)
                newid = Servers.Max(s => s.Id) + 1;
            else
                newid = 1;
            var server = new Server
            {
                Id = newid,
                Name = name,
                Address = address,
                Port = port,
                UseSsl = ssl,
                Description = description,
                Login = login,
                Password = password,
            };
            _serverStorage.Items.Add(server);
            Servers.Add(server);
        }
        private ICommand _editServerCommand;
        /// <summary> Команда редактирования выбранного сервера </summary>
        public ICommand EditServerCommand => _editServerCommand ??=
            new LambdaCommand(OnEditServerCommandExecute, CanEditServerCommandExecute);
        private bool CanEditServerCommandExecute(object p) => p is Server;
        private void OnEditServerCommandExecute(object p)
        {
            if (!(p is Server server))
                return;
            var name = server.Name;
            var address = server.Address;
            var port = server.Port;
            var ssl = server.UseSsl;
            var description = server.Description;
            var login = server.Login;
            var password = server.Password;
            if (!ServerEditWindow.ShowDialog("Редактирование почтового сервера",
                ref name,
                ref address,
                ref port,
                ref ssl,
                ref description,
                ref login,
                ref password))
                return;
            server.Name = name;
            server.Address = address;
            server.Port = port;
            server.UseSsl = ssl;
            server.Description = description;
            server.Login = login;
            server.Password = password;
        }
        private ICommand _deleteServerCommand;
        /// <summary> Команда удаления сервера </summary>
        public ICommand DeleteServerCommand => _deleteServerCommand ??=
            new LambdaCommand(OnDeleteServerCommandExecute, CanDeleteServerCommandExecute);
        private bool CanDeleteServerCommandExecute(object p) => p is Server;
        private void OnDeleteServerCommandExecute(object p)
        {
            if (!(p is Server server))
                return;
            _serverStorage.Items.Remove(server);
            Servers.Remove(server);
        }
        private ICommand _createSenderCommand;
        /// <summary> Команда добавления новго отправителя </summary>
        public ICommand CreateSenderCommand => _createSenderCommand ??=
            new LambdaCommand(OnCreateSenderCommandExecute);
        private void OnCreateSenderCommandExecute(object p)
        {
            if (!SenderEditWindow.Create(
                out var name, 
                out var address,
                out var description))
                return;
            int newid = default;
            if (Senders.Count != 0)
                newid = Senders.Max(s => s.Id) + 1;
            else
                newid = 1;
            var sender = new Sender
            {
                Id = newid,
                Name = name,
                Address = address,
                Description = description,
            };
            _senderStorage.Items.Add(sender);
            Senders.Add(sender);
        }
        private ICommand _editSenderCommand;
        /// <summary> Команда редактирования отправителя </summary>
        public ICommand EditSenderCommand => _editSenderCommand ??=
            new LambdaCommand(OnEditSenderCommandExecute, CanEditSenderCommandExecute);
        private bool CanEditSenderCommandExecute(object p) => p is Sender;
        private void OnEditSenderCommandExecute(object p)
        {
            if (!(p is Sender sender))
                return;
            var name = sender.Name;
            var address = sender.Address;
            var description = sender.Description;
            if (!SenderEditWindow.ShowDialog("Редактирование отправителя",
                ref name,
                ref address,
                ref description))
                return;
            sender.Name = name;
            sender.Address = address;
            sender.Description = description;
        }
        private ICommand _deleteSenderCommand;
        /// <summary> Команда удвления отправителя </summary>
        public ICommand DeleteSenderCommand => _deleteSenderCommand ??=
            new LambdaCommand(OnDeleteSenderCommandExecute, CanDeleteSenderCommandExecute);
        private bool CanDeleteSenderCommandExecute(object p) => p is Sender;
        private void OnDeleteSenderCommandExecute(object p)
        {
            if (!(p is Sender sender))
                return;
            _senderStorage.Items.Remove(sender);
            Senders.Remove(sender);
        }

        #endregion

        private ICommand _sendMessageCommand;
        /// <summary> Команда отправки сообщения </summary>
        public ICommand SendMessageCommand => _sendMessageCommand ??=
            new LambdaCommand(OnSendMessageCommandExecute, CanSendMessageCommandExecute);
        private bool CanSendMessageCommandExecute(object p)
        {
            return SelectedServer != null && SelectedSender != null && SelectedRecipient != null &&
                   SelectedMessage != null;
        }
        private void OnSendMessageCommandExecute(object p)
        {
            if (string.IsNullOrEmpty(SelectedMessage.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(SelectedMessage.Subject))
            {
                return;
            }
            var server = SelectedServer;
            var client = _MailService.GetSender(server.Address, server.Port, server.UseSsl, server.Login,
                server.Password);
            var sender = SelectedSender;
            var recipient = SelectedRecipient;
            var message = SelectedMessage;
            var recipients = ((IList) p).Cast<Recipient>().Select(l => l.Address).ToArray();
            if (recipients.Length <= 1)
                client.Send(sender.Address, recipient.Address, message.Subject, message.Text);
            else
                client.Send(sender.Address, recipients, message.Subject, message.Text);
        }

        #region Вспомогательные команды

        private ICommand _showDialogCommand;
        /// <summary> Команда показа простого диалогового окна приложения </summary>
        public ICommand ShowDialogCommand => _showDialogCommand ??= new LambdaCommand(OnShowDialogCommandExecute);
        private void OnShowDialogCommandExecute(object p)
        {
            var message = p as string ?? "Привет, Мир!";
            MessageBox.Show(message, "Сообщение приложения", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private ICommand _toTabItemCommand;
        /// <summary> Команда перехода на закладку главного окна приложения </summary>
        public ICommand ToTabItemCommand => _toTabItemCommand ??= new LambdaCommand(OnToTabItemCommandExecute, CanToTabItemCommandExecute);

        private bool CanToTabItemCommandExecute(object p) => p is TabItem;
        private void OnToTabItemCommandExecute(object p)
        {
            if (!(p is TabItem tabItem)) return;
            tabItem.IsSelected = true;
        }

        #endregion

        #endregion
    }
}
