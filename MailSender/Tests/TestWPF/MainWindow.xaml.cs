using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            var from = new MailAddress("kanadeiar@gmail.com", "Андрей");
            var to = new MailAddress("kanadeiar@yandex.ru", "Андрей");
            var message = new MailMessage(from, to);
            message.Subject = "Заголовок";
            message.Body = "Текст письма";
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Timeout = 1000;

            client.Credentials = new NetworkCredential
            {
                UserName = TextBoxLogin.Text,
                SecurePassword = PasswordBoxPassword.SecurePassword,
            };

            try
            {
                client.Send(message);
                MessageBox.Show("Письмо успешно отправлено!", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SmtpException)
            {
                MessageBox.Show("Ошибка авторизации!", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Ошибка отправки по таймауту", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отправки письма:\n{ex.Message}", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
