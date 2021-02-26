using System.Linq;
using MailSender.Data.Stores.InMemory.Base;
using MailSender.lib.Models;

namespace MailSender.Data.Stores.InMemory
{
    /// <summary> Репозиторий отправителей </summary>
    class SendersRepositoryInMem : RepositoryInMem<Sender>
    {
        public SendersRepositoryInMem() : base(Enumerable.Range(1, 10_000)
            .Select(i => new Sender
            {
                Id = i,
                Address = $"sender{i}@server{i}.com",
                Description = $"Тестовый отправитель{i}",
                Name = $"Отправитель{i}",
            }))
        {
        }
        public override void Update(Sender item)
        {
            var editItem = GetById(item.Id);
            if (editItem is null) return;
            if (ReferenceEquals(editItem, item)) return;
            editItem.Name = item.Name;
            editItem.Address = item.Address;
            editItem.Description = item.Description;
        }
    }
}
