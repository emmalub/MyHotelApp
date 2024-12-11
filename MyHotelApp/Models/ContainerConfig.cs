using Autofac;
using MyHotelApp.Services.SpecialOffers.Interfaces;
using MyHotelApp.Services.SpecialOffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
            return builder.Build();
        }

    }
}
