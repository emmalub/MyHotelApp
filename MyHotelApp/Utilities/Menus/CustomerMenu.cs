using MyHotelApp.Services;
using MyHotelApp.Services.MenuHandlers;
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
        private readonly CustomerMenuHandler _customerMenuHandler;

        public CustomerMenu(CustomerService customerService, CustomerMenuHandler customerMenuHandler)
        {
            _customerService = customerService;
            _customerMenuHandler = customerMenuHandler;
        }

        protected override string[] MenuOptions =>
        [
            "VISA KUNDUPPGIFTER",
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

        protected override void ShowMenu(string selectedOption)
        {
            int customerId = 0;
            switch (selectedOption)
            {
                case "VISA KUNDUPPGIFTER":
                    _customerMenuHandler.ShowAllCustomers(_customerService.GetCustomers());
                    _customerMenuHandler.ShowCustomer();
                    break;

                case "VISA ALLA KUNDER":
                    var myCustomers = _customerService.GetCustomers();
                    _customerMenuHandler.ShowAllCustomers(myCustomers);
                    break;

                case "VISA BORTTAGNA KUNDER":
                    _customerMenuHandler.ShowDeletedCustomers();
                    break;

                case "LÄGG TILL KUND":
                    _customerMenuHandler.CreateNewCustomer();
                    break;

                case "TA BORT KUND":
                    _customerMenuHandler.ShowAllCustomers(_customerService.GetCustomers());
                    _customerMenuHandler.DeleteCustomer(customerId);
                    break;

                case "ÄNDRA KUNDUPPGIFTER":
                    _customerMenuHandler.ShowAllCustomers(_customerService.GetCustomers());
                    _customerMenuHandler.UpdateCustomer();
                    break;

                case "Tillbaka till huvudmenyn":
                    menuActive = false;
                    break;

                default:
                    break;
            }
        }
    }
}

