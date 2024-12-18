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

        public Invoice CreateInvoice(int bookingId, decimal amount)
        {
            var newInvoice = new Invoice
            {
                Id = _invoices.Count + 1,
                BookingId = bookingId,
                Amount = amount,
                IssueDate = DateTime.Now,
                IsPaid = false,
                IsCanceled = false
            };
            _invoices.Add(newInvoice);
            return newInvoice;
        }
        public List<Invoice> GetInvoiceList()
        {
            return _invoices;
        }

        public Invoice GetInvoiceById(int id)
        {
            return _invoices.First(x => x.Id == id);
        }

        public void CancelInvoice(int id)
        {
            var invoice = GetInvoiceById(id);
            if (invoice != null)
            {
                invoice.IsCanceled = true;
            }
        }
    }
}
