namespace MailSender.Models
{
    /// <summary> Получатель </summary>
    public class Recipient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
