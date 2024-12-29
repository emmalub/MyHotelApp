using MyHotelApp.Data;
using MyHotelApp.Services;
using MyHotelApp.Utilities.Graphics;

namespace MyHotelApp.Utilities.Menus
{
    public class MainMenu : MenuBase
    {
        private readonly InputService _inputService;
        private readonly BookingMenu _bookingMenu;
        private readonly CustomerMenu _customerMenu;
        private readonly RoomMenu _roomMenu;
        private readonly BookingService _bookingService;

        public MainMenu(
            InputService inputService, 
            BookingMenu bookingMenu, 
            CustomerMenu customerMenu,
            RoomMenu roomMenu,
            BookingService bookingService
            )
        {
            _inputService = inputService;
            _bookingMenu = bookingMenu;
            _customerMenu = customerMenu;
            _roomMenu = roomMenu;
            _bookingService = bookingService;
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
                    _bookingMenu.BookRoom();
                    break;

                case "HANTERA BOKNING":
                    _bookingService.DisplayBooking();
                    break;

                case "HANTERA KUND":
                    _customerMenu.ShowMenu();
                    break;

                case "HANTERA RUM":
                    _roomMenu.ShowMenu();
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

