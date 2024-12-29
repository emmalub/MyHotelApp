using MyHotelApp.Data;
using MyHotelApp.Models;
using MyHotelApp.Interfaces;
using Spectre.Console;

namespace MyHotelApp.Services;

public class RoomService : IRoomService
{
    private readonly HotelDbContext _context;

    public RoomService(HotelDbContext context)
    {
        _context = context;
    }

    public List<Room> GetActiveRoom() => _context.Rooms
        .Where(r => r.IsActive)
        .ToList();

    public List<Room> GetInactiveRoom() => _context.Rooms
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

        int extrabeds = (int)(size / 20);

        return new DoubleRoom
        {
            Price = 1000,
            Size = size,
            ExtraBeds = extrabeds,
            IsActive = true
        };
    }

    public void UpdateRoom()
    {
        DisplayActiveRooms();
        Console.WriteLine();
        Console.Write("Ange ID för det rum du vill uppdatera: ");
        int roomId = int.Parse(Console.ReadLine());

        var room = _context.Rooms.Find(roomId);
        if (room == null)
        {
            Console.WriteLine("Rummet finns inte.");
            return;
        }

        Console.WriteLine($"Uppdaterar rum {room.Id}");
        Console.Write("Ange nytt pris: (nuvarande pris: " + room.Price + "): ");
        room.Price = decimal.Parse(Console.ReadLine());

        if (room is DoubleRoom doubleRoom)
        {
            Console.Write("Ange ny storlek: (nuvarande storlek: " + doubleRoom.Size + "): ");
            doubleRoom.Size = double.Parse(Console.ReadLine());
        }
        _context.SaveChanges();
        Console.WriteLine("Rummet har uppdaterats!");
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

    public void ActivateRoom(int roomId)
    {
        int id = AnsiConsole.Prompt(
        new TextPrompt<int>("Ange [green]rumID[/] för att återaktivera rummet:")
            .PromptStyle("green")
            .Validate(value => value > 0 ? ValidationResult.Success() : ValidationResult.Error("Ange ett giltigt rumID"))
    );

        var room = _context.Rooms.Find(id);
        if (room != null)
        {
            room.IsActive = true;
            _context.SaveChanges();
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Ingen åtgärd utfördes.[/]");
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

    public void DisplayActiveRooms()
    {
        var activeRooms = GetActiveRoom();

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
        var activeRooms = GetInactiveRoom();

        if (activeRooms.Any())
        {
            Console.WriteLine("Inaktiva rum: ");
            foreach (var room in activeRooms)
            {
                Console.WriteLine($"Rum {room.Id}: {room.Price} SEK per natt, {room.IsActive}");
            }
        }
        else
        {
            Console.WriteLine("Inga inaktiva rum hittades");
        }
    }

    public void SeedRooms()
    {
        if (!_context.Rooms.Any())
        {
            var rooms = new List<Room>
        {
            new SingleRoom { Price = 600, IsActive = true },
            new DoubleRoom { Price = 1000, Size = 30, ExtraBeds = 1, IsActive = true },
            new SingleRoom { Price = 500, IsActive = false },
            new DoubleRoom { Price = 1000, Size = 45, ExtraBeds = 2, IsActive = true }
        };

            _context.Rooms.AddRange(rooms);
            _context.SaveChanges();
        }
    }
    public Room GetRoomById(int roomId)
    {
        return _context.Rooms.Find(roomId);
    }
}
