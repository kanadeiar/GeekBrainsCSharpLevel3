using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MailSender.Controls
{
    /// <summary>
    /// Логика взаимодействия для ItemsToolBarUserControl.xaml
    /// </summary>
    public partial class ItemsToolBarUserControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(ItemsToolBarUserControl), new PropertyMetadata("Название"));
        [Description("Заголовок")]
        public string Title
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly DependencyProperty HintAddElementProperty = DependencyProperty.Register(
            nameof(HintAddElement), typeof(string), typeof(ItemsToolBarUserControl), new PropertyMetadata("Добавление элемента"));
        [Description("Подсказка добавления элемента")]
        public string HintAddElement
        {
            get => (string) GetValue(HintAddElementProperty);
            set => SetValue(HintAddElementProperty, value);
        }
        public static readonly DependencyProperty HintEditElementProperty = DependencyProperty.Register(
            nameof(HintEditElement), typeof(string), typeof(ItemsToolBarUserControl), new PropertyMetadata("Редактирование элемента"));
        [Description("Подсказка редактирования элемента")]
        public string HintEditElement
        {
            get => (string) GetValue(HintEditElementProperty);
            set => SetValue(HintEditElementProperty, value);
        }
        public static readonly DependencyProperty HintDeleteElementProperty = DependencyProperty.Register(
            nameof(HintDeleteElement), typeof(string), typeof(ItemsToolBarUserControl), new PropertyMetadata("Удаление элемента"));
        [Description("Подсказка удаления элемента")]
        public string HintDeleteElement
        {
            get => (string) GetValue(HintDeleteElementProperty);
            set => SetValue(HintDeleteElementProperty, value);
        }
        public static readonly DependencyProperty HintSelectedBoxProperty = DependencyProperty.Register(
            nameof(HintSelectedBox), typeof(string), typeof(ItemsToolBarUserControl), new PropertyMetadata("Выберите элемент"));
        [Description("Подсказка для выбора")]
        public string HintSelectedBox
        {
            get => (string) GetValue(HintSelectedBoxProperty);
            set => SetValue(HintSelectedBoxProperty, value);
        }
        public static readonly DependencyProperty AddNewItemCommandProperty = DependencyProperty.Register(
            nameof(AddNewItemCommand), typeof(ICommand), typeof(ItemsToolBarUserControl), new PropertyMetadata(default(ICommand)));
        [Description("Добавление нового элемента")]
        public ICommand AddNewItemCommand
        {
            get => (ICommand) GetValue(AddNewItemCommandProperty);
            set => SetValue(AddNewItemCommandProperty, value);
        }
        public static readonly DependencyProperty EditItemCommandProperty = DependencyProperty.Register(
            nameof(EditItemCommand), typeof(ICommand), typeof(ItemsToolBarUserControl), new PropertyMetadata(default(ICommand)));
        [Description("Редактирование элемента")]
        public ICommand EditItemCommand
        {
            get => (ICommand) GetValue(EditItemCommandProperty);
            set => SetValue(EditItemCommandProperty, value);
        }
        public static readonly DependencyProperty DeleteItemCommandProperty = DependencyProperty.Register(
            nameof(DeleteItemCommand), typeof(ICommand), typeof(ItemsToolBarUserControl), new PropertyMetadata(default(ICommand)));
        [Description("Удаление элемента")]
        public ICommand DeleteItemCommand
        {
            get => (ICommand) GetValue(DeleteItemCommandProperty);
            set => SetValue(DeleteItemCommandProperty, value);
        }
        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register(
            nameof(ItemSource), typeof(IEnumerable), typeof(ItemsToolBarUserControl), new PropertyMetadata(default(IEnumerable)));
        [Description("Элементы панели управления")]
        public IEnumerable ItemSource
        {
            get => (IEnumerable) GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            nameof(SelectedItem), typeof(object), typeof(ItemsToolBarUserControl), new PropertyMetadata(default(object)));
        [Description("Выбранный элемент")]
        public object SelectedItem
        {
            get => (object) GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            nameof(ItemTemplate), typeof(DataTemplate), typeof(ItemsToolBarUserControl), new PropertyMetadata(default(DataTemplate)));
        [Description("Шаблон элемента выпадающего списка")]
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate) GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public ItemsToolBarUserControl() => InitializeComponent();
    }
}
