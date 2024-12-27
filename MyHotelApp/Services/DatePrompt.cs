using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services
{
    public class DatePrompt : Prompt<DateTime>
    {
        public DatePrompt Prompt(string label)
        {
            _label = label;
            return _label;
        }
    }
}
