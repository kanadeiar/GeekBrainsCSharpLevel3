using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Task1.Commands;
using Task1.ViewModels.Base;

namespace Task1.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private const int N = 100;
        private int[,] _matrix1;
        private int[,] _matrix2;
        private int[,] _martixResult;
        private static Random rand = new Random();

        #region Свойства

        private string _Title = "Geekbrains. Домашнее задание №6. Асинхронное программирование.";
        /// <summary> Заголовок </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private string _MatrixA;

        /// <summary> Матрица А </summary>
        public string MatrixA
        {
            get => _MatrixA;
            set => Set(ref _MatrixA, value);
        }

        private string _MatrixB;

        /// <summary> Матрица Б </summary>
        public string MatrixB
        {
            get => _MatrixB;
            set => Set(ref _MatrixB, value);
        }

        private string _matrixResult = default;

        /// <summary> Матрица результат </summary>
        public string MatrixResult
        {
            get => _matrixResult;
            set => Set(ref _matrixResult, value);
        }

        #endregion
        
        public MainWindowViewModel()
        {
        }

        #region Команды

        private ICommand _GenerateMatrixCommand;

        /// <summary> Генерация матриц с тестовыми данными </summary>
        public ICommand GenerateMatrixCommand => _GenerateMatrixCommand ??=
            new LambdaCommand(OnGenerateMatrixCommandExecuted, CanGenerateMatrixCommandExecute);

        private bool CanGenerateMatrixCommandExecute(object p) => true;

        private void OnGenerateMatrixCommandExecuted(object p)
        {
            _matrix1 = GenerateMatrix();
            _matrix2 = GenerateMatrix();
            MatrixA = PrintMatrixToText(in _matrix1);
            MatrixB = PrintMatrixToText(in _matrix2);
        }

        private ICommand _CalculateMultiplexCommand;

        /// <summary> Команда синхронного перемножения матриц </summary>
        public ICommand CalculateMultiplexCommand => _CalculateMultiplexCommand ??=
            new LambdaCommand(OnCalculateMultiplexCommandExecuted, CanCalculateMultiplexCommandExecute);

        private bool CanCalculateMultiplexCommandExecute(object p) => true;

        private void OnCalculateMultiplexCommandExecuted(object p)
        {
            _martixResult = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    var sum = 0;
                    for (int k = 0; k < N; k++)
                    {
                        sum += _matrix1[i, k] + _matrix2[k, j];
                    }
                    _martixResult[i, j] = sum;
                }
            }
            MatrixResult = PrintMatrixToText(_martixResult);
        }

        private ICommand _CalculateMultiplexParallelCommand;

        /// <summary> Команда асинхронного параллельного перемножения матриц </summary>
        public ICommand CalculateMultiplexParallelCommand => _CalculateMultiplexParallelCommand ??=
            new LambdaCommand(OnCalculateMultiplexParallelCommandExecuted, CanCalculateMultiplexParallelCommandExecute);

        private bool CanCalculateMultiplexParallelCommandExecute(object p) => true;

        private async void OnCalculateMultiplexParallelCommandExecuted(object p)
        {
            _martixResult = new int[N, N];
            var matrixOne = _matrix1;
            var martixTwo = _matrix2;
            List<Task<int>> tasks = new List<Task<int>>();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    var i0 = i;
                    var j0 = j;
                    tasks.Add(Task.Run(() =>
                    {
                        var sum = 0;
                        for (int k = 0; k < N; k++)
                        {
                            sum += matrixOne[i0, k] + martixTwo[k, j0];
                        }
                        return sum;
                    }));
                }
            }
            var res = await Task.WhenAll(tasks).ConfigureAwait(true);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    _martixResult[i, j] = res[i * 10 + j];
                }
            }
            MatrixResult = PrintMatrixToText(_martixResult);
        }

        #endregion
        
        #region Вспомогалки

        protected string PrintMatrixToText(in int[,] matrix)
        {
            StringBuilder bld = new StringBuilder();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    bld.Append(matrix[i, j]).Append(' ');
                }
                bld.AppendLine();
            }
            return bld.ToString();
        }

        private static int[,] GenerateMatrix()
        {
            var temp = Enumerable.Range(0, N)
                .Select(i => Enumerable.Range(0, N).Select(j => rand.Next(10)).ToArray()).ToArray();
            var tmpMatrix = new int[N, N];
            for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                tmpMatrix[i, j] = temp[i][j];
            return tmpMatrix;
        }

        #endregion
    }
}
