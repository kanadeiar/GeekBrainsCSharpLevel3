using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Task4.Commands;
using Task4.Interfaces;
using Task4.Model;
using Task4.Model.Base;
using Task4.ViewsModels.Base;
using Task4.Windows;

namespace Task4.ViewsModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Person> _Persons;

        #region Свойства

        public ObservableCollection<Person> Persons { get; } = new ();

        private Person _SelectedPerson;

        /// <summary> Выбранный сотрудник </summary>
        public Person SelectedPerson
        {
            get => _SelectedPerson;
            set => Set(ref _SelectedPerson, value);
        }


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
            LoadData();
        }

        #region Команды

        #region Команды работы с данными

        private ICommand _AddNewPersonCommand;
        /// <summary> Команда добавления нового сотрудника </summary>
        public ICommand AddNewPersonCommand => _AddNewPersonCommand ??=
            new LambdaCommand(OnAddNewPersonCommandExecuted);
        private void OnAddNewPersonCommandExecuted(object p)
        {
            if (!PersonEditWindow.Create(
                out var person))
                return;
            _Persons.Add(person);
            Persons.Add(person);
            SelectedPerson = person;
        }
        private ICommand _EditSelectedPersonCommand;
        /// <summary> Команда редактирования выбранного сотрудника </summary>
        public ICommand EditSelectedPersonCommand => _EditSelectedPersonCommand ??=
            new LambdaCommand(OnEditSelectedPersonCommandExecuted, CanEditSelectedPersonCommandExecute);
        private bool CanEditSelectedPersonCommandExecute(object p) => p is Person;
        private void OnEditSelectedPersonCommandExecuted(object p)
        {
            if (!(p is Person person)) 
                return;
            if (!PersonEditWindow.ShowDialog("Редактирование выбранного сотрудника",
                ref person))
                return;
            _Persons.Update(person);
        }
        private ICommand _DeleteSelectedPersonCommand;
        /// <summary> Команда удаления выбранного сотрудника </summary>
        public ICommand DeleteSelectedPersonCommand => _DeleteSelectedPersonCommand ??=
            new LambdaCommand(OnDeleteSelectedPersonCommandExecuted, CanDeleteSelectedPersonCommandExecute);
        private bool CanDeleteSelectedPersonCommandExecute(object p) => p is Person;
        private void OnDeleteSelectedPersonCommandExecuted(object p)
        {
            if (p is Person person)
            {
                _Persons.Delete(person.Id);
                Persons.Remove(person);
            }
        }

        #endregion


        private ICommand _CloseAppCommand;
        /// <summary> Закрыть приложение </summary>
        public ICommand CloseAppCommand => _CloseAppCommand ??=
            new LambdaCommand(OnCloseAppCommandExecuted);
        private void OnCloseAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private ICommand _AddDataFromFileCommand;
        /// <summary> Добавить данные из SCV файла </summary>
        public ICommand AddDataFromFileCommand => _AddDataFromFileCommand ??=
            new LambdaCommand(OnAddDataFromFileCommandExecuted);
        private void OnAddDataFromFileCommandExecuted(object p)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Файл данных (*.csv)|*.csv|Все файлы (*.*)|*.*", 
                FileName = "test.csv",
                InitialDirectory = Environment.CurrentDirectory,
            };
            if (dialog.ShowDialog() == false) return;
            var fileName = dialog.FileName;
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName)) return;

            var strs = File.ReadAllLines(fileName);
            var data = strs.Select(s => s.Split(','))
                .Select(i => new Person
            {
                SNP = i[0],
                Address = i[1],
                Phone = i[2],
            }).ToArray();
            foreach (var pers in data)
            {
                _Persons.Add(pers);
                Persons.Add(pers);
            }
        }

        #endregion

        #region Вспомогательные методы

        private void LoadData()
        {
            Load(Persons, _Persons);
        }

        private static void Load<T>(ObservableCollection<T> collection, IRepository<T> repository) where T : Entity
        {
            collection.Clear();
            foreach (var item in repository.GetAll())
                collection.Add(item);
        }

        #endregion
    }
}
