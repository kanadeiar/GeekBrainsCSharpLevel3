using MailSender.lib.Models;
using Microsoft.EntityFrameworkCore;

namespace MailSender.Data.Stores.InDB
{
    public class MailSenderDB : DbContext
    {
        public DbSet<Server> Servers { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Message> Messages { get; set; }
        //public DbSet<SchedulerMailSender> SchedulerMailSenders { get; set; }

        public MailSenderDB(DbContextOptions<MailSenderDB> options) : base(options)
        {
        }
    }
}
