using System;
using System.Windows;
using MailSender.Data.Stores.InMemory;
using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.lib.Services;
using MailSender.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MailSender
{
    /// <summary> Interaction logic for App.xaml </summary>
    public partial class App : Application
    {
        private static IHost __Hosting;
        /// <summary> Создание хоста сервисов </summary>
        public static IHost Hosting
        {
            get
            {
                if (__Hosting != null) return __Hosting;
                var hostBuilder = Host.CreateDefaultBuilder(Environment.GetCommandLineArgs());
                hostBuilder.ConfigureAppConfiguration(opt => opt.AddJsonFile("appsettings.json", false, true));
                hostBuilder.ConfigureServices(ConfigureServices);
                return __Hosting = hostBuilder.Build();
            }
        }
        /// <summary> Сервисы приложения </summary>
        public static IServiceProvider Services => Hosting.Services;

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<StatisticViewModel>();
#if DEBUG
            services.AddTransient<IMailService, DebugMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();
#endif
            services.AddSingleton<IRepository<Server>, ServersRepositoryInMem>();
            services.AddSingleton<IRepository<Sender>, SendersRepositoryInMem>();
            services.AddSingleton<IRepository<Recipient>, RecipientsRepositoryInMem>();
            services.AddSingleton<IRepository<Message>, MessagesRepositoryInMem>();

            services.AddSingleton<IStatistic, MemoryStatisticService>();

//#if DEBUG
//            var storage = new DebugDataStorage();
//#else
//            var storage = new XmlFileDataStorage("storage.xml");
//#endif
//            services.AddSingleton<IServerStorage>(storage);
//            services.AddSingleton<ISenderStorage>(storage);
//            services.AddSingleton<IRecipientStorage>(storage);
//            services.AddSingleton<IMessageStorage>(storage);


        }
    }
}
