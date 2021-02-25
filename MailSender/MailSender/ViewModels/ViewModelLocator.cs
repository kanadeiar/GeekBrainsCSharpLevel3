using Microsoft.Extensions.DependencyInjection;

namespace MailSender.ViewModels
{
    /// <summary> Сервис-локатор </summary>
    class ViewModelLocator
    {
        /// <summary> Вьюмодель главного окна приложения </summary>
        public MainWindowViewModel MainWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
        /// <summary> Вьюмодель окна статистики </summary>
        public StatisticViewModel StatisticViewModel => App.Services.GetRequiredService<StatisticViewModel>();
    }
}
