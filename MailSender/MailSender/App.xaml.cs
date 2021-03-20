using System;
using System.Diagnostics;
using System.Windows;
using MailSender.Data.Stores.InDB;
using MailSender.Data.Stores.InDB.Base;
using MailSender.Data.Stores.InMemory;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.lib.Services;
using MailSender.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace MailSender
{
    /// <summary> Interaction logic for App.xaml </summary>
    public partial class App : Application
    {
        private static IHost __Hosting;

        /// <summary> Создание хоста сервисов </summary>
        public static IHost Hosting => __Hosting
            ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(opt => opt.AddJsonFile("appsettings.json", false, true))
            .ConfigureServices(ConfigureServices);
        /// <summary> Сервисы приложения </summary>
        public static IServiceProvider Services => Hosting.Services;

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddDbContext<MailSenderDb>(o => o
                .UseLazyLoadingProxies() //использовать ленивую загрузку
                .UseSqlServer(host.Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAddinsService, AddinsService>();
            services.AddScoped<MainWindowViewModel>();
            services.AddScoped<StatisticViewModel>();
#if DEBUG
            services.AddTransient<IMailService, DebugMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();
#endif
            services.AddSingleton<IEncryptService, Rfc2898Encryptor>();

            //services.AddSingleton<IRepository<Server>, ServersRepositoryInMem>();
            //services.AddSingleton<IRepository<Sender>, SendersRepositoryInMem>();
            //services.AddSingleton<IRepository<Recipient>, RecipientsRepositoryInMem>();
            //services.AddSingleton<IRepository<Message>, MessagesRepositoryInMem>();
            //services.AddSingleton<IRepository<Scheduler>, SchedulersRepositoryInMem>();

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddSingleton<IStatistic, MemoryStatisticService>();

            services.AddSingleton<ISchedulerMailService, SchedulerMailService>();

        }
    }
}
