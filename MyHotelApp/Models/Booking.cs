using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public virtual Customer Guest { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal Price { get; set; }
        public Invoice Invoice { get; set; }
        public bool IsActive { get; set; }
        public bool IsPaid { get; set; }
        public string? Conditions { get; set; }


    }
}
