using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public sealed class Program
{
    private static readonly Random Randomnummer = new();

    private static readonly List<int> TeljePositie1 = [];
    private static string _movement1 = "RIGHT";

    private static readonly List<int> TeljePositie2 = [];
    private static string _movement2 = "LEFT";

    private static int _obstacleXpos;
    private static int _obstacleYpos;
    private static readonly int Screenwidth = Console.WindowWidth;
    private static readonly int Screenheight = Console.WindowHeight;

    private static bool PlaceObstacle()
    {
        int x = Randomnummer.Next(1, Screenwidth - 1);
        int y = Randomnummer.Next(1, Screenheight - 1);

        bool isTaken = false;
        
        for (int i = 0; i < TeljePositie1.Count; i += 2)
        {
            if (x == TeljePositie1[i] && y == TeljePositie1[i + 1])
            {
                isTaken = true;
                break;
            }
        }
        
        for (int i = 0; i < TeljePositie2.Count; i += 2)
        {
            if (x != TeljePositie2[i] || y != TeljePositie2[i + 1])
                continue;
            isTaken = true;
            break;
        }

        if (isTaken)
        {
            return false;
        }
        else
        {
            _obstacleXpos = x;
            _obstacleYpos = y;
            return true;
        }
    }

    private static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;

        int score1 = 0;
        int score2 = 0;

        var hoofd1 = new Pixel
            {
                xPos = Screenwidth / 4,
                yPos = Screenheight / 2,
                schermKleur = ConsoleColor.Red
            };
        TeljePositie1.Add(hoofd1.xPos);
        TeljePositie1.Add(hoofd1.yPos);

        var hoofd2 = new Pixel
            {
                xPos = 3 * Screenwidth / 4,
                yPos = Screenheight / 2,
                schermKleur = ConsoleColor.Blue
            };
        TeljePositie2.Add(hoofd2.xPos);
        TeljePositie2.Add(hoofd2.yPos);

        string obstacle = "*";
        while (!PlaceObstacle()) { }

        while (true)
        {
            if (Console.KeyAvailable)
            {
                var info = Console.ReadKey(true);

                switch (info.Key)
                {
                    case ConsoleKey.W:
                        if (_movement1 != "DOWN") _movement1 = "UP";
                        break;
                    case ConsoleKey.S:
                        if (_movement1 != "UP") _movement1 = "DOWN";
                        break;
                    case ConsoleKey.A:
                        if (_movement1 != "RIGHT") _movement1 = "LEFT";
                        break;
                    case ConsoleKey.D:
                        if (_movement1 != "LEFT") _movement1 = "RIGHT";
                        break;

                    case ConsoleKey.UpArrow:
                        if (_movement2 != "DOWN") _movement2 = "UP";
                        break;
                    case ConsoleKey.DownArrow:
                        if (_movement2 != "UP") _movement2 = "DOWN";
                        break;
                    case ConsoleKey.LeftArrow:
                        if (_movement2 != "RIGHT") _movement2 = "LEFT";
                        break;
                    case ConsoleKey.RightArrow:
                        if (_movement2 != "LEFT") _movement2 = "RIGHT";
                        break;
                }
            }

            switch (_movement1)
            {
                case "UP":
                    hoofd1.yPos--;
                    break;
                case "DOWN":
                    hoofd1.yPos++;
                    break;
                case "LEFT":
                    hoofd1.xPos--;
                    break;
                case "RIGHT":
                    hoofd1.xPos++;
                    break;
            }

            switch (_movement2)
            {
                case "UP":
                    hoofd2.yPos--;
                    break;
                case "DOWN":
                    hoofd2.yPos++;
                    break;
                case "LEFT":
                    hoofd2.xPos--;
                    break;
                case "RIGHT":
                    hoofd2.xPos++;
                    break;
            }

            var tailExtended1 = false;
            if (hoofd1.xPos == _obstacleXpos && hoofd1.yPos == _obstacleYpos)
            {
                score1++;
                while (!PlaceObstacle()) { }
                tailExtended1 = true;
            }

            var tailExtended2 = false;
            if (hoofd2.xPos == _obstacleXpos && hoofd2.yPos == _obstacleYpos)
            {
                score2++;
                while (!PlaceObstacle()) { }
                tailExtended2 = true;
            }

            TeljePositie1.Insert(0, hoofd1.xPos);
            TeljePositie1.Insert(1, hoofd1.yPos);
            if (!tailExtended1)
            {
                TeljePositie1.RemoveAt(TeljePositie1.Count - 1);
                TeljePositie1.RemoveAt(TeljePositie1.Count - 1);
            }

            TeljePositie2.Insert(0, hoofd2.xPos);
            TeljePositie2.Insert(1, hoofd2.yPos);
            if (!tailExtended2)
            {
                TeljePositie2.RemoveAt(TeljePositie2.Count - 1);
                TeljePositie2.RemoveAt(TeljePositie2.Count - 1);
            }

            var snake1Dead = hoofd1.xPos == 0 || hoofd1.xPos == Screenwidth - 1 || hoofd1.yPos == 0 || hoofd1.yPos == Screenheight - 1;

            for (int i = 2; i < TeljePositie1.Count; i += 2)
            {
                if (hoofd1.xPos != TeljePositie1[i] || hoofd1.yPos != TeljePositie1[i + 1])
                    continue;
                snake1Dead = true;
                break;
            }

            for (int i = 0; i < TeljePositie2.Count; i += 2)
            {
                if (hoofd1.xPos != TeljePositie2[i] || hoofd1.yPos != TeljePositie2[i + 1])
                    continue;
                snake1Dead = true;
                break;
            }

            var snake2Dead = hoofd2.xPos == 0 || hoofd2.xPos == Screenwidth - 1 || hoofd2.yPos == 0 || hoofd2.yPos == Screenheight - 1;

            for (int i = 2; i < TeljePositie2.Count; i += 2)
            {
                if (hoofd2.xPos != TeljePositie2[i] || hoofd2.yPos != TeljePositie2[i + 1])
                    continue;
                snake2Dead = true;
                break;
            }

            for (int i = 0; i < TeljePositie1.Count; i += 2)
            {
                if (hoofd2.xPos != TeljePositie1[i] || hoofd2.yPos != TeljePositie1[i + 1])
                    continue;
                snake2Dead = true;
                break;
            }

            if (snake1Dead || snake2Dead)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(Screenwidth / 5, Screenheight / 2 - 1);
                Console.WriteLine("Game Over!");
                
                if (snake1Dead && snake2Dead)
                {
                    Console.SetCursorPosition(Screenwidth / 5, Screenheight / 2);
                    Console.WriteLine("Draw!");
                }
                else if (snake2Dead)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(Screenwidth / 5, Screenheight / 2);
                    Console.WriteLine("Red (WASD) Wins!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.SetCursorPosition(Screenwidth / 5, Screenheight / 2);
                    Console.WriteLine("Blue (Arrows) Wins!");
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(Screenwidth / 5, Screenheight / 2 + 1);
                Console.WriteLine("Red Score: " + score1);
                Console.SetCursorPosition(Screenwidth / 5, Screenheight / 2 + 2);
                Console.WriteLine("Blue Score: " + score2);
                Console.SetCursorPosition(Screenwidth / 5, Screenheight / 2 + 3);
                Environment.Exit(0);
            }

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(_obstacleXpos, _obstacleYpos);
            Console.Write(obstacle);

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < Screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }
            for (int i = 0; i < Screenwidth; i++)
            {
                Console.SetCursorPosition(i, Screenheight - 1);
                Console.Write("■");
            }
            for (int i = 0; i < Screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }
            for (int i = 0; i < Screenheight; i++)
            {
                Console.SetCursorPosition(Screenwidth - 1, i);
                Console.Write("■");
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(1, 0);
            Console.Write("R:" + score1);
            
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(Screenwidth - 8, 0);
            Console.Write("B:" + score2);

            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < TeljePositie1.Count; i += 2)
            {
                Console.SetCursorPosition(TeljePositie1[i], TeljePositie1[i + 1]);
                Console.Write("■");
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < TeljePositie2.Count; i += 2)
            {
                Console.SetCursorPosition(TeljePositie2[i], TeljePositie2[i + 1]);
                Console.Write("■");
            }

            Thread.Sleep(150);
        }
    }
}