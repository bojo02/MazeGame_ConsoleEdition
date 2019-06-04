
using MazeDemo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MazeDemo
{
    public class TemplateLevel
    {
        public ConsoleColor color;

        public MazeDemo.Location PlayerCords { get; set; }

        public static int Health;
        public static int Money;
        public static int Keys;

        private int _X_centeredPostion = Console.WindowWidth / 2;
        private int _Y_centeredPostion = Console.WindowHeight / 2;

        private Location checkpointLocation;
        private int _checkpointSide;

        bool levelEnd = true;

        private static int _health;
        private static int _money;
        private static int _keys;
        private static int _ammo;
        private static int _needH;

        private string _playerShootingSymbol = "o";

        private string _destroyingBlockSymbol = "░";

        private string _ammoShopSymbol = "☼";
        //☺
        //☻
        private string _horizontalEnemiesSymbol = "☺";
        private string _verticalEnemiesSymbol = "☻";

        bool enemyThreadStarted = false;

        Task Shooting;
        Task VerticalEnemiesTask;
        Task HorizontalEnemiesTask;
        Task ShootingMachine;

        bool moveEnemies = true;
        bool moveEnemies2 = true;

        bool canRestart = false;
        bool canRestart2 = false;

        private int needToUnlock = 0;
        private int _placed_H = 0;

        private bool here = true;
        private bool doIt = true;

        public int level;

        private int X_X;
        private int X_Y;

        private int bulletSpeed = 20;
        //Player start level cordinations
        private int Player_Default_Start_X = 0;
        private int Player_Default_Start_Y = 0;

        private int X_X_start = 0;
        private int X_Y_start = 0;

        private int _leftTeleporter_X = 0;
        private int _leftTeleporter_Y = 0;

        private int _rightTeleporter_X = 0;
        private int _rightTeleporter_Y = 0;

        private int _exit_X = 0;
        private int _exit_Y = 0;

        private int x_starting = 15;
        private int y_starting = 6;

        private int badGuyTest_X = 0;
        private int badGuyTest_Y = 0;

        private bool _canPlayerMove = true;

        private string player_cursor = "";
        //player looking
        private int _side;
        //player bullets
        private int _bullets = 0;



        private int needBreakingWallShootings = 3;
        private int breakingWallShootings = 0;

        //Horizontal enemies lists
        private List<Location> HorizontalEnemyList = new List<Location>();
        private List<bool> HorizontalMovingState = new List<bool>();
        private List<bool> HorizontalMovingSide = new List<bool>();
        private List<int> HorizontalStatus = new List<int>();
        private List<bool> HorizontalAliveStatus = new List<bool>();

        //Vertical enemies lists
        private List<Location> VerticalEnemyList = new List<Location>();
        private List<bool> VerticalMovingState = new List<bool>();
        private List<bool> VerticalMovingSide = new List<bool>();
        private List<int> VerticalStatus = new List<int>();
        private List<bool> VerticalAliveStatus = new List<bool>();

        //breaking walls

        //list of breaking walls
        private List<Location> breakingWalls = new List<Location>();
        //list of breaking wall state
        private List<int> breakingWallsState = new List<int>();
        //list of existing breaking walls
        private List<bool> breakingWallsExist = new List<bool>();

        //shooting

        //list of bullets that player shoot
        CustomCollection<Task> bullets = new CustomCollection<Task>();
        //list of ammo objects
        CustomCollection<Ammo> AmmoList = new CustomCollection<Ammo>();

        public static System.Text.Encoding Unicode { get; }

        public bool CanFinish;
        //↑←↓→
        //■ ♥
        public string[,] Maze;

        public TemplateLevel(int health, int money, int keys, int ammo, int xtable, int ytable, int level, ConsoleColor c)
        {
            //_health = health;
            _money = money;
            _ammo = ammo;
            _keys = keys;

            Health = health;
            Money = money;
            Keys = keys;
            x_starting = xtable;
            y_starting = ytable;
            this._bullets = ammo;
            this.level = level;

            this.color = c;

            Console.BackgroundColor = this.color;
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Cyan;

            Console.OutputEncoding = System.Text.Encoding.Unicode;


            PlayerCords = new Location();

        }

        public bool Checking(Location loc, string text)
        {
            if (Maze[loc.Y - y_starting, loc.X - x_starting] != text)
            {
                return true;
            }
            if (Maze[loc.Y - y_starting, loc.X - x_starting] == _ammoShopSymbol && Money >= 10)
            {
                Money -= 10;
                _bullets += 5;
                PrintAmmo();
                PrintMoney();
                return false;
            }
            return false;
        }

        public bool CheckingCheckpoint(Location loc, string text, int side)
        {
            int x = PlayerCords.X;
            int y = PlayerCords.Y;


            if (Maze[loc.Y - y_starting, loc.X - x_starting] == "C" && side == 1)
            {
                this.Player_Default_Start_X = x;
                this.Player_Default_Start_Y = y + 1; //DisplayForThreads.WriteInConsoleColors(50, 3, x + " " + y, 6, color);
                return true;
            }

            else if (Maze[loc.Y - y_starting, loc.X - x_starting] == "C" && side == 3)
            {
                this.Player_Default_Start_X = PlayerCords.X + 1;
                this.Player_Default_Start_Y = PlayerCords.Y; //DisplayForThreads.WriteInConsoleColors(50, 3, x + " " + y, 6, color);
                return true;

            }
            else if (Maze[loc.Y - y_starting, loc.X - x_starting] == "C" && side == 0)
            {
                this.Player_Default_Start_X = PlayerCords.X;
                this.Player_Default_Start_Y = PlayerCords.Y - 1; //DisplayForThreads.WriteInConsoleColors(50, 3, x + " " + y, 6, color);
                return true;
            }
            else if (Maze[loc.Y - y_starting, loc.X - x_starting] == "C" && side == 2)
            {
                this.Player_Default_Start_X = PlayerCords.X - 1;
                this.Player_Default_Start_Y = PlayerCords.Y; //DisplayForThreads.WriteInConsoleColors(50, 3, x + " " + y, 6, color);
                return true;
            }
            return true;
        }

        public bool Checking(int x, int y, string text)
        {
            if (Maze[y - y_starting, x - x_starting] != text)
            {
                return true;
            }
            return false;
        }

        public bool CheckIfLevelPassed(Location loc, string text)
        {
            if (Maze[loc.Y - y_starting, loc.X - x_starting] == "E")
            {
                return true;
            }
            return false;
        }

        public void PlayerDead()
        {
            Console.Beep();

            Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = " ";

            DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, " ", 6, color);
            PlayerCords.X = Player_Default_Start_X;
            PlayerCords.Y = Player_Default_Start_Y;
            DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);

            Health--;
            PrintHealth();

            Maze[Player_Default_Start_Y - y_starting, Player_Default_Start_X - x_starting] = player_cursor;

            int x = Player_Default_Start_X - x_starting;
            int y = Player_Default_Start_Y - y_starting;
        }

        public bool CheckIfOpenDoor(Location loc, string text)
        {
            if (Maze[loc.Y - y_starting, loc.X - x_starting] == text)
            {
                if (Keys == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckIfMovingWall(Location loc, int i)
        {
            if (Maze[loc.Y - y_starting, loc.X - x_starting] == "\u25A0")
            {
                Sequence.Lock();

                switch (i)
                {

                    case 0:
                        {
                            Location location = new Location();
                            location.X = loc.X;
                            location.Y = loc.Y;


                            if (Maze[location.Y - y_starting - 1, location.X - x_starting] == " ")
                            {
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.Y = loc.Y - 1;

                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, "■", 1, color);
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                Maze[loc.Y - y_starting - 1, loc.X - x_starting] = "■";
                            }
                            else if (Maze[location.Y - y_starting - 1, location.X - x_starting] == _destroyingBlockSymbol)
                            {
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.Y = loc.Y - 1;

                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, "■", 1, color);
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                Maze[loc.Y - y_starting - 1, loc.X - x_starting] = "■";
                            }
                            else if (Maze[location.Y - y_starting - 1, location.X - x_starting] == "H")
                            {
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.Y = loc.Y - 1;

                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, "■", 1, color);
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                Maze[loc.Y - y_starting - 1, loc.X - x_starting] = "■";
                                _placed_H++;
                                if (_placed_H == needToUnlock)
                                {
                                    Maze[X_Y - y_starting, X_X - x_starting] = " ";
                                    DisplayForThreads.WriteInConsoleColors(X_X, X_Y, " ", 1, color);
                                }
                            }

                            else
                            {
                                return false;
                            }
                            break;
                        }
                    case 1:
                        {
                            Location location = new Location();
                            location.X = loc.X;
                            location.Y = loc.Y;
                            if (Maze[location.Y - y_starting + 1, location.X - x_starting] == " ")
                            {
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.Y = loc.Y + 1;

                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, "■", 1, color);
                                Maze[loc.Y - y_starting + 1, loc.X - x_starting] = "■";
                            }
                            else if (Maze[location.Y - y_starting + 1, location.X - x_starting] == _destroyingBlockSymbol)
                            {
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.Y = loc.Y + 1;

                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, "■", 1, color);
                                Maze[loc.Y - y_starting + 1, loc.X - x_starting] = "■";
                            }
                            else if (Maze[location.Y - y_starting + 1, location.X - x_starting] == "H")
                            {
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.Y = loc.Y + 1;

                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, "■", 1, color);
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                Maze[loc.Y - y_starting + 1, loc.X - x_starting] = "■";
                                _placed_H++;
                                if (_placed_H == needToUnlock)
                                {
                                    Maze[X_Y - y_starting, X_X - x_starting] = " ";
                                    DisplayForThreads.WriteInConsoleColors(X_X, X_Y, " ", 1, color);
                                }
                            }
                            else
                            {
                                return false;
                            }
                            break;
                        }
                    case 2:
                        {
                            Location location = new Location();
                            location.X = loc.X;
                            location.Y = loc.Y;
                            if (Maze[location.Y - y_starting, location.X - x_starting - 1] == " ")
                            {
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.X = loc.X - 1;

                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, "■", 1, color);
                                Maze[loc.Y - y_starting, loc.X - x_starting - 1] = "■";
                            }
                            else if (Maze[location.Y - y_starting, location.X - x_starting - 1] == _destroyingBlockSymbol)
                            {
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.X = loc.X - 1;

                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, "■", 1, color);
                                Maze[loc.Y - y_starting, loc.X - x_starting - 1] = "■";
                            }
                            else if (Maze[location.Y - y_starting, location.X - x_starting - 1] == "H")
                            {
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.X = loc.X - 1;

                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, "■", 1, color);
                                Maze[loc.Y - y_starting, loc.X - x_starting - 1] = "■";
                                _placed_H++;
                                if (_placed_H == needToUnlock)
                                {
                                    Maze[X_Y - y_starting, X_X - x_starting] = " ";
                                    DisplayForThreads.WriteInConsoleColors(X_X, X_Y, " ", 1, color);
                                }
                            }
                            else if (Maze[location.Y - y_starting, location.X - x_starting - 1] == "{")
                            {
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.X = loc.X - 1;
                                DisplayForThreads.WriteInConsoleColors(_leftTeleporter_X - 2, _leftTeleporter_Y, "■", 1, color);
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                Maze[_leftTeleporter_Y - y_starting, _leftTeleporter_X - x_starting - 2] = "■";
                            }
                            else
                            {
                                return false;
                            }

                            break;
                        }
                    case 3:
                        {
                            Location location = new Location();
                            location.X = loc.X;
                            location.Y = loc.Y;

                            if (Maze[location.Y - y_starting, location.X - x_starting + 1] == " ")
                            {
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.X = loc.X + 1;

                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, "■", 1, color);
                                Maze[loc.Y - y_starting, loc.X - x_starting + 1] = "■";
                            }
                            else if (Maze[location.Y - y_starting, location.X - x_starting + 1] == _destroyingBlockSymbol)
                            {
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.X = loc.X + 1;

                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, "■", 1, color);
                                Maze[loc.Y - y_starting, loc.X - x_starting + 1] = "■";
                            }
                            else if (Maze[location.Y - y_starting, location.X - x_starting + 1] == "H")
                            {
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.X = loc.X + 1;

                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, "■", 1, color);
                                Maze[loc.Y - y_starting, loc.X - x_starting + 1] = "■";
                                _placed_H++;
                                if (_placed_H == needToUnlock)
                                {
                                    Maze[X_Y - y_starting, X_X - x_starting] = " ";
                                    DisplayForThreads.WriteInConsoleColors(X_X, X_Y, " ", 1, color);
                                }
                            }
                            else if (Maze[location.Y - y_starting, location.X - x_starting + 1] == "}")
                            {
                                Maze[loc.Y - y_starting, loc.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(location.X, location.Y, " ", 1, color);
                                location.X = loc.X + 1;

                                DisplayForThreads.WriteInConsoleColors(_rightTeleporter_X + 2, _rightTeleporter_Y, "■", 1, color);
                                Maze[_rightTeleporter_Y - y_starting, _rightTeleporter_X - x_starting + 2] = "■";
                            }
                            else
                            {
                                return false;
                            }

                            break;
                        }

                }

            }
            Sequence.Unlock();

            return true;
        }

        public static void ClearKeyBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
            }
        }

        public void PlayerMove()
        {
            Location up = new Location();
            up.Y = PlayerCords.Y;
            up.X = PlayerCords.X;
            up.Y = up.Y - 1;
            Location down = new Location();
            down.Y = PlayerCords.Y;
            down.X = PlayerCords.X;
            down.Y = down.Y + 1;
            Location left = new Location();
            left.Y = PlayerCords.Y;
            left.X = PlayerCords.X;
            left.X = left.X - 1;
            Location right = new Location();
            right.Y = PlayerCords.Y;
            right.X = PlayerCords.X;
            right.X = right.X + 1;



            if (Console.KeyAvailable == true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                ClearKeyBuffer();

                if (keyInfo.Key == ConsoleKey.Spacebar)
                {

                    if (_bullets > 0)
                    {
                        _bullets--;

                        Location locat = new Location();
                        locat.X = PlayerCords.X;
                        locat.Y = PlayerCords.Y;

                        Ammo amunition = new Ammo(locat, _side);

                        AmmoList.Add(amunition);

                        PrintAmmo();

                    }
                }

                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    Sequence.Lock();

                    if (CheckingCheckpoint(up, "C", 0)
                        && Checking(up, "#")
                        && Checking(up, _destroyingBlockSymbol)
                        && Checking(up, "*")
                        && Checking(up, "H")
                        && Checking(up, _ammoShopSymbol)
                        && CheckIfMovingWall(up, 0)
                        && Checking(up, "X")
                        && CheckIfOpenDoor(up, "-"))
                    {

                        player_cursor = "↑";

                        if (CheckIfOpenDoor(up, "-"))
                        {
                            if (Maze[up.Y - y_starting, up.X - x_starting] == _horizontalEnemiesSymbol)
                            {
                                PlayerDead();
                            }
                            else if (Maze[up.Y - y_starting, up.X - x_starting] == _verticalEnemiesSymbol)
                            {
                                PlayerDead();
                            }
                            else
                            {
                                _side = 0;


                                if (!Checking(up, "$"))
                                {
                                    Money++;
                                    PrintMoney();
                                    Maze[up.Y - y_starting, up.X - x_starting] = " ";
                                }
                                if (!Checking(up, "♥"))
                                {
                                    Health++;
                                    PrintHealth();
                                    Maze[up.Y - y_starting, up.X - x_starting] = " ";
                                }
                                if (!Checking(up, "P"))
                                {
                                    Keys++;
                                    PrintKeys();
                                    Maze[up.Y - y_starting, up.X - x_starting] = " ";
                                }
                                if (CheckExit(up, "E"))
                                {
                                    PrintExit();
                                }
                                if (CheckIfLevelPassed(PlayerCords, "E"))
                                {
                                    CanFinish = true;
                                }
                                if (!Checking(up, "-"))
                                {
                                    Keys--;
                                    PrintKeys();
                                    Maze[up.Y - y_starting, up.X - x_starting] = " ";
                                }
                                PrintPlayerCords();

                                DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, " ", 6, color);
                                Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = " ";
                                PlayerCords.Y--;
                                Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = "↑";
                                DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, "↑", 6, color);

                                PrintPlayerCords();

                                Sequence.Unlock();
                            }
                        }
                    }//↑←↓→
                }

                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    Sequence.Lock();

                    if (CheckingCheckpoint(down, "C", 1)
                        && Checking(down, _destroyingBlockSymbol)
                        && Checking(down, "#")
                        && Checking(down, "H")
                        && Checking(down, "X")
                        && Checking(down, "*")
                        && Checking(down, _ammoShopSymbol)
                        && CheckIfMovingWall(down, 1)
                        && CheckIfOpenDoor(down, "-"))
                    {

                        player_cursor = "↓";

                        if (Maze[down.Y - y_starting, down.X - x_starting] == _horizontalEnemiesSymbol)
                        {
                            PlayerDead();
                        }
                        else if (Maze[down.Y - y_starting, down.X - x_starting] == _verticalEnemiesSymbol)
                        {
                            PlayerDead();
                        }
                        else
                        {
                            _side = 1;
                            if (!Checking(down, "$"))
                            {
                                Money++;
                                PrintMoney();
                                Maze[down.Y - y_starting, down.X - x_starting] = " ";
                            }
                            if (!Checking(down, "P"))
                            {
                                Keys++;
                                PrintKeys();
                                Maze[down.Y - y_starting, down.X - x_starting] = " ";
                            }
                            if (!Checking(down, "♥"))
                            {
                                Health++;
                                PrintHealth();
                                Maze[down.Y - y_starting, down.X - x_starting] = " ";
                            }
                            if (CheckExit(down, "E"))
                            {
                                PrintExit();
                            }
                            if (CheckIfLevelPassed(PlayerCords, "E"))
                            {
                                CanFinish = true;
                            }
                            if (!Checking(down, "-"))
                            {
                                Keys--;
                                PrintKeys();
                                Maze[down.Y - y_starting, down.X - x_starting] = " ";
                            }



                            DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, " ", 6, color);
                            Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = " ";
                            PlayerCords.Y++;
                            Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = "↓";
                            DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, "↓", 6, color);

                            PrintPlayerCords();

                            Sequence.Unlock();
                        }
                    }
                }

                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    Sequence.Lock();

                    if (CheckingCheckpoint(left, "C", 2)
                        && Checking(left, "#")
                        && Checking(left, "H")
                        && Checking(left, _destroyingBlockSymbol)
                        && Checking(left, "*")
                        && CheckIfMovingWall(left, 2)
                        && Checking(left, _ammoShopSymbol)
                        && CheckIfOpenDoor(left, "|")
                        && CheckIfOpenDoor(left, "X"))
                    {
                        player_cursor = "←";

                        if (Maze[left.Y - y_starting, left.X - x_starting] == _horizontalEnemiesSymbol)
                        {
                            PlayerDead();
                        }
                        else if (Maze[left.Y - y_starting, left.X - x_starting] == _verticalEnemiesSymbol)
                        {
                            PlayerDead();
                        }
                        else
                        {
                            _side = 2;

                            if (!Checking(left, "$"))
                            {
                                Money++;
                                PrintMoney();
                                Maze[left.Y - y_starting, left.X - x_starting] = " ";
                            }
                            if (!Checking(left, "P"))
                            {
                                Keys++;
                                PrintKeys();
                                Maze[left.Y - y_starting, left.X - x_starting] = " ";
                            }
                            if (!Checking(left, "♥"))
                            {
                                Health++;
                                PrintHealth();
                                Maze[left.Y - y_starting, left.X - x_starting] = " ";
                            }
                            if (CheckExit(left, "E"))
                            {
                                PrintExit();
                            }
                            if (CheckIfLevelPassed(PlayerCords, "E"))
                            {
                                CanFinish = true;
                            }
                            if (!Checking(left, "|"))
                            {
                                Keys--;
                                PrintKeys();
                                Maze[left.Y - y_starting, left.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, "←", 6, color);
                            }


                            if (Checking(left, "{"))
                            {
                                DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, " ", 6, color);
                                Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = " ";
                                PlayerCords.X--;
                                Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = "←";
                                DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, "←", 6, color);
                            }
                            else
                            {
                                DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, " ", 6, color);
                                Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = " ";
                                PlayerCords.X = _leftTeleporter_X - 1;
                                PlayerCords.Y = _leftTeleporter_Y;
                                Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = "←";
                                DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, "←", 6, color);
                            }
                            PrintPlayerCords();

                            Sequence.Unlock();
                        }
                    }
                }

                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    Sequence.Lock();

                    if (Checking(right, "#")
                        && Checking(right, "H")
                        && Checking(right, _destroyingBlockSymbol)
                        && CheckIfMovingWall(right, 3)
                        && Checking(right, _ammoShopSymbol)
                        && Checking(right, "*")
                        && Checking(right, "X")
                        && CheckingCheckpoint(right, "C", 3)
                        && CheckIfOpenDoor(right, "|"))
                    {

                        player_cursor = "→";

                        if (Maze[right.Y - y_starting, right.X - x_starting] == _horizontalEnemiesSymbol)
                        {
                            PlayerDead();
                        }
                        else if (Maze[right.Y - y_starting, right.X - x_starting] == _verticalEnemiesSymbol)
                        {
                            PlayerDead();
                        }
                        else
                        {
                            _side = 3;

                            if (!Checking(right, "$"))
                            {
                                Money++;
                                PrintMoney();
                                Maze[right.Y - y_starting, right.X - x_starting] = " ";
                            }
                            if (!Checking(right, "P"))
                            {
                                Keys++;
                                PrintKeys();
                                Maze[right.Y - y_starting, right.X - x_starting] = " ";
                            }
                            if (CheckExit(right, "E"))
                            {
                                PrintExit();
                            }
                            if (CheckIfLevelPassed(PlayerCords, "E"))
                            {
                                CanFinish = true;
                            }
                            if (!Checking(right, "♥"))
                            {
                                Health++;
                                PrintHealth();
                                Maze[right.Y - y_starting, right.X - x_starting] = " ";
                            }
                            if (!Checking(right, "|"))
                            {
                                Keys--;
                                PrintKeys();
                                Maze[right.Y - y_starting, right.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, "→", 6, color);
                            }

                            if (Checking(right, "}"))
                            {
                                DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, " ", 6, color);
                                Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = " ";
                                PlayerCords.X++;
                                Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = "→";
                                DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, "→", 6, color);
                            }
                            else
                            {
                                DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, " ", 6, color);
                                Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = " ";
                                PlayerCords.X = _rightTeleporter_X + 1;
                                PlayerCords.Y = _rightTeleporter_Y;
                                Maze[PlayerCords.Y - y_starting, PlayerCords.X - x_starting] = "→";
                                DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, "→", 6, color);
                                PrintPlayerCords();
                            }
                            PrintPlayerCords();

                            Sequence.Unlock();
                        }
                    }
                }

                else if (keyInfo.Key == ConsoleKey.R && Health > 0)
                {
                    Sequence.Lock();

                    RestartLevel();

                    Sequence.Unlock();
                }
            }
        }

        public void RestartLevel()
        {
            Keys = _keys;
            _bullets = _ammo;
            Money = _money;


            moveEnemies = false;

            Levels lvl = new Levels();
            this.Maze = null;

            ClearAll();

            Health--;

            if (this.level == 1)
            {
                this.Maze = lvl.Maze1;
            }
            else if (this.level == 2)
            {
                this.Maze = lvl.Maze2;
            }
            else if (this.level == 3)
            {
                this.Maze = lvl.Maze3;
            }
            else if (this.level == 4)
            {
                this.Maze = lvl.Maze4;
            }
            else if (this.level == 5)
            {
                this.Maze = lvl.Maze5;
            }
            else if (this.level == 6)
            {
                this.Maze = lvl.Maze6;
            }
            else if (this.level == 7)
            {
                this.Maze = lvl.Maze7;
            }
            else if (this.level == 8)
            {
                this.Maze = lvl.Maze8;
            }

            bool loop = true;
            bool doing = true;

            while (doing)
            {
                if (canRestart == true)
                {
                    needToUnlock = 0;
                    _needH = 0;
                    Draw();

                    doing = false;
                }
            }
            moveEnemies = true;
        }

        public bool CheckExit(Location loc, string text)
        {
            if (Maze[loc.Y - y_starting, loc.X - x_starting] == "E")
            {
                PrintExit();
                CanFinish = true;
                return true;
            }
            return false;
        }

        public void Start()
        {
            Draw();
            while (!CanFinish)
            {
                if (_canPlayerMove)
                {
                    PlayerMove();
                }

            }

            levelEnd = false;

            Console.Clear();



            if (this.level == 1)
            {
                ClearAll();
                LoadingBar.Load();
                TemplateLevel levels = new TemplateLevel(3, 0, 0, 0, _X_centeredPostion - 13, _Y_centeredPostion - 8, 2, this.color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze2;
                levels.Start();
            }
            else if (this.level == 2)
            {
                ClearAll();
                LoadingBar.Load();
                TemplateLevel levels = new TemplateLevel(3, 0, 0, 0, _X_centeredPostion - 16, _Y_centeredPostion - 8, 3, this.color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze3;
                levels.Start();

            }
            else if (this.level == 3)
            {
                ClearAll();
                LoadingBar.Load();
                TemplateLevel levels = new TemplateLevel(3, 0, 0, 0, _X_centeredPostion - 19, _Y_centeredPostion - 8, 4, this.color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze4;
                levels.Start();
            }
            else if (this.level == 4)
            {
                ClearAll();
                TemplateLevel levels = new TemplateLevel(3, 0, 0, 0, _X_centeredPostion - 23, _Y_centeredPostion - 12, 5, this.color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze5;
                levels.Start();
            }
            else if (this.level == 5)
            {
                ClearAll();
                LoadingBar.Load();
                TemplateLevel levels = new TemplateLevel(3, 0, 0, 0, _X_centeredPostion - 45, _Y_centeredPostion - 11, 6, this.color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze6;
                levels.Start();
            }
            else if (this.level == 6)
            {
                ClearAll();
                LoadingBar.Load();
                TemplateLevel levels = new TemplateLevel(5, 0, 0, 0, _X_centeredPostion - 45, _Y_centeredPostion - 11, 7, this.color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze7;
                levels.Start();
            }
            else if (this.level == 7)
            {
                ClearAll();
                LoadingBar.Load();
                TemplateLevel levels = new TemplateLevel(Health, Money, Keys, _bullets, 15, 6, 8, this.color);
                Levels lvl = new Levels();
                levels.Maze = lvl.Maze8;
                levels.Start();
            }
            //else if (this.level == 8)
            //{
            //    ClearAll();
            //    TemplateLevel levels = new TemplateLevel(Health, Money, Keys, Ammo, 15, 6, 8, this.color);
            //    Levels lvl = new Levels();
            //    levels.Maze = lvl.Maze9;
            //    levels.Start();
            //}
            else
            {
                ClearAll();
                Console.Clear();
                //StartingMenu.DrawEndTitle();
            }

        }

        public void ClearAll()
        {
            needToUnlock = _needH;
            HorizontalEnemyList.Clear();
            HorizontalMovingState.Clear();
            HorizontalMovingSide.Clear();
            HorizontalStatus.Clear();
            HorizontalAliveStatus.Clear();

            VerticalEnemyList.Clear();
            VerticalMovingState.Clear();
            VerticalMovingSide.Clear();
            VerticalStatus.Clear();
            VerticalAliveStatus.Clear();

            breakingWalls.Clear();
            breakingWallsState.Clear();
            breakingWallsExist.Clear();

            bullets.RemoveAll();
        }

        public void PrintMoney()
        {
            DisplayForThreads.WriteInConsoleColors(3, 5, "      " + "   " + "  ", 5, color);
            DisplayForThreads.WriteInConsoleColors(3, 5, "Money: " + Money + "$", 5, color);
        }

        public void PrintPlayerCords()
        {
            int pl_X = PlayerCords.X - x_starting;
            int pl_Y = PlayerCords.Y - y_starting;

            DisplayForThreads.WriteInConsoleColors(20, 1, "                    ", 0, color);
            DisplayForThreads.WriteInConsoleColors(20, 1, "Cords: " + pl_X + " " + pl_Y, 0, color);
        }

        public void PrintExit()
        {
            DisplayForThreads.WriteInConsoleColors(_exit_X, _exit_Y, "E", 0, color);
        }

        public void PrintHealth()
        {
            if (0 > Health)
            {
                _canPlayerMove = false;
                ClearAll();
                Console.Clear();

                DisplayForThreads.WriteInConsoleColors(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2, "Game Over...", 3, color);
                DisplayForThreads.WriteInConsoleColors(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 + 2, "You Died..", 3, color);
                Thread.Sleep(10000);
                Environment.Exit(0);

            }
            DisplayForThreads.WriteInConsoleColors(3, 3, "        " + "   ", 3, color);
            DisplayForThreads.WriteInConsoleColors(3, 3, "Health: " + Health + "♥", 3, color);
        }

        public void PrintKeys()
        {
            DisplayForThreads.WriteInConsoleColors(3, 1, "         ", 1, color);
            DisplayForThreads.WriteInConsoleColors(3, 1, "Keys: " + Keys + " P", 1, color);
        }

        public void PrintAmmo()
        {
            DisplayForThreads.WriteInConsoleColors(20, 3, "                                        ", 1, color);
            DisplayForThreads.WriteInConsoleColors(20, 3, "Ammo: " + _bullets + " A", 1, color);
        }

        public void PrintLevel()
        {
            DisplayForThreads.WriteInConsoleColors(3, 7, "         ", 0, color);
            DisplayForThreads.WriteInConsoleColors(3, 7, "Level: " + level, 0, color);

            if (level == 1)
            {
                DisplayForThreads.WriteInConsoleColors(_X_centeredPostion - 9, _Y_centeredPostion + 6, "Cheat code: ACD", 0, color);
            }
            else if (level == 2)
            {
                DisplayForThreads.WriteInConsoleColors(_X_centeredPostion - 9, _Y_centeredPostion + 6, "Cheat code: FGH", 0, color);
            }
            else if (level == 3)
            {
                DisplayForThreads.WriteInConsoleColors(_X_centeredPostion - 5, _Y_centeredPostion + 6, "Cheat code: OPL", 0, color);
            }
            else if (level == 4)
            {
                DisplayForThreads.WriteInConsoleColors(_X_centeredPostion - 6, _Y_centeredPostion + 6, "Cheat code: JJK", 0, color);
            }
            else if (level == 5)
            {
                DisplayForThreads.WriteInConsoleColors(_X_centeredPostion - 6, _Y_centeredPostion + 13, "Cheat code: QWA", 0, color);
            }
            else if (level == 6)
            {
                DisplayForThreads.WriteInConsoleColors(_X_centeredPostion - 6, _Y_centeredPostion + 14, "Cheat code: LOP", 0, color);
            }
            else if (level == 7)
            {
                DisplayForThreads.WriteInConsoleColors(_X_centeredPostion - 6, _Y_centeredPostion + 14, "Cheat code: ZCX", 0, color);
            }
            else if (level == 8)
            {
                DisplayForThreads.WriteInConsoleColors(_X_centeredPostion - 6, _Y_centeredPostion + 14, "Cheat code: KYF", 0, color);
            }
        }

        public void Draw()
        {
            Location loc = new Location();
            loc.X = x_starting;
            loc.Y = y_starting;

            for (int i = 0; i < Maze.GetLength(0); i++)
            {

                for (int i1 = 0; i1 < Maze.GetLength(1); i1++)
                {
                    if (Maze[i, i1] != " ")
                    {
                        //Thread.Sleep(5);
                    }
                    if (Maze[i, i1] == "↑")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 6, color);
                        this.X_X_start = loc.X;
                        this.X_Y_start = loc.Y;

                        Player_Default_Start_X = loc.X;
                        Player_Default_Start_Y = loc.Y;

                        PlayerCords.X = X_X_start;
                        PlayerCords.Y = X_Y_start; ;

                        this.player_cursor = Maze[i, i1];

                        _side = 0;
                    }  //↑←↓→
                    else if (Maze[i, i1] == "↓")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 6, color);
                        this.X_X_start = loc.X;
                        this.X_Y_start = loc.Y;

                        Player_Default_Start_X = loc.X;
                        Player_Default_Start_Y = loc.Y;

                        PlayerCords.X = X_X_start;
                        PlayerCords.Y = X_Y_start; ;

                        this.player_cursor = Maze[i, i1];

                        _side = 1;
                    }
                    else if (Maze[i, i1] == "→")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 6, color);
                        this.X_X_start = loc.X;
                        this.X_Y_start = loc.Y;

                        Player_Default_Start_X = loc.X;
                        Player_Default_Start_Y = loc.Y;

                        PlayerCords.X = X_X_start;
                        PlayerCords.Y = X_Y_start; ;

                        this.player_cursor = Maze[i, i1];

                        _side = 3;

                    }
                    else if (Maze[i, i1] == "←")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 6, color);
                        this.X_X_start = loc.X;
                        this.X_Y_start = loc.Y;

                        Player_Default_Start_X = loc.X;
                        Player_Default_Start_Y = loc.Y;

                        PlayerCords.X = X_X_start;
                        PlayerCords.Y = X_Y_start; ;

                        this.player_cursor = Maze[i, i1];

                        _side = 2;
                    }
                    else if (Maze[i, i1] == "$")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 5, color);
                    }
                    else if (Maze[i, i1] == "?")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 1, color);
                    }
                    else if (Maze[i, i1] == "C")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 8, ConsoleColor.DarkBlue);
                    }
                    else if (Maze[i, i1] == "E")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 0, color);
                        this._exit_X = loc.X;
                        this._exit_Y = loc.Y;
                    }
                    else if (Maze[i, i1] == "P")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 1, color);
                    }

                    else if (Maze[i, i1] == _horizontalEnemiesSymbol)
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 3, color);
                        Location locBadGuys = new Location();
                        locBadGuys.X = loc.X;
                        locBadGuys.Y = loc.Y;
                        HorizontalEnemyList.Add(locBadGuys);
                        HorizontalMovingState.Add(true);
                        HorizontalMovingSide.Add(false);
                        HorizontalAliveStatus.Add(false);
                        HorizontalStatus.Add(0);
                    }
                    else if (Maze[i, i1] == _verticalEnemiesSymbol)
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 3, color);
                        Location locBadGuys2 = new Location();
                        locBadGuys2.X = loc.X;
                        locBadGuys2.Y = loc.Y;
                        VerticalEnemyList.Add(locBadGuys2);
                        VerticalMovingState.Add(true);
                        VerticalMovingSide.Add(false);
                        VerticalAliveStatus.Add(false);
                        VerticalStatus.Add(0);
                    }
                    else if (Maze[i, i1] == "H")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 2, color);
                        needToUnlock += 1;
                        _needH += 1;
                    }
                    else if (Maze[i, i1] == "}")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 2, color);
                        this._leftTeleporter_X = loc.X;
                        this._leftTeleporter_Y = loc.Y;
                    }
                    else if (Maze[i, i1] == "*")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 2, color);
                        Location location = new Location();
                        location.X = loc.X;
                        location.Y = loc.Y;
                        breakingWalls.Add(location);
                        breakingWallsExist.Add(true);
                        breakingWallsState.Add(0);
                    }
                    else if (Maze[i, i1] == "{")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 2, color);
                        this._rightTeleporter_X = loc.X;
                        this._rightTeleporter_Y = loc.Y;
                    }
                    else if (Maze[i, i1] == "♥")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 3, color);
                    }
                    else if (Maze[i, i1] == _ammoShopSymbol)
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 1, color);
                    }
                    else if (Maze[i, i1] == _destroyingBlockSymbol)
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 3, color);
                    }
                    else if (Maze[i, i1] == "X")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 3, color);
                        this.X_X = loc.X;
                        this.X_Y = loc.Y;
                    }
                    else if (Maze[i, i1] == "|")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 1, color);
                    }
                    else if (Maze[i, i1] == "#")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, "▒", 8, color);
                    }
                    else if (Maze[i, i1] == "-")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 1, color);
                    }
                    else if (Maze[i, i1] == "\u25A0")
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 1, color);
                    }
                    else
                    {
                        DisplayForThreads.WriteInConsoleColors(loc.X, loc.Y, Maze[i, i1], 8, color);
                    }
                    loc.X++;
                }
                loc.Y++;
                loc.X = x_starting;
                Console.WriteLine();

            }

                DisplayForThreads.WriteInConsoleColors(Console.BufferWidth - 30, 1, "Press 'R' to restart level ! ", 0, color);
           
            PrintMoney();

            PrintHealth();

            PrintKeys();

            PrintPlayerCords();

            PrintLevel();

            PrintAmmo();

            if (enemyThreadStarted == false)
            {
                Threads();
                enemyThreadStarted = true;
            }
        }

        public void StartThreads()
        {
            VerticalEnemiesTask.Start();

            HorizontalEnemiesTask.Start();

            Shooting.Start();
        }

        public void Threads()
        {
            VerticalEnemiesTask = new Task(() =>
            {
                while (levelEnd)
                {
                    try
                    {

                        if (moveEnemies2 == true)
                        {
                            canRestart2 = false;
                            Thread.Sleep(200);
                            int i = 0;

                            for (i = 0; i < VerticalEnemyList.Count; i++)
                            {
                                Sequence.Lock();

                                if (VerticalAliveStatus[i] == false)
                                {
                                    if (VerticalStatus[i] >= 2)
                                    {
                                        VerticalAliveStatus[i] = true;
                                        Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting] = " ";
                                        DisplayForThreads.WriteInConsoleColors(VerticalEnemyList[i].X, VerticalEnemyList[i].Y, " ", 0, color);
                                    }
                                }
                                if (VerticalAliveStatus[i] == false)
                                {
                                    if (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting] == _playerShootingSymbol)
                                    {
                                        VerticalStatus[i]++;
                                    }
                                    else if (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == _playerShootingSymbol)
                                    {
                                        VerticalStatus[i]++;
                                    }
                                    else if (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == _playerShootingSymbol)
                                    {
                                        VerticalStatus[i]++;
                                    }



                                    if (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == "#" ||
                                    Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == "■" ||
                                     Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == _verticalEnemiesSymbol ||
                                         Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == _horizontalEnemiesSymbol)
                                    {
                                        VerticalMovingSide[i] = false;
                                    }

                                    if (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == "#" ||
                                    Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == "■" ||
                                     Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == _verticalEnemiesSymbol ||
                                      Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == _horizontalEnemiesSymbol)
                                    {
                                        VerticalMovingSide[i] = true;
                                    }


                                    if (VerticalStatus[i] == 2)
                                    {
                                        VerticalAliveStatus[i] = true;
                                        Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting] = " ";
                                        DisplayForThreads.WriteInConsoleColors(VerticalEnemyList[i].X, VerticalEnemyList[i].Y, " ", 0, color);
                                    }


                                    if ((Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == "#" && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == "#")
                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == "■" && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == "■")
                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == _horizontalEnemiesSymbol && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == _horizontalEnemiesSymbol)
                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == _verticalEnemiesSymbol && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == _verticalEnemiesSymbol)

                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == "#" && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == "■")
                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == "■" && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == "#")

                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == "■" && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == _horizontalEnemiesSymbol)
                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == _horizontalEnemiesSymbol && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == "■")

                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == _horizontalEnemiesSymbol && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == "#")
                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == "#" && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == _horizontalEnemiesSymbol)

                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == _verticalEnemiesSymbol && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == _horizontalEnemiesSymbol)
                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == _horizontalEnemiesSymbol && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == _verticalEnemiesSymbol)

                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == "#" && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == _verticalEnemiesSymbol)
                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == _verticalEnemiesSymbol && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == "#")

                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == "■" && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == _verticalEnemiesSymbol)
                                    || (Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] == _verticalEnemiesSymbol && Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] == "■"))
                                    {

                                        VerticalMovingState[i] = false;

                                    }

                                    else
                                    {

                                        VerticalMovingState[i] = true;
                                    }


                                    if (VerticalMovingSide[i] == true)
                                    {
                                        if (VerticalMovingState[i] == true)
                                        {
                                            if (VerticalStatus[i] == 0)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(VerticalEnemyList[i].X, VerticalEnemyList[i].Y, " ", 9, color);
                                                Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting] = " ";

                                                Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] = _verticalEnemiesSymbol;
                                                DisplayForThreads.WriteInConsoleColors(VerticalEnemyList[i].X - 1, VerticalEnemyList[i].Y, _verticalEnemiesSymbol, 3, color);
                                                VerticalEnemyList[i].X--;
                                            }
                                            else if (VerticalStatus[i] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(VerticalEnemyList[i].X, VerticalEnemyList[i].Y, " ", 0, color);
                                                Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting] = " ";

                                                Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting - 1] = _verticalEnemiesSymbol;
                                                DisplayForThreads.WriteInConsoleColors(VerticalEnemyList[i].X - 1, VerticalEnemyList[i].Y, _verticalEnemiesSymbol, 10, color);
                                                VerticalEnemyList[i].X--;
                                            }
                                            else if (VerticalStatus[i] >= 2)
                                            {
                                                VerticalMovingState[i] = false;
                                                VerticalAliveStatus[i] = true;
                                                Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting] = " ";
                                                DisplayForThreads.WriteInConsoleColors(VerticalEnemyList[i].X, VerticalEnemyList[i].Y, " ", 0, color);
                                            }

                                            if ((VerticalEnemyList[i].Y == PlayerCords.Y && VerticalEnemyList[i].X == PlayerCords.X))
                                            {
                                                PlayerDead();
                                            }
                                        }
                                    }
                                    if (VerticalMovingSide[i] == false)
                                    {
                                        if (VerticalMovingState[i] == true)
                                        {
                                            if (VerticalStatus[i] == 0)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(VerticalEnemyList[i].X, VerticalEnemyList[i].Y, " ", 9, color);
                                                Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting] = " ";

                                                Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] = _verticalEnemiesSymbol;
                                                DisplayForThreads.WriteInConsoleColors(VerticalEnemyList[i].X + 1, VerticalEnemyList[i].Y, _verticalEnemiesSymbol, 3, color);
                                                VerticalEnemyList[i].X++;
                                            }
                                            else if (VerticalStatus[i] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(VerticalEnemyList[i].X, VerticalEnemyList[i].Y, " ", 0, color);
                                                Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting] = " ";

                                                Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting + 1] = _verticalEnemiesSymbol;
                                                DisplayForThreads.WriteInConsoleColors(VerticalEnemyList[i].X + 1, VerticalEnemyList[i].Y, _verticalEnemiesSymbol, 10, color);
                                                VerticalEnemyList[i].X++;
                                            }

                                            else if (VerticalStatus[i] >= 2)
                                            {
                                                VerticalMovingState[i] = false;
                                                VerticalAliveStatus[i] = true;
                                                Maze[VerticalEnemyList[i].Y - y_starting, VerticalEnemyList[i].X - x_starting] = " ";
                                                DisplayForThreads.WriteInConsoleColors(VerticalEnemyList[i].X, VerticalEnemyList[i].Y, " ", 0, color);
                                            }

                                            if ((VerticalEnemyList[i].Y == PlayerCords.Y && VerticalEnemyList[i].X == PlayerCords.X))
                                            {
                                                PlayerDead();
                                            }
                                        }
                                    }
                                    //Thread.Sleep(10);
                                }
                                Sequence.Unlock();
                            }

                        }

                        canRestart2 = true;
                    }
                    catch (Exception e) { }
                }

            });

           HorizontalEnemiesTask = new Task(() =>
            {
                while (levelEnd)
                {
                    try
                    {

                        if (moveEnemies == true)
                        {
                            canRestart = false;
                            Thread.Sleep(200);
                            int i = 0;

                            for (i = 0; i < HorizontalEnemyList.Count; i++)
                            {
                                Sequence.Lock();

                                if (HorizontalAliveStatus[i] != true)
                                {
                                    if (HorizontalStatus[i] >= 2)
                                    {
                                        HorizontalAliveStatus[i] = true;
                                        Maze[HorizontalEnemyList[i].Y - y_starting, HorizontalEnemyList[i].X - x_starting] = " ";
                                        DisplayForThreads.WriteInConsoleColors(HorizontalEnemyList[i].X, HorizontalEnemyList[i].Y, " ", 0, color);
                                    }
                                }

                                if (HorizontalAliveStatus[i] != true)
                                {
                                    //DisplayForThreads.WriteInConsoleColors(30, 1,list[i].X + " " + list[i].Y + ", i is: " + i + ", up is: " + listCanMove[i] + "  ", 2);
                                    if (Maze[HorizontalEnemyList[i].Y - y_starting, HorizontalEnemyList[i].X - x_starting] == _playerShootingSymbol)
                                    {
                                        HorizontalStatus[i]++;
                                    }
                                    else if (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == _playerShootingSymbol)
                                    {
                                        HorizontalStatus[i]++;
                                    }
                                    else if (Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == _playerShootingSymbol)
                                    {
                                        HorizontalStatus[i]++;
                                    }

                                    if (Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == "#"
                                    || Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == "■"
                                    || Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == _horizontalEnemiesSymbol
                                    || Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == _verticalEnemiesSymbol)
                                    {
                                        HorizontalMovingSide[i] = false;
                                    }

                                    if (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == "#"
                                    || Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == "■"
                                    || Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == _horizontalEnemiesSymbol
                                    || Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == _verticalEnemiesSymbol)
                                    {
                                        HorizontalMovingSide[i] = true;
                                    }


                                    if (HorizontalStatus[i] == 2)
                                    {
                                        HorizontalAliveStatus[i] = true;
                                        Maze[HorizontalEnemyList[i].Y - y_starting, HorizontalEnemyList[i].X - x_starting] = " ";
                                        DisplayForThreads.WriteInConsoleColors(HorizontalEnemyList[i].X, HorizontalEnemyList[i].Y, " ", 0, color);
                                    }


                                    if ((Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == "#" && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == "#")
                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == "■" && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == "■")
                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == _verticalEnemiesSymbol && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == _verticalEnemiesSymbol)
                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == _horizontalEnemiesSymbol && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == _horizontalEnemiesSymbol)

                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == "#" && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == "■")
                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == "■" && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == "#")

                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == _verticalEnemiesSymbol && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == "■")
                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == "■" && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == _verticalEnemiesSymbol)

                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == _verticalEnemiesSymbol && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == "#")
                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == "#" && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == _verticalEnemiesSymbol)

                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == _verticalEnemiesSymbol && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == _horizontalEnemiesSymbol)
                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == _horizontalEnemiesSymbol && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == _verticalEnemiesSymbol)

                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == "#" && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == _horizontalEnemiesSymbol)
                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == _horizontalEnemiesSymbol && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == "#")

                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == "■" && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == _horizontalEnemiesSymbol)
                                     || (Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] == _horizontalEnemiesSymbol && Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] == "■"))
                                    {

                                        HorizontalMovingState[i] = false;

                                    }

                                    else
                                    {

                                        HorizontalMovingState[i] = true;
                                    }


                                    if (HorizontalMovingSide[i])
                                    {
                                        if (HorizontalMovingState[i] == true)
                                        {
                                            if (HorizontalStatus[i] == 0)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(HorizontalEnemyList[i].X, HorizontalEnemyList[i].Y, " ", 9, color);
                                                Maze[HorizontalEnemyList[i].Y - y_starting, HorizontalEnemyList[i].X - x_starting] = " ";

                                                Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] = _horizontalEnemiesSymbol;
                                                DisplayForThreads.WriteInConsoleColors(HorizontalEnemyList[i].X, HorizontalEnemyList[i].Y - 1, _horizontalEnemiesSymbol, 3, color);
                                                HorizontalEnemyList[i].Y--;
                                            }
                                            if (HorizontalStatus[i] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(HorizontalEnemyList[i].X, HorizontalEnemyList[i].Y, " ", 0, color);
                                                Maze[HorizontalEnemyList[i].Y - y_starting, HorizontalEnemyList[i].X - x_starting] = " ";

                                                Maze[HorizontalEnemyList[i].Y - y_starting - 1, HorizontalEnemyList[i].X - x_starting] = _horizontalEnemiesSymbol;
                                                DisplayForThreads.WriteInConsoleColors(HorizontalEnemyList[i].X, HorizontalEnemyList[i].Y - 1, _horizontalEnemiesSymbol, 10, color);
                                                HorizontalEnemyList[i].Y--;
                                            }

                                            if ((HorizontalEnemyList[i].Y == PlayerCords.Y && HorizontalEnemyList[i].X == PlayerCords.X))
                                            {
                                                PlayerDead();
                                            }
                                            else if (HorizontalStatus[i] >= 2)
                                            {
                                                HorizontalAliveStatus[i] = true;
                                                Maze[HorizontalEnemyList[i].Y - y_starting, HorizontalEnemyList[i].X - x_starting] = " ";
                                                DisplayForThreads.WriteInConsoleColors(HorizontalEnemyList[i].X, HorizontalEnemyList[i].Y, " ", 0, color);
                                            }
                                        }
                                    }
                                    if (!HorizontalMovingSide[i])
                                    {
                                        if (HorizontalMovingState[i] == true)
                                        {
                                            if (HorizontalStatus[i] == 0)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(HorizontalEnemyList[i].X, HorizontalEnemyList[i].Y, " ", 9, color);
                                                Maze[HorizontalEnemyList[i].Y - y_starting, HorizontalEnemyList[i].X - x_starting] = " ";

                                                Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] = _horizontalEnemiesSymbol;
                                                DisplayForThreads.WriteInConsoleColors(HorizontalEnemyList[i].X, HorizontalEnemyList[i].Y + 1, _horizontalEnemiesSymbol, 3, color);
                                                HorizontalEnemyList[i].Y++;
                                            }
                                            else if (HorizontalStatus[i] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(HorizontalEnemyList[i].X, HorizontalEnemyList[i].Y, " ", 0, color);
                                                Maze[HorizontalEnemyList[i].Y - y_starting, HorizontalEnemyList[i].X - x_starting] = " ";

                                                Maze[HorizontalEnemyList[i].Y - y_starting + 1, HorizontalEnemyList[i].X - x_starting] = _horizontalEnemiesSymbol;
                                                DisplayForThreads.WriteInConsoleColors(HorizontalEnemyList[i].X, HorizontalEnemyList[i].Y + 1, _horizontalEnemiesSymbol, 10, color);
                                                HorizontalEnemyList[i].Y++;
                                            }
                                            else if (HorizontalStatus[i] >= 2)
                                            {
                                                HorizontalAliveStatus[i] = true;
                                                Maze[HorizontalEnemyList[i].Y - y_starting, HorizontalEnemyList[i].X - x_starting] = " ";
                                                DisplayForThreads.WriteInConsoleColors(HorizontalEnemyList[i].X, HorizontalEnemyList[i].Y, " ", 0, color);
                                            }

                                            //↑←↓→
                                            if ((HorizontalEnemyList[i].Y == PlayerCords.Y && HorizontalEnemyList[i].X == PlayerCords.X))
                                            {
                                                PlayerDead();
                                            }

                                        }
                                    }
                                    //Thread.Sleep(10);
                                }
                                Sequence.Unlock();
                            }
                        }
                        canRestart = true;
                    }
                    catch (Exception e) { }
                }

            });

            Shooting = new Task(() =>
            {
                while (levelEnd)
                {




                    for (int i = 0; i < AmmoList.List.Count; i++)
                    {
                        if (AmmoList.List[i].Side == 0)
                        {
                            Location up = new Location();
                            up.X = AmmoList.List[i].Location.X;
                            up.Y = AmmoList.List[i].Location.Y;


                            if (Maze[up.Y - y_starting - 1, up.X - x_starting] == " ")
                            {
                                AmmoList.List[i].Location.Y--;
                                up.Y--;
                                Maze[up.Y - y_starting + 1, up.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(up.X, up.Y + 1, " ", 3, color);

                                Maze[up.Y - y_starting, up.X - x_starting] = _playerShootingSymbol;
                                DisplayForThreads.WriteInConsoleColors(up.X, up.Y, _playerShootingSymbol, 3, color);
                                //DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);

                            }
                            else
                            {
                                Maze[up.Y - y_starting, up.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(up.X, up.Y, " ", 3, color);

                                if (Maze[up.Y - y_starting - 1, up.X - x_starting] == _horizontalEnemiesSymbol)
                                {
                                    up.Y--;
                                    for (int j = 0; j < HorizontalEnemyList.Count; j++)
                                    {
                                        if (HorizontalEnemyList[j].X == up.X && HorizontalEnemyList[j].Y == up.Y)
                                        {
                                            HorizontalStatus[j]++;
                                            if (HorizontalStatus[j] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(up.X, up.Y, _horizontalEnemiesSymbol, 10, color);
                                            }
                                        }
                                    }
                                }
                                else if (Maze[up.Y - y_starting - 1, up.X - x_starting] == _verticalEnemiesSymbol)
                                {
                                    up.Y--;
                                    for (int j = 0; j < VerticalEnemyList.Count; j++)
                                    {
                                        if (VerticalEnemyList[j].X == up.X && VerticalEnemyList[j].Y == up.Y)
                                        {
                                            VerticalStatus[j]++;
                                            if (VerticalStatus[j] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(up.X, up.Y, _verticalEnemiesSymbol, 10, color);
                                            }
                                        }
                                    }
                                }
                                else if (Maze[up.Y - y_starting - 1, up.X - x_starting] == "*")
                                {
                                    up.Y--;
                                    for (int j = 0; j < breakingWalls.Count; j++)
                                    {
                                        if (breakingWalls[j].X == up.X && breakingWalls[j].Y == up.Y)
                                        {
                                            breakingWallsState[j]++;
                                            if (breakingWallsState[j] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(breakingWalls[j].X, breakingWalls[j].Y, "*", 9, color);
                                            }
                                            else if (breakingWallsState[j] == 2)
                                            {
                                                breakingWallsExist[j] = false;
                                                Maze[breakingWalls[j].Y - y_starting, breakingWalls[j].X - x_starting] = " ";
                                                DisplayForThreads.WriteInConsoleColors(breakingWalls[j].X, breakingWalls[j].Y, " ", 9, color);
                                            }
                                        }

                                    }
                                }
                                AmmoList.Ready[i] = false;
                                AmmoList.Refresh();
                                //DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);
                            }
                            DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);
                        }
                        else if (AmmoList.List[i].Side == 1)
                        {
                            Location down = new Location();
                            down.X = AmmoList.List[i].Location.X;
                            down.Y = AmmoList.List[i].Location.Y;


                            if (Maze[down.Y - y_starting + 1, down.X - x_starting] == " ")
                            {
                                AmmoList.List[i].Location.Y++;
                                down.Y++;
                                Maze[down.Y - y_starting - 1, down.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(down.X, down.Y - 1, " ", 3, color);

                                Maze[down.Y - y_starting, down.X - x_starting] = _playerShootingSymbol;
                                DisplayForThreads.WriteInConsoleColors(down.X, down.Y, _playerShootingSymbol, 3, color);
                                // DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);

                            }
                            else
                            {
                                Maze[down.Y - y_starting, down.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(down.X, down.Y, " ", 3, color);

                                if (Maze[down.Y - y_starting + 1, down.X - x_starting] == _horizontalEnemiesSymbol)
                                {
                                    down.Y++;
                                    for (int j = 0; j < HorizontalEnemyList.Count; j++)
                                    {
                                        if (HorizontalEnemyList[j].X == down.X && HorizontalEnemyList[j].Y == down.Y)
                                        {
                                            HorizontalStatus[j]++;
                                            if (HorizontalStatus[j] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(down.X, down.Y, _horizontalEnemiesSymbol, 10, color);
                                            }
                                        }
                                    }
                                }
                                else if (Maze[down.Y - y_starting + 1, down.X - x_starting] == _verticalEnemiesSymbol)
                                {
                                    down.Y++;
                                    for (int j = 0; j < VerticalEnemyList.Count; j++)
                                    {
                                        if (VerticalEnemyList[j].X == down.X && VerticalEnemyList[j].Y == down.Y)
                                        {
                                            VerticalStatus[j]++;
                                            if (VerticalStatus[j] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(down.X, down.Y, _verticalEnemiesSymbol, 10, color);
                                            }
                                            else if (VerticalStatus[j] == 2)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(down.X, down.Y, " ", 10, color);
                                                Maze[down.Y - y_starting, down.X - x_starting] = " ";
                                            }
                                        }
                                    }
                                }
                                else if (Maze[down.Y - y_starting + 1, down.X - x_starting] == "*")
                                {
                                    down.Y++;
                                    for (int j = 0; j < breakingWalls.Count; j++)
                                    {
                                        if (breakingWalls[j].X == down.X && breakingWalls[j].Y == down.Y)
                                        {
                                            breakingWallsState[j]++;
                                            if (breakingWallsState[j] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(breakingWalls[j].X, breakingWalls[j].Y, "*", 9, color);
                                            }
                                            else if (breakingWallsState[j] == 2)
                                            {
                                                breakingWallsExist[j] = false;
                                                Maze[breakingWalls[j].Y - y_starting, breakingWalls[j].X - x_starting] = " ";
                                                DisplayForThreads.WriteInConsoleColors(breakingWalls[j].X, breakingWalls[j].Y, " ", 9, color);
                                            }
                                        }

                                    }
                                }
                                AmmoList.Ready[i] = false;
                                AmmoList.Refresh();
                                // DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);
                            }
                            DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);
                        }
                        else if (AmmoList.List[i].Side == 2)
                        {
                            Location left = new Location();
                            left.X = AmmoList.List[i].Location.X;
                            left.Y = AmmoList.List[i].Location.Y;


                            if (Maze[left.Y - y_starting, left.X - x_starting - 1] == " ")
                            {
                                AmmoList.List[i].Location.X--;
                                left.X--;
                                Maze[left.Y - y_starting, left.X - x_starting + 1] = " ";
                                DisplayForThreads.WriteInConsoleColors(left.X + 1, left.Y, " ", 3, color);

                                Maze[left.Y - y_starting, left.X - x_starting] = _playerShootingSymbol;
                                DisplayForThreads.WriteInConsoleColors(left.X, left.Y, _playerShootingSymbol, 3, color);
                                // DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);
                            }
                            else
                            {
                                Maze[left.Y - y_starting, left.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(left.X, left.Y, " ", 3, color);

                                if (Maze[left.Y - y_starting, left.X - x_starting - 1] == _horizontalEnemiesSymbol)
                                {
                                    left.X--;
                                    for (int j = 0; j < HorizontalEnemyList.Count; j++)
                                    {
                                        if (HorizontalEnemyList[j].X == left.X && HorizontalEnemyList[j].Y == left.Y)
                                        {
                                            HorizontalStatus[j]++;
                                            if (HorizontalStatus[j] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(left.X, left.Y, _horizontalEnemiesSymbol, 10, color);
                                            }
                                        }
                                    }
                                }
                                else if (Maze[left.Y - y_starting, left.X - x_starting - 1] == _verticalEnemiesSymbol)
                                {
                                    left.X--;
                                    for (int j = 0; j < VerticalEnemyList.Count; j++)
                                    {
                                        if (VerticalEnemyList[j].X == left.X && VerticalEnemyList[j].Y == left.Y)
                                        {
                                            VerticalStatus[j]++;
                                            if (VerticalStatus[j] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(left.X, left.Y, _verticalEnemiesSymbol, 10, color);
                                            }
                                        }
                                    }
                                }
                                else if (Maze[left.Y - y_starting, left.X - x_starting - 1] == "*")
                                {
                                    left.X--;
                                    for (int j = 0; j < breakingWalls.Count; j++)
                                    {
                                        if (breakingWalls[j].X == left.X && breakingWalls[j].Y == left.Y)
                                        {
                                            breakingWallsState[j]++;
                                            if (breakingWallsState[j] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(breakingWalls[j].X, breakingWalls[j].Y, "*", 9, color);
                                            }
                                            else if (breakingWallsState[j] == 2)
                                            {
                                                breakingWallsExist[j] = false;
                                                Maze[breakingWalls[j].Y - y_starting, breakingWalls[j].X - x_starting] = " ";
                                                DisplayForThreads.WriteInConsoleColors(breakingWalls[j].X, breakingWalls[j].Y, " ", 9, color);
                                            }
                                        }

                                    }
                                }

                                AmmoList.Ready[i] = false;
                                AmmoList.Refresh();
                                //DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);
                            }
                            DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);
                        }
                        else if (AmmoList.List[i].Side == 3)
                        {
                            Location right = new Location();
                            right.X = AmmoList.List[i].Location.X;
                            right.Y = AmmoList.List[i].Location.Y;

                            if (Maze[right.Y - y_starting, right.X - x_starting + 1] == " ")
                            {
                                AmmoList.List[i].Location.X++;
                                right.X++;
                                Maze[right.Y - y_starting, right.X - x_starting - 1] = " ";
                                DisplayForThreads.WriteInConsoleColors(right.X - 1, right.Y, " ", 3, color);

                                Maze[right.Y - y_starting, right.X - x_starting] = _playerShootingSymbol;
                                DisplayForThreads.WriteInConsoleColors(right.X, right.Y, _playerShootingSymbol, 3, color);
                                //DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);
                            }
                            else
                            {
                                Maze[right.Y - y_starting, right.X - x_starting] = " ";
                                DisplayForThreads.WriteInConsoleColors(right.X, right.Y, " ", 3, color);

                                if (Maze[right.Y - y_starting, right.X - x_starting + 1] == _horizontalEnemiesSymbol)
                                {
                                    right.X++;
                                    for (int j = 0; j < HorizontalEnemyList.Count; j++)
                                    {
                                        if (HorizontalEnemyList[j].X == right.X && HorizontalEnemyList[j].Y == right.Y)
                                        {
                                            HorizontalStatus[j]++;
                                            if (HorizontalStatus[j] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(right.X, right.Y, _horizontalEnemiesSymbol, 10, color);
                                            }
                                        }
                                    }
                                }
                                else if (Maze[right.Y - y_starting, right.X - x_starting + 1] == _verticalEnemiesSymbol)
                                {
                                    right.X++;
                                    for (int j = 0; j < VerticalEnemyList.Count; j++)
                                    {
                                        if (VerticalEnemyList[j].X == right.X && VerticalEnemyList[j].Y == right.Y)
                                        {
                                            VerticalStatus[j]++;
                                            if (VerticalStatus[j] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(right.X, right.Y, _verticalEnemiesSymbol, 10, color);
                                            }
                                        }
                                    }
                                }
                                else if (Maze[right.Y - y_starting, right.X - x_starting + 1] == "*")
                                {
                                    right.X++;
                                    for (int j = 0; j < breakingWalls.Count; j++)
                                    {
                                        if (breakingWalls[j].X == right.X && breakingWalls[j].Y == right.Y)
                                        {
                                            breakingWallsState[j]++;
                                            if (breakingWallsState[j] == 1)
                                            {
                                                DisplayForThreads.WriteInConsoleColors(breakingWalls[j].X, breakingWalls[j].Y, "*", 9, color);
                                            }
                                            else if (breakingWallsState[j] == 2)
                                            {
                                                breakingWallsExist[j] = false;
                                                Maze[breakingWalls[j].Y - y_starting, breakingWalls[j].X - x_starting] = " ";
                                                DisplayForThreads.WriteInConsoleColors(breakingWalls[j].X, breakingWalls[j].Y, " ", 9, color);
                                            }
                                        }

                                    }
                                }

                                AmmoList.Ready[i] = false;
                                AmmoList.Refresh();
                                //DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);
                            }


                            DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);
                        }
                    }
                    //DisplayForThreads.WriteInConsoleColors(PlayerCords.X, PlayerCords.Y, player_cursor, 6, color);
                    Thread.Sleep(bulletSpeed);

                    //Console.WriteLine(AmmoList.List.Count);

                }


            });

            StartThreads();
        }
    }
}