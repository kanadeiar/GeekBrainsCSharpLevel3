using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MailSender.Windows
{
    /// <summary>
    /// Логика взаимодействия для ServerEditWindow.xaml
    /// </summary>
    public partial class ServerEditWindow : Window
    {
        public ServerEditWindow()
        {
            InitializeComponent();
        }
        /// <summary> Диалог в режиме редактирования сервера</summary>
        /// <param name="Title">Заголовок</param>
        /// <param name="Name">Имя сервера</param>
        /// <param name="Address">Адрес сервера</param>
        /// <param name="Port">Порт сервера</param>
        /// <param name="UseSsl">Шифрование</param>
        /// <param name="Description">Описание</param>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <returns>Успешное выполнение</returns>
        public static bool ShowDialog(string Title, ref string Name, ref string Address, ref int Port,
            ref bool UseSsl, ref string Description, ref string Login, ref string Password)
        {
            var window = new ServerEditWindow
            {
                Title = Title,
                TextBoxServerName = {Text = Name},
                TextBoxServerAddress = {Text = Address},
                TextBoxServerPort = {Text = Port.ToString()},
                CheckBoxServerSsl = {IsChecked = UseSsl},
                TextBoxLogin = {Text = Login},
                PasswordBoxPassword = {Password = Password},
                TextBoxDescription = {Text = Description},
                Owner = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(win => win.IsActive),
            };
            if (window.ShowDialog() != true) return false;
            Name = window.TextBoxServerName.Text;
            Address = window.TextBoxServerAddress.Text;
            Port = int.Parse(window.TextBoxServerPort.Text);
            UseSsl = window.CheckBoxServerSsl.IsChecked == true;
            Login = window.TextBoxLogin.Text;
            Password = window.PasswordBoxPassword.Password;
            Description = window.TextBoxDescription.Text;
            return true;
        }
        /// <summary> Диалог в режиме создания сервера </summary>
        /// <param name="Name">Имя</param>
        /// <param name="Address">Адрес</param>
        /// <param name="Port">Порт</param>
        /// <param name="UseSsl">Использование шифрования</param>
        /// <param name="Description">Описание</param>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <returns>Успешное выполнение</returns>
        public static bool Create(out string Name, out string Address, out int Port, out bool UseSsl,
            out string Description, out string Login, out string Password)
        {
            Name = default;
            Address = default;
            Port = 25;
            UseSsl = default;
            Description = default;
            Login = default;
            Password = default;
            return ShowDialog("Создать почтовый сервер", ref Name, ref Address, ref Port, ref UseSsl, ref Description,
                ref Login, ref Password);
        }
        /// <summary> Закрытие окна </summary>
        private void WindowButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button) e.OriginalSource).IsCancel;
            Close();
        }
        /// <summary> Перед вводом текста в окно для ввода цифры </summary>
        private void TextBoxServerPort_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox) || string.IsNullOrEmpty(textBox.Text)) return;
            e.Handled = !int.TryParse(textBox.Text, out _);
        }
    }
}
