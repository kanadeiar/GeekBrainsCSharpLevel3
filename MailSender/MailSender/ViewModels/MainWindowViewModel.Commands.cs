using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MailSender.Infrastructure.Commands;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.lib.Models.Base;
using MailSender.lib.Services;
using MailSender.ViewModels.Base;
using MailSender.Windows;

namespace MailSender.ViewModels
{
    partial class MainWindowViewModel : ViewModel
    {
        #region Команды

        #region Команды работы с данными

        private ICommand _loadDataFileCommand;

        /// <summary> Команда загрузки данных </summary>
        public ICommand LoadDataFileCommand => _loadDataFileCommand ??= new LambdaCommand(OnLoadDataFileCommandExecute);

        private void OnLoadDataFileCommandExecute(object p)
        {
            LoadData();
            OnPropertyChanged(nameof(FilteredRecipients));
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
            var server = new Server
            {
                Name = name,
                Address = address,
                Port = port,
                UseSsl = ssl,
                Description = description,
                Login = login,
                Password = password.Encrypt(),
            };
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
            var password = server.Password.Decrypt();
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
            server.Password = password.Encrypt();
            _Servers.Update(server);
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
            var sender = new Sender
            {
                Name = name,
                Address = address,
                Description = description,
            };
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
            _Senders.Update(sender);
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
            Senders.Remove(sender);
        }

        #endregion

        #region Команды работы с сервисом отправки сообщений

        private CancellationTokenSource _AsyncSendCancellation; //отмена отправки сообщений`

        private double _ProgressSendMessages;

        /// <summary> Прогресс отправки сообщений </summary>
        public double ProgressSendMessages
        {
            get => _ProgressSendMessages;
            set => Set(ref _ProgressSendMessages, value);
        }

        #region Команда отправки сообщений

        private bool _sendMessageCommandAsyncReady = true;

        /// <summary> Занятость команды выполнением отправки сообщений </summary>
        public bool SendMessageCommandAsyncReady
        {
            get => _sendMessageCommandAsyncReady;
            set => Set(ref _sendMessageCommandAsyncReady, value);
        }

        private ICommand _sendMessageCommand;
        /// <summary> Команда отправки сообщения </summary>
        public ICommand SendMessageCommand => _sendMessageCommand ??=
            new LambdaCommand(OnSendMessageCommandExecute, CanSendMessageCommandExecute);

        private bool CanSendMessageCommandExecute(object p)
        {
            return SelectedServer != null && SelectedSender != null && SelectedRecipient != null &&
                   SelectedMessage != null;
        }

        private async void OnSendMessageCommandExecute(object p)
        {
            if (string.IsNullOrEmpty(SelectedMessage.Text))
            {
                return;
            }

            if (string.IsNullOrEmpty(SelectedMessage.Subject))
            {
                return;
            }
            
            Status = "Идет отправка сообщений";
            SendMessageCommandAsyncReady = SendFastMessageCommandAsyncReady = false;
            var server = SelectedServer;
            var client = _MailService.GetSender(server.Address, server.Port, server.UseSsl, server.Login,
                server.Password);
            var sender = SelectedSender;
            var recipient = SelectedRecipient;
            var message = SelectedMessage;
            var recipients = ((IList) p).Cast<Recipient>().Select(l => l.Address).ToArray();

            _AsyncSendCancellation = new CancellationTokenSource();
            var progress = new Progress<double>(
                value =>
                {
                    ProgressSendMessages = value;
                });

            try
            {
                if (recipients.Length <= 1)
                    await client.SendAsync(sender.Address, recipient.Address, message.Subject, message.Text,
                            _AsyncSendCancellation.Token)
                        .ConfigureAwait(true);
                else
                    await client.SendAsync(sender.Address, recipients, message.Subject, message.Text,
                        _AsyncSendCancellation.Token, progress).ConfigureAwait(true);
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Произошла отмена операции отправки сообщений", "Отмена операции", MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
            }
            ProgressSendMessages = 1;
            Status = "Готов, письма успешно отправлены!";
            SendMessageCommandAsyncReady = SendFastMessageCommandAsyncReady = true;
        }

        #endregion

        #region Команда очень быстрой отправки сообщений

        private bool _SendFastMessageCommandAsyncReady = true;

        /// <summary> Занятость команды выполнением очень быстрой отправки сообщений </summary>
        public bool SendFastMessageCommandAsyncReady
        {
            get => _SendFastMessageCommandAsyncReady;
            set => Set(ref _SendFastMessageCommandAsyncReady, value);
        }

        private ICommand _SendFastMessageCommand;
        /// <summary> Команда быстрой отправки большого числа сообщений </summary>
        public ICommand SendFastMessageCommand => _SendFastMessageCommand ??=
            new LambdaCommand(OnSendFastMessageCommandExecuted, CanSendFastMessageCommandExecute);

        private bool CanSendFastMessageCommandExecute(object p)
        {
            return SelectedServer != null && SelectedSender != null && SelectedRecipient != null &&
                   SelectedMessage != null;
        }

