using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using WebApi.Controlles;
using Extensions;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models 
{
    /// <summary>
    /// Статусы заказа.
    /// </summary>
    public enum OrderStatus
    {
        [Description("Зарегистрирован")]
        Registered = 1,

        [Description("Принят на складе")]
        Accepted,

        [Description("Выдан курьеру")]
        Courier,

        [Description("Доставлен в постамат")]
        InParcelAutomat,

        [Description("Доставлен получателю")]
        DeliveredRecipient,

        [Description("Отменен")]
        Cancelled,
    }

    /// <summary>
    /// Заказ.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Статус заказа по умолчанию.
        /// </summary>
        private const int _statusDefault = (int)OrderStatus.Registered;

        /// <summary>
        /// Максимлаьное кол-во товаров в одном заказе.
        /// </summary>
        private const int _maxOrdersStructurece = 10;

        /// <summary>
        /// Регулярное выражение для валидации номера телефона.
        /// </summary>
        private Regex phoneReg = new Regex(@"^\+7\d{3}-\d{3}-\d{2}-\d{2}$");

        private int _status = _statusDefault;
        private decimal _price;
        private string[] _ordersStructurece;
        private string _phoneRecipient;

        /// <summary>
        /// Номер заказа
        /// </summary>
        [Key]
        public int Number { get; set; }

        /// <summary>
        /// Статус заказа.
        /// </summary>
        [Required]
        public int Status
        {
            get => this._status;
            set 
            {
                if (Enum.IsDefined(typeof(OrderStatus), value))
                {
                    this._status = value;
                }
                else
                {
                    throw new KeyNotFoundException($"Не существует статуса заказа со значением {value} !");
                }
            }
        }

        /// <summary>
        /// Состав заказа: массив товаров.
        /// </summary>
        [Required]
        public string[] OrdersStructure
        {
            get => this._ordersStructurece;
            set
            {
                if (value != null)
                {
                    if (value.Length <= _maxOrdersStructurece)
                    {
                        this._ordersStructurece = value;
                    }
                    else
                    {
                        Console.WriteLine($"Кол-во товаров в 1-ом заказе не должно превышать {_maxOrdersStructurece}");
                        throw new ValidationException("Ошибка запроса");
                    }
                }
            }
        }

        /// <summary>
        /// Стоимость заказа.
        /// </summary>
        [Required]
        public decimal Price
        {
            get => this._price;
            set
            {
                if (value > 0)
                {
                    this._price = value;
                }
                else
                {
                    throw new ValidationException("Стоимость заказа должна быть числом положительным !");
                }
            }
        }

        /// <summary>
        /// Номер постамата доставки.
        /// </summary>
        [Required]
        public int NumberPostDeliver { get; set; }

        /// <summary>
        /// Номер телефона получателя.
        /// </summary>
        /// <value>Номер телефона в формате +7XXX-XXX-XX-XX</value>
        [Required]
        public string PhoneRecipient
        {
            get => this._phoneRecipient;
            set
            {
                if (this.phoneReg.IsMatch(value))
                {
                    this._phoneRecipient = value;
                }
                else
                {
                    Console.WriteLine("Не корректный номер телефона");
                    throw new ValidationException(
                        OrderController.ResponseCode
                                               .RequestError
                                               .ToName()
                    );
                }
            }
        }

        /// <summary>
        /// ФИО получателя.
        /// </summary>
        [Required]
        public string FIO  { get; set; }
    }
}