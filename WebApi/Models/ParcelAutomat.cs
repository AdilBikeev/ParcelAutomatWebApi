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
        public readonly string _numberPostDeliver;

        /// <summary>
        /// Адрес поставмата.
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Статус поставмата.
        /// </summary>
        /// <value>true - если в рабочем состоянии (не закрыт).</value>
        [Required]
        public bool IsOpen { get; set; } = _isOpenDefault;

        public ParcelAutomat(string pastomatId)
        {
            this._numberPostDeliver = pastomatId;
        }
    }
}
