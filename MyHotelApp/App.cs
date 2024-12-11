using MyHotelApp.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.ComponentModel.DataAnnotations;
using MyHotelApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyHotelApp.Utilities.Graphics;

namespace MyHotelApp
{
    internal class App
    {
        public static void Run()
        {
            //var welcomeScreen = new WelcomeScreen();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(20, 16);
            WelcomeScreen.PrintStartScreenViking1();
            Console.ForegroundColor= ConsoleColor.DarkYellow;
            Console.SetCursorPosition(20, 10);
            WelcomeScreen.PrintStartScreenHotel4();
            Console.ResetColor();
            Console.ReadKey();

            var mainMenu = new MainMenu();
            mainMenu.ShowMenu();
        }
    }
}
