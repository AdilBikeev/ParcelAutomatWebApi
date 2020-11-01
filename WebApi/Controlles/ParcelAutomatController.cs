using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data.ParcelAutomatRepo;

namespace WebApi.Controlles
{
    [ApiController]
    [Route("api/postamat")]
    [Produces("application/json")]
    public class ParcelAutomatController : ControllerBase
    {
        private readonly IParcelAutomatRepo _postamatRepo;

        /// <summary>
        /// Логгирует сообщение в консоль. 
        /// </summary>
        private void LogConsole(string info) => Console.WriteLine($"{DateTime.Now.ToString("dd/mm/yy hh:mm:ss:mm")} {nameof(ParcelAutomatController)}: {info}");

        /// <summary>
        /// Коды ответа на запросы к контроллеру <see cref="ParcelAutomatController"/>
        /// </summary>
        public enum ResponseCode
        {
            [Description("Операция успешно проведена")]
            Ok = 0,

            [Description("ошибка запроса")]
            RequestError = 400,

            [Description("не найден")]
            PostamatNotFound = 404,
        }

        public ParcelAutomatController(IParcelAutomatRepo postamatRepo)
        {
            this._postamatRepo = postamatRepo;
        }

        /// <summary>
        /// Запрос на получение данных заказа.
        /// </summary>
        /// <param name="postamatId">Идентификатор постамата.</param>
        /// <response code="200">Информация по постамату найдена.</response>
        /// <response code="400">Процесс нахождения постамата завершился ошибкой.</response>
        // GET api/postamat/{postamatId}
        [HttpGet]
        [Route("{postamatId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public JsonResult GetPostamat(string postamatId)
        {
            try
            {
                this.LogConsole($"postamatId={postamatId}");
                var order = this._postamatRepo.GetParcelAutomat(postamatId);
                if (order is null)
                {
                    return new JsonResult(ResponseCode.PostamatNotFound.ToName());
                }

                return new JsonResult(order);
            }
            catch (Exception exc)
            {
                this.LogConsole($"exception={exc.Message}");
                return new JsonResult(ResponseCode.RequestError.ToName());
            }
        }
    }
}
