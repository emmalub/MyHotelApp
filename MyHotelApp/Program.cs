using Autofac;
using MyHotelApp.Models;
using MyHotelApp.Data;
using MyHotelApp.Services;
using MyHotelApp.Utilities.Menus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyHotelApp.Data;

namespace MyHotelApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();
            var hotelApp = container.Resolve<App>();

            App.Run();
        }
    }
}
