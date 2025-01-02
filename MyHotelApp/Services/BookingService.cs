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
        private readonly InvoiceService _invoiceService;
        private List<Booking> bookings;


        public BookingService(HotelDbContext context, InvoiceService invoiceService)
        {
            _context = context;
            _invoiceService = invoiceService;
        }

        public void CreateBooking(int guestId, int roomId, DateTime checkInDate, DateTime checkOutDate, string conditions, decimal pricePerNight, int extraBeds)
        {
            var booking = new Booking
            {
                GuestId = guestId,
                RoomId = roomId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                TotalPrice = pricePerNight,
                IsPaid = false,
                ExtraBeds = extraBeds,
                Conditions = ""
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            decimal totalAmount = _invoiceService.CalculateTotalPrice(checkInDate, checkOutDate, pricePerNight);
            var dueDate = DateTime.Now.AddDays(10);

            _invoiceService.CreateInvoice(booking.Id, totalAmount, dueDate);
        }

        public void ConfirmBooking(string customerName, string roomType, DateTime bookingDate)
        {
            string confirmationMessage = $"Bokningsbekräftelse!\nKund: {customerName}\nRum: {roomType}\nDatum: {bookingDate
                .ToShortDateString()}";

            _messageService.SendMessage(customerName, confirmationMessage);
        }
        
        public void DeleteBooking(int bookingId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null)
            {
                AnsiConsole.MarkupLine("[bold red]Bokningen hittades inte.[/]");
                return;
            }
            booking.IsActive = false;
            _context.Bookings.Update(booking);
            _context.SaveChanges();
            AnsiConsole.MarkupLine("[bold green]Bokningen har tagits bort.[/]");
        }

        //public void DisplayBooking()
        //{
        //    var bookings = _context.Bookings?.ToList();
        //    if (bookings == null || !bookings.Any())
        //    {
        //        Console.WriteLine("Det finns inga bokningar att visa.");
        //        return;
        //    }

        //    var table = new Spectre.Console.Table
        //    {
        //        Border = TableBorder.Rounded
        //    };

        //    table.AddColumn("[bold white]Bokningsnummer[/]");
        //    table.AddColumn("[bold white]Gäst[/]");
        //    table.AddColumn("[bold white]Rum[/]");
        //    table.AddColumn("[bold white]Incheckning[/]");
        //    table.AddColumn("[bold white]Utcheckning[/]");
        //    table.AddColumn("[bold white]Totalpris[/]");
        //    table.AddColumn("[bold white]Betald[/]");
        //    table.AddColumn("[bold white]Noteringar[/]");

        //    foreach (var booking in bookings)
        //    {
        //        table.AddRow(
        //            booking.Id.ToString(),
        //            booking.GuestId.ToString(),
        //            booking.RoomId.ToString(),
        //            booking.CheckInDate.ToString("yyyy-MM-dd"),
        //            booking.CheckOutDate.ToString("yyyy-MM-dd"),
        //            booking.TotalPrice.ToString("C"),
        //            booking.IsPaid ? "[bold green]Ja[/]" : "[bold red]Nej[/]",
        //            string.IsNullOrEmpty(booking.Conditions) ? "-" : booking.Conditions
        //            );
        //    }
        //    AnsiConsole.Write(table);
        //}

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
