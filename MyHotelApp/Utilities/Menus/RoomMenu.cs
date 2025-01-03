using MyHotelApp.Interfaces;
using MyHotelApp.Services;
using MyHotelApp.Services.MenuHandlers;
using MyHotelApp.Utilities.Graphics;
using Spectre.Console;

namespace MyHotelApp.Utilities.Menus
{
    public class RoomMenu : MenuBase
    {
        private readonly IRoomService _roomService;
        private readonly InputService _inputService;
        private readonly RoomMenuHandler _roomMenuHandler;

        public RoomMenu(RoomService roomService, InputService inputService)
        {
            _roomService = roomService;
            _inputService = inputService;
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

        protected override void ShowMenu(string selectedOption)
        {
            switch (selectedOption)
            {
                case "VISA ALLA AKTIVA RUM":
                    Console.Clear();
                    _roomMenuHandler.DisplayActiveRooms();
                    break;

                case "VISA EJ AKTIVA RUM":
                    _roomMenuHandler.DisplayInactiveRooms();
                    break;

                case "LÄGG TILL RUM":
                    _roomService.CreateRoom();
                    break;

                case "AVAKTIVERA RUM":
                    int deactivateRoomId = _inputService.GetRoomIdFromUser("Ange rumID för att avaktivera rummet:");
                    _roomService.DeleteRoom(deactivateRoomId);
                    break;

                case "ÅTERAKTIVERA RUM":
                    //int activateRoomId = _inputService.GetRoomIdFromUser("Ange rumID för att aktivera rummet:");
                    _roomMenuHandler.ActivateRoom();
                    break;

                case "REDIGERA RUM":
                    _roomMenuHandler.HandleUpdateRoom();
                    break;

                case "Tillbaka till huvudmenyn":
                    //var mainMenu = new MainMenu();
                    //mainMenu.ShowMenu();
                    break;

                default:
                    AnsiConsole.MarkupLine("[red]Ogiltigt val. Försök igen.[/]");
                    break;
            }
        }
    }
}


