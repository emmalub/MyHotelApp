using MyHotelApp.Data;
using MyHotelApp.Models;
using MyHotelApp.Services;
using Spectre.Console;

namespace MyHotelApp.Utilities.Menus
{
    public class BookingMenu
    {
        private readonly BookingService _bookingService;
        private readonly InputService _inputService;
        private readonly CustomerService _customerService;
        private readonly RoomService _roomService;
        private readonly InvoiceService _invoiceService;
        private BookingCalendar _bookingCalendar;

        // private readonly IMessageService _messageService;

        //public BookingMenu()
        //{
        //    _inputService = new InputService(); 
        //    var dbContext = new HotelDbContext(); 
        //    _customerService = new CustomerService(dbContext); 
        //    _roomService = new RoomService(dbContext); 
        //    _bookingService = new BookingService(dbContext); 
        //    _bookingCalendar = new BookingCalendar(); 
        //}

        public BookingMenu(
            BookingService bookingService,
            InputService inputService,
            CustomerService customerService,
            RoomService roomService,
            BookingCalendar bookingCalendar,
            InvoiceService invoiceService
            )
        {
            _bookingService = bookingService;
            _inputService = inputService;
            _customerService = customerService;
            _roomService = roomService;
            _bookingCalendar = bookingCalendar;
            _invoiceService = invoiceService;
            var dbContext = new HotelDbContext();
            // _messageService = messageService;
        }
        public void BookRoom()
        {
            Console.Clear();
            int guestId = SelectGuest();

            var checkInDate = SelectCheckInDate();
            if (!checkInDate.HasValue)
            {
                AnsiConsole.MarkupLine("[bold red]Incheckningsdatum måste vara senare än dagens datum.[/]");
                return;
            }
            Console.WriteLine($"Valt datum för incheckning: {checkInDate.Value:yyyy-MM-dd}");

            var checkOutDate = SelectCheckOutDate(checkInDate.Value);
            if (!checkOutDate.HasValue)
            {
                AnsiConsole.MarkupLine("[bold red]Utcheckningsdatum måste vara efter datum för incheckning.[/]");
                return;
            }
            Console.Clear();
            Console.WriteLine($"Valt datum för incheckning: {checkInDate.Value:yyyy-MM-dd}");
            Console.WriteLine($"Valt datum för utcheckning: {checkOutDate.Value:yyyy-MM-dd}");

            int roomId = SelectRoom(checkInDate.Value, checkOutDate.Value);
            var room = _roomService.GetRoomById(roomId);
            int extraBeds = 0;

            if (room is DoubleRoom doubleRoom)
            {
                extraBeds = SelectExtraBeds(doubleRoom.MaxExtraBeds);
            }

            decimal pricePerNight = _roomService.GetRoomById(roomId).Price;

            _bookingService.CreateBooking(guestId, roomId, checkInDate.Value, checkOutDate.Value, "", pricePerNight, extraBeds);
            AnsiConsole.MarkupLine("[bold green]Bokning och faktura skapad![/]");
        }

        private DateTime? SelectCheckOutDate(DateTime checkInDate)
        {
            var bookedDates = _bookingService.GetBookedDatesForRoom(0);
            _bookingCalendar = new BookingCalendar(bookedDates);

            DateTime? selectedCheckOutDate = null;

            while (selectedCheckOutDate == null || selectedCheckOutDate <= checkInDate)
            {
                selectedCheckOutDate = _bookingCalendar.Show("Välj datum för utcheckning");

                if (selectedCheckOutDate.HasValue && selectedCheckOutDate.Value <= checkInDate)
                {
                    AnsiConsole.MarkupLine("[bold red]Utcheckningsdatum måste vara efter datum för incheckning.[/]");
                    Thread.Sleep(1500);
                }

            }
            return selectedCheckOutDate;
        }

        private DateTime? SelectCheckInDate()
        {
            var bookedDates = _bookingService.GetBookedDatesForRoom(0);
            _bookingCalendar = new BookingCalendar(bookedDates);

            DateTime? selectedCheckInDate = null;

            while (selectedCheckInDate == null || selectedCheckInDate.Value < DateTime.Today)
            {
                selectedCheckInDate = _bookingCalendar.Show("Välj datum för incheckning");

                if (selectedCheckInDate.HasValue)
                {
                    if (selectedCheckInDate.Value < DateTime.Today)
                    {
                        AnsiConsole.MarkupLine("[bold red]Incheckningsdatum kan inte vara bakåt i tiden.[/]");
                        Thread.Sleep(1500);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return selectedCheckInDate;
        }

        private int SelectGuest()
        {
            Console.WriteLine();
            Console.WriteLine("Välj gäst:");
            Console.WriteLine();

            var guests = _customerService.GetCustomers();
            foreach (var guest in guests)
            {
                Console.WriteLine($"{guest.Id}. {guest.FirstName} {guest.LastName}");
            }

            int guestId;
            while (true)
            {
                guestId = _inputService.GetId("\nAnge gästens ID: ");
                if (guests.Any(g => g.Id == guestId))
                {
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]Ogiltigt ID. Försök igen.[/]");
                }
            }
            return guestId;
        }

        private int SelectRoom(DateTime checkInDate, DateTime checkOutDate)
        {
            var rooms = _roomService.GetAvailableRooms(checkInDate, checkOutDate);
            Console.WriteLine();
            Console.WriteLine("Välj rum:");

            foreach (var room in rooms)
            {
                if (room is DoubleRoom doubleRoom)
                {
                    Console.WriteLine($"{room.Id}. {room.RoomType} - Extrasängar: {doubleRoom.MaxExtraBeds} - Pris: {room.Price} kr/natt");
                }
                else
                {
                    Console.WriteLine($"{room.Id}. {room.RoomType} - Pris: {room.Price} kr/natt");
                }
            }

            int roomId;
            while (true)
            {
                roomId = _inputService.GetId("\nAnge rummets ID: ");
                if (rooms.Any(r => r.Id == roomId))
                {
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]Ogiltigt ID. Försök igen[/]");
                }
            }
                return roomId;
        }

        private int SelectExtraBeds(int maxExtraBeds)
        {
            Console.WriteLine($"Rummet har plats för upp till {maxExtraBeds} extrasängar.");
            int selectedExtraBeds = -1;

            while (selectedExtraBeds < 0 || selectedExtraBeds > maxExtraBeds)
            {
                Console.Write($"Hur många extrasängar vill du lägga till (0-{maxExtraBeds})? ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out selectedExtraBeds)
                    && selectedExtraBeds >= 0
                    && selectedExtraBeds <= maxExtraBeds)
                {
                    if (selectedExtraBeds < 0 || selectedExtraBeds > maxExtraBeds)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt antal extrasängar. Försök igen.");
                }
            }
            return selectedExtraBeds;
        }
    }
}
