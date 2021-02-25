using System.Windows;
using MailSender.lib.Commands.Base;

namespace MailSender.lib.Commands
{
    public class CloseAppCommand : Command
    {
        protected override void Execute(object p) => Application.Current.Shutdown();
    }
}
