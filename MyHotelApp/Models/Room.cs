using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
