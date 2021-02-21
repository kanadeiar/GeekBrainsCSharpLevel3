using System;
using System.Collections.Generic;

namespace MailSender.Models
{
    /// <summary> Задание планировщика </summary>
    public class SchedulerTask
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public Server Server { get; set; }
        public Sender Sender { get; set; }
        public List<Recipient> Recipients { get; set; }
        public Message Message { get; set; }
    }
}
