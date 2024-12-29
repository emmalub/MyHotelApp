using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHotelApp.Data;
using MyHotelApp.Models;

namespace MyHotelApp.Services
{
    public class InvoiceService
    {
        private readonly HotelDbContext _context;

        public InvoiceService(HotelDbContext context)
        {
            _context = context;
        }

        private readonly List<Invoice> _invoices = new();

        public Invoice CreateInvoice(int bookingId, decimal amount, DateTime dueDate)
        {
            var newInvoice = new Invoice(bookingId, amount, dueDate);
            _context.Invoices.Add(newInvoice);
            _context.SaveChanges();
            Console.WriteLine("Faktura skapad!");
            return newInvoice;
        }
        public List<Invoice> GetInvoiceList()
        {
            return _context.Invoices.ToList();
        }

        public Invoice GetInvoiceById(int id)
        {
            return _invoices.First(x => x.Id == id);
        }

        public void CancelInvoice(int id)
        {
            var invoice = _context.Invoices.FirstOrDefault(x => x.Id == id);
            if (invoice != null)
            {
                invoice.IsCanceled = true;
                _context.SaveChanges();
            }
        }

        public Invoice CreateInvoiceFromBooking(int bookingId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return null;
            
            decimal totalAmount = CalculateTotalPrice(booking.CheckInDate, booking.CheckOutDate, booking.Price);
            var dueDate = booking.CheckOutDate.AddDays(10);

            return CreateInvoice(bookingId, totalAmount, dueDate);
        }

        public decimal CalculateTotalPrice(DateTime checkInDate, DateTime checkOutDate, decimal price)
        {
            var totalDays = (checkOutDate - checkInDate).Days;
            return totalDays * price;
        }

    }
}
