using Autofac;
using Microsoft.EntityFrameworkCore;
using MyHotelApp.Data;
using MyHotelApp.Interfaces;
using MyHotelApp.Models;
using MyHotelApp.Services;
using MyHotelApp.Utilities.Graphics;
using MyHotelApp.Utilities.Menus;

namespace MyHotelApp
{
    internal class App
    {
        private readonly IRoomService _roomService;
        private readonly MainMenu _mainMenu;
        public App(IRoomService roomService, MainMenu mainMenu)
        {
            _roomService = roomService;
            _mainMenu = mainMenu;
        }

        public static void Start()
        {
            using (var scope = ContainerConfig.Configure().BeginLifetimeScope())
            {
                var dbContext = scope.Resolve<HotelDbContext>();
                dbContext.Database.Migrate();
            }
        }

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(20, 16);
            WelcomeScreen.PrintStartScreenViking1();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(20, 10);
            WelcomeScreen.PrintStartScreenHotel4();
            Console.ResetColor();

            Thread.Sleep(1000);
            //Console.ReadKey();

            _roomService.SeedRooms();
            _mainMenu.ShowMenu();
        }
    }
}
