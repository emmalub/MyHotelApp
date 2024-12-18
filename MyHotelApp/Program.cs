using Autofac;
using MyHotelApp.Interfaces;
using MyHotelApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyHotelApp.Data;


namespace MyHotelApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App.Run();
        }
    }
}
