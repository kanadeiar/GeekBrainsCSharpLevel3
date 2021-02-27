using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MailSender.Controls
{
    /// <summary>
    /// Логика взаимодействия для RecipientsToolBarUserControl.xaml
    /// </summary>
    public partial class RecipientsToolBarUserControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(RecipientsToolBarUserControl), new PropertyMetadata("Название"));
        [Description("Заголовок")]
        public string Title
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly DependencyProperty HintAddElementRecipientsProperty = DependencyProperty.Register(
            nameof(HintAddElementRecipients), typeof(string), typeof(ItemsToolBarUserControl), new PropertyMetadata("Добавление элемента"));
        [Description("Подсказка добавления элемента")]
        public string HintAddElementRecipients
        {
            get => (string)GetValue(HintAddElementRecipientsProperty);
            set => SetValue(HintAddElementRecipientsProperty, value);
        }
        public static readonly DependencyProperty HintDeleteElementRecipientsProperty = DependencyProperty.Register(
            nameof(HintDeleteElementRecipients), typeof(string), typeof(ItemsToolBarUserControl), new PropertyMetadata("Удаление элемента"));
        [Description("Подсказка удаления элемента")]
        public string HintDeleteElementRecipients
        {
            get => (string)GetValue(HintDeleteElementRecipientsProperty);
            set => SetValue(HintDeleteElementRecipientsProperty, value);
        }
        public static readonly DependencyProperty AddNewItemCommandRecipientsProperty = DependencyProperty.Register(
            nameof(AddNewItemCommandRecipients), typeof(ICommand), typeof(ItemsToolBarUserControl), new PropertyMetadata(default(ICommand)));
        [Description("Добавление нового элемента")]
        public ICommand AddNewItemCommandRecipients
        {
            get => (ICommand) GetValue(AddNewItemCommandRecipientsProperty);
            set => SetValue(AddNewItemCommandRecipientsProperty, value);
        }
        public static readonly DependencyProperty DeleteItemCommandRecipientsProperty = DependencyProperty.Register(
            nameof(DeleteItemCommandRecipients), typeof(ICommand), typeof(ItemsToolBarUserControl), new PropertyMetadata(default(ICommand)));
        [Description("Удаление элемента")]
        public ICommand DeleteItemCommandRecipients
        {
            get => (ICommand) GetValue(DeleteItemCommandRecipientsProperty);
            set => SetValue(DeleteItemCommandRecipientsProperty, value);
        }

        public RecipientsToolBarUserControl() => InitializeComponent();
    }
}
