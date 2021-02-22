using MailSender.ViewModels.Base;

namespace MailSender.ViewModels
{
    /// <summary> Вьюмодель главного окна приложения </summary>
    class WpfMailSenderViewModel : ViewModel
    {
        public WpfMailSenderViewModel()
        {
        }
        private string _title = "Geekbrains. Домашнее задание №3. MVVM.";
        /// <summary> Заголовок главного окна </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        private string _description = "Geekbrains. Домашнее задание №3. Разработка WPF-приложений с использованием шаблона MVVM на примере MVVM Light Toolkit";
        /// <summary> Описание приложения </summary>
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }
    }
}
