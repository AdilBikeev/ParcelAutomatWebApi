using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using static WebApi.Controlles.OrderController;

namespace WebApi.Data.OrderRepo
{
    public class MockOrderRepo : IOrderRepo
    {
        /// <summary>
        /// Список заказов.
        /// </summary>
        public IList<Order> orders = new List<Order>
        {
            new Order() 
            { 
                FIO = "Иванов Иван Иванович", 
                Number = 0,
                NumberPostDeliver = 0, 
                OrdersStructure = new string [] { "Кредитная карта Тинькофф", "Шампунь", "Электробритва" },
                PhoneRecipient = "+7800-555-35-35",
                Price = 5000,
                Status = (int)OrderStatus.Registered
            }
        };

        public ResponseCode CreateOrder(Order order)
        {
            if (this.orders.FirstOrDefault(item => item.Number == order.Number) is null)
            {
                this.orders.Add(order);

                return ResponseCode.Ok;
            }

            return ResponseCode.OrderAlreadyExist;
        }

        public ResponseCode UpdateOrder(Order updOrder)
        {
            Order currOrder = this.orders.FirstOrDefault(item => item.Number == updOrder.Number);
            if (currOrder != null)
            {
                currOrder.FIO = updOrder.FIO;
                currOrder.NumberPostDeliver = updOrder.NumberPostDeliver;
                currOrder.OrdersStructure = updOrder.OrdersStructure;
                currOrder.PhoneRecipient = updOrder.PhoneRecipient;
                currOrder.Price = updOrder.Price;
                currOrder.Status = updOrder.Status;

                return ResponseCode.Ok;
            }

            return ResponseCode.OrderNotFound;
        }
    }
}
