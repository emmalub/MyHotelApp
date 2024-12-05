using Autofac;
using MyHotelApp.Interfaces;
using MyHotelApp.Services;
using Microsoft.EntityFrameworkCore;


namespace MyHotelApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterType<EmailMessageService>()
                .As<IMessageService>();
            var container = builder.Build();

            var messageService = container.Resolve<IMessageService>();
            messageService = container.Resolve<IMessageService>();

            App.Run();
        }
    }
}
