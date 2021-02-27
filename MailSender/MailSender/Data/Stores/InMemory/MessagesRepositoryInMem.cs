using System.Linq;
using MailSender.Data.Stores.InMemory.Base;
using MailSender.lib.Models;

namespace MailSender.Data.Stores.InMemory
{
    /// <summary> Репозиторий сообщений </summary>
    class MessagesRepositoryInMem : RepositoryInMem<Message>
    {
        public MessagesRepositoryInMem() : base(Enumerable.Range(1, 1_000)
            .Select(i => new Message
            {
                Id = i,
                Subject = $"Заголовок{i}",
                Text = $"Текст сообщения {i}",
            }))
        {
        }
        public override void Update(Message item)
        {
            var editItem = GetById(item.Id);
            if (editItem is null) return;
            if (ReferenceEquals(editItem, item)) return;
            editItem.Subject = item.Subject;
            editItem.Text = item.Text;
        }
    }
}
