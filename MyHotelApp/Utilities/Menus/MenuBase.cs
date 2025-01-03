using Spectre.Console;

namespace MyHotelApp.Utilities.Menus
{
    public abstract class MenuBase
    {
        protected bool menuActive = true;
        protected abstract string[] MenuOptions { get; }

        public void ShowMenu()
        {
            while (menuActive)
            {
                Console.Clear();
                DisplayMenuHeader();

                try
                {
                    var selectedOption = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .PageSize(10)
                            .AddChoices(MenuOptions)
                        );

                    ShowMenu(selectedOption);

                if (selectedOption == "Tillbaka till huvudmenyn")
                {
                    menuActive = false;
                }

                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[bold red]Ett fel uppstod: {ex.Message}[/]");
                    AnsiConsole.WriteLine("Försök igen.");
                }
            }
        }
        protected abstract void DisplayMenuHeader();
        protected abstract void ShowMenu(string selectedOption);

    }
}