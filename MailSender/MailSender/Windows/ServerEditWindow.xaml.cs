using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MailSender.lib.Models;

namespace MailSender.Windows
{
    /// <summary>
    /// Логика взаимодействия для ServerEditWindow.xaml
    /// </summary>
    public partial class ServerEditWindow : Window
    {
        public Server Server { get; set; }
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
                Server = new Server
                {
                    Name = Name,
                    Address = Address,
                    Port = Port,
                    UseSsl = UseSsl,
                    Description = Description,
                    Login = Login,
                    Password = Password,
                },
                Owner = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(win => win.IsActive),
            };
            window.DockPanelServerEdit.DataContext = window.Server;
            if (window.ShowDialog() != true) return false;
            Name = window.Server.Name;
            Address = window.Server.Address;
            Port = window.Server.Port;
            UseSsl = window.Server.UseSsl;
            Description = window.Server.Description;
            Login = window.Server.Login;
            Password = window.Server.Password;
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
    }
}
