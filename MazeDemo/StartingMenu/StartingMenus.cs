using System;
using System.Threading;

namespace MazeDemo
{
    public class StartingMenu
    {
        private string[,] _title = {
                                                         {" "," "," ","M"," "," "," "," ","M"," "," "," "," "," "," "," "," "," ","A","A","A"," "," "," "," "," ","Z","Z","Z","Z","Z","Z","Z","Z"," "," "," "," "," "," ","E","E","E","E","E"},
                                                         {" "," ","M","M","M"," "," ","M","M","M"," "," "," "," "," "," "," ","A","A"," ","A","A"," "," "," "," "," "," "," "," "," "," ","Z","Z"," "," "," "," "," ","E","E","E"," "," "," "},
                                                         {" "," ","M","M","M"," "," ","M","M","M"," "," "," "," "," "," ","A","A"," "," "," ","A","A"," "," "," "," "," "," "," "," ","Z","Z"," "," "," "," "," ","E","E"," "," "," "," "," "},
                                                         {" ","M","M"," ","M","M","M","M"," ","M","M"," "," "," "," "," ","A","A"," "," "," ","A","A"," "," "," "," "," "," "," "," ","Z"," "," "," "," "," "," ","E","E"," "," "," "," "," "},
                                                         {" ","M","M"," ","M","M","M","M"," ","M","M"," "," "," "," "," ","A","A"," "," "," ","A","A"," "," "," "," "," "," "," ","Z","Z"," "," "," "," "," "," ","E","E"," "," "," "," "," "},
                                                         {"M","M"," "," "," ","M","M"," "," "," ","M","M"," "," "," "," ","A","A","A","A","A","A","A"," "," "," "," "," "," "," ","Z"," "," "," "," "," "," "," ","E","E","E","E","E","E","E"},
                                                         {"M","M"," "," "," ","M","M"," "," "," ","M","M"," "," "," "," ","A","A"," "," "," ","A","A"," "," "," "," "," "," ","Z","Z"," "," "," "," "," "," "," ","E","E"," "," "," "," "," "},
                                                         {"M","M"," "," "," ","M","M"," "," "," ","M","M"," "," "," "," ","A","A"," "," "," ","A","A"," "," "," "," "," "," ","Z"," "," "," "," "," "," "," "," ","E","E"," "," "," "," "," "},
                                                         {"M","M"," "," "," "," "," "," "," "," ","M","M"," "," "," "," ","A","A"," "," "," ","A","A"," "," "," "," "," ","Z","Z"," "," "," "," "," "," "," "," ","E","E"," "," "," "," "," "},
                                                         {"M","M"," "," "," "," "," "," "," "," ","M","M"," "," "," "," ","A","A"," "," "," ","A","A"," "," "," "," ","Z","Z"," "," "," "," "," "," "," "," "," ","E","E","E"," "," "," "," "},
                                                         {"M","M"," "," "," "," "," "," "," "," ","M","M"," "," "," "," ","A","A"," "," "," ","A","A"," "," "," ","Z","Z","Z","Z","Z","Z","Z","Z"," "," "," "," "," ","E","E","E","E","E","E"},
                                                         {" "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
                                                         };

        private string[,] _endTitle = {
                                                         {" ","E","E","E","E","E","E"," "," "," "," ","N","N"," "," "," "," ","N"," "," "," "," ","D","D"," "," "," "," "},
                                                         {"E","E","E"," "," "," "," "," "," "," "," ","N","N"," "," "," "," ","N"," "," "," "," ","D","D","D","D"," "," "},
                                                         {"E","E"," "," "," "," "," "," "," "," "," ","N","N","N"," "," "," ","N"," "," "," "," ","D"," "," ","D","D"," "},
                                                         {"E","E"," "," "," "," "," "," "," "," "," ","N"," ","N"," "," "," ","N"," "," "," "," ","D"," "," "," ","D","D"},
                                                         {"E","E"," "," "," "," "," "," "," "," "," ","N"," ","N","N"," "," ","N"," "," "," "," ","D"," "," "," "," ","D"},
                                                         {"E","E","E","E","E","E","E"," "," "," "," ","N"," "," ","N"," "," ","N"," "," "," "," ","D"," "," "," "," ","D"},
                                                         {"E","E"," "," "," "," "," "," "," "," "," ","N"," "," ","N","N"," ","N"," "," "," "," ","D"," "," "," "," ","D"},
                                                         {"E","E"," "," "," "," "," "," "," "," "," ","N"," "," "," ","N"," ","N"," "," "," "," ","D"," "," "," ","D","D"},
                                                         {"E","E"," "," "," "," "," "," "," "," "," ","N"," "," "," ","N","N","N"," "," "," "," ","D"," "," ","D","D"," "},
                                                         {"E","E","E"," "," "," "," "," "," "," "," ","N"," "," "," "," ","N","N"," "," "," "," ","D","D","D","D"," "," "},
                                                         {" ","E","E","E","E","E","E"," "," "," "," ","N"," "," "," "," ","N","N"," "," "," "," ","D","D"," "," "," "," "}
                                                         };

        private bool _exitControls = false;

        private int _titleShowingSpeed = 10;

        private string _cheatCode = "";

        public ConsoleColor _color { get; set; }

        private int _X_centeredPostion = Console.WindowWidth / 2;
        private int _Y_centeredPostion = Console.WindowHeight / 2;

        private int _X_MaxWidthPosition = Console.WindowWidth;

        private int _X_MinWidthPosition = 0;

        private int _selection = 0;

        public StartingMenu()
        {
            _color = ConsoleColor.Black;
        }

        public void DrawEndTitle()
        {
            Console.Clear();
            Console.CursorVisible = false;

            for (int i = 0; i < _endTitle.GetLength(0); i++)
            {
                for (int index = 0; index < _X_centeredPostion - 10; index++)
                {
                    Console.BackgroundColor = _color;
                    Console.Write(" ");
                }
                for (int i1 = 0; i1 < _endTitle.GetLength(1); i1++)
                {
                    if (_title[i, i1] != null)
                    {
                        Console.BackgroundColor = _color;
                        Thread.Sleep(_titleShowingSpeed);
                    }

                    else if (_endTitle[i, i1] == "E")
                    {
                        Console.BackgroundColor = _color;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (_endTitle[i, i1] == "N")
                    {
                        Console.BackgroundColor = _color;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (_endTitle[i, i1] == "D")
                    {
                        Console.BackgroundColor = _color;
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.BackgroundColor = _color;
                    Console.Write(_endTitle[i, i1]);
                    Console.ResetColor();
                }

                Console.WriteLine();
            }
            MenuPrinting("exit");

            while (true)
            {
                Console.BackgroundColor = _color;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Environment.Exit(0);
                }
            }

        }

        private void MenuPrinting(string option)
        {
            switch (option)
            {
                case "start":
                    {
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteYellow(_X_centeredPostion - 5, 13, "> Start <     ");
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 15, "  Enter code: " + _cheatCode + "  ");
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 17, "  Controls  ");
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 19, "  Exit  ");
                        Console.BackgroundColor = _color;

                        break;
                    }
                case "code":
                    {
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 13, "  Start      ");
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteYellow(_X_centeredPostion - 5, 15, "> Enter code: " + _cheatCode + " " + "<");
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 17, "  Controls  ");
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 19, "  Exit  ");

