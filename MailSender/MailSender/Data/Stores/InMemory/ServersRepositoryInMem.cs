using System.Linq;
using MailSender.Data.Stores.InMemory.Base;
using MailSender.lib.Models;
using MailSender.lib.Services;

namespace MailSender.Data.Stores.InMemory
{
    /// <summary> Репозиторий серверов </summary>
    class ServersRepositoryInMem : RepositoryInMem<Server>
    {
        public ServersRepositoryInMem() : base(Enumerable.Range(1, 100).Select(i => new Server
        {
            Id = i,
            Address = $"smtp.server{i}.com",
            Description = $"Тестовый сервер {i}",
            Login = $"Login{i}",
            Name = $"Сервер-{i}",
            Password = $"Password{i}".Encrypt(),
            Port = 25,
            UseSsl = i % 2 != 0,
        }))
        {
        }
        public override void Update(Server item)
        {
            var editItem = GetById(item.Id);
            if (editItem is null) return;
            if (ReferenceEquals(editItem, item)) return;
            editItem.Name = item.Name;
            editItem.Address = item.Address;
            editItem.Port = item.Port;
            editItem.Description = item.Description;
            editItem.Login = item.Login;
            editItem.Password = item.Password;
        }
    }
}
