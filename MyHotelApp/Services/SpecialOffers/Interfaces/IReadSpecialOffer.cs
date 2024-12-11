using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services.SpecialOffers.Interfaces
{
    public interface IReadSpecialOffer
    {
        List<string> GetAllOffers();
    }
}
