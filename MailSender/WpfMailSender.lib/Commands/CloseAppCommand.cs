using System.Windows;
using WpfMailSender.lib.Commands.Base;

namespace WpfMailSender.lib.Commands
{
    public class CloseAppCommand : Command
    {
        protected override void Execute(object p) => Application.Current.Shutdown();
    }
}
