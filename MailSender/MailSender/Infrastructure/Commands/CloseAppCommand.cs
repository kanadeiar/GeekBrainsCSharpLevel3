using System.Windows;
using MailSender.Infrastructure.Commands.Base;

namespace MailSender.Infrastructure.Commands
{
    public class CloseAppCommand : Command
    {
        protected override void Execute(object p) => Application.Current.Shutdown();
    }
}
