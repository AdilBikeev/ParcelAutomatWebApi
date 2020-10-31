using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Коды ответа на запросы к контроллеру <see cref="OrderController"/>
        /// </summary>
        public enum ResponseCode
        {
            [Description("ошибка запроса")]
            RequestError = 5
        }
    }
}
