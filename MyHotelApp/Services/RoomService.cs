using MyHotelApp.Data;
using MyHotelApp.Models;
using MyHotelApp.Interfaces;
using Spectre.Console;
using MyHotelApp.Services.MenuHandlers;


namespace MyHotelApp.Services;

public class RoomService : IRoomService
{
    private readonly HotelDbContext _context;
    private readonly InputService _inputService; 
    private readonly RoomMenuHandler _roomMenuHandler;

    public RoomService(HotelDbContext context, InputService inputService)
    {
        _context = context;
        _inputService = inputService;
    }

    public void AddRoom(Room room)
    {
        _context.Rooms.Add(room);
        _context.SaveChanges();
    }
    public List<Room> GetActiveRoom() => _context.Rooms
        .Where(r => r.IsActive)
        .ToList();

    public List<Room> GetInactiveRoom() => _context.Rooms
        .Where(r => !r.IsActive)
        .ToList();

    public void CreateRoom()
    {
        Console.Write("Vilken typ av rum vill du lägga till? (1: Enkelrum, 2: Dubbelrum) ");
        int choice = int.Parse(Console.ReadLine());

        Room newRoom = choice switch
        {
            1 => new SingleRoom { Price = 600, IsActive = true },
            2 => CreateDoubleRoom(),
            _ => throw new ArgumentException("Ogiltigt val")
        };

        AddRoom(newRoom);
    }

    private DoubleRoom CreateDoubleRoom()
    {
        Console.Write("Ange storlek på rummet i kvm: ");
        double size = double.Parse(Console.ReadLine());

        if (size < 10 || size > 120)
        {
            Console.WriteLine("Ogiltig storlek. Storleken måste vara mellan 10 och 120 kvm");
            return null;
        }

        int maxExtraBeds = (int)(size / 20);

        return new DoubleRoom
        {
            Price = 1000,
            Size = size,
            MaxExtraBeds = maxExtraBeds,
            IsActive = true
        };
    }
    public void UpdateRoom(int roomId, decimal newPrice, double? newSize = null)
    {
        var room = _context.Rooms.Find(roomId);
        if (room != null)
        {
            room.Price = newPrice;

            if (room is DoubleRoom doubleRoom)
            {
                doubleRoom.Size = newSize.Value;
            }
            _context.SaveChanges();
        }
    }

    public void DeleteRoom(int roomId)
    {
        var room = _context.Rooms.Find(roomId);
        if (room != null)
        {
            room.IsActive = false;
            _context.SaveChanges();
        }
    }

    public List<Room> GetAllRooms(bool includeInactive = false)
    {
        return includeInactive
            ? _context.Rooms.ToList()
            : _context.Rooms.Where(r => r.IsActive).ToList();
    }
    public List<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
    {
        var allRooms = _context.Rooms.ToList();
        var availableRooms = new List<Room>();
        foreach (var room in allRooms)
        {
            if (_roomMenuHandler.IsRoomAvalible(room.Id, checkInDate, checkOutDate))
            {
                availableRooms.Add(room);
            }
        }
        return availableRooms;
    }
    public Room GetRoomById(int roomId)
    {
        return _context.Rooms.Find(roomId);
    }
}
