using System;
using System.Runtime.InteropServices;
using System.Threading.Channels;

namespace Task2
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ToRussian();
            Console.WriteLine("Задание № 2");
            Console.WriteLine("Есть ли проблемы в следующем коде?\n");
            Console.WriteLine(@"int i = 1;
object obj = i;
++i;
Console.WriteLine(i);
Console.WriteLine(obj);
Console.WriteLine((short)obj);");
            Console.WriteLine("\nДа, имеются проблемы.");
            Console.WriteLine("В последней строке неудается привести тип int32 к типу int16");
            Console.WriteLine("Нужно сделать так:\n");
            Console.WriteLine(@"Console.WriteLine(Convert.ToInt32(obj));");
            Console.WriteLine("\nВот результат:\n");

            int i = 1;
            object obj = i;
            ++i;
            Console.WriteLine(i);
            Console.WriteLine(obj);
            Console.WriteLine(Convert.ToInt32(obj));

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
