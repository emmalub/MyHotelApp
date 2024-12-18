using MyHotelApp.Models;

namespace MyHotelApp.Services.Interfaces
{
    public interface RoomService
    {
        List<Room> GetActiveRoom();
        List<Room> GetInactiveRoom();
        void AddRoom(Room room);
        void CreateRoom();
        void UpdateRoom(Room room);
        void SoftDeleteRoom(int roomId);
        List<Room> GetAllRooms(bool includeInactive = false);
        bool IsRoomAvalible(int roomId, DateTime checkInDate, DateTime checkOutDate);
        void DisplayActiveRooms();
    }
}
