using System.Linq;
using MailSender.Data.Stores.InMemory.Base;
using MailSender.lib.Models;

namespace MailSender.Data.Stores.InMemory
{
    /// <summary> Репозиторий получателей </summary>
    class RecipientsRepositoryInMem : RepositoryInMem<Recipient>
    {
        public RecipientsRepositoryInMem() : base(Enumerable.Range(1, 10_000)
            .Select(i => new Recipient
            {
                Id = i,
                Address = $"recipient{i}@server{i}.com",
                Description = $"Тестовый получатель{i}",
                Name = $"Получатель{i}",
            }))
        {
        }
        public override void Update(Recipient item)
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
