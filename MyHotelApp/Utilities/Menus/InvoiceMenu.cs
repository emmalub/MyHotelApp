using MyHotelApp.Services;
using MyHotelApp.Services.MenuHandlers;
using MyHotelApp.Utilities.Graphics;
using Spectre.Console;
using System;

namespace MyHotelApp.Utilities.Menus
{
    public class InvoiceMenu : MenuBase
    {
        private readonly InvoiceManagementService _invoiceManagementService;
        private readonly InvoiceMenuHandler _invoiceMenuHandler;

        public InvoiceMenu(InvoiceManagementService invoiceManagementService, InvoiceMenuHandler invoiceMenuHandler)
        {
            _invoiceManagementService = invoiceManagementService;
            _invoiceMenuHandler = invoiceMenuHandler;
        }
        protected override string[] MenuOptions =>
      [
          "VISA ALLA FAKTUROR",
          "BETALA FAKTURA",
          "REDIGERA FAKTURA",
          "MAKULERA FAKTURA",
          "KONTROLLERA FÖRFALLNA FAKTUROR",
          "Tillbaka till huvudmenyn"];
        protected override void DisplayMenuHeader()
        {
            MenuHeader.InvoiceMenuHeader();
        }
        protected override void ShowMenu(string selectedOption)
        {
            switch (selectedOption)
            {
                case "VISA ALLA FAKTUROR":
                    _invoiceManagementService.DisplayInvoices();
                    break;
                case "BETALA FAKTURA":
                    _invoiceManagementService.PayInvoice();
                    break;
                case "REDIGERA FAKTURA":
                    _invoiceManagementService.EditInvoice();
                    break;
                case "MAKULERA FAKTURA":
                    _invoiceManagementService.CancelInvoice();
                    break;
                case "KONTROLLERA FÖRFALLNA FAKTUROR":
                    _invoiceMenuHandler.HandleOverdueInvoices();
                    break;
                case "Tillbaka till huvudmenyn":
                    menuActive = false;
                    return;
            }
        }
    }
}
