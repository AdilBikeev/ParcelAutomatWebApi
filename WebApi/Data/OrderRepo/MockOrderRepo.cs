using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

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

        public bool CreateOrder(Order order)
        {
            if (this.orders.FirstOrDefault(item => item.Number == order.Number) is null)
            {
                this.orders.Add(order);

                return true;
            }

            return false;
        }
    }
}
