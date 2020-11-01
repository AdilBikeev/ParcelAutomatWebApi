using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data.ParcelAutomatRepo
{
    public interface IParcelAutomatRepo
    {
        /// <summary>
        /// Возвращает инфрмацию о постамате по его идентификатору.
        /// </summary>
        /// <param name="postamatId">Идентификатор постамата.</param>
        public ParcelAutomat GetParcelAutomat(string postamatId);
    }
}
