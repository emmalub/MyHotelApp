using Microsoft.VisualBasic;
using MyHotelApp.Utilities.Menus;
using MyHotelApp.Services;
using MyHotelApp.Utilities.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MyHotelApp.Utilities.Menus
{
    public class MainMenu : MenuBase
    {
        private RoomService _roomService;

        public MainMenu(RoomService roomService)
        {
            _roomService = roomService;
        }
        protected override string[] MenuOptions => new[]
        {
            "BOKA RUM",
            "HANTERA BOKNING",
            "HANTERA KUND",
            "HANTERA RUM",
            "FAKTUROR",
            "STATISTIK",
            "Logga ut"
        };

        protected override void DisplayMenuHeader()
        {
            MenuHeader.MainMenuHeader();
        }
        protected override void HandleUserSelection()
        {
            switch (currentOption)
            {
                case 0:

                    break;

                case 1:

                    break;

                case 2:
                    var customerMenu = new CustomerMenu();
                    customerMenu.ShowMenu();
                    break;

                case 3:
                    var roomMenu = new RoomMenu(_roomService); 
                    roomMenu.ShowMenu();
                    break;

                case 4:
                    break;

                case 5:
                    break;

                case 6: // Avsluta
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

