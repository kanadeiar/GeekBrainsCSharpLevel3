using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Task4.Model;

namespace Task4.Windows
{
    /// <summary> Логика взаимодействия для PersonEditWindow.xaml </summary>
    public partial class PersonEditWindow : Window
    {
        public string LocTitle { get; set; } = "Заголовок";

        public static readonly DependencyProperty SNPProperty = DependencyProperty.Register(
            "SNP", typeof(string), typeof(PersonEditWindow), new PropertyMetadata(default(string)));
        public string SNP
        {
            get { return (string) GetValue(SNPProperty); }
            set { SetValue(SNPProperty, value); }
        }
        public static readonly DependencyProperty AddressProperty = DependencyProperty.Register(
            "Address", typeof(string), typeof(PersonEditWindow), new PropertyMetadata(default(string)));
        public string Address
        {
            get { return (string) GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }
        public static readonly DependencyProperty PhoneProperty = DependencyProperty.Register(
            "Phone", typeof(string), typeof(PersonEditWindow), new PropertyMetadata(default(string)));
        public string Phone
        {
            get { return (string) GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }
        //public string SNP { get; set; }
        //public string Address { get; set; }
        //public string Phone { get; set; }
        //public Person Person { get; set; }
        public PersonEditWindow() => InitializeComponent();

        public static bool ShowDialog(string Title, ref Person Person)
        {
            var window = new PersonEditWindow
            {
                LocTitle = Title,
                Owner = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(win => win.IsActive),
            };
            window.SNP = Person.SNP;
            window.Address = Person.Address;
            window.Phone = Person.Phone;
            if (window.ShowDialog() != true) return false;
            Person.SNP = window.SNP;
            Person.Address = window.Address;
            Person.Phone = window.Phone;
            return true;
        }
        public static bool Create(out Person Person)
        {
            Person = new Person();
            return ShowDialog("Создание нового сотрудника", ref Person);
        }
        private void WindowButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }
    }
}
