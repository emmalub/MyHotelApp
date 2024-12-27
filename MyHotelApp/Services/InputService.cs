using Spectre.Console;

namespace MyHotelApp.Services
{
    public class InputService
    {
        public int GetRoomIdFromUser(string prompt = "Ange rumID:")
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

