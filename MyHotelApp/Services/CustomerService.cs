using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyHotelApp.Data;
using MyHotelApp.Models;
using Spectre.Console;

namespace MyHotelApp.Services
{
    public class CustomerService
    {
        private readonly HotelDbContext _context;
        private readonly InputService _inputservice = new InputService();
        public CustomerService(HotelDbContext context)
        {
            _context = context;
        }
        public void CreateCustomer()
        {
            Console.WriteLine("Skapa en ny kund: ");

            var customer = new Customer
            {
                FirstName = _inputservice.GetString("Förnamn: "),
                LastName = _inputservice.GetString("Efternamn: "),
                Address = _inputservice.GetString("Adress: "),
                City = _inputservice.GetString("Stad: "),
                PostalCode = _inputservice.GetString("Postnummer: "),
                Country = _inputservice.GetString("Land: "),
                Phone = _inputservice.GetString("Telefonnummer: "),
                Email = _inputservice.GetString("E-postadress: "),
                IsVip = _inputservice.GetBool("Är kunden VIP? (Ja/Nej): "),
                IsActive = true
            };

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
        public async void DeleteCustomer(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
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

        public void UpdateCustomer(int customerId)
        {
            var customer = GetCustomerById(customerId);
            if (customer == null)
            {
                Console.WriteLine("Kunden finns inte.");
                return;
            }
            Console.WriteLine("Uppdatera kund: ");
            Console.WriteLine($"Förnamn: {customer.FirstName}");
            Console.WriteLine($"Efternamn: {customer.LastName}");
            Console.WriteLine($"Adress: {customer.Address}");
            Console.WriteLine($"Stad: {customer.City}");
            Console.WriteLine($"Postnummer: {customer.PostalCode}");
            Console.WriteLine($"Land: {customer.Country}");
            Console.WriteLine($"Telefonnummer: {customer.Phone}");
            Console.WriteLine($"E-postadress: {customer.Email}");
            Console.WriteLine($"Är kunden VIP? {customer.IsVip}");
            Console.WriteLine($"Är kunden aktiv? {customer.IsActive}");
            customer.FirstName = _inputservice.GetString("Förnamn: ");
            customer.LastName = _inputservice.GetString("Efternamn: ");
            customer.Address = _inputservice.GetString("Adress: ");
            customer.City = _inputservice.GetString("Stad: ");
            customer.PostalCode = _inputservice.GetString("Postnummer: ");
            customer.Country = _inputservice.GetString("Land: ");
            customer.Phone = _inputservice.GetString("Telefonnummer: ");
            customer.Email = _inputservice.GetString("E-postadress: ");
            customer.IsVip = _inputservice.GetBool("Är kunden VIP? (Ja/Nej): ");
            customer.IsActive = _inputservice.GetBool("Är kunden aktiv? (Ja/Nej): ");
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void ShowDeletedCustomers()
        {
            foreach (var customer in _context.Customers.Where(c => !c.IsActive))
            {
                Console.WriteLine($"Namn: {customer.Name}");
                Console.WriteLine($"Ålder: {customer.Name}");
                Console.WriteLine($"=====================");
            }
        }


        // KOD FRÅN RICHARD
        public void ShowAllCustomers(List<Customer> myCustomers)
        {
            var table = new Spectre.Console.Table();
            table.Border = Spectre.Console.TableBorder.Rounded;

            table.AddColumn("[bold white]KundID[/]");
            table.AddColumn("[blue]Namn[/]");
            table.AddColumn("[blue]Adress[/]");
            table.AddColumn("[blue]Nummer[/]");
            table.AddColumn("[blue]VIP[/]");
            table.AddColumn("[blue]Status[/]");

            foreach (var customer in myCustomers)
            {
                table.AddRow(
                        customer.Id.ToString(),
                        $"{customer.FirstName} {customer.LastName}",
                        $"{customer.Address}, {customer.City}, {customer.Country}",
                        customer.Phone,
                        customer.IsVip ? "[bold yellow]Ja[/]" : "Nej",
                        customer.IsActive ? "[bold green]Aktiv[/]" : "[bold red]Inaktiv[/]"
                    );
            }
            Spectre.Console.AnsiConsole.Write(table);
        }
    }
}
