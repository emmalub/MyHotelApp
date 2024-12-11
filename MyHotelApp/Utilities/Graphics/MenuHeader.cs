using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Utilities.Graphics
{
    internal class MenuHeader
    {
        public static void MainMenuHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"
                                      _  _ _   ___   ___   _ ___  __  __ ___ _  ___   __
                                     | || | | | \ \ / / | | |   \|  \/  | __| \| \ \ / /
                                     | __ | |_| |\ V /| |_| | |) | |\/| | _|| .` |\ V / 
                                     |_||_|\___/  \_/  \___/|___/|_|  |_|___|_|\_| |_|  
                ");
            Console.ResetColor();
        }

        public static void CustomerMenuHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"

                                        _  ___   _ _  _ ___  __  __ ___ _  ___   __
                                       | |/ / | | | \| |   \|  \/  | __| \| \ \ / /
                                       | ' <| |_| | .` | |) | |\/| | _|| .` |\ V / 
                                       |_|\_\\___/|_|\_|___/|_|  |_|___|_|\_| |_|  
");
            Console.ResetColor();
        }

        public static void RoomMenuHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"
                                        ___ _   _ __  __ ___ __  __ ___ _  ___   __
                                       | _ \ | | |  \/  / __|  \/  | __| \| \ \ / /
                                       |   / |_| | |\/| \__ \ |\/| | _|| .` |\ V / 
                                       |_|_\\___/|_|  |_|___/_|  |_|___|_|\_| |_|  
");

            Console.ResetColor();
        }
    }
}
