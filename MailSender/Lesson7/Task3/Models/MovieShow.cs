using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Task3.Models.Base;

namespace Task3.Models
{
    /// <summary> Киносеанс </summary>
    class MovieShow : Entity, IDataErrorInfo
    {
        private DateTime _BeginTime;
        /// <summary> Время начала </summary>
        [Required]
        public DateTime BeginTime
        {
            get => _BeginTime;
            set => Set(ref _BeginTime, value);
        }
        private string _Name;
        /// <summary> Название фильма </summary>
        [Required, MaxLength(40)]
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }
        /// <summary> Заказы по этому киносеансу </summary>
        public virtual IEnumerable<Order> Orders { get; set; }

        #region Валидация
        [NotMapped]
        public string Error => null;
        [NotMapped]
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(BeginTime):
                        var dateTime = BeginTime;
                        if (dateTime == default) return "Не задано время начало сеанса";
                        return null;
                    case nameof(Name):
                        var name = Name;
                        if (name is null) return "Название не может быть пустой строкой";
                        if (name.Length > 40) return "Название не может быть длиннее 40 симоволов";
                        return null;
                    default:
                        return null;
                }
            }
        }

        #endregion
    }
}
