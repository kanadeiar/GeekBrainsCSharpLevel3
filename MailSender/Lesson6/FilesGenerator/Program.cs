using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FilesGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            [DllImport("kernel32.dll")] static extern bool SetConsoleCP(uint pagenum);
            [DllImport("kernel32.dll")] static extern bool SetConsoleOutputCP(uint pagenum);
            SetConsoleCP(65001);        //установка кодовой страницы utf-8 (Unicode) для вводного потока
            SetConsoleOutputCP(65001);  //установка кодовой страницы utf-8 (Unicode) для выводного потока
            Console.WriteLine("Генерирование тестовых файлов для задачи №2");
            


            //Random rand = new Random(); // ошибка
            Queue<Task> tasks = new Queue<Task>();
            for (int i = 0; i < 100; i++)
            {
                int i0 = i;
                Random rand = new Random();
                tasks.Enqueue(Task.Run(() =>
                {
                    var data = Enumerable.Range(1, 1_000).Select(s => $"{((s % 2 == 0) ? 1 : 2)} {rand.NextDouble() * 1_000.0} {rand.NextDouble() * 100.0}");
                    File.WriteAllLines($"test{i0}.txt", data, Encoding.UTF8);
                }));
            }
            Task.WhenAll(tasks);



            Console.WriteLine("Файлы сгенерированы, нажмите любую кнопку");
            Console.ReadKey();
        }
    }
}
