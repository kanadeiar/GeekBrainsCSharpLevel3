using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.ViewModels.Base;

namespace Task2.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        
        #region Свойства

        private string _Title = "Geekbrains.Домашнее задание №6. Асинхронное программирование.";

        /// <summary> Заголовк окна </summary>
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
