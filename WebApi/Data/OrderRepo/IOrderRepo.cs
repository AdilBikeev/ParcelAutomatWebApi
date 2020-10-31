using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using static WebApi.Controlles.OrderController;

namespace WebApi.Data.OrderRepo
{
    public interface IOrderRepo
    {
        /// <summary>
        /// Создает заказ.
        /// </summary>
        /// <param name="order">Данные заказа.</param>
        ResponseCode CreateOrder(Order order);

        /// <summary>
        /// Изменяет данные заказа.
        /// </summary>
        /// <param name="updOrder">Обновленные данные заказа.</param>
        ResponseCode UpdateOrder(Order updOrder);

        /// <summary>
        /// Возвращает информацию по orderId.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        Order GetOrder(int orderId);

        /// <summary>
        /// Отменяет заказ по orderId.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        ResponseCode CancelOrder(int orderId);
    }
}
