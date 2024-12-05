using MyHotelApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services
{
    internal class BookingService : IBookingService
    {
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
    }
}
