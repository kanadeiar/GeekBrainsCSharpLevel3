using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MailSender.Windows
{
    /// <summary> Логика взаимодействия для SernderEditWindow.xaml </summary>
    public partial class SenderEditWindow : Window
    {
        public SenderEditWindow()
        {
            InitializeComponent();
        }
        /// <summary> Диалог в режиме редактирования отправителя </summary>
        /// <param name="Title">Заголовок</param>
        /// <param name="Name">Имя</param>
        /// <param name="Address">Адрес</param>
        /// <param name="Description">Описание</param>
        /// <returns>Успешное выполнение</returns>
        public static bool ShowDialog(string Title, ref string Name, ref string Address, ref string Description)
        {
            var window = new SenderEditWindow
            {
                Title = Title,
                TextBoxName = { Text = Name},
                TextBoxAddress = {Text = Address},
                TextBoxDescription = {Text = Description},
                Owner = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(win => win.IsActive),
            };
            if (window.ShowDialog() != true) return false;
            Name = window.TextBoxName.Text;
            Address = window.TextBoxAddress.Text;
            Description = window.TextBoxDescription.Text;
            return true;
        }
        /// <summary> Диалог в режиме создания отправителя </summary>
        /// <param name="name">имя</param>
        /// <param name="address">адрес</param>
        /// <param name="description">описание</param>
        /// <returns>Успешное выполнение</returns>
        public static bool Create(out string name, out string address, out string description)
        {
            name = default;
            address = default;
            description = default;
            return ShowDialog("Создать нового отправителя", ref name, ref address, ref description);
        }
        /// <summary> Закрытие окна </summary>
        private void WindowButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button) e.OriginalSource).IsCancel;
            Close();
        }
    }
}
