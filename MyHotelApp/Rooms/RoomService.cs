using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Rooms
{
    public class RoomService
    {
        private List<Room> rooms = new List<Room>();

        public List<Room> GetActiveRoom() => rooms
            .Where(r => r.IsActive)
            .ToList();

        public List<Room> GetInactiveRooms() => rooms
            .Where(r => !r.IsActive)
            .ToList();

        public bool IsRoomAvalible(int roomId)
        {
            // lägg till något för att kolla om rummet är tillgängligt
            return true;
        }
    }
}
