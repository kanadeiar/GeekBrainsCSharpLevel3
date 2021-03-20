using System;
using System.Data;
using System.Runtime.InteropServices;
using Microsoft.Data.SqlClient;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            ToRussian();

            Console.WriteLine("Пересоздать таблицу с тестовыми данными Users в базе данных Task3.DB ? (Y)");
            var comm = Console.ReadLine();
            if (comm != null && comm.Contains('y', StringComparison.CurrentCultureIgnoreCase))
            {
                try
                {
                    GenerateNewTestSQLTable();
                    Console.WriteLine("Таблица Users пересоздана успешно!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Облом! " + e.Message);
                }
            }

            


            Console.WriteLine("\nНажмите любую кнопку ...");
            Console.ReadKey();
        }

        private static void GenerateNewTestSQLTable()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Task3.DB";
                connection.Open();
                string sql = @"--Пересоздание новой таблицы с юзерами в базе данных
IF OBJECT_ID('Users' ) IS NOT NULL DROP TABLE Users;
CREATE TABLE Users
(
id INT IDENTITY(1,1) NOT NULL,
name NVARCHAR(256) NOT NULL,
CONSTRAINT PK_id_Users PRIMARY KEY (id),
);
--Пользователи с именами на букву А
INSERT INTO Users(name)
	VALUES (N'Аноним1');
INSERT INTO Users(name)
	VALUES (N'Антон1');
--Остальные Тестовые данные запихиваем в эту таблицу
DECLARE @count_users INT = 100;
DECLARE @i INT = 1;
WHILE @i <= @count_users
BEGIN
	INSERT INTO Users(name)
	VALUES (CONCAT(N'Тестович', @i));
	SET @i = @i + 1;
END;
--Еще Пользователи с именами на букву А
INSERT INTO Users(name)
	VALUES (N'Антон1');
INSERT INTO Users(name)
	VALUES (N'Абгузин19');";
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
