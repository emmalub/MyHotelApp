using MyHotelApp.Data;
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
        public void Booking()
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
            decimal pricePerNight = _roomService.GetRoomById(roomId).Price;

            _bookingService.CreateBooking(guestId, roomId, checkInDate.Value, checkOutDate.Value, "", pricePerNight);
            AnsiConsole.MarkupLine("[bold green]Bokning skapad![/]");
        }

        private DateTime? SelectCheckOutDate(DateTime checkOutDate)
        {
            var bookedDates = _bookingService.GetBookedDatesForRoom(0);
            _bookingCalendar = new BookingCalendar(bookedDates);

            var selectedDate = _bookingCalendar.Show("Välj datum för utcheckning");
          
            return selectedDate;
        }

        private DateTime? SelectCheckInDate()
        {
            var bookedDates = _bookingService.GetBookedDatesForRoom(0);
            _bookingCalendar = new BookingCalendar(bookedDates);

            var selectedDate = _bookingCalendar.Show("Välj datum för incheckning");
            
            AnsiConsole.MarkupLine("[bold red]Välj datum för utcheckning (måste vara senare än datum för incheckning)[/]");
            return selectedDate;
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
            return _inputService.GetId("\nAnge gästens ID: ");
        }

        private int SelectRoom(DateTime checkInDate, DateTime checkOutDate)
        {
            var rooms = _roomService.GetAvailableRooms(checkInDate, checkOutDate);
            Console.WriteLine();
            Console.WriteLine("Välj rum:");

            foreach (var room in rooms)
            {
                Console.WriteLine($"{room.Id}. {room.RoomType} - {room.Price} kr/natt");
            }
            return _inputService.GetId("\nAnge rummets ID: ");
        }
    }
}
