using MyHotelApp.Utilities.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Utilities.Menus
{
    public class CustomerMenu : MenuBase
    {
        protected override string[] MenuOptions =>
        [
            "VISA KUND",
            "VISA ALLA KUNDER",
            "VISA BORTTAGNA KUNDER",
            "LÄGG TILL KUND",
            "TA BORT KUND",
            "ÄNDRA KUNDUPPGIFTER",
            "Tillbaka till huvudmenyn" ];


        protected override void DisplayMenuHeader()
        {
            MenuHeader.CustomerMenuHeader();
        }

        protected override void HandleUserSelection()
        {
            switch (currentOption)
            {
                case 0:

                    break;

                case 1:

                    break;

                case 2:

                    break;

                case 3:

                    break;

                case 4:

                    break;

                case 5:
                    break;

                case 6: // Avsluta
                    //var mainMenu = new MainMenu();
                    //mainMenu.ShowMenu();
                    break;

                default:
                    break;

            }
        }
    }
}

