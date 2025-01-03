using MyHotelApp.Interfaces;
using MyHotelApp.Models;

namespace MyHotelApp.Services.MenuHandlers
{
    public class RoomMenuHandler
    {
        private readonly InputService _inputService;
        private readonly RoomManagementService _roomManagementService;
        private readonly IRoomService _roomService;

        public RoomMenuHandler(
            InputService inputService,
            RoomManagementService roomManagementService,
            IRoomService roomService)
        {
            _inputService = inputService;
            _roomManagementService = roomManagementService;
            _roomService = roomService;
        }

        public void HandleUpdateRoom()
        {
            _roomManagementService.DisplayActiveRooms();
            int roomId = _inputService.GetRoomIdFromUser("Ange rumID för att uppdatera rummet:");
            var room = _roomService.GetAllRooms().FirstOrDefault(r => r.Id == roomId);

            if (room == null)
            {
                decimal newPrice = _inputService.GetDecimal("Ange nytt pris: ", room.Price);
                double? newSize = null;
                if (room is DoubleRoom doubleRoom)
                {
                    newSize = _inputService.GetDouble("Ange ny storlek: ", doubleRoom.Size);
                }

                _roomService.UpdateRoom(roomId, newPrice, newSize);
                Console.WriteLine("Rummet har uppdaterats!");
            }
            else
            {
                Console.WriteLine("Rummet finns inte.");
            }
        }
    }
}
