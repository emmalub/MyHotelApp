using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyHotelApp.Models;

namespace MyHotelApp.Services
{
    internal class CustomerService
    {
        public bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        // KOD FRÅN RICHARD
        public static void ShowAllCustomersSpectre(List<Customer> myCustomers)
        {
            var table = new Spectre.Console.Table();
            table.Border = Spectre.Console.TableBorder.Double; //man kan också använda rounded
            table.AddColumn("[bold white on green]Ordernummer[/]");
            table.AddColumn("[blue]Namn[/]");
            table.AddColumn("[blue]Datum[/]");
            table.AddColumn("[blue]Produkter[/]");
            table.AddColumn("[blue]Total kostnad[/]");
            table.AddColumn("[blue]Förfallodatum[/]");

            foreach (var customer in myCustomers)
            {
                foreach (var order in customer.Bookings)
                {
                    var productNames = string
                        .Join(", ", order.Items
                        .Select(i => i.ProductName));

                    table.AddRow(
                        customer.CustomerId.ToString(),
                        customer.GetFullName(),
                        booking.CheckIn.ToString("yyyy-MM-dd"),
                        booking.CheckOut.ToString("yyyy-MM-dd"),
                        room.RoomID,
                        $"{invoice.Amount} kr",
                        invoice.DueDate.ToString("yyyy-MM-dd")
                    );
                }
            }

            Spectre.Console.AnsiConsole.Write(table);
        }

    }
}
