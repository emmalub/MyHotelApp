using MyHotelApp.Services.MenuHandlers;
using MyHotelApp.Utilities.Graphics;

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
           "Tillbaka till huvudmenyn"
       ];
        protected override void DisplayMenuHeader()
        {
            MenuHeader.BookingMenuHeader();
        }
        protected override void ShowMenu(string selectedOption)
        {
            switch (selectedOption)
            {
                case "BOKA RUM":
                    _bookingMenuHandler.BookRoom();
                    break;
                case "AVBOKA RUM":
                    _bookingMenuHandler.HandleDeleteBooking();
                    break;
                case "VISA BOKNINGAR":
                    _bookingMenuHandler.DisplayBooking();
                    break;
                case "Tillbaka till huvudmenyn":
                    menuActive = false;
                    return;
            }
        }
    }
}



