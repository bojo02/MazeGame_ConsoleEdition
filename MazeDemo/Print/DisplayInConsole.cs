using System;

namespace MazeDemo
{
    public class DisplayInConsole
    {
        Location location = new Location();

        public static void WriteRed(Location Cords, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Cords.X, Cords.Y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteGreen(Location Cords, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(Cords.X, Cords.Y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteGray(Location Cords, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(Cords.X, Cords.Y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteDarkGray(Location Cords, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(Cords.X, Cords.Y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteYellow(Location Cords, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(Cords.X, Cords.Y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteWhite(Location Cords, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Cords.X, Cords.Y);
            Console.Write(text);
            Console.ResetColor();

        }
        public static void WriteMagenta(Location Cords, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(Cords.X, Cords.Y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteCyan(Location Cords, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(Cords.X, Cords.Y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteBlue(Location Cords, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(Cords.X, Cords.Y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteDarkRed(Location Cords, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(Cords.X, Cords.Y);
            Console.Write(text);
            Console.ResetColor();
        }







        public static void WriteRed(int x, int y, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteGreen(int x, int y, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteGray(int x, int y, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteDarkGray(int x, int y, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteYellow(int x, int y, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteWhite(int x, int y, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.ResetColor();

        }
        public static void WriteMagenta(int x, int y, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteCyan(int x, int y, string text)
        {

            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.ResetColor();

        }
        public static void WriteBlue(int x, int y, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteDarkRed(int x, int y, string text)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
