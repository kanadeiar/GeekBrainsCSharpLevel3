using System.Windows;
using WpfMailSender.lib.Infrastructure.Commands.Base;

namespace WpfMailSender.lib.Infrastructure.Commands
{
    public class CloseAppCommand : Command
    {
        protected override void Execute(object p) => Application.Current.Shutdown();
    }
}
