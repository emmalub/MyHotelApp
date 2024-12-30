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
        void AddRoom(Room room);
        void CreateRoom();
        void UpdateRoom(int roomId, decimal newPrice, double? newSize = null);
        void DeleteRoom(int id);
        List<Room> GetAllRooms(bool includeInactive = false);
        Room GetRoomById(int id);
     
    }
}
