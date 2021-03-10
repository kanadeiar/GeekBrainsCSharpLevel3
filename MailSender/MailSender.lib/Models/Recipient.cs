using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MailSender.lib.Models.Base;

namespace MailSender.lib.Models
{
    /// <summary> Получатель </summary>
    public class Recipient : Entity, IDataErrorInfo
    {
        private string _name;
        /// <summary> Имя </summary>
        [Required, MaxLength(20)]
        public string Name
        {
            get => _name; 
            set => Set(ref _name, value);
        }
        private string _address;
        /// <summary> Адрес email </summary>
        [Required, MaxLength(30)]
        public string Address
        {
            get => _address; 
            set => Set(ref _address, value);
        }
        private string _description;
        /// <summary> Описание </summary>
        [MaxLength(250)]
        public string Description
        {
            get => _description; 
            set => Set(ref _description, value);
        }

        #region Валидация

        [NotMapped]
        string IDataErrorInfo.Error => null;
        [NotMapped]
        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case nameof(Name):
                        var name = Name;
                        if (name is null) return "Имя не может быть пустой строкой";
                        if (name.Length < 2) return "Имя не может быть короче двух символов";
                        if (name.Length > 20) return "Имя не может быть длиннее 20 символов";
                        if (name.Contains("Путин") || name.Contains("Ленин")) return "Имя не может быть таким";
                        return null;
                    case nameof(Address):
                        var addr = Address;
                        if (addr is null) return "Почтовый адрес не может быть пустой строкой";
                        if (addr.Length < 2) return "Почтовый адрес не может быть короче двух символов";
                        if (addr.Length > 30) return "Почтовый адрес не может быть длиннее 30 символов";
                        return null;
                    case nameof(Description):
                        var description = Description;
                        if (description is null) return null;
                        if (description.Length > 250) return "Описание адреса не может быть больше, чем 250 символов";
                        return null;
                    default:
                        return null;
                }
            }
        }

        #endregion
    }
}
