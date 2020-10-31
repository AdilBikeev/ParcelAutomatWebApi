using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data.OrderRepo
{
    public interface IOrderRepo
    {
        /// <summary>
        /// Создает заказ.
        /// </summary>
        /// <param name="order">Данные заказа.</param>
        /// <returns>true - если заказ успешно создан.</returns>
        bool CreateOrder(Order order);
    }
}
