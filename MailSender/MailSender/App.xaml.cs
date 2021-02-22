using System;
using System.Windows;
using MailSender.lib.Interfaces;
using MailSender.lib.Services;
using MailSender.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost __Hosting;
        public static IHost Hosting
        {
            get
            {
                if (__Hosting != null) return __Hosting;
                var hostBuilder = Host.CreateDefaultBuilder(Environment.GetCommandLineArgs());
                hostBuilder.ConfigureServices(ConfigureServices);
                return __Hosting = hostBuilder.Build();
            }
        }
        /// <summary> Сервисы приложения </summary>
        public static IServiceProvider Services => Hosting.Services;
        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<WpfMailSenderViewModel>();
#if DEBUG
            services.AddTransient<IMailService, DebugMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();
#endif
            
            var storage = new DebugDataStorage();

            services.AddSingleton<IServerStorage>(storage);
            services.AddSingleton<ISenderStorage>(storage);
            services.AddSingleton<IRecipientStorage>(storage);
            services.AddSingleton<IMessageStorage>(storage);


        }
    }
}
