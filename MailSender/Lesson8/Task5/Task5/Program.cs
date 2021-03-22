using System;
using System.Runtime.InteropServices;
using Microsoft.Data.SqlClient;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            ToRussian();
            Console.WriteLine("Задание № 5");
            Console.WriteLine(@"* Есть таблица:
Необходимо сформировать SQL-запрос, выбирающий данные следующим образом:");

            Console.WriteLine("\n\nПересоздать таблицу с тестовыми данными Data в базе данных Task5.DB ? (Y)");
            var comm = Console.ReadLine();
            if (comm != null && comm.Contains('y', StringComparison.CurrentCultureIgnoreCase))
            {
                try
                {
                    GenerateNewTestSQLTable();
                    Console.WriteLine("Таблица Data пересоздана успешно!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Не удалось пересоздать таблицу!\n" + e.Message);
                }
            }

            Console.WriteLine("Результат выполнения SQL запроса:");

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Task5.DB";
                connection.Open();
                string sql = @"SELECT [group_id]
	, [descr] = STUFF(CAST((
        SELECT [text()] = ', ' + dta2.descr
        FROM [Data] dta2
        WHERE dta2.[group_id] = dta.[group_id]
        FOR XML PATH(''), TYPE) AS NVARCHAR(100)), 1, 2, '')
  FROM (SELECT DISTINCT [group_id]
  FROM [Data]) dta;";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int gr_id = reader.GetOrdinal("group_id");
                    int desc = reader.GetOrdinal("descr");
                    Console.WriteLine("group_id\tgroup_id");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[gr_id]}\t\t{reader[desc]}");
                    }
                }
                connection.Close();
            }


            Console.WriteLine("\nНажмите любую кнопку ...");
            Console.ReadKey();
        }

        private static void GenerateNewTestSQLTable()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Task5.DB";
                connection.Open();
                string sql = @"--Пересоздание новой таблицы с данными в базе данных
IF OBJECT_ID('Data' ) IS NOT NULL DROP TABLE Data;
CREATE TABLE Data
(
id INT IDENTITY(1,1) NOT NULL,
group_id INT NOT NULL,
descr NVARCHAR(256) NOT NULL,
CONSTRAINT PK_id_Users PRIMARY KEY (id),
);
--Тестовые данные
INSERT INTO Data(group_id, descr)
	VALUES (1, N'Один');
INSERT INTO Data(group_id, descr)
	VALUES (2, N'Два');
INSERT INTO Data(group_id, descr)
	VALUES (1, N'Три');
INSERT INTO Data(group_id, descr)
	VALUES (2, N'Четыре');
INSERT INTO Data(group_id, descr)
	VALUES (2, N'Пять');
";
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
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
