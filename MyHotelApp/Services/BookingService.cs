using MyHotelApp.Data;
using MyHotelApp.Interfaces;
using MyHotelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services
{
    internal class BookingService : IBookingService
    {
        private readonly HotelDbContext _context;

        public BookingService(HotelDbContext context)
        {
            _context = context;
        }

        private readonly IMessageService _messageService;

        public BookingService(IMessageService messageService)
        { 
            _messageService = messageService; 
        }

        public void ConfirmBooking(string customerName, string roomType, DateTime bookingDate)
        {
            string confirmationMessage = $"Bokningsbekräftelse!\nKund: {
                customerName}\nRum: {
                roomType}\nDatum: {
                bookingDate
                .ToShortDateString()}";

            _messageService.SendMessage(customerName, confirmationMessage);
        }

        public void DisplayBookings(List<Booking> bookings)
        {
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine("| Bokningsnummer | Gäst  | Rum  | Incheckning  | Utcheckning  | Pris     | Betald  | Noteringar   |");
            Console.WriteLine("------------------------------------------------------------------------------------");

            foreach (var booking in bookings)
            {
                Console.WriteLine($"| {
                    booking.Id,-11} | {
                    booking.GuestId,-7} | {
                    booking.Room,-4} | {
                    booking.CheckInDate:yyy-MM-dd} | {
                    booking.CheckOutDate:yyyy-MM-dd} | {
                    booking.Price,-6:C} | {
                    (booking.IsPaid ? "J" : "N"),-6} | {(string
                    .IsNullOrEmpty(
                        booking.Conditions) ? "Inga" : 
                        booking.Conditions),-8} |");
            }
        }
    }
}
