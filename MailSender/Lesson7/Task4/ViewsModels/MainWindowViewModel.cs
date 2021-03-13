using Task4.ViewsModels.Base;

namespace Task4.ViewsModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Свойства

        private string _Title = "Geekbrains. Домашнее задание №7. Базы данных.";
        /// <summary> Название приложения </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        public MainWindowViewModel()
        {
            
        }



    }
}
