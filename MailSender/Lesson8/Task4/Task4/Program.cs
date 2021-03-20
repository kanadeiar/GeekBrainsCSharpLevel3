using System;
using System.Runtime.InteropServices;

namespace Task4
{
    class Program
    {
        private enum SomeEnum
        {
            First = 4,
            Second,
            Third = 7
        }
        static void Main(string[] args)
        {
            ToRussian();
            Console.WriteLine("Задание № 4");
            Console.WriteLine("Каков результат вывода следующего кода?");
            Console.WriteLine(@"private enum SomeEnum
{
	First = 4,
	Second,
	Third = 7
}
static void Main(string[] args)
{
	Console.WriteLine((int)SomeEnum.Second);
}
");
            Console.WriteLine("Результат вывода кода:");
            
            Console.WriteLine((int)SomeEnum.Second);


            Console.WriteLine("\nНажмите любую кнопку ...");
            Console.ReadKey();
        }

        private static void ToRussian()
        {
            [DllImport("kernel32.dll")] static extern bool SetConsoleCP(uint pagenum);
            [DllImport("kernel32.dll")] static extern bool SetConsoleOutputCP(uint pagenum);
            SetConsoleCP(65001);        //установка кодовой страницы utf-8 (Unicode) для вводного потока
            SetConsoleOutputCP(65001);  //установка кодовой страницы utf-8 (Unicode) для выводного потока
        }
    }
}
