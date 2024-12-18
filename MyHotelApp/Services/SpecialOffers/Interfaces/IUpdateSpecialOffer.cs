using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services.SpecialOffers.Interfaces
{
    public interface IUpdateSpecialOffer
    {
        void Update(int offerId, string newDescription, decimal newDiscountPercentage);
    }
}
