using MyHotelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Interfaces
{
    internal interface IRoomService
    {
        void SeedRooms();
        void AddRoom(Room room);
        Room GetRoomById(int id);
        List<Room> GetAllRooms(bool includeInactive = false);
        void UpdateRoom(Room room);
        void DeleteRoom(int id);
    }
}
