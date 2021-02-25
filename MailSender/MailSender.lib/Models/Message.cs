using MailSender.lib.Models.Base;

namespace MailSender.lib.Models
{
    /// <summary> Сообщение </summary>
    public class Message : Model
    {
        public int Id { get; set; }
        private string _subject;
        /// <summary> Заголовок </summary>
        public string Subject
        {
            get => _subject; 
            set => Set(ref _subject, value);
        }
        private string _text;
        /// <summary> Само сообщение </summary>
        public string Text
        {
            get => _text; 
            set => Set(ref _text, value);
        }
    }
}
