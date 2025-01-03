using MyHotelApp.Data;
using MyHotelApp.Services;
using MyHotelApp.Services.MenuHandlers;
using MyHotelApp.Utilities.Graphics;

namespace MyHotelApp.Utilities.Menus
{
    public class MainMenu : MenuBase
    {
        private readonly CustomerMenu _customerMenu;
        private readonly RoomMenu _roomMenu;
        private readonly BookingMenu _bookingMenu;
        private readonly BookingMenuHandler _bookingMenuHandler;
        private readonly InvoiceMenu _invoiceMenu;

        public MainMenu(
            BookingMenu bookingMenu,
            CustomerMenu customerMenu,
            RoomMenu roomMenu,
            BookingMenuHandler bookingMenuHandler,
            InvoiceMenu invoiceMenu
            )
        {
            _bookingMenu = bookingMenu;
            _customerMenu = customerMenu;
            _roomMenu = roomMenu;
            _bookingMenuHandler = bookingMenuHandler;
            _invoiceMenu = invoiceMenu;
        }
        protected override string[] MenuOptions =>
        [
        "BOKA RUM",
        "HANTERA BOKNINGAR",
        "HANTERA KUND",
        "HANTERA RUM",
        "FAKTUROR",
        "Logga ut"
        ];
        protected override void DisplayMenuHeader()
        {
            MenuHeader.MainMenuHeader();
        }
        protected override void ShowMenu(string selectedOption)
        {
            switch (selectedOption)
            {
                case "BOKA RUM":
                    _bookingMenuHandler.BookRoom();
                    break;

                case "HANTERA BOKNINGAR":
                    _bookingMenu.ShowMenu();
                    break;

                case "HANTERA KUND":
                    _customerMenu.ShowMenu();
                    break;

                case "HANTERA RUM":
                    _roomMenu.ShowMenu();
                    break;

                case "FAKTUROR":
                    _invoiceMenu.ShowMenu();
                    break;

                case "Logga ut":
                    Console.WriteLine("Avslutar...");
                    menuActive = false;
                    break;

                default:
                    Console.WriteLine("Gör ett val för att fortsätta..");
                    break;
            }
        }
    }
}

