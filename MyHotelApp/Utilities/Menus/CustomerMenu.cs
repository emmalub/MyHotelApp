using MyHotelApp.Services;
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
        private readonly CustomerService _customerService;
        private readonly InputService _inputService;

        public CustomerMenu(CustomerService customerService, InputService inputService)
        {
            _customerService = customerService;
            _inputService = inputService;
        }

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
            int customerId = 0;
            switch (selectedOption)
            {
                case "VISA KUND":
                    customerId = _inputService.GetId("Ange kundID för att visa kund: ");
                    var customer = _customerService.GetCustomerById(customerId);
                    break;

                case "VISA ALLA KUNDER":
                    var myCustomers = _customerService.GetCustomers();
                    _customerService.ShowAllCustomers(myCustomers);
                    break;

                case "VISA BORTTAGNA KUNDER":
                    _customerService.ShowDeletedCustomers();
                    break;

                case "LÄGG TILL KUND":
                    _customerService.CreateCustomer();
                    break;

                case "TA BORT KUND":
                    _inputService.GetId("Ange kundID för att visa kund: ");
                    _customerService.DeleteCustomer(customerId);
                    break;

                case "ÄNDRA KUNDUPPGIFTER":
                    _customerService.UpdateCustomer(customerId);
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

