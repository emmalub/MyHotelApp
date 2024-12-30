using MyHotelApp.Services;
using MyHotelApp.Services.MenuHandlers;
using Spectre.Console;
using System;

namespace MyHotelApp.Utilities.Menus
{
    public class InvoiceMenu
    {
        private readonly InvoiceService _invoiceService;
        private readonly InvoiceManagementService _invoiceManagementService;
        private readonly InvoiceMenuHandler _invoiceMenuHandler;

        public InvoiceMenu(InvoiceService invoiceService, InvoiceManagementService invoiceManagementService, InvoiceMenuHandler invoiceMenuHandler)
        {
            _invoiceService = invoiceService;
            _invoiceManagementService = invoiceManagementService;
            _invoiceMenuHandler = invoiceMenuHandler;
        }
        void DisplayInvoiceMenu()
        {
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold yellow]Välj ett alternativ för att hantera fakturor:[/]")
                        .PageSize(6)
                        .AddChoices(new[]
                        {
                    "Visa alla fakturor",
                    "Betala faktura",
                    "Redigera faktura",
                    "Makulera faktura",
                    "Kontrollera förfallna fakturor",
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
                        return;
                }
            }
        }
    }
}