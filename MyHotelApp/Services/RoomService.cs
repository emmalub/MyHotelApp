using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHotelApp.Data;
using MyHotelApp.Models;

namespace MyHotelApp.Services
{
    public class RoomService
    {
        private readonly HotelDbContext _context;

        public RoomService(HotelDbContext context)
        {
            _context = context;
        }

        private List<Room> rooms = new List<Room>();

        public List<Room> GetActiveRoom() => rooms
            .Where(r => r.IsActive)
            .ToList();

        public List<Room> GetInactiveRooms() => rooms
            .Where(r => !r.IsActive)
            .ToList();

        public bool IsRoomAvalible(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            if (checkInDate >= checkOutDate)
                throw new ArgumentException("Startdatum måste vara tidigare än slutdatum");

            return !_context.Bookings.Any(b =>
                b.RoomId == roomId &&
                b.IsActive &&
                b.CheckInDate <= checkOutDate && b.CheckOutDate >= checkInDate);
        }
    }
}
