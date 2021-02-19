using System.Windows;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для WindowSuccessSend.xaml
    /// </summary>
    public partial class WindowSuccessSend : Window
    {
        public WindowSuccessSend()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
