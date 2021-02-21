namespace MailSender.Models
{
    /// <summary> Сообщение </summary>
    public class Message
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}
