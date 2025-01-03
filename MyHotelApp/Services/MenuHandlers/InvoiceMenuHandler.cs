using Microsoft.EntityFrameworkCore;
using MyHotelApp.Data;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services.MenuHandlers
{
    public class InvoiceMenuHandler
    {
        private readonly InvoiceService _invoiceService;
        private readonly InvoiceManagementService _invoiceManagementService;
        private readonly InputService _inputService;
        private readonly HotelDbContext _context;
        public void HandlePayInvoice()
        {
            int invoiceId = _inputService.GetId("Ange fakturanummer att betala: ");
            _invoiceService.PayInvoice(invoiceId);
        }
        public void HandleEditInvoice()
        {
            int invoiceId = _inputService.GetId("Ange fakturanummer att redigera: ");
            decimal newAmount = AnsiConsole.Ask<decimal>("Ange nytt belopp: ");
            DateTime newDueDate = _inputService.GetDate("Ange ny förfallodatum: ");
            _invoiceManagementService.EditInvoice();
        }
        public void HandleCancelInvoice()
        {
            int invoiceId = _inputService.GetId("Ange fakturanummer att makulera: ");
            _invoiceManagementService.CancelInvoice();
        }
       
    }
}
