using MyHotelApp.Services.SpecialOffers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services.SpecialOffers
{
    public class DeleteSpecialOffer : IDeleteSpecialOffer
    {
        public void Delete(string offerName, int offerId)
        {
            // SKRIV KOD FÖR ATT TA BORT ERBJUDANDE 
            Console.WriteLine($"Erbjudandet {offerName} med ID: {offerId} har tagits bort!");
        }
    }
}
