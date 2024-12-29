using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyHotelApp.Data;
using MyHotelApp.Interfaces;
using MyHotelApp.Models;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services
{
    public class BookingService
    {
        private readonly HotelDbContext _context;
        private readonly IMessageService _messageService;
        private List<Booking> bookings;


        public BookingService(HotelDbContext context)
        {
            _context = context;
        }

        //public MessageService(IMessageService messageService)
        //{ 
        //    _messageService = messageService; 
        //}


        public void CreateBooking(int guestId, int roomId, DateTime checkInDate, DateTime checkOutDate, string conditions)
        {
            var booking = new Booking
            {
                GuestId = guestId,
                RoomId = roomId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                IsPaid = false,
                Conditions = ""
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void ConfirmBooking(string customerName, string roomType, DateTime bookingDate)
        {
            string confirmationMessage = $"Bokningsbekräftelse!\nKund: {customerName}\nRum: {roomType}\nDatum: {bookingDate
                .ToShortDateString()}";

            _messageService.SendMessage(customerName, confirmationMessage);
        }

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
            table.AddColumn("[bold white]Pris[/]");
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
                    booking.Price.ToString("C"),
                    booking.IsPaid ? "[bold green]Ja[/]" : "[bold red]Nej[/]",
                    string.IsNullOrEmpty(booking.Conditions) ? "-" : booking.Conditions
                    );
            }
            AnsiConsole.Write(table);
        }

        //    bookings.Select(booking =>
        //    $"| {booking
        //    .Id,-11} | {booking
        //    .GuestId,-7} | {booking
        //    .Room,-4} | {booking
        //    .CheckInDate:yyy-MM-dd} | {booking
        //    .CheckOutDate:yyyy-MM-dd} | {booking
        //    .Price,-6:C} | {(booking
        //    .IsPaid ? "J" : "N"),-6} | {(string
        //        .IsNullOrEmpty(
        //            booking.Conditions) ? "Inga" :
        //            booking.Conditions),-8} |")
        //        .ToList()
        //        .ForEach(Console.WriteLine);


        public List<DateTime> GetBookedDatesForRoom(int roomId)
        {
            var bookings = _context.Bookings
                .Where(b => b.RoomId == roomId && b.IsActive)
                .ToList();

            var bookedDates = new List<DateTime>();
            foreach (var booking in bookings)
            {
                for (var date = booking
                    .CheckInDate; date <= booking
                    .CheckOutDate; date = date
                    .AddDays(1))
                {
                    bookedDates.Add(date);
                }
            }
            return bookedDates;
        }
    }
}
