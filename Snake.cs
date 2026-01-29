using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program

{
    static Random randomnummer = new Random();
    static List<int> teljePositie = new List<int>();
    static int obstacleXpos;
    static int obstacleYpos;
    static int screenwidth = Console.WindowWidth;
    static int screenheight = Console.WindowHeight;

    static void PlaceObstacle()
    {
        int x = randomnummer.Next(1, screenwidth-1);
        int y = randomnummer.Next(1, screenheight-1);

        bool isTaken = false;
        for (int i = 2; i < teljePositie.Count; i += 2)
        {
            if (x ==  teljePositie[i] && y == teljePositie[i+1])
            {
                isTaken = true; break;
            }
        }

        if (isTaken)
        {
            PlaceObstacle();
        }
        else
        {
            obstacleXpos = x;
            obstacleYpos = y;
        }

    }

    static void Main()

    {

        Console.WindowHeight = 16;

        Console.WindowWidth = 32;

        string movement = "RIGHT";

        List<int> telje = new List<int>();

        int score = 0;

        Pixel hoofd = new Pixel();

        hoofd.xPos = screenwidth / 2;

        hoofd.yPos = screenheight / 2;

        hoofd.schermKleur = ConsoleColor.Red;

        teljePositie.Add(hoofd.xPos);

        teljePositie.Add(hoofd.yPos);



        DateTime tijd = DateTime.Now;

        string obstacle = "*";

        PlaceObstacle();

        while (true)

        {

            Console.Clear();

            //Draw Obstacle

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.SetCursorPosition(obstacleXpos, obstacleYpos);

            Console.Write(obstacle);



            Console.ForegroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);

            Console.Write("■");



            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < screenwidth; i++)

            {

                Console.SetCursorPosition(i, 0);

                Console.Write("■");

            }

            for (int i = 0; i < screenwidth; i++)

            {

                Console.SetCursorPosition(i, screenheight - 1);

                Console.Write("■");

            }

            for (int i = 0; i < screenheight; i++)

            {

                Console.SetCursorPosition(0, i);

                Console.Write("■");

            }

            for (int i = 0; i < screenheight; i++)

            {

                Console.SetCursorPosition(screenwidth - 1, i);

                Console.Write("■");

            }

            Console.ForegroundColor =  ConsoleColor.Green;

            Console.WriteLine("Score: " + score);

            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 2; i < teljePositie.Count; i += 2)

            {

                Console.SetCursorPosition(teljePositie[i], teljePositie[i + 1]);

                Console.Write("■");

            }

            //Draw Snake

            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);

            Console.Write("■");

            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);

            Console.Write("■");

            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);

            Console.Write("■");

            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);

            Console.Write("■");



            ConsoleKeyInfo info = Console.ReadKey();

            //Game Logic

            switch (info.Key)

            {

                case ConsoleKey.UpArrow:

                    movement = "UP";

                    break;

                case ConsoleKey.DownArrow:

                    movement = "DOWN";

                    break;

                case ConsoleKey.LeftArrow:

                    movement = "LEFT";

                    break;

                case ConsoleKey.RightArrow:

                    movement = "RIGHT";

                    break;

            }

            if (movement == "UP")

                hoofd.yPos--;

            if (movement == "DOWN")

                hoofd.yPos++;

            if (movement == "LEFT")

                hoofd.xPos--;

            if (movement == "RIGHT")

                hoofd.xPos++;

            //Hindernis treffen

            bool tailExtended = false;
            if (hoofd.xPos == obstacleXpos && hoofd.yPos == obstacleYpos)

            {

                score++;

                PlaceObstacle();

                tailExtended = true;

            }

            teljePositie.Insert(0, hoofd.xPos);

            teljePositie.Insert(1, hoofd.yPos);

            if (!tailExtended)
            {
                teljePositie.RemoveAt(teljePositie.Count - 1);
                teljePositie.RemoveAt(teljePositie.Count - 1);
            }

            //Kollision mit Wände oder mit sich selbst

            if (hoofd.xPos == 0 || hoofd.xPos == screenwidth - 1 || hoofd.yPos == 0 || hoofd.yPos == screenheight - 1)

            {

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Red;

                Console.SetCursorPosition(screenwidth / 5, screenheight / 2);

                Console.WriteLine("Game Over");

                Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);

                Console.WriteLine("Dein Score ist: " + score);

                Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 2);

                Environment.Exit(0);

            }

            for (int i = 2; i < teljePositie.Count; i += 2)

            {

                if (hoofd.xPos == teljePositie[i] && hoofd.yPos == teljePositie[i + 1])

                {

                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2);

                    Console.WriteLine("Game Over");

                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);

                    Console.WriteLine("Dein Score ist: " + score);

                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 2);

                    Environment.Exit(0);

                }

            }

            Thread.Sleep(50);

        }

    }

}




