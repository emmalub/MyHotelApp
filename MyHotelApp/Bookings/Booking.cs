using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Bookings
{
    public class Booking
    {
        public int ID { get; set; }  
        public Customer Guest { get; set; }
        public Room Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Invoice Price { get; set; }
        public Invoice IsPaid { get; set; }
        public string Conditions { get; set; }

        public void DisplayBookings(List<Booking> bookings)
        {
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine("| Bokningsnummer | Gäst  | Rum  | Incheckning  | Utcheckning  | Pris     | Betald  | Noteringar   |");
            Console.WriteLine("------------------------------------------------------------------------------------");

            foreach (var booking in bookings)
            {
                Console.WriteLine($"| {
                    booking.Id,-11} | {
                    booking.GuestId, -7} | {
                    booking.Room,-4} | {
                    booking.CheckInDate:yyy-MM-dd} | {
                    booking.CheckOutDate:yyyy-MM-dd} | {
                    booking.Price,-6:C} | {
                    (booking.isPaid ? "J" : "N"),-6} | {
                    (string.IsNullOrEmpty(booking.Conditions) ? "Inga" : booking.Conditions), -8} |");
            }
        }
    }
}
