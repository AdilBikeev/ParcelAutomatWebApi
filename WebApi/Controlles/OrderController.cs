﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApi.Data.OrderRepo;
using WebApi.Data.ParcelAutomatRepo;
using WebApi.Models;

namespace WebApi.Controlles
{
    [ApiController]
    [Route("api/order")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IParcelAutomatRepo _postamatRepo;

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

           [Description("ошибка запроса")]
           RequestError = 400,

           [Description("запрещено")]
           Forbidden = 403,

           [Description("не найден")]
           NotFound = 404,
        }

        public OrderController(IOrderRepo orderRepo, IParcelAutomatRepo postamatRepo)
        {
            this._orderRepo = orderRepo;
            this._postamatRepo = postamatRepo;
        }

        /// <summary>
        /// Запрос на получение данных заказа.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <response code="200">Информация по заказу найдена.</response>
        /// <response code="400">Процесс нахождения заказа завершился ошибкой.</response>
        // GET api/order/{orderId}
        [HttpGet]
        [Route("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public JsonResult GetOrder(int orderId)
        {
            try
            {
                this.LogConsole($"orderId={orderId}");
                var order = this._orderRepo.GetOrder(orderId);
                if (order is null)
                {
                    return new JsonResult(ResponseCode.NotFound.ToName());
                }

                return new JsonResult(order);
            }
            catch (Exception exc)
            {
                this.LogConsole($"exception={exc.Message}");
                return new JsonResult(ResponseCode.RequestError.ToName());
            }
        }

        /// <summary>
        /// Запрос на отмену заказа.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <response code="200">Заказ успешно отменён.</response>
        /// <response code="400">Процесс отмены заказа завершился ошибкой.</response>
        // PUT api/order/cancel/{orderId}
        [HttpPut]
        [Route("cancel/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public JsonResult CancelOrder(int orderId)
        {
            try
            {
                this.LogConsole($"orderId={orderId}");
                return new JsonResult(this._orderRepo.CancelOrder(orderId).ToName());
            }
            catch (Exception exc)
            {
                this.LogConsole($"exception={exc.Message}");
                return new JsonResult(ResponseCode.RequestError.ToName());
            }
        }

        /// <summary>
        /// Запрос на создание заказа.
        /// </summary>
        /// <param name="order">Данные заказа.</param>
        /// <response code="200">Заказ успешно создан.</response>
        /// <response code="400">Процесс добавления заказа завершился ошибкой.</response>
        /// <response code="403">Запрещено регистрировать заказ на закрытый постамат.</response>
        // POST api/order/create
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public JsonResult CreateOrder(Order order)
        {
            var postamat = this._postamatRepo.GetParcelAutomat(order.NumberPostDeliver.ToString());

            if (postamat is null)
            {
                return new JsonResult(ResponseCode.NotFound.ToName());
            }
            else if (!postamat.IsOpen)
            {
                return new JsonResult(ResponseCode.Forbidden.ToName());
            }
            
            return ProccessingRequest(order, this._orderRepo.CreateOrder);
        }

        /// <summary>
        /// Запрос на обновление данных заказа.
        /// </summary>
        /// <param name="order">Данные заказа.</param>
        /// <response code="200">Заказ успешно обновлен.</response>
        /// <response code="400">Процесс обновления заказа завершился ошибкой.</response>
        // PUT api/order/update
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public JsonResult UpdateOrder(Order order) => ProccessingRequest(order, this._orderRepo.UpdateOrder);
    }
}
