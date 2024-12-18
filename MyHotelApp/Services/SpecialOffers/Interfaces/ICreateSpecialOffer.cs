using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services.SpecialOffers.Interfaces
{
    public interface ICreateSpecialOffer
    {
        void Create(string offerName, int offerId, string offerType, DateTime validFrom, DateTime validTo, decimal discountAmount);
    }
}
