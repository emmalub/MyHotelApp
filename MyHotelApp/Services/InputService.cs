using Spectre.Console;

namespace MyHotelApp.Services
{
    public class InputService
    {
        public int GetId(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            int customerId;

            if (int.TryParse(input, out customerId))
            {
                return customerId;
            }
            else
            {
                Console.WriteLine("Ogiltigt ID. Försäk igen.");
                return 0;
            }
        }
        public DateTime GetDate(string prompt)
        {
            return AnsiConsole.Ask<DateTime>($"{prompt}");
        }
        public string GetString(string prompt)
        {
            while(true)
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
    }
}

