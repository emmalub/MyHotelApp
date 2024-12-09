using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public Customer GuestId { get; set; }
        public Room Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Invoice Price { get; set; }
        public Invoice IsPaid { get; set; }
        public string Conditions { get; set; }


    }
}
