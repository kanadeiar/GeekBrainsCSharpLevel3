namespace MailSender.lib.Models
{
    /// <summary> Сервер </summary>
    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
    }
}
