using System;
using System.Net;
using System.Net.Mail;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Тестовая консоль";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ///////////////////////////////////////////////////////////////////////////////
            //var from = new MailAddress("kanadeiar@yandex.ru", "Андрей");
            //var to = new MailAddress("kanadeiar@gmail.com", "Андрей");
            var from = new MailAddress("kanadeiar@gmail.com", "Андрей");
            var to = new MailAddress("kanadeiar@yandex.ru", "Андрей");
            var message = new MailMessage(from, to);
            message.Subject = "Заголовок";
            message.Body = "Текст письма";

            ///////////////////////////////////////////////////////////////////////////////
            //var client = new SmtpClient("smtp.yandex.ru", 587);
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            
            client.Credentials = new NetworkCredential
            {
                //UserName = "kanadeiar@yandex.ru",
                UserName = "kanadeiar@gmail.com",
                Password = "*",
            };
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            

            ///////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Нажмите любую кнопку ...");
            Console.ReadKey();
        }
    }
}
