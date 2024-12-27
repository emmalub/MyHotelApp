using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyHotelApp.Data;
using MyHotelApp.Interfaces;
using MyHotelApp.Services;
using MyHotelApp.Utilities.Menus;
using MyHotelApp.Utilities.Graphics;
using MyHotelApp.Models;
using MyHotelApp.Services.Interfaces;

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
           
            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile($"appsettings.json", true, true);
            //var config = builder.Build();
            //var connectionString = config.GetConnectionString("DefaultConnection");

            //var options = new DbContextOptionsBuilder<HotelDbContext>()
            //    .UseSqlServer(connectionString);

            //using (var dbContext = new HotelDbContext(options.Options))
            //{
            //    dbContext.Database.Migrate();
            //}
        }

        public void Run()
        {
            //using (var scope = _container.BeginLifetimeScope())
            //{
            //    var roomService = scope.Resolve<RoomService>();
                _roomService.SeedRooms();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(20, 16);
                WelcomeScreen.PrintStartScreenViking1();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(20, 10);
                WelcomeScreen.PrintStartScreenHotel4();
                Console.ResetColor();
                Console.ReadKey();

                //var mainMenu = scope.Resolve<MainMenu>();
                _mainMenu.ShowMenu();
            //}
        }
    }
}
