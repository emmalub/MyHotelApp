using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services
{
    public class BookingCalendar
    {
        private DateTime _currentMonth;
        private List<DateTime> _bookedDates; // Lista med bokade datum

        public BookingCalendar()
        {
            _currentMonth = DateTime.Today;
            _bookedDates = new List<DateTime>(); // Här kan du lägga till faktiska bokade datum
        }

        public void ShowCalendar()
        {
            while (true)
            {
                // Visa kalendern för den aktuella månaden
                DisplayMonthCalendar(_currentMonth);

                // Navigering mellan månader
                var navigation = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Navigera i kalendern")
                        .AddChoices("Förra månaden", "Nästa månad", "Avsluta")
                );

                if (navigation == "Förra månaden")
                {
                    _currentMonth = _currentMonth.AddMonths(-1);
                }
                else if (navigation == "Nästa månad")
                {
                    _currentMonth = _currentMonth.AddMonths(1);
                }
                else
                {
                    break; // Avsluta
                }

                // Efter att ha navigerat, välj ett datum att boka
                var selectedDate = AnsiConsole.Ask<DateTime>("Välj ett datum att boka (ange dag):");

                // Kontrollera om datumet är ledigt
                if (!_bookedDates.Contains(selectedDate))
                {
                    _bookedDates.Add(selectedDate); // Lägg till det valda datumet i listan
                    AnsiConsole.MarkupLine($"[bold green]Bokningen för {selectedDate.ToShortDateString()} är bekräftad![/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]Detta datum är redan bokat. Vänligen välj ett annat datum.[/]");
                }
            }
        }

        private void DisplayMonthCalendar(DateTime month)
        {
            // Skriv ut månadsrubrik
            AnsiConsole.MarkupLine($"[bold green]{month.ToString("MMMM yyyy")}[/]");

            // Skriv ut veckodagarna
            AnsiConsole.MarkupLine("[bold]M T W T F S S[/]");

            // Hitta första dagen i månaden
            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
            DateTime firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
            int startDay = (int)firstDayOfMonth.DayOfWeek;

            // Fyll i de tomma dagarna före den första dagen
            string calendarOutput = new string(' ', startDay * 3);

            // Lägg till alla dagar i månaden
            for (int day = 1; day <= daysInMonth; day++)
            {
                // Markera om datumet är bokat
                DateTime currentDate = new DateTime(month.Year, month.Month, day);
                string dayDisplay = _bookedDates.Contains(currentDate) ? $"[bold red]{day,2}[/]" : $"{day,2}";

                calendarOutput += dayDisplay + " "; // Skriv ut dagen
                startDay++;

                if (startDay > 6) // Om vi når söndag, gå till nästa rad
                {
                    calendarOutput += Environment.NewLine;
                    startDay = 0;
                }
            }

            // Skriv ut kalendern
            AnsiConsole.MarkupLine(calendarOutput);
        }
    }
}
