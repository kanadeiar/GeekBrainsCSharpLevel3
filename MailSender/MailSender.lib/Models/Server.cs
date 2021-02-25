using MailSender.lib.Models.Base;

namespace MailSender.lib.Models
{
    /// <summary> Сервер </summary>
    public class Server : Entity
    {
        private string _name;
        /// <summary> Имя сервера </summary>
        public string Name
        {
            get => _name; 
            set => Set(ref _name, value);
        }
        private string _address;
        /// <summary> Адрес сервера </summary>
        public string Address
        {
            get => _address; 
            set => Set(ref _address, value);
        }
        private int _port;
        /// <summary> Порт сервера </summary>
        public int Port
        {
            get => _port; 
            set => Set(ref _port, value);
        }
        private bool _useSsl;
        /// <summary> Шифрование </summary>
        public bool UseSsl
        {
            get => _useSsl; 
            set => Set(ref _useSsl, value);
        }
        private string _login;
        /// <summary> Логин пользователя </summary>
        public string Login
        {
            get => _login; 
            set => Set(ref _login, value);
        }
        private string _password;
        /// <summary> Пароль пользователя </summary>
        public string Password
        {
            get => _password; 
            set => Set(ref _password, value);
        }
        private string _description;
        /// <summary> Описание сервера </summary>
        public string Description
        {
            get => _description; 
            set => Set(ref _description, value);
        }
    }
}
