using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyHotelApp.Data;
using MyHotelApp.Models;

namespace MyHotelApp.Services
{
    public class CustomerService 
    {
        private readonly HotelDbContext _context;

        public CustomerService(HotelDbContext context)
        {
            _context = context;
        }
        public void CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public List<Customer> GetCustomers()
        {

            return _context.Customers.ToList();
        }
        public Customer GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }
        public void ReadCustomers()
        {
                foreach (var customer in _context.Customers.Where(c => c.IsActive))
                {
                    Console.WriteLine($"Namn: {customer.Name}");
                    Console.WriteLine($"Ålder: {customer.Name}");
                    Console.WriteLine($"=====================");
                }
        }
        public async void DeleteCustomerAsync(int cusstomerId)
        {
            var customer = await _context.Customers.FindAsync(cusstomerId);
            if (customer != null)
            {
                customer.IsActive = false;
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
            }
        }
        public bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }


        // KOD FRÅN RICHARD
        //public static void ShowAllCustomersSpectre(List<Customer> myCustomers)
        //{
        //    var table = new Spectre.Console.Table();
        //    table.Border = Spectre.Console.TableBorder.Double; //man kan också använda rounded
        //    table.AddColumn("[bold white on green]Ordernummer[/]");
        //    table.AddColumn("[blue]Namn[/]");
        //    table.AddColumn("[blue]Datum[/]");
        //    table.AddColumn("[blue]Produkter[/]");
        //    table.AddColumn("[blue]Total kostnad[/]");
        //    table.AddColumn("[blue]Förfallodatum[/]");

        //    foreach (var customer in myCustomers)
        //    {
        //        foreach (var order in customer.Bookings)
        //        {
        //            var productNames = string
        //                .Join(", ", order.Items
        //                .Select(i => i.ProductName));

        //            table.AddRow(
        //                customer.CustomerId.ToString(),
        //                customer.GetFullName(),
        //                booking.CheckIn.ToString("yyyy-MM-dd"),
        //                booking.CheckOut.ToString("yyyy-MM-dd"),
        //                room.RoomID,
        //                $"{invoice.Amount} kr",
        //                invoice.DueDate.ToString("yyyy-MM-dd")
        //            );
        //        }
        //    }

        //    Spectre.Console.AnsiConsole.Write(table);
        //}

    }
}
