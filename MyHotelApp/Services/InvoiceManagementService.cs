using MyHotelApp.Data;
using MyHotelApp.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services
{
    public class InvoiceManagementService
    {
        private readonly HotelDbContext _context;
        private readonly InputService _inputService;
        private readonly InvoiceService _invoiceService;

        public InvoiceManagementService(HotelDbContext context, InputService inputService, InvoiceService invoiceService)
        {
            _context = context;
            _inputService = inputService;
            _invoiceService = invoiceService;
        }

        public void DisplayInvoices()
        {
            var invoices = _context.Invoices.ToList();

            if (invoices.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Det finns inga fakturor att visa.[/]");
                return;
            }

            var table = new Table
            {
                Border = TableBorder.Double,
                Expand = true
            };

            table.AddColumn("[bold]Fakturanr.[/]");
            table.AddColumn("[bold]Summa[/]");
            table.AddColumn("[bold]Skapad[/]");
            table.AddColumn("[bold]Förfallodatum[/]");
            table.AddColumn("[bold]Betald[/]");
            table.AddColumn("[bold]Makulerad[/]");

            foreach (var invoice in invoices)
            {
                table.AddRow(
                    invoice.Id.ToString(),
                    $"{invoice.TotalAmount:C}",
                    invoice.IssueDate.ToString("yyyy-MM-dd"),
                    invoice.DueDate.ToString("yyyy-MM-dd"),
                    invoice.IsPaid ? "[green]Ja[/]" : "[red]Nej[/]",
                    invoice.IsCanceled ? "[red]Ja[/]" : "[green]Nej[/]"
                );
            }

            AnsiConsole.Write(table);
        }
        public void PayInvoice()
        {
            DisplayInvoices();

            int invoiceId = AnsiConsole.Ask<int>("Ange fakturanummer att betala: ");
            var invoice = _context.Invoices.FirstOrDefault(x => x.Id == invoiceId);
            
            if (invoice != null)
            {
                invoice.IsPaid = true;
                _context.SaveChanges();
                Console.WriteLine("Fakturan är nu betald!");
            }
            else
            {
                Console.WriteLine("Fakturan kunde inte hittas eller är redan betald.");
            }
        }

        public void EditInvoice()
        {
            DisplayInvoices();

            int invoiceId = _inputService.GetId("Ange fakturanummer att redigera: ");
            var invoice = _context.Invoices.FirstOrDefault(x => x.Id == invoiceId);
           
            if (invoice != null)
            {
                decimal newAmount = AnsiConsole.Ask<decimal>("Ange ett nytt belopp: ");
                DateTime newDueDate = AnsiConsole.Ask<DateTime>("Ange ett nytt förfallodatum: ");

                invoice.TotalAmount = newAmount;
                invoice.DueDate = newDueDate;
                _context.SaveChanges();
                Console.WriteLine("Fakturan är nu uppdaterad!");
            }
            else
            {
                Console.WriteLine("Fakturan kunde inte hittas.");
            }
        }

        public void CancelInvoice()
        {
            DisplayInvoices();

            int invoiceId = _inputService.GetId("Ange fakturanummer att makulera: ");
            var invoice = _context.Invoices.FirstOrDefault(x => x.Id == invoiceId);

            if (invoice != null)
            {
                invoice.IsCanceled = true;
                _context.SaveChanges();

                Console.WriteLine("Fakturan har makulerats!");
            }
            else
            {
                Console.WriteLine("Fakturan kunde inte hittas eller är redan makulerad.");
            }

        }
        public Invoice CreateInvoiceFromBooking(int bookingId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return null;

            decimal totalAmount = _invoiceService.CalculateTotalPrice(booking.CheckInDate, booking.CheckOutDate, booking.TotalPrice);
            var dueDate = booking.CheckOutDate.AddDays(10);

            return _invoiceService.CreateInvoice(bookingId, totalAmount, dueDate);
        }
        public void HandleOverdueInvoices()
        {
            var overdueInvoices = _context.Invoices
                .Where(i => i.DueDate < DateTime.Now && !i.IsPaid && !i.IsCanceled)
                .ToList();

            foreach (var invoice in overdueInvoices)
            {
                var booking = _context.Bookings.FirstOrDefault(b => b.Id == invoice.BookingId);
                if (booking != null && booking.CheckInDate > DateTime.Now)
                {
                    _context.Invoices.Remove(invoice);
                    invoice.IsCanceled = true;
                }
            }
            _context.SaveChanges();
        }
    }
}
