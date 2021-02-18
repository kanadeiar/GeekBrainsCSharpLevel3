using System.Windows;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для WindowErrorSend.xaml
    /// </summary>
    public partial class WindowErrorSend : Window
    {
        public WindowErrorSend()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
