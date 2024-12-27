using Microsoft.EntityFrameworkCore;
using MyHotelApp.Data;
using MyHotelApp.Interfaces;
using MyHotelApp.Services;
using MyHotelApp.Utilities.Graphics;
using Spectre.Console;

namespace MyHotelApp.Utilities.Menus
{
    public class MainMenu : MenuBase
    {
        private readonly InputService _inputService;

        public MainMenu(InputService inputService)
        {
            _inputService = inputService;
        }
        protected override string[] MenuOptions =>
        [
        "BOKA RUM",
        "HANTERA BOKNING",
        "HANTERA KUND",
        "HANTERA RUM",
        "FAKTUROR",
        "STATISTIK",
        "Logga ut"
        ];
        protected override void DisplayMenuHeader()
        {
            MenuHeader.MainMenuHeader();
        }
        protected override void HandleUserSelection(string selectedOption)
        {
            switch (selectedOption)
            {
                case "BOKA RUM":

                    break;

                case "HANTERA BOKNING":

                    break;

                case "HANTERA KUND":
                    var customerMenu = new CustomerMenu();
                    customerMenu.ShowMenu();
                    break;

                case "HANTERA RUM":
                    var dbContext = new HotelDbContext();
                    var roomService = new RoomService(dbContext);
                    var roomMenu = new RoomMenu(roomService, _inputService);
                    roomMenu.ShowMenu();
                    break;

                case "FAKTUROR":
                    break;

                case "STATISTIK":
                    break;

                case "Logga ut": // Avsluta
                    Console.SetCursorPosition(0, 40);
                    Console.WriteLine("Avslutar...");
                    break;

                default:
                    Console.WriteLine("Gör ett val för att fortsätta..");
                    break;
            }
        }
    }
}

