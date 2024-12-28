using Autofac;
using MyHotelApp.Models;

namespace MyHotelApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();
            var hotelApp = container.Resolve<App>();

            App.Start();
            hotelApp.Run();
        }
    }
}
