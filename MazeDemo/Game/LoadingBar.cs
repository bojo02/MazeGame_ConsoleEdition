using System;
using System.Threading;

namespace MazeDemo
{
    public class LoadingBar
    {
        private static string[,] bar = {
                                  {"#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#"},
                                  {"#"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","#"},
                                  {"#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#"}
        };

        public static void Load()
        {
            Console.Clear();

            int x = Console.WindowWidth / 2 - 12;
            int y = Console.WindowHeight / 2 - 1;

            for (int i = 0; i < LoadingBar.bar.GetLength(0); i++)
            {
                for (int i1 = 0; i1 < LoadingBar.bar.GetLength(1); i1++)
                {
                    DisplayInConsole.WriteWhite(x, y, LoadingBar.bar[i, i1]);
                    x++;
                }
                x = Console.WindowWidth / 2 - 12;
                y++;
            }

            x = Console.WindowWidth / 2 - 11;
            y = Console.WindowHeight / 2;

            for (int i = 0; i < 21; i++)
            {
                DisplayInConsole.WriteYellow(x, y, "♦");
                Thread.Sleep(20);
                x++;
            }

            Console.Clear();
        }
    }
}

