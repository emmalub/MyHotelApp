using MyHotelApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly BookingCalendar _bookingCalendar;
        // private readonly IMessageService _messageService;

        public BookingMenu(BookingService bookingService, InputService inputService, CustomerService customerService, RoomService roomService, BookingCalendar bookingCalendar)
        {
            _bookingService = bookingService;
            _inputService = inputService;
            _customerService = customerService;
            _roomService = roomService;
            _bookingCalendar = bookingCalendar;
            // _messageService = messageService;
        }
        public void ShowMenu()
        {
            int guestId = SelectGuest();
            int roomId = SelectRoom();

            _bookingCalendar.ShowCalendar();

            var checkInDate = _inputService.GetDate("Ange incheckningsdatum: ");
            var checkOutDate = _inputService.GetDate("Ange utcheckningsdatum: ");

            //var checkInDate = _bookingCalendar.SelectDate("Ange incheckningsdatum: ");
            //var checkOutDate = _bookingCalendar.SelectDate("Ange utcheckningsdatum: ");

            if (checkOutDate <= checkInDate)
            {
                AnsiConsole.MarkupLine("[bold red]Utcheckningsdatum måste vara senare än incheckningsdatum.[/]");
                return;
            }

            _bookingService.CreateBooking(guestId, roomId, checkInDate, checkOutDate);
        }

        private int SelectGuest()
        {
            Console.WriteLine("Välj gäst:");
            var guests = _customerService.GetCustomers();
            foreach (var guest in guests)
            {
                Console.WriteLine($"{guest.Id}. {guest.FirstName} {guest.LastName}");
            }
            return _inputService.GetId("Ange gästens ID: ");
        }

        private int SelectRoom()
        {
            Console.WriteLine("Välj rum:");

            var checkInDate = _inputService.GetDate("Ange incheckningsdatum: ");
            var checkOutDate = _inputService.GetDate("Ange utcheckningsdatum: ");

            var rooms = _roomService.GetAvailableRooms(checkInDate, checkOutDate);

            foreach (var room in rooms)
            {
                Console.WriteLine($"{room.Id}. {room.RoomType} - {room.Price} kr/natt");
            }
            return _inputService.GetId("Ange rummets ID: ");
        }
    }
}
