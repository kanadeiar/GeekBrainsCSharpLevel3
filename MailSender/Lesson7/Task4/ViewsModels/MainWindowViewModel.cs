using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Task4.Commands;
using Task4.Interfaces;
using Task4.Model;
using Task4.ViewsModels.Base;

namespace Task4.ViewsModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Person> _Persons;

        #region Свойства

        public ObservableCollection<Person> Persons { get; } = new ();


        private string _Title = "Geekbrains. Домашнее задание №7. Базы данных.";
        /// <summary> Название приложения </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        public MainWindowViewModel(IRepository<Person> Persons)
        {
            _Persons = Persons;
        }

        #region Команды

        private ICommand _CloseAppCommand;
        /// <summary> Закрыть приложение </summary>
        public ICommand CloseAppCommand => _CloseAppCommand ??=
            new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
        private bool CanCloseAppCommandExecute(object p) => true;
        private void OnCloseAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

    }
}
