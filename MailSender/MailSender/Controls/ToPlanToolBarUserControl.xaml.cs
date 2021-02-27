using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MailSender.Controls
{
    /// <summary>
    /// Логика взаимодействия для ToPlanToolBarUserControl.xaml
    /// </summary>
    public partial class ToPlanToolBarUserControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(ToPlanToolBarUserControl), new PropertyMetadata("Название"));
        [Description("Заголовок")]
        public string Title
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly DependencyProperty HintButtonToPlanToolBarProperty = DependencyProperty.Register(
            nameof(HintButtonToPlanToolBar), typeof(string), typeof(ToPlanToolBarUserControl), new PropertyMetadata(default(string)));
        [Description("Подсказка для кнопки")]
        public string HintButtonToPlanToolBar
        {
            get => (string) GetValue(HintButtonToPlanToolBarProperty);
            set => SetValue(HintButtonToPlanToolBarProperty, value);
        }
        public static readonly DependencyProperty GoToPlanFromToolBarCommandProperty = DependencyProperty.Register(
            nameof(GoToPlanFromToolBarCommand), typeof(ICommand), typeof(ToPlanToolBarUserControl), new PropertyMetadata(default(ICommand)));
        [Description("Команда перехода к планированию")]
        public ICommand GoToPlanFromToolBarCommand
        {
            get => (ICommand) GetValue(GoToPlanFromToolBarCommandProperty);
            set => SetValue(GoToPlanFromToolBarCommandProperty, value);
        }
        public static readonly DependencyProperty GoToPlanFromToolBarCommandParameterProperty = DependencyProperty.Register(
            nameof(GoToPlanFromToolBarCommandParameter), typeof(object), typeof(ToPlanToolBarUserControl), new PropertyMetadata(default(object)));
        [Description("Параметер команды перехода к планированию")]
        public object GoToPlanFromToolBarCommandParameter
        {
            get => (object) GetValue(GoToPlanFromToolBarCommandParameterProperty);
            set => SetValue(GoToPlanFromToolBarCommandParameterProperty, value);
        }

        public ToPlanToolBarUserControl() => InitializeComponent();
    }
}
