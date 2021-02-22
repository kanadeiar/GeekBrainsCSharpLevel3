using MailSender.lib.Models.Base;

namespace MailSender.lib.Models
{
    /// <summary> Получатель </summary>
    public class Recipient : Model
    {
        public int Id { get; set; }
        private string _name;
        /// <summary> Имя </summary>
        public string Name
        {
            get => _name; 
            set => Set(ref _name, value);
        }
        private string _address;
        /// <summary> Адрес email </summary>
        public string Address
        {
            get => _address; 
            set => Set(ref _address, value);
        }
        private string _description;
        /// <summary> Описание </summary>
        public string Description
        {
            get => _description; 
            set => Set(ref _description, value);
        }
    }
}
