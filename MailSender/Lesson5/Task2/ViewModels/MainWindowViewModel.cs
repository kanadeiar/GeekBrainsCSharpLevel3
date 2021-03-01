using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Win32;
using Task2.Commands;
using Task2.ViewModels.Base;

namespace Task2.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Geekbrains. Домашнее задание №5. Многопоточное программирование.";

        public record Student(int Id, string Last, string Name, string Patronymic);

        public ObservableCollection<Student> Students { get; } = new();


        /// <summary> Заголовок </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private readonly Timer _timer;
        public DateTime CurrentTime => DateTime.Now;

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }

        public MainWindowViewModel()
        {
            _timer = new Timer
            {
                Interval = 100,
                AutoReset = true,
                Enabled = true,
            };
            _timer.Elapsed += OnTimerElapsed;
        }

        private ICommand _OpenFileCommand;

        /// <summary> Открыть файл </summary>
        public ICommand OpenFileCommand => _OpenFileCommand ??=
            new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);

        private bool CanOpenFileCommandExecute(object p) => true;

        private void OnOpenFileCommandExecuted(object p)
        {
            var openDialog = new OpenFileDialog
            {
                Title = "Открыть Scv файл",
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "Scv файл (*.scv)|*.scv|Все файлы (*.*)|*.*"
            };
            if (openDialog.ShowDialog() == false) return;
            var filename = openDialog.FileName;
            if (File.Exists(filename) == false) return;
            Students.Clear();
            try
            {
                var students = File.ReadAllLines(filename, Encoding.UTF8).Select(s => s.Split(';')).Skip(1);
                foreach (var line in students)
                {
                    Students.Add(new Student(int.Parse( line[0] ), line[1], line[2], line[3]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть этот файл: {filename}\nОшибка: {ex.Message}");
            }
        }
        
        private ICommand _SaveFileCommand;

        /// <summary> Сохранить в файл </summary>
        public ICommand SaveFileCommand => _SaveFileCommand ??=
            new LambdaCommand(OnSaveFileCommandExecuted, CanSaveFileCommandExecute);

        private bool CanSaveFileCommandExecute(object p) => Students.Count > 0;

        private void OnSaveFileCommandExecuted(object p)
        {
            var saveDialog = new SaveFileDialog
            {
                Title = "Сохранить в TXT файл",
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "Txt файл (*.txt)|*.txt|Все файлы (*.*)|*.*",
                FileName = "test.txt",
            };
            if (saveDialog.ShowDialog() == false) return;
            var filename = saveDialog.FileName;
            var strings = new Queue<string>(Students.Count + 1);
            strings.Enqueue("Id Фамилия Имя Отчество");
            foreach (var student in Students)
            {
                strings.Enqueue($"{student.Id} {student.Last} {student.Name} {student.Patronymic}");
            }
            try
            {
                File.WriteAllLines(filename, strings, Encoding.UTF8);
                MessageBox.Show("Готово!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось записать в этот файл: {filename}\nОшибка: {ex.Message}");
            }
        }
    }
}
