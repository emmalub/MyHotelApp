using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Interfaces
{
    internal interface IMessageService
    {
        public void SendMessage(string recipient, string message);
        
      
    }
}
