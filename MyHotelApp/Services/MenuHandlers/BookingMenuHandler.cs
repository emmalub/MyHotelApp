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
        private readonly InputService _inputService;
        private readonly HotelDbContext _context;
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

        //public void BookRoom()
        //{
        //    Console.Clear();
        //    int guestId = SelectGuest();

        //    var checkInDate = SelectCheckInDate();
        //    if (!checkInDate.HasValue)
        //    {
        //        AnsiConsole.MarkupLine("[bold red]Incheckningsdatum måste vara senare än dagens datum.[/]");
        //        return;
        //    }
        //    Console.WriteLine($"Valt datum för incheckning: {checkInDate.Value:yyyy-MM-dd}");

        //    var checkOutDate = SelectCheckOutDate(checkInDate.Value);
        //    if (!checkOutDate.HasValue)
        //    {
        //        AnsiConsole.MarkupLine("[bold red]Utcheckningsdatum måste vara efter datum för incheckning.[/]");
        //        return;
        //    }
        //    Console.Clear();
        //    Console.WriteLine($"Valt datum för incheckning: {checkInDate.Value:yyyy-MM-dd}");
        //    Console.WriteLine($"Valt datum för utcheckning: {checkOutDate.Value:yyyy-MM-dd}");

        //    int roomId = SelectRoom(checkInDate.Value, checkOutDate.Value);
        //    var room = _roomService.GetRoomById(roomId);
        //    int extraBeds = 0;

        //    if (room is DoubleRoom doubleRoom)
        //    {
        //        extraBeds = SelectExtraBeds(doubleRoom.MaxExtraBeds);
        //    }

        //    decimal pricePerNight = _roomService.GetRoomById(roomId).Price;

        //    _bookingService.CreateBooking(guestId, roomId, checkInDate.Value, checkOutDate.Value, "", pricePerNight, extraBeds);
        //    AnsiConsole.MarkupLine("[bold green]Bokning och faktura skapad![/]");
        //}

        public void HandleDeleteBooking()
        {
            int bookingId = _inputService.GetId("Ange bokningsnummer att ta bort: ");
            _bookingService.DeleteBooking(bookingId);
        }
    }
}