                        break;
                    }
                case "controls":
                    {
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 13, "  Start        ");
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 15, "  Enter code: " + _cheatCode + "        ");
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteYellow(_X_centeredPostion - 5, 17, "> Controls <");
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 19, "  Exit  ");

                        break;
                    }
                case "exit":
                    {
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 13, "  Start       ");
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 15, "  Enter code: " + _cheatCode + " " + "       ");
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 17, "  Controls  ");
                        Console.BackgroundColor = _color;
                        DisplayInConsole.WriteYellow(_X_centeredPostion - 5, 19, "> Exit <");

                        break;
                    }
            }
        }

        public void DrawGameTitle()
        {
            for (int i = 0; i < _title.GetLength(0); i++)
            {
                for (int index = 0; index < _X_centeredPostion - 25; index++)
                {
                    Console.BackgroundColor = _color;
                    Console.Write(" ");
                }
                for (int i1 = 0; i1 < _title.GetLength(1); i1++)
                {
                    if (_title[i, i1] == "M")
                    {
                        Console.CursorVisible = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    if (_title[i, i1] == "A")
                    {
                        Console.CursorVisible = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    if (_title[i, i1] == "Z")
                    {
                        Console.CursorVisible = false;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (_title[i, i1] == "E")
                    {
                        Console.CursorVisible = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    if (_title[i, i1] != " ")
                    {
                        Thread.Sleep(_titleShowingSpeed);
                    }
                    Console.BackgroundColor = _color;
                    Console.Write(_title[i, i1]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            if (_titleShowingSpeed == 10)
            {
                _titleShowingSpeed = 0;
            }
        }

        public void StartMenu()
        {
            DrawGameTitle();

            MenuPrinting("start");

            while (true)
            {
                ClearKeyBuffer();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (_selection - 1 == 0)
                    {
                        _selection--;
                        MenuPrinting("start");
                    }
                    else if (_selection - 1 == 1)
                    {
                        _selection--;
                        MenuPrinting("code");
                    }
                    else if (_selection - 1 == 2)
                    {
                        _selection--;
                        MenuPrinting("controls");
                    }
                    else if (_selection - 1 == -1)
                    {

                        _selection = 3;

                        MenuPrinting("exit");
                    }
                }

                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {

                    if (_selection + 1 == 1)
                    {
                        _selection++;
                        MenuPrinting("code");
                    }
                    else if (_selection + 1 == 2)
                    {

                        _selection++;
                        MenuPrinting("controls");
                    }
                    else if (_selection + 1 == 3)
                    {
                        _selection++;
                        MenuPrinting("exit");
                    }
                    else if (_selection + 1 == 4)
                    {
                        _selection = 0;
                        MenuPrinting("start");
                    }
                }

                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.BackgroundColor = _color;

                    MenuSelection(_selection);

                    _exitControls = false;
                }
            }
        }

        private void StartGame()
        {
            LoadingBar.Load();

            if (_cheatCode == "ACD")
            {
                TemplateLevel levels = new TemplateLevel(3, 0, 0, 0, _X_centeredPostion - 13, _Y_centeredPostion - 8, 1, _color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze1;
                levels.Start();
            }
            else if (_cheatCode == "FGH")
            {
                TemplateLevel levels = new TemplateLevel(3, 0, 0, 0, _X_centeredPostion - 13, _Y_centeredPostion - 8, 2, _color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze2;
                levels.Start();
            }
            else if (_cheatCode == "OPL")
            {
                TemplateLevel levels = new TemplateLevel(3, 0, 0, 0, _X_centeredPostion - 16, _Y_centeredPostion - 8, 3, _color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze3;
                levels.Start();
            }
            else if (_cheatCode == "JJK")
            {
                TemplateLevel levels = new TemplateLevel(3, 0, 0, 0, _X_centeredPostion - 19, _Y_centeredPostion - 8, 4, _color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze4;
                levels.Start();
            }
            else if (_cheatCode == "QWA")
            {

                TemplateLevel levels = new TemplateLevel(3, 0, 0, 0, _X_centeredPostion - 23, _Y_centeredPostion - 12, 5, _color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze5;
                levels.Start();
            }
            else if (_cheatCode == "LOP")
            {
                TemplateLevel levels = new TemplateLevel(3, 0, 0, 0, _X_centeredPostion - 45, _Y_centeredPostion - 11, 6, _color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze6;
                levels.Start();
            }
            else if (_cheatCode == "ZCX")
            {
                TemplateLevel levels = new TemplateLevel(5, 0, 0, 0, _X_centeredPostion - 45, _Y_centeredPostion - 11, 7, _color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze7;
                levels.Start();
            }
            else if (_cheatCode == "KYF")
            {
                TemplateLevel levels = new TemplateLevel(5, 0, 0, 0, _X_centeredPostion - 45, _Y_centeredPostion - 11, 8, _color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze8;
                levels.Start();
            }
            else if (_cheatCode == "KDG")
            {
                TemplateLevel levels = new TemplateLevel(5, 0, 0, 0, _X_centeredPostion - 45, _Y_centeredPostion - 11, 9, _color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze9;
                levels.Start();
            }
            else if (_cheatCode == "TEST")
            {
                TemplateLevel levels = new TemplateLevel(999, 999, 999, 999, 15, 6, 8, _color);
                Levels lvl = new Levels();
                levels.Maze = lvl.MazeTest;
                levels.Start();
            }
            else
            {
                TemplateLevel levels = new TemplateLevel(3, 0, 0, 0, _X_centeredPostion - 13, _Y_centeredPostion - 8 , 1, _color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze1;
                levels.Start();
            }
            Console.Clear();
        } 
        private void ClearKeyBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
        }

        private void ControlsMenu()
        {
            PrintControlsMenu();
        }

        private void PrintControlsMenu()
        {
            Console.Clear();
            DrawGameTitle();
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteYellow(_X_MaxWidthPosition - 30, 12, "Controls:");
            DisplayInConsole.WriteWhite(_X_MaxWidthPosition - 30, 14, "       ^");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteWhite(_X_MaxWidthPosition - 30, 15, "Up   : |    Left : <--");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteWhite(_X_MaxWidthPosition - 30, 17, "Down : |    Right : -->");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteWhite(_X_MaxWidthPosition - 30, 18, "       v ");
            DisplayInConsole.WriteWhite(_X_MaxWidthPosition - 30, 20, "Shooting -> Spacebar");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteYellow(_X_MinWidthPosition + 5, 20, "■ : Moving block");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteGreen(_X_MinWidthPosition + 5, 22, "$ : Money");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteYellow(_X_MinWidthPosition + 5, 12, "☼ : 5 ammo for 10$");
            Console.SetCursorPosition(_X_MinWidthPosition + 9, 12);
            Console.ResetColor();
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteCyan(_X_MinWidthPosition + 5, 30, "* : Shoot it to destroy");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteRed(_X_MinWidthPosition + 5, 32, "░ : Destroy it with moving block");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteRed(_X_MinWidthPosition + 5, 28, "♥ : Health");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteRed(_X_MinWidthPosition + 5, 24, "G / ! : Enemies");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteCyan(_X_MinWidthPosition + 5, 26, "{ / } : Teleporters");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteYellow(_X_MinWidthPosition + 5, 18, "| / - : Doors");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteRed(_X_MinWidthPosition + 5, 16, "X : Place all H to unlock it");
            Console.SetCursorPosition(_X_MinWidthPosition + 19, 16);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("H");
            Console.ResetColor();
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteCyan(_X_MinWidthPosition + 5, 14, "H : Place ■ over it");
            Console.SetCursorPosition(_X_MinWidthPosition + 15, 14);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("■");
            Console.ResetColor();
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteMagenta(_X_MinWidthPosition + 5, 10, "↑ ← ↓ → : You");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteYellow(_X_MinWidthPosition + 5, 8, "P - Keys");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteWhite(_X_MinWidthPosition + 5, 4, "Legend: ");
            Console.BackgroundColor = _color;
            DisplayInConsole.WriteYellow(_X_centeredPostion - 5, 21, "> Back <");



            while (!_exitControls)
            {
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    ClearKeyBuffer();

                    Console.BackgroundColor = _color;

                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();

                        DrawGameTitle();

                        MenuPrinting("controls");

                        _exitControls = true;
                    }
                }
            }
        }

        private void Exit()
        {
            Environment.Exit(0);
        }

        private void MenuSelection(int index)
        {
            if (index == 0)
            {
                StartGame();
            }

            else if (index == 1)
            {
                string word = "";

                for (int i = 0; i < _cheatCode.Length; i++)
                {
                    word += " ";
                }
                DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 15, "  Enter code: " + word + "  ");

                _cheatCode = null;

                MenuPrinting("code");

                Console.BackgroundColor = _color;
                DisplayInConsole.WriteWhite(_X_centeredPostion - 5, 15, "  Enter code: " + _cheatCode + "  ");

                Console.SetCursorPosition(_X_centeredPostion + 9, 15);

                Console.CursorVisible = true;

                _cheatCode = Console.ReadLine();

                MenuPrinting("code");
               // word = "";
                Console.CursorVisible = false;

            }

            else if (index == 2)
            {
                PrintControlsMenu();
            }

            else if (index == 3)
            {
                Exit();
            }

        }
    }
}
