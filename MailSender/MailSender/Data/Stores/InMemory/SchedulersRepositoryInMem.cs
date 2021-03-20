using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.Stores.InMemory.Base;
using MailSender.lib.Models;

namespace MailSender.Data.Stores.InMemory
{
    /// <summary> Репозиторий заданий планировщика </summary>
    class SchedulersRepositoryInMem : RepositoryInMem<Scheduler>
    {
        public SchedulersRepositoryInMem(IEnumerable<Scheduler> items = null) : base(items)
        {
        }
        public override void Update(Scheduler item)
        {
            var editItem = GetById(item.Id);
            if (editItem is null) return;
            if (ReferenceEquals(editItem, item)) return;
            editItem.DateTimeSend = item.DateTimeSend;
            editItem.Server = item.Server;
            editItem.Sender = item.Sender;
            editItem.Recipients = item.Recipients;
            editItem.Message = item.Message;
        }
    }
}
