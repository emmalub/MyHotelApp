using MyHotelApp.Interfaces;
using MyHotelApp.Services;
using MyHotelApp.Utilities.Graphics;
using Spectre.Console;

namespace MyHotelApp.Utilities.Menus
{
    public class RoomMenu : MenuBase
    {
        private readonly IRoomService _roomService;
        private readonly InputService _inputService;

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

        protected override void HandleUserSelection(string selectedOption)
        {
            switch (selectedOption)
            {
                case "VISA ALLA AKTIVA RUM":
                    Console.Clear();
                    _roomService.DisplayActiveRooms();
                    break;

                case "VISA EJ AKTIVA RUM":
                    _roomService.DisplayInactiveRooms();
                    break;

                case "LÄGG TILL RUM":
                    _roomService.CreateRoom();
                    break;

                case "AVAKTIVERA RUM":
                    int deactivateRoomId = _inputService.GetRoomIdFromUser("Ange rumID för att avaktivera rummet:");
                    _roomService.DeleteRoom(deactivateRoomId);
                    break;

                case "AKTIVERA RUM":
                    int activateRoomId = _inputService.GetRoomIdFromUser("Ange rumID för att aktivera rummet:");
                    _roomService.ActivateRoom(activateRoomId);
                    break;

                case "REDIGERA RUM":
                    _roomService.UpdateRoom();
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


