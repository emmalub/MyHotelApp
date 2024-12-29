using Spectre.Console;

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

                AnsiConsole.MarkupLine($"[bold yellow]{_calendarMessage}[/]");

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
            var panel = new Panel(grid).Header($"[bold blue]{_currentDate: MMMM yyyy}[/]");
            AnsiConsole.Write(panel);
        }
    }
}
