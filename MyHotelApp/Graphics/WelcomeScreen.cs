using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MyHotelApp.Graphics
{
    internal class WelcomeScreen
    {
        public static void PrintStartScreenOne()
        {
            Console.WriteLine(@"
 ,ggg,        gg                                            
dP''Y8b       88                I8             ,dPYb, ,dPYb,
Yb, `88       88                I8             IP'`Yb IP'`Yb
 `'  88       88             88888888          I8  8I I8  8I
     88aaaaaaa88                I8             I8  8' I8  8'
     88'''''''88     ,ggggg,    I8     ,ggg,   I8 dP  I8 dP 
     88       88   dP'  'Y8ggg¨ I8    i8' '8i  I8dP   I8dP  
     88       88  i8'    ,8I   ,I8,   I8, ,8I  I8P    I8P   
     88       Y8,,d8,   ,d8'  ,d88b,  `YbadP' ,d8b,_ ,d8b,_ 
     88       `Y8P'Y8888P'' ¨88P''Y88888P''Y8888P''Y888P''Y88.

");
        }

        public static void PrintStartScreenTwo()
        {
            Console.WriteLine(@"
                                          ooooo   ooooo               .             oooo  oooo  
                                          `888'   `888'             .o8             `888  `888  
                                           888     888   .ooooo.  .o888oo  .ooooo.   888   888  
                                           888ooooo888  d88' `88b   888   d88' `88b  888   888  
                                           888     888  888   888   888   888ooo888  888   888  
                                           888     888  888   888   888 . 888    .o  888   888  
                                          o888o   o888o `Y8bod8P'  ''888''`Y8bod8P' o888o o888o 
");
            }
        public static void PrintStartScreenHotel3()
        {
            Console.WriteLine(@"
                                    ,--.  ,--. ,-----. ,--------.,------.,--.   ,--.    
                                    |  '--'  |'  .-.  ''--.  .--'|  .---'|  |   |  |    
                                    |  .--.  ||  | |  |   |  |   |  `--, |  |   |  |    
                                    |  |  |  |'  '-'  '   |  |   |  `---.|  '--.|  '--. 
                                    `--'  `--' `-----'    `--'   `------'`-----'`-----' 
");
        }
        public static void PrintStartScreenHotel4()
        {
            Console.WriteLine(@"
                                    db   db  .d88b.  d888888b d88888b db      db      
                                    88   88 .8P  Y8. `~~88~~' 88'     88      88      
                                    88ooo88 88    88    88    88ooooo 88      88      
                                    88~~~88 88    88    88    88~~~~~ 88      88      
                                    88   88 `8b  d8'    88    88.     88booo. 88booo. 
                                    YP   YP  `Y88P'     YP    Y88888P Y88888P Y88888P 
");
        }

        public static void PrintStartScreenEmma1()
        {
            Console.WriteLine(@"
oooooooooooo                                               
`888'     `8                                               
 888         ooo. .oo.  .oo.   ooo. .oo.  .oo.    .oooo.   
 888oooo8    `888P''Y88bP'Y88b`888P'Y88bP''Y88b  `P  )88b  
 888    ''    888   888   888   888   888   888  .oP''888  
 888       o  888   888   888   888   888   888  d8(  888  
o888ooooood8 o888o o888o o888o o888o o888o o888o `Y888'''8o 
");
        }

        public static void PrintStartScreenEmma2()
        {
            Console.WriteLine(@"
   ,ggggggg,                                                  
 ,dP''''''Y8b                                                 
 d8'    a  Y8                                                 
 88     'Y8P'                                                 
 `8baaaa                                                      
,d8P'''''      ,ggg,,ggg,,ggg,    ,ggg,,ggg,,ggg,     ,gggg,gg 
d8'          ,8' '8P' '8P' '8,  ,8' '8P' '8P' '8,   dP'  'Y8I 
Y8,          I8   8I   8I   8I  I8   8I   8I   8I  i8'    ,8I 
`Yba,,_____,,dP   8I   8I   Yb,,dP   8I   8I   Yb,,d8,   ,d8b,
  `'Y88888888P'   8I   8I   `Y88P'   8I   8I   `Y8P'Y8888P'`Y8
                                                              
");
        }
        public static void PrintStartScreenViking1()
        {
            Console.WriteLine(@"
                     ,ggg,         ,gg                                               
                     dP''Y8a       ,8P      ,dPYb,                                    
                     Yb, `88       d8'      IP'`Yb                                    
                      `'  88       88  gg   I8  8I      gg                            
                          88       88  ''   I8  8bgg,   ''                            
                          I8       8I  gg   I8 dP' '8   gg    ,ggg,,ggg,     ,gggg,gg 
                          `8,     ,8'  88   I8d8bggP'   88   ,8' '8P' '8,   dP'  'Y8I 
                           Y8,   ,8P   88   I8P' 'Yb,   88   I8   8I   8I  i8'    ,8I 
                            Yb,_,dP  _,88,_,d8    `Yb,_,88,_,dP   8I   Yb,,d8,   ,d8I 
                             'Y8P'   8P''Y888P      Y88P''Y88P'   8I   `Y8P'Y8888P'888
                                                                                 ,d8I'
                                                                              ,dP'8I 
                                                                            ,8'  8I 
                                                                            I8   8I 
                                                                            `8, ,8I 
                                                                             `Y8P'                                                   
");
        }
    }
}
