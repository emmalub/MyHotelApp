using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyHotelApp.Data;
using MyHotelApp.Interfaces;
using MyHotelApp.Services;
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


            builder.RegisterType<CreateSpecialOffer>().As<ICreateSpecialOffer>();
            builder.RegisterType<ReadSpecialOffer>().As<IReadSpecialOffer>();
            builder.RegisterType<UpdateSpecialOffer>().As<IUpdateSpecialOffer>();
            builder.RegisterType<DeleteSpecialOffer>().As<IDeleteSpecialOffer>();

            builder.RegisterType<HotelDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RoomService>().As<IRoomService>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<RoomMenu>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<BookingService>().As<BookingService>().InstancePerLifetimeScope();
            builder.RegisterType<BookingMenu>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BookingCalendar>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<CustomerService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CustomerMenu>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<InvoiceService>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<MainMenu>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<InputService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EmailMessageService>().As<IMessageService>();
            builder.RegisterType<WelcomeScreen>().AsSelf();

            builder.RegisterType<App>().AsSelf().InstancePerLifetimeScope();
            return builder.Build();
        }

    }
}
