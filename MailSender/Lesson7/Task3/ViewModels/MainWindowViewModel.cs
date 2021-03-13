using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Task3.Commands;
using Task3.Data;
using Task3.Interfaces;
using Task3.Models;
using Task3.Models.Base;
using Task3.ViewModels.Base;

namespace Task3.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<MovieShow> _MovieShows;
        private readonly IRepository<Order> _Orders;


        #region Свойства

        /// <summary> Киносеансы </summary>
        public ObservableCollection<MovieShow> MovieShows { get; } = new();
        /// <summary> Заказы </summary>
        public ObservableCollection<Order> Orders { get; } = new();

        private string _Title = "Geekbrains. Домашнее задание №7. Базы данных.";
        /// <summary> Название приложения </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private MovieShow _selectedEditMovieShow;

        /// <summary> Выбранный киносеанс </summary>
        public MovieShow SelectedEditMovieShow
        {
            get => _selectedEditMovieShow;
            set => Set(ref _selectedEditMovieShow, value);
        }

        private Order _NewOrder = new() {Count = 0, DateTime = DateTime.Now, MovieShow = default};

        /// <summary> Новый заказ </summary>
        public Order NewOrder
        {
            get => _NewOrder;
            set => Set(ref _NewOrder, value);
        }

        #endregion




        public MainWindowViewModel(IRepository<MovieShow> MovieShows, IRepository<Order> Orders)
        {
            using var db = new CinemaBoxDb();
            CinemaBoxDb.InitDb(db);
            _MovieShows = MovieShows;
            _Orders = Orders;
            LoadData();

        }

        #region Команды

        #region Редактирование данных

        private ICommand _AddMovieShowCommand;

        /// <summary> Команда добавления нового киносеанса </summary>
        public ICommand AddMovieShowCommand => _AddMovieShowCommand ??=
            new LambdaCommand(OnAddMovieShowCommandExecuted);
        private void OnAddMovieShowCommandExecuted(object p)
        {
            var newitem = new MovieShow
            {
                BeginTime = DateTime.Now,
                Name = "Шаблон киносеанса",
            };
            _MovieShows.Add(newitem);
            MovieShows.Add(newitem);
            SelectedEditMovieShow = newitem;
        }
        private ICommand _EditMovieShowCommand;
        /// <summary> Команда сохранения изменения выбранного киносеанса </summary>
        public ICommand EditMovieShowCommand => _EditMovieShowCommand ??=
            new LambdaCommand(OnEditMovieShowCommandExecuted, CanEditMovieShowCommandExecute);
        private bool CanEditMovieShowCommandExecute(object p) => SelectedEditMovieShow != null;
        private void OnEditMovieShowCommandExecuted(object p)
        {
            _MovieShows.Update(SelectedEditMovieShow);
        }
        private ICommand _DeleteMovieShowCommand;
        /// <summary> Команда удаления выбранного киносеанса </summary>
        public ICommand DeleteMovieShowCommand => _DeleteMovieShowCommand ??=
            new LambdaCommand(OnDeleteMovieShowCommandExecuted, CanDeleteMovieShowCommandExecute);
        private bool CanDeleteMovieShowCommandExecute(object p) => SelectedEditMovieShow != null;
        private void OnDeleteMovieShowCommandExecuted(object p)
        {
            _MovieShows.Delete(SelectedEditMovieShow.Id);
            MovieShows.Remove(SelectedEditMovieShow);
        }

        private ICommand _AddNewOrderCommand;
        /// <summary> Команда добавления нового заказа </summary>
        public ICommand AddNewOrderCommand => _AddNewOrderCommand ??=
            new LambdaCommand(OnAddNewOrderCommandExecuted, CanAddNewOrderCommandExecute);
        private bool CanAddNewOrderCommandExecute(object p) => true;
        private void OnAddNewOrderCommandExecuted(object p)
        {
            if (NewOrder.DateTime == default) return;
            if (NewOrder.MovieShow == null) return;
            _Orders.Add(NewOrder);
            Orders.Add(NewOrder);
            NewOrder = new() {Count = 0, DateTime = DateTime.Now, MovieShow = default};
        }
        private ICommand _ClearOrderCommand;
        /// <summary> Команда очистки заказа </summary>
        public ICommand ClearOrderCommand => _ClearOrderCommand ??=
            new LambdaCommand(OnClearOrderCommandExecuted);
        private void OnClearOrderCommandExecuted(object p)
        {
            NewOrder.DateTime = DateTime.Now;
            NewOrder.Count = 0;
            NewOrder.MovieShow = null;
        }

        #endregion

        private ICommand _LoadDataCommand;
        /// <summary> Команда загрузки данных </summary>
        public ICommand LoadDataCommand => _LoadDataCommand ??=
            new LambdaCommand(OnLoadDataCommandExecuted);
        private void OnLoadDataCommandExecuted(object p)
        {
            LoadData();
        }



        private ICommand _ShowDialogCommand;
        /// <summary> Команда показа диалога </summary>
        public ICommand ShowDialogCommand => _ShowDialogCommand ??=
            new LambdaCommand(OnShowDialogCommandExecuted, CanShowDialogCommandExecute);
        private bool CanShowDialogCommandExecute(object p) => true;
        private void OnShowDialogCommandExecuted(object p)
        {
            var message = p as string ?? "Собщение от команды";
            App.Services.GetService<IDialogService>()?.ShowInfo(message);
        }
        private ICommand _CloseAppCommand;
        /// <summary> Выход из приложения </summary>
        public ICommand CloseAppCommand => _CloseAppCommand ??=
            new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
        private bool CanCloseAppCommandExecute(object p) => true;
        private void OnCloseAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region Вспомогательные методы

        private void LoadData()
        {
            Load(MovieShows, _MovieShows);
            Load(Orders, _Orders);
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