        private async void OnSendFastMessageCommandExecuted(object p)
        {
            if (string.IsNullOrEmpty(SelectedMessage.Text))
            {
                return;
            }

            if (string.IsNullOrEmpty(SelectedMessage.Subject))
            {
                return;
            }

            Status = "Идет очень-очень быстрая отправка сообщений";
            SendMessageCommandAsyncReady = SendFastMessageCommandAsyncReady = false;
            var server = SelectedServer;
            var client = _MailService.GetSender(server.Address, server.Port, server.UseSsl, server.Login,
                server.Password);
            var sender = SelectedSender;
            var recipient = SelectedRecipient;
            var message = SelectedMessage;
            var recipients = ((IList)p).Cast<Recipient>().Select(l => l.Address).ToArray();
            try
            {
                await client.SendFastAsync(sender.Address, recipients, message.Subject, message.Text)
                    .ConfigureAwait(true);
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Произошла отмена операции отправки сообщений", "Отмена операции", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            Status = "Готов, письма очень быстро отправлены!";
            SendMessageCommandAsyncReady = SendFastMessageCommandAsyncReady = true;
        }

        #endregion

        #region Команда отмены отправки сообщений

        private ICommand _CancelSendMessagesCommand;

        /// <summary> Команда отмены отправки сообщений </summary>
        public ICommand CancelSendMessagesCommand => _CancelSendMessagesCommand ??=
            new LambdaCommand(OnCancelSendMessagesCommandExecuted, CanCancelSendMessagesCommandExecute);

        private bool CanCancelSendMessagesCommandExecute(object p) => true;

        private void OnCancelSendMessagesCommandExecuted(object p)
        {
            _AsyncSendCancellation?.Cancel();
        }

        #endregion


        #region Команда планирования отправки сообщений

        private ICommand _schedulerSendMailMessageCommand;
        /// <summary> Команда добавления задания на отправку сообщения </summary>
        public ICommand SchedulerSendMailMessageCommand => _schedulerSendMailMessageCommand ??=
            new LambdaCommand(OnSchedulerSendMessageCommandExecute, CanSchedulerSendMessageCommandExecute);
        private bool CanSchedulerSendMessageCommandExecute(object p)
        {
            return SelectedServer != null && SelectedSender != null && 
                   SelectedRecipient != null && SelectedMessage != null;
        }

        private void OnSchedulerSendMessageCommandExecute(object p)
        {
            var server = SelectedServer;
            var client = _MailService.GetSender(server.Address, server.Port, server.UseSsl, server.Login,
                server.Password);
            var schedulerMailSender = _SchedulerService.GetScheduler(client);
            var recipients = ((IList) p).Cast<Recipient>().ToArray();
            var date = SelectedDate;
            var scheduler = new Scheduler
            {
                DateTimeSend = date,
                Server = SelectedServer,
                Sender = SelectedSender,
                Recipients = recipients,
                Message = SelectedMessage,
            };
            schedulerMailSender.Start(scheduler);

            var context = SynchronizationContext.Current;
            schedulerMailSender.MissionCompleted += (_, _) =>
            {
                context?.Send(x => SchedulerMailSenders.Remove((SchedulerMailSender) schedulerMailSender), null);
            };
            SchedulerMailSenders.Add((SchedulerMailSender)schedulerMailSender);
        }

        private ICommand _SchedulerDeleteMessageCommand;

        /// <summary> Команда удаления задания на отправку сообщения </summary>
        public ICommand SchedulerDeleteMessageCommand => _SchedulerDeleteMessageCommand ??=
            new LambdaCommand(OnSchedulerDeleteMessageCommandExecuted, CanSchedulerDeleteMessageCommandExecute);

        private bool CanSchedulerDeleteMessageCommandExecute(object p) => true;

        private void OnSchedulerDeleteMessageCommandExecuted(object p)
        {
            if (!(p is SchedulerMailSender scheduler))
                return;
            scheduler.Stop();
            SchedulerMailSenders.Remove(scheduler);
        }

        #endregion

        #endregion

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
        public ICommand ToTabItemCommand => _toTabItemCommand ??=
            new LambdaCommand(OnToTabItemCommandExecute, CanToTabItemCommandExecute);

        private bool CanToTabItemCommandExecute(object p) => p is TabItem;

        private void OnToTabItemCommandExecute(object p)
        {
            if (!(p is TabItem tabItem)) return;
            tabItem.IsSelected = true;
        }

        #endregion

        #endregion

        #region Вспомогательные методы

 
        /// <summary> Загрузка данных </summary>
        private void LoadData()
        {
            UnmountLinks();
            Load(Servers, _Servers);
            Load(Senders, _Senders);
            Load(Recipients, _Recipients);
            Load(Messages, _Messages);
            MountLinks();
        }
        /// <summary> Монтаж связей между коллекциями и базами данных </summary>
        private void MountLinks()
        {
            Servers.CollectionChanged += ServersOnCollectionChanged;
            Senders.CollectionChanged += SendersOnCollectionChanged;
            Recipients.CollectionChanged += RecipientsOnCollectionChanged;
            Messages.CollectionChanged += MessagesOnCollectionChanged;
        }
        /// <summary> Демонтаж связей между коллекциями и базами данных </summary>
        private void UnmountLinks()
        {
            Servers.CollectionChanged -= ServersOnCollectionChanged;
            Senders.CollectionChanged -= SendersOnCollectionChanged;
            Recipients.CollectionChanged -= RecipientsOnCollectionChanged;
            Messages.CollectionChanged -= MessagesOnCollectionChanged;
        }
        /// <summary> Заргузка информации из базы данных в коллекцию </summary>
        private static void Load<T>(ObservableCollection<T> collection, IRepository<T> repository) where T : Entity
        {
            collection.Clear();
            foreach (var item in repository.GetAll())
                collection.Add(item);
        }


        #endregion
    }

}
