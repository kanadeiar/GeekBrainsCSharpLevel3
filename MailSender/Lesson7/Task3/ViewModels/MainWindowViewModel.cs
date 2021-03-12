using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Task3.Commands;
using Task3.Interfaces;
using Task3.ViewModels.Base;

namespace Task3.ViewModels
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

        #region Команды

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


    }
}
