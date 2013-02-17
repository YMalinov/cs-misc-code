using System;
using System.Threading;

class FallingRocks
{
    struct rockValues
    {
        public int X;
        public int Y;
        public char rockSymbol;
        public ConsoleColor rockColor;
        public rockValues(int x, int y, char rockSymbol, ConsoleColor rockColor)
        {
            this.X = x;
            this.Y = y;
            this.rockSymbol = rockSymbol;
            this.rockColor = rockColor;
        }
    }

    static void Main()
    {
        //console settings
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;
        Console.CursorVisible = false;

        //a rather labourious way to center the text in the console...
        string text = "";

        text = "FALLING ROCKS";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, 0);
        Console.WriteLine(text);

        text = "Press 1 for easy difficulty";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) - 2);
        Console.WriteLine(text);

        text = "Press 2 for medium difficulty";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) - 1);
        Console.WriteLine(text);

        text = "Press 3 for hard difficulty";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2);
        Console.WriteLine(text);

        text = "Press 4 for INSANE difficulty";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 + 1);
        Console.WriteLine(text);

        text = "Press E to exit";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) + 2);
        Console.WriteLine(text);

        text = "The game will pause on Esc or Spacebar";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) + 4);
        Console.WriteLine(text);

        string difficulty = "";

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                if ((pressedKey.Key == ConsoleKey.NumPad1) || (pressedKey.Key == ConsoleKey.D1))
                {
                    difficulty = "easy";
                    break;
                }
                else if ((pressedKey.Key == ConsoleKey.NumPad2) || (pressedKey.Key == ConsoleKey.D2))
                {
                    difficulty = "medium";
                    break;
                }
                else if ((pressedKey.Key == ConsoleKey.NumPad3) || (pressedKey.Key == ConsoleKey.D3))
                {
                    difficulty = "hard";
                    break;
                }
                else if ((pressedKey.Key == ConsoleKey.NumPad4) || (pressedKey.Key == ConsoleKey.D4))
                {
                    difficulty = "INSANE";
                    break;
                }
                else if ((pressedKey.Key == ConsoleKey.E))
                {
                    Console.Clear();
                    return;
                }
            }
        }

        //checking the difficulty and assigning the appropriate values
        int sleepTime = 0;
        int rockCount = 0;
        if (difficulty == "easy")
        {
            sleepTime = 150;
            rockCount = 30;
        }
        else if (difficulty == "medium")
        {
            sleepTime = 125;
            rockCount = 45;
        }
        else if (difficulty == "hard")
        {
            sleepTime = 100;
            rockCount = 60;
        }
        else if (difficulty == "INSANE")
        {
            sleepTime = 30;
            rockCount = 100;
        }

        //assigning some other stuff
        char[] allowedRocks = new char[10] { '?', '!', '&', '+', '=', '~', '$', '^', '#', '%' }; //rock symbols, you can change them
        Random Randomizer = new Random(); //used for generating random values for the rocks
        int dwarfX = Console.BufferWidth / 2; //width / 2, because we want the dwarf to start at the center
        string dwarf = "(0)"; //you can use any dwarf you like, as long as it's 3 characters in length
        rockValues[] rocks = new rockValues[rockCount];

        //generating random values for the rocks
        for (int i = 0; i < rockCount; i++)
        {
            rocks[i].X = Randomizer.Next(1, (Console.BufferWidth - 1));
            rocks[i].Y = Randomizer.Next(1, (Console.BufferHeight - 5));
            rocks[i].rockColor = (ConsoleColor)Randomizer.Next(1, 16);
            rocks[i].rockSymbol = allowedRocks[Randomizer.Next(0, allowedRocks.Length)];
        }

        //scoring system (will add +1/+5/+10/+20 on every iteration of the loop, based on the difficulty)
        int gameScore = 0;

        //used to make the game faster with with every 15th iteration of the cycle
        int harder = 0;

        //running the game
        while (true)
        {
            Console.Clear();

            //read keys (we are interested only in the left and right arrows)
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();

                //moving the dwarf - X++ means right, X-- means left
                if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    dwarfX++;
                }
                else if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    dwarfX--;
                }
                else if ((pressedKey.Key == ConsoleKey.Spacebar) || (pressedKey.Key == ConsoleKey.Escape))
                {
                    //pausing...
                    Console.ForegroundColor = (ConsoleColor)Randomizer.Next(10, 16);

                    text = "PAUSED";
                    Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2);
                    Console.WriteLine(text);

                    text = "Press Enter to continue...";
                    Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) + 1);
                    Console.WriteLine(text);

                    Console.ReadLine();
                }
            }

            //making sure the dwarf won't fall off, causing an exception
            if (dwarfX == Console.BufferWidth - dwarf.Length)
            {
                dwarfX--;

            }
            else if (dwarfX == 0)
            {
                dwarfX++;
            }

            //write the new dwarf
            Console.SetCursorPosition(dwarfX, Console.BufferHeight - 1);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(dwarf);

            //write the new rocks
            for (int i = 0; i < rocks.Length; i++)
            {
                if (rocks[i].Y == (Console.BufferHeight - 1))
                {
                    rocks[i].X = Randomizer.Next(0, Console.BufferWidth - 1);
                    rocks[i].Y = 0;
                    rocks[i].rockColor = (ConsoleColor)Randomizer.Next(1, 16); ;
                }
                else
                {
                    Console.SetCursorPosition(rocks[i].X, rocks[i].Y);
                    rocks[i].Y++;
                }
                Console.SetCursorPosition(rocks[i].X, rocks[i].Y);
                Console.ForegroundColor = rocks[i].rockColor;
                Console.Write(rocks[i].rockSymbol);

                //checking if there are any rock collisions with the dwarf
                if (((rocks[i].X == dwarfX) || (rocks[i].X == dwarfX + 1) || (rocks[i].X == dwarfX + 2)) && rocks[i].Y == Console.BufferHeight - 1)
                {
                    Console.Clear();
                    for (int l = 0; l < rocks.Length; l++)
                    {
                        Console.SetCursorPosition(rocks[l].X, rocks[l].Y);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(rocks[l].rockSymbol);
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    text = "GAME OVER!";
                    Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) - 1);
                    Console.WriteLine(text);

                    text = "Your score is " + gameScore + " on " + difficulty + " difficulty.";
                    Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) + 1);
                    Console.WriteLine(text);

                    text = "Press Enter to exit";
                    Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight - 1);
                    Console.WriteLine(text);

                    Console.SetCursorPosition(0, Console.BufferHeight - 1);
                    Console.ReadLine();
                    return;
                }
            }

            //giving the appropriate score, based on the game difficulty
            if (difficulty == "easy")
            {
                gameScore += 1;
            }
            else if (difficulty == "medium")
            {
                gameScore += 3;
            }
            else if (difficulty == "hard")
            {
                gameScore += 7;
            }
            else if (difficulty == "INSANE")
            {
                gameScore += 10;
            }

            //showing the score on the screen
            text = "Score: " + gameScore;
            Console.SetCursorPosition(Console.BufferWidth - text.Length, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);

            //making the game harder (faster)
            harder += 1;
            if (harder % 11 == 0)
            {
                sleepTime -= 1;
            }

            Thread.Sleep(sleepTime);
        }
    }
}