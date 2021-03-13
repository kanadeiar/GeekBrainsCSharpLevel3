using System;
using System.ComponentModel.DataAnnotations;
using Task3.Models.Base;

namespace Task3.Models
{
    /// <summary> Заказ </summary>
    class Order : Entity
    {
        private DateTime _DateTime;
        /// <summary> Дата заказа </summary>
        [Required]
        public DateTime DateTime
        {
            get => _DateTime;
            set => Set(ref _DateTime, value);
        }
        private int _Count;
        /// <summary> Количество билетов </summary>
        [Required]
        public int Count
        {
            get => _Count;
            set => Set(ref _Count, value);
        }
        /// <summary> Киносеанс этого заказа </summary>
        [Required]
        public virtual MovieShow MovieShow { get; set; }
    }
}
