using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MailSender.Data;
using MailSender.lib.Models;
using MailSender.lib.Services;
using MailSender.Windows;

namespace MailSender
{
    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class WpfMailSender : Window
    {
        public WpfMailSender()
        {
            InitializeComponent();
        }


        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void ButtonAddServer_OnClick(object sender, RoutedEventArgs e)
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
                Id = TestData.Servers
                    .DefaultIfEmpty()
                    .Max(s => s.Id) + 1,
                Name = name,
                Address = address,
                Port = port,
                UseSsl = ssl,
                Description = description,
                Login = login,
                Password = password,
            };
            TestData.Servers.Add(server);
            ComboBoxServers.Items.Refresh();
            ComboBoxServers.SelectedItem = server;
        }
        private void ButtonEditServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ComboBoxServers.SelectedItem is Server server))
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
            ComboBoxServers.Items.Refresh();
            ComboBoxServers.SelectedItem = server;
        }
        private void ButtonDeleteServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ComboBoxServers.SelectedItem is Server server))
                return;
            TestData.Servers.Remove(server);
            ComboBoxServers.Items.Refresh();
            ComboBoxServers.SelectedItem = server;
        }
        private void ButtonSendNow_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ComboBoxServers.SelectedItem is Server server)) return;
            if (!(ComboBoxSenders.SelectedItem is Sender mySender)) return;
            if (!(ListBoxMessages.SelectedItem is Message message)) return;
            if (!(DataGridRecipients.SelectedItem is Recipient recipient)) return;

            if (string.IsNullOrEmpty(TextBoxMailMessage.Text))
            {
                TabItemLetter.IsSelected = true;
                MessageBox.Show("Письмо без текста нельзя отправить, пожалуйста заполните тело письма.", 
                    "Недостаточно данных", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }

            var mailSender = new SmtpSenderSerivce(server.Address, server.Port, server.UseSsl, server.Login, server.Password);
            try
            {
                var timer = Stopwatch.StartNew(); 
                mailSender.SendMessage(mySender.Address, recipient.Address, message.Subject, message.Text);
                timer.Stop();
                MessageBox.Show($"Почтовое сообщение успешно отправлено за {timer.Elapsed.TotalSeconds:0.##} секунд", 
                    "Выполнено", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка отправки почты:\n" + ex.Message, "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonToPlan_OnClick(object sender, RoutedEventArgs e)
        {
            TabItemPlan.IsSelected = true;
        }
    }
}
