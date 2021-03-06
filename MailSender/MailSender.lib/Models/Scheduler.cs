﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MailSender.lib.Models.Base;

namespace MailSender.lib.Models
{
    public class Scheduler : Entity
    {
        /// <summary> Время отправления письма </summary>
        public DateTime DateTimeSend { get; set; } = DateTime.Now;
        /// <summary> Сервер отправки почты </summary>
        [Required]
        public virtual Server Server { get; set; }
        /// <summary> Отправитель </summary>
        [Required]
        public virtual Sender Sender { get; set; }
        /// <summary> Получатели </summary>
        [Required]
        public virtual IEnumerable<Recipient> Recipients { get; set; }
        /// <summary> Сообщение </summary>
        [Required]
        public virtual Message Message { get; set; }

        public Scheduler()
        {
        }
    }
}
