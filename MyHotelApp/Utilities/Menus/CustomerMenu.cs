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

        protected override void HandleUserSelection(string selectedOption)
        {
            switch (selectedOption)
            {
                case "VISA KUND":

                    break;

                case "VISA ALLA KUNDER":

                    break;

                case "VISA BORTTAGNA KUNDER":

                    break;

                case "LÄGG TILL KUND":

                    break;

                case "TA BORT KUND":

                    break;

                case "ÄNDRA KUNDUPPGIFTER":
                    break;

                case "Tillbaka till huvudmenyn":
                    //var mainMenu = new MainMenu();
                    //mainMenu.ShowMenu();
                    break;

                default:
                    break;

            }
        }
    }
}

