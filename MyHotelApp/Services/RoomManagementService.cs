using MyHotelApp.Data;
using MyHotelApp.Interfaces;
using MyHotelApp.Models;
using MyHotelApp.Services.MenuHandlers;
using Spectre.Console;

namespace MyHotelApp.Services
{
    public class RoomManagementService
    {
        private readonly HotelDbContext _context;
        private readonly InputService _inputService;

        public RoomManagementService(
            HotelDbContext hotelDbContext,
            InputService inputService
            )
        {
            _context = hotelDbContext;
            _inputService = inputService;
        }

        public void DisplayActiveRooms()
        {
            var activeRooms = _context.Rooms.Where(r => r.IsActive).ToList();

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
            var activeRooms = _context.Rooms.Where(r => !r.IsActive).ToList();
            if (activeRooms.Any())
            {
                Console.WriteLine("Inaktiva rum: ");
                foreach (var room in activeRooms)
                {
                    Console.WriteLine($"Rum {room.Id}: {room.RoomType} - {room.Price} SEK per natt");
                }
            }
            else
            {
                Console.WriteLine("Inga inaktiva rum hittades");
            }
        }

        public void ActivateRoom()
        {
            DisplayInactiveRooms();

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

        public void DeactivateRoom()
        {
            DisplayActiveRooms();

            int roomId = _inputService.GetRoomIdFromUser("Ange rumID för att inaktivera rummet:");
            var room = _context.Rooms.Find(roomId);
            if (room != null)
            {
                room.IsActive = false;
                _context.SaveChanges();
                AnsiConsole.MarkupLine($"[red]Rum {roomId} har inaktiverats![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Ingen åtgärd utfördes.[/]");
            }
        }
        public List<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            var allRooms = _context.Rooms.ToList();
            var availableRooms = new List<Room>();

            foreach (var room in allRooms)
            {
                if (IsRoomAvalible(room.Id, checkInDate, checkOutDate))
                {
                    availableRooms.Add(room);
                }
            }
            return availableRooms;
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

        public List<Room> GetInactiveRooms()
        {
            return _context.Rooms
                .Where(r => !r.IsActive)
                .ToList();
        }
        public Room GetRoomById(int roomId)
        {
            return _context.Rooms.Find(roomId);
        }
    }
}
