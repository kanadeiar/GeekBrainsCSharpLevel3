using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Task3.Data;
using Task3.Interfaces;
using Task3.Models;
using Task3.Services;
using Task3.ViewModels;

namespace Task3
{
    /// <summary> Interaction logic for App.xaml </summary>
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
            services.AddDbContext<CinemaBoxDb>();

            services.AddTransient<IDialogService, WindowDialogService>();

            services.AddScoped<MainWindowViewModel>();

            services.AddScoped<IRepository<MovieShow>, MovieShowRepository>();
            services.AddScoped<IRepository<Order>, OrdersReporitory>();

        }
    }
}
