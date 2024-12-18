using MyHotelApp.Data;
using MyHotelApp.Models;
using MyHotelApp.Services.Interfaces;

namespace MyHotelApp.Services;

public class RoomService : Interfaces.RoomService
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

    public List<Room> GetInactiveRoom() => rooms
        .Where(r => !r.IsActive)
        .ToList();

    public void AddRoom(Room room)
    {
        _context.Rooms.Add(room);
        _context.SaveChanges();
    }

    public void CreateRoom()
    {
        Console.Write("Vilken typ av rum vill du lägga till? (1: Enkelrum, 2: Dubbelrum) ");
        int choice = int.Parse(Console.ReadLine());

        Room newRoom = choice switch
        {
            1 => new SingleRoom { Price = 600 },
            2 => CreateDoubleRoom(),
            _ => throw new ArgumentException("Ogiltigt val")
        };

        var room = new Room()
        {
            RoomType = newRoom.RoomType,
            IsActive = true,
            Price = newRoom.Price
        };
            
        AddRoom(room);
    }

    private DoubleRoom CreateDoubleRoom()
    {
        Console.Write("Ange storlek på rummet i kvm: ");
        double size = double.Parse(Console.ReadLine());

        int extrabeds = (int)(size / 20);

        return new DoubleRoom
        {
            Price = 1000,
            Size = size,
            ExtraBeds = extrabeds,
        };
    }

    public void UpdateRoom(Room room)
    {
        var existingRoom = _context.Rooms.Find(room.Id);
        if (existingRoom != null)
        {
            existingRoom.Price = room.Price;
            if (existingRoom is DoubleRoom doubleroom && room is DoubleRoom updatedDoubleRoom)
            {
                doubleroom.ExtraBeds = updatedDoubleRoom.ExtraBeds;
            }
            _context.SaveChanges();
        }
    }

    public void SoftDeleteRoom(int roomId)
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
    public bool IsRoomAvalible(int roomId, DateTime checkInDate, DateTime checkOutDate)
    {
        if (checkInDate >= checkOutDate)
            throw new ArgumentException("Startdatum måste vara tidigare än slutdatum");

        return !_context.Bookings.Any(b =>
            b.RoomId == roomId &&
            b.IsActive &&
            b.CheckInDate <= checkOutDate && b.CheckOutDate >= checkInDate);
    }

    public void DisplayActiveRooms()
    {
        var activeRooms = GetActiveRoom();

        if (activeRooms.Any())
        {
            Console.WriteLine("Aktiva rum: ");
            foreach (var room in activeRooms)
            {
                Console.WriteLine($"Rum {room.Id}: {room.Price} SEK per natt, {room.IsActive}");
            }
        }
    }
}
