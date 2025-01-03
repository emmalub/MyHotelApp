using MyHotelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Interfaces
{
    public interface IRoomService
    {
        void AddRoom(Room room);
        void CreateRoom();
        void UpdateRoom(int roomId, decimal newPrice, double? newSize = null);
        void DeleteRoom(int id);
        List<Room> GetAllRooms(bool includeInactive = false);
        List<Room> GetActiveRoom();
        List<Room> GetInactiveRoom();
        Room GetRoomById(int id);

    }
}
