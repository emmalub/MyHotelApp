using Microsoft.VisualBasic;
using MyHotelApp.Utilities.Menus;
using MyHotelApp.Services;
using MyHotelApp.Utilities.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Utilities.Menus
{
    public class MainMenu : MenuBase
    {

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
                    var roomMenu = new RoomMenu();
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


//while (menuActive)
//    {
//        Console.Clear();
//        Console.ForegroundColor = ConsoleColor.DarkYellow;
//        MenuHeader.MainMenuHeader();

//        int centerX = (Console.WindowWidth / 2);

//        // Skriv menyalternativen med markering på det valda alternativet
//        for (int i = 0; i < menuOptions.Length; i++)
//        {
//            string optionText = menuOptions[i];
//            int optionLength = optionText.Length;
//            int optionX = centerX - (optionLength / 2);
//            int optionY = 10 + i * 2;

//            Console.SetCursorPosition(optionX, optionY);

//            // Om det här alternativet är valt, markera det med en annan färg
//            if (i == currentOption)
//            {
//                Console.BackgroundColor = ConsoleColor.DarkGray;
//                Console.ForegroundColor = ConsoleColor.White;
//            }
//            else
//            {
//                Console.BackgroundColor = ConsoleColor.Black;
//                Console.ForegroundColor = ConsoleColor.White;
//            }
//            Console.WriteLine(optionText);
//            Console.ResetColor(); // Återställ färger efter varje alternativ
//        }

//        // Läsa in användarens tangenttryckning
//        currentOption = MenuService
//            .GetUpdatedOption(currentOption, menuOptions
//            .Length);

///////////////////////////////////
// LAGT I MENUSERVICE
//var key = Console.ReadKey(true);

//if (key.Key == ConsoleKey.UpArrow)
//{
//    // Om användaren trycker på upppilen, gå upp i listan (om vi inte är längst upp)
//    if (currentOption > 0)
//    {
//        currentOption--;
//    }
//}
//else if (key.Key == ConsoleKey.DownArrow)
//{
//    // Om användaren trycker på nedpilen, gå ner i listan (om vi inte är längst ner)
//    if (currentOption < menuOptions.Length - 1)
//    {
//        currentOption++;
//    }
//}
//else if (key.Key == ConsoleKey.Enter)
//////////////////////////////////////

//if (key.Key == ConsoleKey.Enter)
//{
//    MenuService.HandleUserSelection(currentOption);
//}


// När menyn avslutas, återställ färger
//Console.ResetColor();
//        }

//    }
//}

