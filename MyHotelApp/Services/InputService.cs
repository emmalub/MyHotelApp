using Spectre.Console;

namespace MyHotelApp.Services
{
    public class InputService
    {
        public int GetId(string prompt)
        {
            Console.WriteLine(prompt);
            while(true)
            {
                if (int.TryParse(Console.ReadLine(), out int id) && id > 0)
                {
                    return id;
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning. Försök igen.");
                }
            }
        }
        public DateTime GetDate(string prompt)
        {
            return AnsiConsole.Ask<DateTime>($"{prompt}");
        }
        public string GetString(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Felaktig inmatning. Försök igen.");
                Console.Write(prompt);
                input = Console.ReadLine();
            }
            return input;
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

