using System.ComponentModel.DataAnnotations;
using Task4.Model.Base;

namespace Task4.Model
{
    public class Person : Entity
    {
        private string _SNP;
        /// <summary> Ф.И.О. </summary>
        [Required, MaxLength(100)]
        public string SNP
        {
            get => _SNP;
            set => Set(ref _SNP, value);
        }
        private string _Address;
        /// <summary> Адрес </summary>
        [Required, MaxLength(60)]
        public string Address
        {
            get => _Address;
            set => Set(ref _Address, value);
        }
        private string _Phone;
        /// <summary> Телефон </summary>
        [Required, MaxLength(20)]
        public string Phone
        {
            get => _Phone;
            set => Set(ref _Phone, value);
        }
    }
}
