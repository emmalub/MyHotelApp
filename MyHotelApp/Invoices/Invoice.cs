using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Invoices
{
    public class Invoice
    {
        public int Id { get; set; }
        public string BookingId { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssueDate { get; set; }
        public bool IsPaid {  get; set; }
        public bool IsCanceled { get; set; }
    }
}
