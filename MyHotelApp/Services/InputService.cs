using Spectre.Console;

namespace MyHotelApp.Services
{
    public class InputService
    {
        public int GetId(string prompt)
        {
            return AnsiConsole.Ask<int>("Ange ID: ");
        }
        public DateTime GetDate(string prompt)
        {
            return AnsiConsole.Ask<DateTime>($"{prompt}");
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

