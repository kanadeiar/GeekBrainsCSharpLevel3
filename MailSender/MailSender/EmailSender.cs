using System;
using System.Net;
using System.Net.Mail;
using System.Security;

namespace MailSender
{
    /// <summary> Сервис отправки сообщений по электронной почте </summary>
    public class EmailSender
    {
        private readonly string _login;
        private readonly SecureString _password;
        public EmailSender(string login, SecureString password)
        {
            _login = login;
            _password = password;
        }
        /// <summary> Отправка сообщения по электронной почте </summary>
        /// <param name="from">от</param>
        /// <param name="to">кому</param>
        /// <param name="subject">заголовок</param>
        /// <param name="message">сообщение</param>
        /// <param name="nameServer">имя сервера</param>
        /// <param name="port">порт</param>
        /// <param name="enableSsl">шифрование</param>
        /// <returns></returns>
        public int SendMail(string from, string to, string subject, string message, string nameServer, int port, bool enableSsl)
        {
            using (MailMessage myMessage = new MailMessage(from, to, subject, message))
            {
                myMessage.IsBodyHtml = false;
                using (SmtpClient client = new SmtpClient(nameServer, port))
                {
                    client.EnableSsl = enableSsl;
                    try
                    {
                        client.Credentials = new NetworkCredential(_login, _password);
                        client.Send(myMessage);
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        return 1;
                    }
                }
            }
        }
    }
}
