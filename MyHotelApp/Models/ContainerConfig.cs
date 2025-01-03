using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyHotelApp.Data;
using MyHotelApp.Interfaces;
using MyHotelApp.Services;
using MyHotelApp.Services.MenuHandlers;
using MyHotelApp.Services.SpecialOffers;
using MyHotelApp.Services.SpecialOffers.Interfaces;
using MyHotelApp.Utilities.Graphics;
using MyHotelApp.Utilities.Menus;

namespace MyHotelApp.Models
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            builder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();
                optionsBuilder.UseSqlServer(connectionString);
                return new HotelDbContext(optionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<HotelDbContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<RoomService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RoomService>().As<IRoomService>().InstancePerLifetimeScope();
            builder.RegisterType<InputService>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<BookingService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceService>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<RoomManagementService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RoomMenuHandler>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<BookingMenuHandler>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CustomerMenuHandler>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceMenuHandler>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<RoomMenu>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BookingMenu>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CustomerMenu>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceMenu>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<InvoiceManagementService>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<MainMenu>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<BookingCalendar>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<MessageService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<WelcomeScreen>().AsSelf();

            builder.RegisterType<App>().AsSelf().InstancePerLifetimeScope();


            return builder.Build();

        }

    }
}
