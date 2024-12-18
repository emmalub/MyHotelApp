using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Models
{
    public class SpecialOffer
    {
        public int Id { get; set; }
        public string OfferName { get; set; }
        public string OfferType { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public decimal DiscountAmount { get; set; } 

    }
}
