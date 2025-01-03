using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHotelApp.Interfaces;

namespace MyHotelApp.Services
{
    public class MessageService
    {
        public void SendMessage(string recipient, string message)
        {
            Console.WriteLine($"Skickar e-post till {recipient} med följande meddelande: {message}");
        }

    }
}
