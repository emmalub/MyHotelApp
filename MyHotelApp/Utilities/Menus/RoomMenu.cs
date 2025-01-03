using MyHotelApp.Interfaces;
using MyHotelApp.Services;
using MyHotelApp.Services.MenuHandlers;
using MyHotelApp.Utilities.Graphics;
using Spectre.Console;

namespace MyHotelApp.Utilities.Menus
{
    public class RoomMenu : MenuBase
    {
        private readonly RoomService _roomService;
        private readonly InputService _inputService;
        private readonly RoomMenuHandler _roomMenuHandler;
        private readonly RoomManagementService _roomManagementService;

        public RoomMenu(
            RoomService roomService, 
            InputService inputService, 
            RoomMenuHandler roomMenuHandler, 
            RoomManagementService roomManagementService)
        {
            _roomService = roomService;
            _inputService = inputService;
            _roomMenuHandler = roomMenuHandler;
            _roomManagementService = roomManagementService;
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
                    _roomManagementService.DisplayActiveRooms();
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
                    _roomManagementService.ActivateRoom();
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


