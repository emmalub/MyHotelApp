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
        private readonly MessageService _messageService;
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
