using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApi.Data.OrderRepo;
using WebApi.Models;

namespace WebApi.Controlles
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;

        /// <summary>
        /// Логгирует сообщение в консоль. 
        /// </summary>
        private void LogConsole(string info) => Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(OrderController)}: {info}");

        /// <summary>
        /// Коды ответа на запросы к контроллеру <see cref="OrderController"/>
        /// </summary>
        public enum ResponseCode
        {
           [Description("Заказ успешно создан")]
           Ok = 0,

           [Description("ошибка запроса")]
           RequestError = 5
        }

        public OrderController(IOrderRepo orderRepo)
        {
            this._orderRepo = orderRepo;
        }

        /// <summary>
        /// Запрос на создание заказа.
        /// </summary>
        /// <param name="order">Данные заказа.</param>
        /// <response code="200">Заказ успешно создан.</response>
        /// <response code="400">Процесс добавления заказа завершился ошибкой.</response>
        // POST api/order/create
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> CreateOrder(Order order)
        {
            try
            {
                this.LogConsole(JObject.FromObject(order).ToString());

                if (this._orderRepo.CreateOrder(order))
                {
                    return Ok(ResponseCode.Ok.ToName());
                }
                else
                {
                    return BadRequest(ResponseCode.RequestError.ToName());
                }
            }
            catch (Exception exc)
            {
                this.LogConsole($"exception={exc.Message}");
                return BadRequest(ResponseCode.RequestError.ToName());
            }
        }
    }
}
