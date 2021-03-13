using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Task4.ViewsModels;

namespace Task4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider _Services;
        private static IServiceCollection GetServices()
        {
            var services = new ServiceCollection();
            InitializeServices(services);
            return services;
        }
        public static IServiceProvider Services => _Services ??= GetServices().BuildServiceProvider();
        /// <summary> Инит сервисов </summary>
        /// <param name="services">сервисы приложения</param>
        private static void InitializeServices(IServiceCollection services)
        {

            services.AddScoped<MainWindowViewModel>();



        }
    }
}
