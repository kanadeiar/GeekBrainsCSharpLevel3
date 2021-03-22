using System;
using System.IO;
using System.Reflection;
using System.Windows;
using MailSender.lib.Interfaces;

namespace MailSender.lib.Services
{
    public class AddinsService : IAddinsService
    {
        public ITextsAddin getTextsAddin()
        {
            const string fileAddin = "MailSender.Addin.dll";
            if (!File.Exists(fileAddin))
                return null;
            try
            {
                Assembly lib = Assembly.LoadFile(Path.GetFullPath(fileAddin)); 
                var testsType = lib.GetType("MailSender.Addin.Texts");
                object texts = Activator.CreateInstance(testsType);
                return new Tests
                {
                    Title = testsType.GetProperty("Title")?.GetValue(texts) as string,
                    Description = testsType.GetProperty("Description")?.GetValue(texts) as string,
                    Status = testsType.GetProperty("Status")?.GetValue(texts) as string,
                };
            }
            catch (Exception e)
            {
                MessageBox.Show($"Не удалось загрузить файл дополнений \"MailSender.Addin.dll\"\n ошибка: {e.Message}", "Загрузка дополнения", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }
        private class Tests : ITextsAddin
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Status { get; set; }
        }
    }
}
