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

namespace MyHotelApp.Models
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CreateSpecialOffer>().As<ICreateSpecialOffer>();
            builder.RegisterType<ReadSpecialOffer>().As<IReadSpecialOffer>();
            builder.RegisterType<UpdateSpecialOffer>().As<IUpdateSpecialOffer>();
            builder.RegisterType<DeleteSpecialOffer>().As<IDeleteSpecialOffer>();

            builder.RegisterType<HotelDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RoomService>().As<RoomService>().InstancePerLifetimeScope();
            builder.RegisterType<RoomMenu>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MainMenu>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<EmailMessageService>().As<IMessageService>();
            builder.RegisterType<Services.RoomService>().As<Services.RoomService>();
            builder.RegisterType<MainMenu>().AsSelf().InstancePerDependency();

            builder.RegisterType<WelcomeScreen>().AsSelf();


            builder.RegisterType<App>().AsSelf();
            return builder.Build();
        }

    }
}
