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

        public decimal CalculateTotalPrice(DateTime checkInDate, DateTime checkOutDate, decimal price)
        {
            var totalDays = (checkOutDate - checkInDate).Days;
            return totalDays * price;
        }

        public void PayInvoice(int id)
        {
            var invoice = _context.Invoices.FirstOrDefault(x => x.Id == id);
            if (invoice != null)
            {
                invoice.IsPaid = true;
                _context.SaveChanges();
            }
        }

        //public void HandleOvedueInvoices()
        //{
        //    var overdueInvoices = _context.Invoices
        //        .Where(i => i.DueDate < DateTime.Now && !i.IsPaid && !i.IsCanceled)
        //        .ToList();

        //    foreach (var invoice in overdueInvoices)
        //    {
        //        var booking = _context.Bookings.FirstOrDefault(b => b.Id == invoice.BookingId);
        //        if (booking != null && booking.CheckInDate > DateTime.Now)
        //        {
        //            _context.Invoices.Remove(invoice);
        //            invoice.IsCanceled = true;
        //        }
        //    }
        //    _context.SaveChanges();
        //}
        public void UpdateInvoice(int id, decimal? newAmount = null, DateTime? newDueDate = null)
        {
            var invoice = _context.Invoices.FirstOrDefault(x => x.Id == id);
            if (invoice == null && !invoice.IsCanceled && !invoice.IsPaid)
            {
                if (newAmount.HasValue) invoice.TotalAmount = newAmount.Value;
                if (newDueDate.HasValue) invoice.DueDate = newDueDate.Value;
            _context.SaveChanges();
            }
        }

    }
}
