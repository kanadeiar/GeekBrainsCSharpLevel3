using System;
using System.Diagnostics;
using System.Windows;
using MailSender.lib.Models;
using MailSender.lib.Services;

namespace MailSender
{
    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class WpfMailSender : Window
    {
        public WpfMailSender()
        {
            InitializeComponent();
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


    }
}
