using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotelApp.Utilities.Menus
{
    public abstract class MenuBase
    {
        protected int currentOption = 0;
        protected bool menuActive = true;

        protected abstract string[] MenuOptions { get; }

        public void ShowMenu()
        {
            while (menuActive)
            {
                Console.Clear();
                DisplayMenuHeader();

                int centerX = Console.WindowWidth / 2;

                for (int i = 0; i < MenuOptions.Length; i++)
                {
                    string optionText = MenuOptions[i];
                    int optionLength = optionText.Length;
                    int optionX = centerX - optionLength / 2;
                    int optionY = 10 + i * 2;

                    Console.SetCursorPosition(optionX, optionY);

                    SetMenuOptionColors(i);
                    Console.WriteLine(optionText);
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true);

                HandleKeyPress(key);

                if (key.Key == ConsoleKey.Enter)
                {
                    HandleUserSelection();
                }
            }
        }
        protected void HandleKeyPress(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.UpArrow && currentOption > 0)
            {
                currentOption--;
            }
            else if (key.Key == ConsoleKey.DownArrow && currentOption < MenuOptions.Length - 1)
            {
                currentOption++;
            }
        }

        protected void SetMenuOptionColors(int i)
        {
            if (i == currentOption)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        protected abstract void DisplayMenuHeader();
        protected abstract void HandleUserSelection();
    }
}