using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
                Phone = _inputservice.GetString("Telefonnummer: "),
                Email = _inputservice.GetString("E-postadress: "),
                IsVip = _inputservice.GetBool("Är kunden VIP? (Ja/Nej): "),
                IsActive = true
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();
            Console.WriteLine("Ny kund sparad!");
            Console.ReadKey();
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
        public void DeleteCustomer(int customerId)
        {
            int id = _inputservice.GetId("Ange kundID för att ta bort kund: ");

            if (id == 0)
            {
                Console.WriteLine("Ogiltigt ID.");
                return;
            }
            var customer = _context.Customers
                .Include(c => c.Bookings)
                .First(c => c.Id == id);

            if (customer != null)
            {
                if (customer.Bookings != null && customer.Bookings.Any())
                {
                    Console.WriteLine("Kunden har aktiva bokningar och kan inte tas bort.");
                    return;
                }
                customer.IsActive = false;
                _context.Customers.Update(customer);
                try
                { 
                    _context.SaveChanges();
                    Console.WriteLine("Kund har inaktiverats!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ett error uppsotd: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Kunden finns inte.");
            }
        }
        public bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        public void UpdateCustomer(int customerId)
        {
            int id = _inputservice.GetId("Ange kundID på kund du vill redigera: ");

            if (id == 0)
            {
                Console.WriteLine("Ogiltigt ID.");
                return;
            }


            var customer = GetCustomerById(id);
            if (customer == null)
            {
                Console.WriteLine("Kunden finns inte.");
                return;
            }
            Console.Clear();
            Console.WriteLine();
            customer.FirstName = GetUpdatedValue("Förnamn ", customer.FirstName);
            customer.LastName = GetUpdatedValue("Efternamn ", customer.LastName);
            customer.Address = GetUpdatedValue("Adress ", customer.Address);
            customer.City = GetUpdatedValue("Stad ", customer.City);
            customer.PostalCode = GetUpdatedValue("Postnummer ", customer.PostalCode);
            customer.Phone = GetUpdatedValue("Telefonnummer ", customer.Phone);
            customer.Email = GetUpdatedValue("E-postadress ", customer.Email);
            customer.IsVip = _inputservice.GetBool($"Är kunden VIP? ({(customer.IsVip ? "Ja" : "Nej")})");
            customer.IsActive = _inputservice.GetBool($"Är kunden aktiv? ({(customer.IsActive ? "Ja" : "Nej")})");
           
            _context.Customers.Update(customer);
            _context.SaveChanges();
            Console.WriteLine("Ändringar har sparats!");
            Console.ReadKey();
        }

        private string GetUpdatedValue(string fieldName, string currentValue)
        {
            //Console.WriteLine($"{fieldName}: {currentValue}");
            string newValue = _inputservice.GetOptionalString($"Uppdatera {fieldName} (Tryck ENTER för att behålla '{currentValue}': ");
            return string.IsNullOrWhiteSpace(newValue) ? currentValue : newValue;
        }

        public void ShowDeletedCustomers()
        {
            foreach (var customer in _context.Customers.Where(c => !c.IsActive))
            {
                Console.WriteLine($"Namn: {customer.Name}");
                Console.WriteLine($"ID: {customer.Id}");
                Console.WriteLine($"=====================");
            }
        }

        public void ShowCustomer()
        {
            int id = _inputservice.GetId("Ange kundID på kunden för att visa alla uppgifter: ");

            if (id == 0)
            {
                Console.WriteLine("Ogiltigt ID.");
                return;
            }

            var customer = GetCustomerById(id);

            if (customer == null)
            {
                Console.WriteLine("Kunden finns inte.");
                return;
            }

            Console.Clear();
            var table = new Spectre.Console.Table();
            table.Border = Spectre.Console.TableBorder.Rounded;

            table.AddColumn("[bold white]Kunduppgifter[/]");
            table.AddColumn("[blue][/]");

            table.AddRow("Förnamn:", customer.FirstName);
            table.AddRow("Efternamn:", customer.LastName);
            table.AddRow("Adress:", $"{customer.Address}, {customer.City}, {customer.PostalCode}");
            table.AddRow("Telefonnummer:", customer.Phone);
            table.AddRow("E-postadress:", customer.Email);
            table.AddRow("VIP:", customer.IsVip ? "Ja" : "Nej");
            table.AddRow("Aktiv:", customer.IsActive ? "Ja" : "Nej");

            Spectre.Console.AnsiConsole.Write(table);

            //Console.WriteLine($"Förnamn: {customer.FirstName}");
            //Console.WriteLine($"Efternamn: {customer.LastName}");
            //Console.WriteLine($"Adress: {customer.Address}");
            //Console.WriteLine($"Stad: {customer.City}");
            //Console.WriteLine($"Postnummer: {customer.PostalCode}");
            //Console.WriteLine($"Telefonnummer: {customer.Phone}");
            //Console.WriteLine($"E-postadress: {customer.Email}");
            //Console.WriteLine($"Är kunden VIP? {customer.IsVip}");
            //Console.WriteLine($"Är kunden aktiv? {customer.IsActive}");
        }

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
                        $"{customer.Address}, {customer.City}",
                        customer.Phone,
                        customer.IsVip ? "[bold yellow]Ja[/]" : "Nej",
                        customer.IsActive ? "[bold green]Aktiv[/]" : "[bold red]Inaktiv[/]"
                    );
            }
            Spectre.Console.AnsiConsole.Write(table);
        }
    }
}
