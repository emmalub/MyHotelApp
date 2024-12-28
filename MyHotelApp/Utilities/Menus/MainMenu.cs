using MyHotelApp.Data;
using MyHotelApp.Services;
using MyHotelApp.Utilities.Graphics;

namespace MyHotelApp.Utilities.Menus
{
    public class MainMenu : MenuBase
    {
        private readonly InputService _inputService;
        private readonly BookingCalendar _bookingCalendar;

        public MainMenu(InputService inputService, BookingCalendar bookingCalendar)
        {
            _inputService = inputService;
            _bookingCalendar = bookingCalendar;
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
                    _bookingCalendar.Show("Bokningskalender");
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

