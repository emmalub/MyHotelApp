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
        protected override void ShowMenu(string SelectedOption)
        {
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold yellow]Välj ett alternativ för att hantera fakturor:[/]")
                        .PageSize(6)
                        .AddChoices(new[]
                        {
                    "VISA ALLA FAKTUROR",
                    "BETALA FAKTURA",
                    "REDIGERA FAKTURA",
                    "MAKULERA FAKTURA",
                    "KONTROLLERA FÖRFALLNA FAKTUROR",
                    "Tillbaka till huvudmenyn"
                        }));
                switch (choice)
                {
                    case "Visa alla fakturor":
                        _invoiceManagementService.DisplayInvoices();
                        break;
                    case "Betala faktura":
                        _invoiceManagementService.PayInvoice();
                        break;
                    case "Redigera faktura":
                        _invoiceManagementService.EditInvoice();
                        break;
                    case "Makulera faktura":
                        _invoiceManagementService.CancelInvoice();
                        break;
                    case "Kontrollera förfallna fakturor":
                        _invoiceMenuHandler.HandleOverdueInvoices();
                        break;
                    case "Tillbaka till huvudmenyn":
                        menuActive = false;
                        return;
                }
            }
        }
    }
}