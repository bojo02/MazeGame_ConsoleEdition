using System;

namespace MazeDemo
{
    public class DisplayForThreads
    {
        private static readonly object obj = new object();

        public static void WriteInConsoleColors(int x, int y, string text, int colorNumber, ConsoleColor color)
        {
            lock (obj)
            {

                if (colorNumber == 0)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();
                }
                else if (colorNumber == 1)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();
                }
                else if (colorNumber == 2)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();
                }
                else if (colorNumber == 3)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();
                }
                else if (colorNumber == 4)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();
                }
                else if (colorNumber == 5)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();
                }
                else if (colorNumber == 6)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();
                }
                else if (colorNumber == 7)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();
                }
                else if (colorNumber == 8)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();
                }
                else if (colorNumber == 9)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();
                }
                else if (colorNumber == 10)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();

                }
                else if (colorNumber == 11)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();

                }
                else if (colorNumber == 12)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();
                }
                else if (colorNumber == 13)
                {
                    Console.BackgroundColor = color;
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                    Console.ResetColor();
                }
            }
        }
    }
}