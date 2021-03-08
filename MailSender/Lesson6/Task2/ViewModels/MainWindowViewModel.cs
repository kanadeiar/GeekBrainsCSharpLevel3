using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Task2.Commands;
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

        #region Команды

        private ICommand _RunFastWorkWithFilesCommand;

        /// <summary> Быстрая переработка файлов по условию задачи </summary>
        public ICommand RunFastWorkWithFilesCommand => _RunFastWorkWithFilesCommand ??=
            new LambdaCommand(OnRunFastWorkWithFilesCommandExecuted, CanRunFastWorkWithFilesCommandExecute);

        private bool CanRunFastWorkWithFilesCommandExecute(object p) => true;

        private async void OnRunFastWorkWithFilesCommandExecuted(object p)
        {
            var stringResults = new Queue<string>();
            for (int i = 0; i < 100; i++)
            {
                int i0 = i;
                var currResult = await Task.Run(async () =>
                {
                    var data = await File.ReadAllLinesAsync($"test{i0}.txt", Encoding.UTF8);
                    var results = data
                        .Select(d => d.Split(' '))
                        .Select(s =>
                        {
                            var operat = int.Parse(s[0]);
                            var value1 = double.Parse(s[1]);
                            var value2 = double.Parse(s[2]);
                            if (operat == 1)
                                return value1 * value2;
                            if (value2 != 0)
                                return value1 / value2;
                            return 0;
                        }).ToArray();
                    return results;
                });
                foreach (var r in currResult)
                    stringResults.Enqueue(r.ToString("F"));
            }
            await File.WriteAllLinesAsync("result.dat", stringResults, Encoding.UTF8);

            // TODO: доделать параллельную работу!!!
            //var tasks = new Queue<Task<double[]>>();
            //for (int i = 0; i < 100; i++)
            //{
            //    int i0 = i;
            //    tasks.Append(Task.Run(async () =>
            //    {
            //        var data = await File.ReadAllLinesAsync($"test{i0}.txt", Encoding.UTF8);
            //        var results = data
            //            .Select(d => d.Split(' '))
            //            .Select(s =>
            //            {
            //                var operat = int.Parse(s[0]);
            //                var value1 = double.Parse(s[1]);
            //                var value2 = double.Parse(s[2]);
            //                if (operat == 1)
            //                    return value1 * value2;
            //                if (value2 != 0)
            //                    return value1 / value2;
            //                return 0;
            //            }).ToArray();
            //        return results;
            //    }));
            //}
            //var resultsTasks = await Task.WhenAll(tasks);
            //var results = new Queue<string>();
            //foreach (var result in resultsTasks)
            //    foreach (var r in result)
            //        results.Enqueue(r.ToString("F"));
            //await File.WriteAllLinesAsync("result.dat", results, Encoding.UTF8);

            MessageBox.Show("Выполнено!","Готово", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion


    }
}
