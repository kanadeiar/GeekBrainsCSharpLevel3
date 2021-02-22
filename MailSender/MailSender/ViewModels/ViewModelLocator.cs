using Microsoft.Extensions.DependencyInjection;

namespace MailSender.ViewModels
{
    /// <summary> Сервис-локатор </summary>
    class ViewModelLocator
    {
        /// <summary> Вьюмодель главного окна приложения </summary>
        public WpfMailSenderViewModel GetMainWindowViewModel => App.Services.GetRequiredService<WpfMailSenderViewModel>();
    }
}
