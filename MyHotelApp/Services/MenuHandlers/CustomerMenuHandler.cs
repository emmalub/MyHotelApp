using Microsoft.EntityFrameworkCore;
using MyHotelApp.Data;
using MyHotelApp.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services.MenuHandlers
{
    public class CustomerMenuHandler
    {
        private readonly HotelDbContext _context;
        private readonly InputService _inputservice = new InputService();
        private readonly CustomerService _customerService;

        public void CreateNewCustomer()
        {
           var newCustomer = _inputservice.GetCustomerDetails();
            _customerService.CreateCustomer(newCustomer);
            Console.WriteLine("Kund skapad!");
            Console.ReadKey();
        }
        public void DeleteCustomer(int customerId)
        {
            int id = _inputservice.GetId("Ange kundID för att ta bort kund: ");

            if (id == 0)
            {
                Console.WriteLine("Ogiltigt ID.");
                return;
            }
            
            _customerService.DeactivateCustomer(id);
            Console.ReadKey();
        }
        public void UpdateCustomer()
        {
            int id = _inputservice.GetId("Ange kundID på kund du vill redigera: ");

            if (id == 0)
            {
                Console.WriteLine("Ogiltigt ID.");
                return;
            }


            var customer = _customerService.GetCustomerById(id);
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

            _customerService.UpdateCustomer(customer);
        }

        private string GetUpdatedValue(string fieldName, string currentValue)
        {
            //Console.WriteLine($"{fieldName}: {currentValue}");
            string newValue = _inputservice.GetOptionalString($"Uppdatera {fieldName} (Tryck ENTER för att behålla '{currentValue}': ");
            return string.IsNullOrWhiteSpace(newValue) ? currentValue : newValue;
        }

        public void ShowDeletedCustomers()
        {
            var table = new Table();

            table.AddColumn("Kund ID");
            table.AddColumn("Namn");
            table.AddColumn("Återaktivera");

            foreach (var customer in _context.Customers.Where(c => !c.IsActive))
            {
                var restoreOption = "[bold green]Återaktivera[/]";
                table.AddRow(customer.Id.ToString(), customer.Name, restoreOption);
            }
            AnsiConsole.Write(table);

            var customerIdInput = AnsiConsole.Ask<int>("Ange ID på kund att återaktivera (eller 0 för att avbryta): ");
            if (customerIdInput == 0)
            {
                AnsiConsole.MarkupLine("[bold red]Återaktivering avbruten![/]");
            }
            ActivateCustomer(customerIdInput);
        }

        private void ActivateCustomer(int customerIdInput)
        {
            var customerToRestore = _context.Customers.FirstOrDefault(c => c.Id == customerIdInput);
            if (customerToRestore != null && !customerToRestore.IsActive)
            {
                customerToRestore.IsActive = true;
                _context.SaveChanges();
                AnsiConsole.MarkupLine($"[bold green]Kunden {customerToRestore.Name} har återaktiverats![/]");
            }
            else
            {
                Console.WriteLine("Kunden finns inte eller är redan aktiv.");
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

            var customer = _customerService.GetCustomerById(id);

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
