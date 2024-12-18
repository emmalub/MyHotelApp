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

namespace MyHotelApp
{
    internal class App
    {
        private readonly IContainer _container;
        public App() 
        {
            var builder = new ContainerBuilder();

            ContainerConfig.Configure();

            _container = builder.Build();

        }

        public static void Start()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<HotelDbContext>()
                .UseSqlServer(connectionString);

            using (var dbContext = new HotelDbContext(options.Options))
            {
                dbContext.Database.Migrate();
            }
        }

        public static void Run()
        {

            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(20, 16);
                WelcomeScreen.PrintStartScreenViking1();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(20, 10);
                WelcomeScreen.PrintStartScreenHotel4();
                Console.ResetColor();
                Console.ReadKey();

                var mainMenu = scope.Resolve<MainMenu>();
                mainMenu.ShowMenu();
            }
        }
    }
}
