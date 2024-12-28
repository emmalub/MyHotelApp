using Autofac;
using MyHotelApp.Services.SpecialOffers.Interfaces;
using MyHotelApp.Services.SpecialOffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHotelApp.Services;
using MyHotelApp.Utilities.Menus;
using MyHotelApp.Interfaces;
using MyHotelApp.Utilities.Graphics;
using MyHotelApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

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

            //builder.Register(c => c.Resolve<IContainer>()).As<IContainer>();

            builder.RegisterType<CreateSpecialOffer>().As<ICreateSpecialOffer>();
            builder.RegisterType<ReadSpecialOffer>().As<IReadSpecialOffer>();
            builder.RegisterType<UpdateSpecialOffer>().As<IUpdateSpecialOffer>();
            builder.RegisterType<DeleteSpecialOffer>().As<IDeleteSpecialOffer>();

            //builder.RegisterType<HotelDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RoomService>().As<IRoomService>().InstancePerLifetimeScope();
            builder.RegisterType<RoomMenu>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MainMenu>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<BookingCalendar>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<InputService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EmailMessageService>().As<IMessageService>();
            builder.RegisterType<WelcomeScreen>().AsSelf();
            //builder.RegisterType<Services.RoomService>().As<Services.RoomService>();
            //builder.RegisterType<MainMenu>().AsSelf().InstancePerDependency();

            builder.RegisterType<App>().AsSelf().InstancePerLifetimeScope();
            return builder.Build();
        }

    }
}
