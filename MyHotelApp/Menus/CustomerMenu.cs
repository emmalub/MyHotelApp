﻿using MyHotelApp.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Menus
{
    public class CustomerMenu : MenuBase
    {
        protected override string[] MenuOptions =>
        [
            "VISA KUND",
            "VISA ALLA KUNDER",
            "VISA BORTTAGNA KUNDER",
            "LÄGG TILL KUND",
            "TA BORT KUND",
            "ÄNDRA KUNDUPPGIFTER",
            "Tillbaka till huvudmenyn" ];


        protected override void DisplayMenuHeader()
        {
            MenuHeader.CustomerMenuHeader();
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

                    break;

                case 3:

                    break;

                case 4:

                    break;

                case 5:
                    break;

                case 6: // Avsluta
                    var mainMenu = new MainMenu();
                    mainMenu.ShowMenu();
                    break;

                default:
                    break;

            }
        }
    }
}

//public static void ShowCustomerMenu()
//{

//    int currentOption = 0; // Håller reda på vilket alternativ som är markerat
//    bool menuActive = true;

//    //string[] menuOptions = { "VISA KUND", "VISA ALLA KUNDER", "VISA BORTTAGNA KUNDER", "LÄGG TILL KUND", "TA BORT KUND", "ÄNDRA KUNDUPPGIFTER", "Tillbaka till huvudmenyn" };

//    while (menuActive)
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
//        var key = Console.ReadKey(true);

//        if (key.Key == ConsoleKey.UpArrow)
//        {
//            // Om användaren trycker på upppilen, gå upp i listan (om vi inte är längst upp)
//            if (currentOption > 0)
//            {
//                currentOption--;
//            }
//        }
//        else if (key.Key == ConsoleKey.DownArrow)
//        {
//            // Om användaren trycker på nedpilen, gå ner i listan (om vi inte är längst ner)
//            if (currentOption < menuOptions.Length - 1)
//            {
//                currentOption++;
//            }
//        }
//        else if (key.Key == ConsoleKey.Enter)
//        {

// När menyn avslutas, återställ färger
