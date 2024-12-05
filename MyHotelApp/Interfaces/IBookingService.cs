using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Interfaces
{
    public interface IBookingService
    {
        void ConfirmBooking(string customerName, string roomType, DateTime bookingDate);

    }
}
