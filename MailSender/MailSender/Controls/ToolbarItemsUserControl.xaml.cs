using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MailSender.lib.Models;

namespace MailSender.Controls
{
    /// <summary>
    /// Логика взаимодействия для ToolbarItemsUserControl.xaml
    /// </summary>
    public partial class ToolbarItemsUserControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty = 
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(ToolbarItemsUserControl),
                new PropertyMetadata("Название", PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine(e.NewValue);
        }
        public string Title
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public ToolbarItemsUserControl()
        {
            InitializeComponent();
        }
    }
}
