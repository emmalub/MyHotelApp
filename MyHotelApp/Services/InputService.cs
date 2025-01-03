using MyHotelApp.Models;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace MyHotelApp.Services
{
    public class InputService
    {
        private readonly InvoiceService _invoiceService;
        private readonly InvoiceManagementService _invoiceManagementService;
        public int GetId(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int id) && id > 0)
                {
                    return id;
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]Felaktig inmatning. Ange ett giltigt heltal.[/]");
                }
            }
        }
        public DateTime GetDate(string prompt)
        {
            return AnsiConsole.Ask<DateTime>($"{prompt}");
        }
        public string GetString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Fältet får inte lämnas tomt. Försök igen.");
                }
            }
        }
        public string GetOptionalString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        public bool GetBool(string prompt)
        {
            Console.WriteLine(prompt);
            while (true)
            {
                string? input = Console.ReadLine()?.Trim().ToLower();
                if (input == "ja" || input == "j")
                {
                    return true;
                }
                else if (input == "nej" || input == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning. Skriv J (Ja) eller N (Nej)");
                    Console.WriteLine(prompt);
                }
            }
        }

        public decimal GetDecimal(string fieldName, decimal currentValue)
        {
            Console.WriteLine($"{fieldName}: {currentValue}");
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? currentValue : decimal.Parse(input);
        }

        public double GetDouble(string fieldName, double currentValue)
        {
            Console.WriteLine($"{fieldName}: {currentValue}");
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? currentValue : double.Parse(input);
        }
        public int GetRoomIdFromUser(string prompt = "Ange rumsnummer: ")
        {
            return AnsiConsole.Ask<int>($"{prompt}");
        }
        public string GetMenuChoiceFromUser(string title, string[] options)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(title)
                    .PageSize(10)
                    .AddChoices(options)
            );
        }

        public Customer GetCustomerDetails()
        {
            Console.WriteLine("Skapa en ny kund: ");

            return new Customer
            {
                FirstName = GetString("Förnamn: "),
                LastName = GetString("Efternamn: "),
                Address = GetString("Adress: "),
                City = GetString("Stad: "),
                PostalCode = GetString("Postnummer: "),
                Phone = GetString("Telefonnummer: "),
                Email = GetString("E-postadress: "),
                IsVip = GetBool("Är kunden VIP? (Ja/Nej): "),
                IsActive = true
            };
        }

        public T GetUpdatedValue<T>(string fieldName, T currentValue)
        {
            Console.WriteLine($"{fieldName}: {currentValue}");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                return currentValue;
            }
            try
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }
            catch
            {
                Console.WriteLine("Felaktig inmatning. Försök igen.");
                return currentValue;
            }
        }
        public bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }
    }
}

