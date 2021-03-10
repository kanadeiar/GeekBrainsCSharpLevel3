using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using MailSender.lib.Models.Base;

namespace MailSender.lib.Models
{
    /// <summary> Сервер </summary>
    public class Server : Entity, IDataErrorInfo
    {
        private string _name;
        /// <summary> Имя сервера </summary>
        [Required, MaxLength(30)]
        public string Name
        {
            get => _name; 
            set => Set(ref _name, value);
        }
        private string _address;
        /// <summary> Адрес сервера </summary>
        [Required, MaxLength(30)]
        public string Address
        {
            get => _address; 
            set => Set(ref _address, value);
        }
        private int _port;
        /// <summary> Порт сервера </summary>
        [Required, DefaultValue(25)]
        public int Port
        {
            get => _port; 
            set => Set(ref _port, value);
        }
        private bool _useSsl;
        /// <summary> Шифрование </summary>
        [Required, DefaultValue(false)]
        public bool UseSsl
        {
            get => _useSsl; 
            set => Set(ref _useSsl, value);
        }
        private string _login;
        /// <summary> Логин пользователя </summary>
        [Required, MaxLength(30)]
        public string Login
        {
            get => _login; 
            set => Set(ref _login, value);
        }
        private string _password;
        /// <summary> Пароль пользователя </summary>
        [Required, MaxLength(20)]
        public string Password
        {
            get => _password; 
            set => Set(ref _password, value);
        }
        private string _description;
        /// <summary> Описание сервера </summary>
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
                        if (name is null) return "Имя сервера не может быть пустой строкой";
                        if (name.Length < 3) return "Имя сервера не может быть короче трех символов";
                        if (name.Length > 30) return "Имя сервера не может быть длиннее 30 сиволов";
                        return null;
                    case nameof(Address):
                        var address = Address;
                        if (address is null) return "Адрес сервера не может быть пустой строкой";
                        if (address.Length < 2) return "Адрес сервера не может быть короче двух символов";
                        if (address.Length > 30) return "Адрес сервера не может быть длиннее 30 символов";
                        if (! new Regex(@"^(\w+\.)*\w+[A-za-z\d]+$").IsMatch(address) ) 
                            return "Строка адреса сервера имеет неверный формат";
                        return null;
                    case nameof(Port):
                        var port = Port;
                        if (port < 1) return "Значение порта сервера не может быть меньше одного";
                        if (port > 9999) return "Значение порта сервера не может быть больше 9999";
                        return null;
                    case nameof(Login):
                        var login = Login;
                        if (login is null) return "Значение логина пользователя не может быть пустой строкой";
                        if (login.Length < 3) return "Логин пользователя не может быть короче трех символов";
                        if (login.Length > 30) return "Логин пользователя не может быть длиннее 30 символов";
                        if (! new Regex(@"^\w+[A-za-z\d]+$").IsMatch(login) ) 
                            return "Логин пользователя не может быть таким простым";
                        return null;
                    case nameof(Password):
                        var password = Password;
                        if (password is null) return "Значение пароля пользователя не может быть пустой строкой";
                        if (password.Length > 20) return "Значение пароля не может быть длиннее 20 символов";
                        if (password.Length < 6) return "Значение пароля не может быть короче 6 символов";
                        if (!new Regex(@"^\w+[A-za-z\d]+$").IsMatch(password)) 
                            return "Пароль не может быть таким простым";
                        return null;
                    case nameof(Description):
                        var description = Description;
                        if (description is null) return null;
                        if (description.Length > 250) return "Описание не может быть длиннее 250 сиволов";
                        if (description.Contains("Туфта", StringComparison.CurrentCultureIgnoreCase))
                            return "Запрещено вводить такое слово как \"Туфта\"";
                        return null;
                    default:
                        return null;
                }
            }
        }

        #endregion
    }
}
