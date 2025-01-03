using MyHotelApp.Data;
using MyHotelApp.Models;
using MyHotelApp.Services;
using MyHotelApp.Services.MenuHandlers;
using MyHotelApp.Utilities.Graphics;
using Spectre.Console;

namespace MyHotelApp.Utilities.Menus
{
    public class BookingMenu : MenuBase
    {
        private readonly BookingMenuHandler _bookingMenuHandler;

        public BookingMenu(BookingMenuHandler bookingMenuHandler)
        {
            _bookingMenuHandler = bookingMenuHandler;
        }
        protected override string[] MenuOptions =>
       [
           "BOKA RUM",
           "AVBOKA RUM",
           "VISA BOKNINGAR",
           "Tillbaka till huvudmenyn" ];
        protected override void DisplayMenuHeader()
        {
            MenuHeader.BookingMenuHeader();
        }
        protected override void ShowMenu(string selectedOption)
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(
                    new Panel("[bold yellow]Bokningsmeny[/]")
                        .Border(BoxBorder.Rounded)
                        .Expand());

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Vad vill du göra?")
                        .AddChoices(new[]
                        {
                        "BOKA RUM",
                        "AVBOKA RUM",
                        "VISA BOKNINGAR",
                        "Tillbaka till huvudmenyn"
                        }));

                switch (choice)
                {
                    case "Boka rum":
                        _bookingMenuHandler.BookRoom();
                        break;
                    case "Avboka rum":
                        _bookingMenuHandler.HandleDeleteBooking();
                        break;
                    case "Visa bokningar":
                        _bookingMenuHandler.DisplayBooking();
                        break;
                    case "Tillbaka till huvudmenyn":
                        return;
                }
            }
        }
    }
}
      

