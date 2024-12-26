using MyHotelApp.Services;
using MyHotelApp.Utilities.Graphics;
using MyHotelApp.Models;

namespace MyHotelApp.Utilities.Menus
{
    public class RoomMenu : MenuBase
    {
        private readonly RoomService _roomService;

        public RoomMenu(RoomService roomService)
        {
            _roomService = roomService;
        }

        protected override string[] MenuOptions =>
        [
            "VISA ALLA AKTIVA RUM",
            "VISA EJ AKTIVA RUM",
            "LÄGG TILL RUM",
            "AVAKTIVERA RUM",
            "ÅTERAKTIVERA RUM",
            "REDIGERA RUM",
            "Tillbaka till huvudmenyn"
            ];

        protected override void DisplayMenuHeader()
        {
            MenuHeader.RoomMenuHeader();
        }

        protected override void HandleUserSelection()
        {
            //////////////////////// TEST
            //if (_roomService == null)
            //{
            //    Console.WriteLine("RoomService is null!");
            //    return;
            //}

            //Console.WriteLine($"Current option selected: {currentOption}"); // Debugging


            ///////////////////////// TEST

            switch (currentOption)
            {
                case 0:
                    _roomService.DisplayActiveRooms();
                    break;

                case 1:
                    _roomService.DisplayInactiveRooms();
                    break;

                case 2:
                    _roomService.CreateRoom();
                    break;

                case 3:

                    break;

                case 4:

                    break;

                case 5:
                    
                    break;

                case 6: // Avsluta
                    //var mainMenu = new MainMenu();
                    //mainMenu.ShowMenu();
                    break;

                default:
                    Console.WriteLine("Gör ett val för att fortsätta");
                    break;
            }
        }
    }
}


