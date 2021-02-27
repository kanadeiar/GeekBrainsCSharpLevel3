using System.Windows.Controls;
using MailSender.ViewModels;

namespace MailSender.Views
{
    /// <summary>
    /// Логика взаимодействия для RecipientsFilterView.xaml
    /// </summary>
    public partial class RecipientsFilterView : UserControl
    {
        private MainWindowViewModel vm;
        public RecipientsFilterView()
        {
            InitializeComponent();
            vm = (MainWindowViewModel)this.DataContext;
        }

        private void DataGridRecipients_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.SelectedRecipients = DataGridRecipients.SelectedItems;
        }
    }
}
