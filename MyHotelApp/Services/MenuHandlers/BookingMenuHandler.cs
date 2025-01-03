using Microsoft.EntityFrameworkCore;
using MyHotelApp.Data;
using MyHotelApp.Interfaces;
using MyHotelApp.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services.MenuHandlers
{
    public class BookingMenuHandler
    {
        private readonly BookingService _bookingService;
        private BookingCalendar _bookingCalendar;
        private readonly RoomService _roomService;
        private readonly CustomerService _customerService;
        private readonly InputService _inputService;
        private readonly HotelDbContext _context;
        private readonly RoomManagementService _roomManagementService;
        public void DisplayBooking()
        {
            var bookings = _context.Bookings?.ToList();
            if (bookings == null || !bookings.Any())
            {
                Console.WriteLine("Det finns inga bokningar att visa.");
                return;
            }

            var table = new Spectre.Console.Table
            {
                Border = TableBorder.Rounded
            };

            table.AddColumn("[bold white]Bokningsnummer[/]");
            table.AddColumn("[bold white]Gäst[/]");
            table.AddColumn("[bold white]Rum[/]");
            table.AddColumn("[bold white]Incheckning[/]");
            table.AddColumn("[bold white]Utcheckning[/]");
            table.AddColumn("[bold white]Totalpris[/]");
            table.AddColumn("[bold white]Betald[/]");
            table.AddColumn("[bold white]Noteringar[/]");

            foreach (var booking in bookings)
            {
                table.AddRow(
                    booking.Id.ToString(),
                    booking.GuestId.ToString(),
                    booking.RoomId.ToString(),
                    booking.CheckInDate.ToString("yyyy-MM-dd"),
                    booking.CheckOutDate.ToString("yyyy-MM-dd"),
                    booking.TotalPrice.ToString("C"),
                    booking.IsPaid ? "[bold green]Ja[/]" : "[bold red]Nej[/]",
                    string.IsNullOrEmpty(booking.Conditions) ? "-" : booking.Conditions
                    );
            }
            AnsiConsole.Write(table);
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
            var room = _roomManagementService.GetRoomById(roomId);
            int extraBeds = 0;

            if (room is DoubleRoom doubleRoom)
            {
                extraBeds = SelectExtraBeds(doubleRoom.MaxExtraBeds);
            }

            decimal pricePerNight = _roomManagementService.GetRoomById(roomId).Price;

            _bookingService.CreateBooking(guestId, roomId, checkInDate.Value, checkOutDate.Value, "", pricePerNight, extraBeds);
            AnsiConsole.MarkupLine("[bold green]Bokning och faktura skapad![/]");
        }

        public void HandleDeleteBooking()
        {
            int bookingId = _inputService.GetId("Ange bokningsnummer att ta bort: ");
            _bookingService.DeleteBooking(bookingId);
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
            var rooms = _roomManagementService.GetAvailableRooms(checkInDate, checkOutDate);
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

