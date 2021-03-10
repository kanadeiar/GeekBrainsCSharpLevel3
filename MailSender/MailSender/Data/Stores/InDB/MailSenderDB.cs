using System.Collections.Generic;
using System.Linq;
using MailSender.lib.Models;
using MailSender.lib.Services;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Server>().HasData(Enumerable.Range(1,4).Select(i => new Server
            {
                Id = i,
                Address = $"smtp.server{i}.com",
                Description = $"Тестовый сервер {i}",
                Login = $"Login{i}",
                Name = $"Сервер-{i}",
                Password = $"Password{i}".Encrypt(),
                Port = 25,
                UseSsl = i % 2 != 0,
            }));
            modelBuilder.Entity<Sender>().HasData(Enumerable.Range(1, 10).Select(i => new Sender
            {
                Id = i,
                Address = $"sender{i}@server{i}.com",
                Description = $"Тестовый отправитель{i}",
                Name = $"Отправитель{i}",
            }));
            modelBuilder.Entity<Recipient>().HasData(Enumerable.Range(1, 10).Select(i => new Recipient
            {
                Id = i,
                Address = $"recipient{i}@server{i}.com",
                Description = $"Тестовый получатель{i}",
                Name = $"Получатель{i}",
            }));
            modelBuilder.Entity<Message>().HasData(Enumerable.Range(1, 10).Select(i => new Message
            {
                Id = i,
                Subject = $"Заголовок{i}",
                Text = $"Текст сообщения {i}",
            }));
        }
    }
}
