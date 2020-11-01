using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    /// <summary>
    /// Постамат.
    /// </summary>
    public class ParcelAutomat
    {
        /// <summary>
        /// Флаг по умолчанию, указывающий на рабочее состояние постамата.
        /// </summary>
        private const bool _isOpenDefault = true;

        /// <summary>
        /// Номер поставмата.
        /// </summary>
        [Key]
        public int NumberPostDeliver { get; set; }

        /// <summary>
        /// Адрес поставмата.
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Статус поставмата.
        /// </summary>
        [Required]
        [DefaultValue(_isOpenDefault)]
        public bool IsOpen { get; set; }
    }
}
