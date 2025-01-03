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

        public InvoiceManagementService(HotelDbContext context)
        {
            _context = context;
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

            table.AddColumn("[bold]Fakturanummer[/]");
            table.AddColumn("[boldBokningsnummer[/]");
            table.AddColumn("[bold]Summa[/]");
            table.AddColumn("[bold]Skapad[/]");
            table.AddColumn("[bold]Förfallodatum[/]");
            table.AddColumn("[bold]Betald[/]");
            table.AddColumn("[bold]Makulerad[/]");

            foreach (var invoice in invoices)
            {
                table.AddRow(
                    invoice.Id.ToString(),
                    invoice.BookingId.ToString(),
                    $"{invoice.TotalAmount:C}",
                    invoice.IssueDate.ToString("yyyy-MM-dd"),
                    invoice.DueDate.ToString("yyyy-MM-dd"),
                    invoice.IsPaid ? "[green]Yes[/]" : "[red]No[/]",
                    invoice.IsCanceled ? "[red]Yes[/]" : "[green]No[/]"
                );
            }

            AnsiConsole.Write(table);
        }
        public void PayInvoice()
        {
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

    }
}
