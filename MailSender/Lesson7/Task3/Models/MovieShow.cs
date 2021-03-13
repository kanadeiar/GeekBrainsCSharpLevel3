using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Task3.Models.Base;

namespace Task3.Models
{
    /// <summary> Киносеанс </summary>
    class MovieShow : Entity
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
    }
}
