using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }

        public Invoice(int bookingId, decimal totalAmount, DateTime dueDate, bool isPaid = false, bool isCanceled = false)
        {
            BookingId = bookingId;
            TotalAmount = totalAmount;
            IssueDate = DateTime.Now;
            DueDate = dueDate;
            IsPaid = isPaid;
            IsCanceled = isCanceled;
        }
    }
}
