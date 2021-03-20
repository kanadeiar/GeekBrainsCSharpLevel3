using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MailSender.lib.Models.Base;

namespace MailSender.lib.Models
{
    /// <summary> Сообщение </summary>
    public class Message : Entity, IDataErrorInfo
    {
        private string _subject;
        /// <summary> Заголовок </summary>
        [Required, MaxLength(30)]
        public string Subject
        {
            get => _subject; 
            set => Set(ref _subject, value);
        }
        private string _text;
        /// <summary> Само сообщение </summary>
        [Required, MaxLength(250)]
        public string Text
        {
            get => _text; 
            set => Set(ref _text, value);
        }

        #region Валидация

        [NotMapped]
        public string Error => null;
        [NotMapped]
        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case nameof(Subject):
                        var subject = Subject;
                        if (subject is null) return "Заголовок не может быть пустой строкой";
                        if (subject.Length < 2) return "Заголовок не может быть короче двух символов";
                        if (subject.Length > 50) return "Заголовок не может быть длиннее 30 символов";
                        if (subject.Contains("Спам", StringComparison.CurrentCultureIgnoreCase))
                            return "Нельзя рассылать спам!";
                        return null;
                    case nameof(Text):
                        var text = Text;
                        if (text is null) return "Тело сообщения не может быть пустой строкой";
                        if (text.Length < 2) return "Текст сообщения не может быть короче двух симоволов";
                        if (text.Length > 250) return "Текст сообщения не может быть длиннее 250 симоволов";
                        if (text.Contains("Спам", StringComparison.CurrentCultureIgnoreCase))
                            return "Нельзя рассылать спам!";
                        return null;
                    default:
                        return null;
                }
            }
        }

        #endregion
    }
}
