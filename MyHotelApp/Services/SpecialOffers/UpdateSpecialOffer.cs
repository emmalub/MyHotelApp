using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHotelApp.Data;
using MyHotelApp.Services.SpecialOffers.Interfaces;


namespace MyHotelApp.Services.SpecialOffers
{
    internal class UpdateSpecialOffer : IUpdateSpecialOffer
    {
        private readonly HotelDbContext _context;

        public UpdateSpecialOffer(HotelDbContext context)
        {
            _context = context;
        }
        public void Update(int offerId, string newDescription, decimal newDiscountPercentage)
        { 
            var offer = _context.SpecialOffer.First(o => o.Id == offerId);
            if (offer == null)
            {
                throw new Exception("Specialerbjudande hittades inte");
            }
            offer.Description = newDescription;
            offer.DiscountAmount = newDiscountPercentage;

            _context.SaveChanges();
        }

    }
}
