using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data.ParcelAutomatRepo
{
    public class MockParcelAutomatRepo : IParcelAutomatRepo
    {
        /// <summary>
        /// Список постаматов.
        /// </summary>
        private readonly IList<ParcelAutomat> _parcelAutomats = ImmutableList.Create<ParcelAutomat>
        (
            new ParcelAutomat()
            {
                NumberPostDeliver = 0,
                Address = "г. Москва, ул. Пр. Вернадского 78"
            }    
        );

        public ParcelAutomat GetParcelAutomat(int postamatId) => this._parcelAutomats.FirstOrDefault(item => item.NumberPostDeliver == postamatId);
    }
}
