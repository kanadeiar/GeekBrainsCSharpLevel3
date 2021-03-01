using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileGeneratorConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Генератор тестовых данных");

            var _lastNames = new List<string>(100);
            var _firstNames = new List<string>(100);
            var _patronymics = new List<string>(100);

            string[] servers = { "mail.ru", "yandex.ru", "gmail.com", "rambler.ru", "aport.ru" };

            foreach (var (first, last, patronymic) in GetStudents())
            {
                _lastNames.Add(last);
                _firstNames.Add(first);
                _patronymics.Add(patronymic);
            }

            var count = 900_000;

            var rnd = new Random();
            const string dataFile = "text.scv";
            using var writer = new StreamWriter(dataFile, false, Encoding.UTF8);
            writer.WriteLine("id;Фамилия;Имя;Отчество;Адрес");
            for (int i = 1; i < count; i++)
            {
                var strlast = rnd.Next(_lastNames);
                var strfirst = rnd.Next(_firstNames);
                var strpart = rnd.Next(_patronymics);

                var str = $"{i};{strfirst};{strlast};{strpart};{strfirst.Transliterate()}@{rnd.Next(servers)}";
                writer.WriteLine(str);
            }
            Console.WriteLine("Работа завершена. Нажмите любую кнопку.");

            Console.ReadKey();
        }

        private static T Next<T>(this Random rnd, IList<T> items) => items[rnd.Next(items.Count)];

        private class Student
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Patronymic { get; set; }

            public void Deconstruct(out string first, out string last, out string patronymic)
            {
                first = FirstName;
                last = LastName;
                patronymic = Patronymic;
            }
        }

        private static IEnumerable<Student> GetStudents()
        {
            return Enumerable.Range(1, 100).Select(s => new Student
            {
                FirstName = $"Иванов{s}",
                LastName = $"Иван{s}",
                Patronymic = $"Иванович{s}",
            });
        }

        private static string Transliterate(this string str) => str
            .Aggregate(new StringBuilder(),
            (s, c) => s.Append(c.Transliterate()), 
            s => s.ToString());
        
        private static string Transliterate(this char c) => c switch
        {
            'а' => "a",
            'б' => "b",
            'в' => "v",
            'г' => "g",
            'д' => "d",
            'е' => "e",
            'ё' => "yo",
            'ж' => "j",
            'з' => "z",
            'и' => "i",
            'й' => "j",
            'к' => "k",
            'л' => "l",
            'м' => "m",
            'н' => "n",
            'о' => "o",
            'п' => "p",
            'р' => "r",
            'с' => "s",
            'т' => "t",
            'у' => "u",
            'ф' => "f",
            'х' => "h",
            'ц' => "c",
            'ч' => "ch",
            'ш' => "sh",
            'щ' => "sch",
            'ъ' => "'",
            'ы' => "i",
            'ь' => "'",
            'э' => "a",
            'ю' => "yu",
            'я' => "ya",

            'А' => "A",
            'Б' => "B",
            'В' => "V",
            'Г' => "G",
            'Д' => "D",
            'Е' => "E",
            'Ё' => "Yo",
            'Ж' => "J",
            'З' => "Z",
            'И' => "I",
            'Й' => "J",
            'К' => "K",
            'Л' => "L",
            'М' => "M",
            'Н' => "N",
            'О' => "O",
            'П' => "P",
            'Р' => "R",
            'С' => "S",
            'Т' => "T",
            'У' => "U",
            'Ф' => "F",
            'Х' => "H",
            'Ц' => "C",
            'Ч' => "Ch",
            'Ш' => "Sh",
            'Щ' => "Sch",
            'Ъ' => "'",
            'Ы' => "I",
            'Ь' => "'",
            'Э' => "A",
            'Ю' => "Yu",
            'Я' => "Ya",

            char ch when ch > '0' && ch < '9' => ch.ToString(),

            _ => "",
        };
    }
}
