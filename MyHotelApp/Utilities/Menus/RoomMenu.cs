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
                    _roomManagementService.DisplayActiveRooms();
                    break;

                case "VISA EJ AKTIVA RUM":
                    _roomManagementService.DisplayInactiveRooms();
                    break;

                case "LÄGG TILL RUM":
                    _roomService.CreateRoom();
                    break;

                case "AVAKTIVERA RUM":
                    _roomManagementService.DeactivateRoom();
                    break;

                case "ÅTERAKTIVERA RUM":
                    _roomManagementService.ActivateRoom();
                    break;

                case "REDIGERA RUM":
                    _roomMenuHandler.HandleUpdateRoom();
                    break;

                case "Tillbaka till huvudmenyn":
                    menuActive = false;
                    break;

                default:
                    AnsiConsole.MarkupLine("[red]Ogiltigt val. Försök igen.[/]");
                    break;
            }
        }
    }
}


