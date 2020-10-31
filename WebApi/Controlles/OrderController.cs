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
    [ApiController]
    [Route("api/order")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;

        /// <summary>
        /// Логгирует сообщение в консоль. 
        /// </summary>
        private void LogConsole(string info) => Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(OrderController)}: {info}");


        /// <summary>
        /// Обработка запроса.
        /// </summary>
        /// <param name="order">Данные заказа.</param>
        /// <param name="proccess">Функция для обработки заказа.</param>
        private JsonResult ProccessingRequest(Order order, Func<Order, ResponseCode> proccess)
        {
            try
            {
                this.LogConsole(JObject.FromObject(order).ToString());

                return new JsonResult(proccess(order).ToName());
            }
            catch (Exception exc)
            {
                this.LogConsole($"exception={exc.Message}");
                return new JsonResult(ResponseCode.RequestError.ToName());
            }
        }

        /// <summary>
        /// Коды ответа на запросы к контроллеру <see cref="OrderController"/>
        /// </summary>
        public enum ResponseCode
        {
           [Description("Операция успешно проведена")]
           Ok = 0,

           [Description("Заказ с данным номером уже существует")]
           OrderAlreadyExist = 5,

           [Description("не найден")]
           OrderNotFound = 404,

           [Description("ошибка запроса")]
           RequestError = 400
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
        public JsonResult CreateOrder(Order order) => ProccessingRequest(order, this._orderRepo.CreateOrder);

        /// <summary>
        /// Запрос на обновление данных заказа.
        /// </summary>
        /// <param name="order">Данные заказа.</param>
        /// <response code="200">Заказ успешно обновлен.</response>
        /// <response code="400">Процесс обновления заказа завершился ошибкой.</response>
        // POST api/order/update
        [HttpPost]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public JsonResult UpdateOrder(Order order) => ProccessingRequest(order, this._orderRepo.UpdateOrder);
    }
}
