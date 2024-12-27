using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyHotelApp.Data;
using MyHotelApp.Services;
using MyHotelApp.Utilities.Menus;
using MyHotelApp.Models;

namespace MyHotelApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();
            var hotelApp = container.Resolve<App>();

            hotelApp.Run();
        }
    }
}
