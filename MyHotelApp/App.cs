﻿using Autofac;
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
        private readonly MainMenu _mainMenu;
        private readonly InvoiceManagementService _invoiceManagementService;
        public App(MainMenu mainMenu, InvoiceManagementService invoiceManagementService)
        {
            _mainMenu = mainMenu;
            _invoiceManagementService = invoiceManagementService;
        }

        public static void Start()
        {
            using (var scope = ContainerConfig.Configure().BeginLifetimeScope())
            {
                var dbContext = scope.Resolve<HotelDbContext>();
                dbContext.Database.Migrate();
                DatabaseSeeder.Seed(dbContext);
            }
        }

        public void Run()
        {
            _invoiceManagementService.HandleOverdueInvoices();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(20, 16);
            WelcomeScreen.PrintStartScreenViking1();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(20, 10);
            WelcomeScreen.PrintStartScreenHotel4();
            Console.ResetColor();

            Thread.Sleep(3500);
            
            _mainMenu.ShowMenu();
        }
    }
}
