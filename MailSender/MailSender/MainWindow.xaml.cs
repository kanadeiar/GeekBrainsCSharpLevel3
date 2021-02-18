using System;
using System.Windows;

namespace MailSender
{
    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonSend_OnClick(object sender, RoutedEventArgs e)
        {
            EmailSender mySender = new EmailSender(TextBoxLogin.Text, PasswordBoxPassword.SecurePassword);
            var server = TextBoxServer.Text;
            var port = Int32.Parse(TextBoxPort.Text);
            var from = TextBoxFrom.Text;
            var to = TextBoxTo.Text;
            var subject = TextBoxSubject.Text;
            var message = TextBoxMessage.Text;
            var enableSsl = CheckBoxSSL.IsChecked == true;
            if (mySender.SendMail(from, to, subject, message, server, port, enableSsl) == 0)
            {
                var window = new WindowSuccessSend {Owner = this};
                window.ShowDialog();
            }
            else
            {
                var window = new WindowErrorSend { Owner = this };
                window.ShowDialog();
            }
        }
        private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
        {
            TextBoxMessage.Text = String.Empty;
        }
        private void ItemExit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
