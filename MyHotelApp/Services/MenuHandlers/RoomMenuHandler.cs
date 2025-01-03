using MyHotelApp.Data;
using MyHotelApp.Models;
using Spectre.Console;
using MyHotelApp.Interfaces;

namespace MyHotelApp.Services.MenuHandlers
{
    public class RoomMenuHandler
    {
        private readonly IRoomService _roomService;
        private readonly InputService _inputService;
        private readonly HotelDbContext _context;

        public RoomMenuHandler(IRoomService roomService, InputService inputService, HotelDbContext context)
        {
            _roomService = roomService;
            _inputService = inputService;
            _context = context;
        }

        public void DisplayActiveRooms()
        {
            var activeRooms = _roomService.GetActiveRoom();

            if (activeRooms.Any())
            {
                Console.WriteLine("Aktiva rum: ");
                foreach (var room in activeRooms)
                {
                    Console.WriteLine($"Rum {room.Id}: {room.RoomType} - {room.Price} SEK per natt");
                }
            }
            else
            {
                Console.WriteLine("Inga aktiva rum hittades");
            }
        }
        public void DisplayInactiveRooms()
        {
            var activeRooms = _roomService.GetInactiveRoom();

            if (activeRooms.Any())
            {
                Console.WriteLine("Inaktiva rum: ");
                foreach (var room in activeRooms)
                {
                    Console.WriteLine($"Rum {room.Id}: {room.Price} SEK per natt");
                }
            }
            else
            {
                Console.WriteLine("Inga inaktiva rum hittades");
            }
        }
        public bool IsRoomAvalible(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            if (checkInDate >= checkOutDate)
                throw new ArgumentException("Startdatum måste vara tidigare än slutdatum");

            return !_context.Bookings.Any(b =>
                b.RoomId == roomId &&
                b.IsActive &&
                b.CheckInDate <= checkOutDate && b.CheckOutDate >= checkInDate);
        }
        public void ActivateRoom()
        {
            DisplayActiveRooms();

            int roomId = _inputService.GetRoomIdFromUser("Ange rumID för att aktivera rummet:");

            var room = _context.Rooms.Find(roomId);
            if (room != null)
            {
                room.IsActive = true;
                _context.SaveChanges();
                AnsiConsole.MarkupLine($"[green]Rum {roomId} har återaktiverats![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Ingen åtgärd utfördes.[/]");
            }
        }
        public void HandleUpdateRoom()
        {
            DisplayActiveRooms();
            Console.WriteLine();
            Console.Write("Ange ID för det rum du vill uppdatera: ");
            int roomId = int.Parse(Console.ReadLine());

            var room = _roomService.GetRoomById(roomId);
            if (room == null)
            {
                Console.WriteLine("Rummet finns inte.");
                return;
            }

            Console.WriteLine($"Uppdaterar rum {room.Id}");
            Console.Write("Ange nytt pris: (nuvarande pris: " + room.Price + "): ");
            var newPrice = decimal.Parse(Console.ReadLine());

            double? newSize = null;
            if (room is DoubleRoom doubleRoom)
            {
                Console.Write("Ange ny storlek: (nuvarande storlek: " + doubleRoom.Size + "): ");
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    newSize = double.Parse(input);
                }
            }
            _roomService.UpdateRoom(roomId, newPrice, newSize ?? null);
            Console.WriteLine("Rummet har uppdaterats!");
        }
    }
}
