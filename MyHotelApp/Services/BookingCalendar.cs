using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Services
{
    public class BookingCalendar
    {
        private DateTime _currentDate = DateTime.Today;
        private DateTime? _selectedDate = null;
        private List<DateTime> _bookedDates;
        private string _calendarMessage;

        public BookingCalendar() 
        {
            _bookedDates = new List<DateTime>();
        }

        public BookingCalendar(List<DateTime> bookedDates)
        {
            _currentDate = DateTime.Today;
            _bookedDates = bookedDates ?? new List<DateTime>();
            _calendarMessage = "Välj ett incheckningsdatum";
        }

        public DateTime? Show(string message)
        {
            _calendarMessage = message;

            while (true)
            {
                Console.Clear();
                RenderCalendar();

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.LeftArrow)
                    _currentDate = _currentDate.AddDays(-1);
                else if (key.Key == ConsoleKey.RightArrow)
                    _currentDate = _currentDate.AddDays(1);
                else if (key.Key == ConsoleKey.UpArrow)
                    _currentDate = _currentDate.AddDays(-7);
                else if (key.Key == ConsoleKey.DownArrow)
                    _currentDate = _currentDate.AddDays(7);

                else if (key.Key == ConsoleKey.Enter)
                {
                    if (!_bookedDates.Contains(_currentDate))
                    {
                        _selectedDate = _currentDate;
                        break;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold red]Det valda datumet är inte tillgängligt. Vänligen välj ett annat datum.[/]");
                        Thread.Sleep(1500);
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    _selectedDate = null;
                    break;
                }
            }
            return _selectedDate;
        }

        private void RenderCalendar()
        {
            var firstDayOfMonth = new DateTime(_currentDate.Year, _currentDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var grid = new Grid();
                grid.AddColumns(0);

            foreach (var day in new[] { "Mån", "Tis", "Ons", "Tors", "Fre", "Lör", "Sön" })
                grid.AddColumn(new GridColumn().Centered());

            grid.AddRow(new Markup("[bold]Mån[/]"), new Markup("[bold]Tis[/]"), new Markup("[bold]Ons[/]"),
                        new Markup("[bold]Tors[/]"), new Markup("[bold]Fre[/]"), new Markup("[bold]Lör[/]"), new Markup("[bold]Sön[/]"));

            var currentDay = firstDayOfMonth;
            var row = new List<string>();
            for (int i = 1; i < (int)firstDayOfMonth.DayOfWeek; i++)
                row.Add(" ");

            while (currentDay <= lastDayOfMonth)
            {
                if (currentDay == _currentDate)
                    row.Add($"[bold green]{currentDay.Day}[/]");

                else if (_bookedDates.Contains(currentDay))
                    row.Add($"[bold red]{currentDay.Day}[/]");

                else
                    row.Add(currentDay.Day.ToString());

                if (row.Count == 7 || currentDay == lastDayOfMonth)
                {
                    grid.AddRow(row.ToArray());
                    row.Clear();
                }

                currentDay = currentDay.AddDays(1);
            }
            AnsiConsole.Write(new Panel(grid).Header($"[bold blue]{_currentDate: MMMM yyyy}[/]"));
        }
    
           
        //public void ShowCalendar()
        //{
        //    while (true)
        //    {
        //        // Visa kalendern för den aktuella månaden
        //        DisplayMonthCalendar(_currentDate);

        //        // Navigering mellan månader
        //        var navigation = AnsiConsole.Prompt(
        //            new SelectionPrompt<string>()
        //                .Title("Navigera i kalendern")
        //                .AddChoices("Förra månaden", "Nästa månad", "Avsluta")
        //        );


        //        // Efter att ha navigerat, välj ett datum att boka
        //        var selectedDate = AnsiConsole.Ask<DateTime>("Välj ett datum att boka (ange dag):");

        //        // Kontrollera om datumet är ledigt
        //        if (!_bookedDates.Contains(selectedDate))
        //        {
        //            _bookedDates.Add(selectedDate); // Lägg till det valda datumet i listan
        //            AnsiConsole.MarkupLine($"[bold green]Bokningen för {selectedDate.ToShortDateString()} är bekräftad![/]");
        //        }
        //        else
        //        {
        //            AnsiConsole.MarkupLine("[bold red]Detta datum är redan bokat. Vänligen välj ett annat datum.[/]");
        //        }
        //    }
        //}

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
