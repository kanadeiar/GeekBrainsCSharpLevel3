using System.ComponentModel;

namespace MailSender.Addin
{
    [Description("Изменения в текстах приложения")]
    public class Texts
    {
        public string Title { get; set; } = "Geekbrains. Домашнее задание №8. Рефлексия.";
        public string Description { get; set; } = "Geekbrains. Домашнее задание №8. Рефлексия.";
        public string Status { get; set; } = "Готово с помощью рефлексии!";
    }
}
