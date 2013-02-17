using System;
using System.Threading;

class FallingRocks
{
    static string dwarf = "(0000)"; //the game will work with any dwarf
    static ConsoleColor dwarfColor = ConsoleColor.Cyan; //you can change the color of the dwarf
    static int startLifes = dwarf.Length; //you can change how many lifes does the dwarf have on game start
    static int highScore = 0;

    static Random Randomizer = new Random(); //used for generating random values for the rocks, ignore it
    static string text = ""; //used to center the text, ignore it
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

        //running the menu and getting the desired difficulty
        object[] difficulty = GetLevel();
        int sleepTime = int.Parse(difficulty[0].ToString());
        int rockCount = int.Parse(difficulty[1].ToString());
        int scoreAdder = int.Parse(difficulty[2].ToString());

        //assigning some other stuff
        char[] allowedRocks = { '?', '!', '&', '+', '=', '~', '$', '^', '#', '%' }; //rock symbols, you can change them
        int dwarfX = Console.BufferWidth / 2 - dwarf.Length / 2; //width / 2, because we want the dwarf to start at the center
        rockValues bonusRock = new rockValues();
        bonusRock.rockSymbol = '@';
        bonusRock.Y = -1;
        int gameScore = 0; //scoring system - duh!
        int harder = 0; //will make the game harder (faster) on every 15th interation of the cycle
        int lifes = startLifes;

        //initiating and generating initial random values for the rocks
        rockValues[] rocks = new rockValues[rockCount];
        for (int i = 0; i < rockCount; i++)
        {
            rocks[i].X = Randomizer.Next(1, (Console.BufferWidth - 1));
            rocks[i].Y = Randomizer.Next(1, (Console.BufferHeight - 5));
            do
            {
                rocks[i].rockColor = (ConsoleColor)Randomizer.Next(1, 16);
            } while (rocks[i].rockColor == dwarfColor);

            rocks[i].rockSymbol = allowedRocks[Randomizer.Next(0, allowedRocks.Length)];
        }

        //running the game
        while (true)
        {
            Console.Clear();

            //showing the score on the screen
            text = "Score: " + gameScore;
            Console.SetCursorPosition(Console.BufferWidth - text.Length, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);

            //showing the remaining lifes on screen
            text = "Lifes: " + lifes;
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);

            //read keys (we are only interested in the left and right arrows)
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
                    Pause();
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
            WriteDwarf(dwarfX);

            //write the new rocks
            for (int i = 0; i < rocks.Length; i++)
            {
                if (rocks[i].Y == (Console.BufferHeight - 1))
                {
                    rocks[i].X = Randomizer.Next(0, Console.BufferWidth - 1);
                    rocks[i].Y = 1;

                    do
                    {
                        rocks[i].rockColor = (ConsoleColor)Randomizer.Next(1, 16);
                    } while (rocks[i].rockColor == dwarfColor);
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
                bool areThereRockCollisions = false;
                for (int l = dwarfX; l < dwarfX + dwarf.Length; l++)
                {
                    if (rocks[i].X == l)
                    {
                        areThereRockCollisions = true;
                        break;
                    }
                }

                if (areThereRockCollisions && (rocks[i].Y == Console.BufferHeight - 1))
                {
                    lifes--;
                    WriteDwarf(dwarfX, true); //if the dwarf is hit by a rock, display the appropriate animation

                    if (lifes == 0)
                    {
                        GameOver(difficulty[3].ToString(), rocks, gameScore);
                        return;
                    }
                }
            }

            //write the bonus
            if (Randomizer.Next(0, 24) == 23 && bonusRock.Y == -1)
            {
                bonusRock.Y = 1;
                bonusRock.X = Randomizer.Next(0, Console.BufferWidth - 1);
            }
            else if (bonusRock.Y == Console.BufferHeight)
            {
                bonusRock.Y = -1;
            }
            else if (bonusRock.Y > 0)
            {
                Console.SetCursorPosition(bonusRock.X, bonusRock.Y);
                Console.ForegroundColor = (ConsoleColor)Randomizer.Next(1, 16);
                Console.Write(bonusRock.rockSymbol);
                bonusRock.Y++;
            }

            //if the dwarf is hit by a bonus, display the appropriate animation
            if (bonusRock.Y - 1 == Console.BufferHeight - 1)
            {
                for (int l = dwarfX; l < dwarfX + dwarf.Length; l++)
                {
                    if (bonusRock.X == l)
                    {
                        WriteDwarf(dwarfX, false, true);
                        lifes++;
                    }
                }
            }

            //giving the appropriate score, based on the game difficulty
            gameScore += scoreAdder;

            //making the game harder (faster)
            harder += 1;
            if (harder % 11 == 0)
            {
                sleepTime -= 1;
            }

            Thread.Sleep(sleepTime);
        }
    }

    static object[] GetLevel()
    {
        Console.Clear();
        Console.ResetColor();

        text = "FALLING ROCKS"; //a rather labourious way to center the text in a console window
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, 0);
        Console.Write(text);

        text = "Press 1 for easy difficulty";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) - 2);
        Console.Write(text);

        text = "Press 2 for medium difficulty";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) - 1);
        Console.Write(text);

        text = "Press 3 for hard difficulty";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2);
        Console.Write(text);

        text = "Press 4 for INSANE difficulty";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 + 1);
        Console.Write(text);

        text = "Press E to exit";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) + 2);
        Console.Write(text);

        text = "The game will pause on Esc or Spacebar";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) + 4);
        Console.Write(text);

        text = "Current high score: " + highScore;
        Console.SetCursorPosition(0, Console.BufferHeight - 1);
        Console.Write(text);

        while (true) //wait for an input and then assign the appropriate game settings
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true); //this overload (true) doesn't display the entered character
                if ((pressedKey.Key == ConsoleKey.NumPad1) || (pressedKey.Key == ConsoleKey.D1))
                {
                    return new object[] { 150, 30, 1, "easy" }; //{sleepTime, rockCount, scoreAdder, difficulty}
                }
                else if ((pressedKey.Key == ConsoleKey.NumPad2) || (pressedKey.Key == ConsoleKey.D2))
                {
                    return new object[] { 125, 45, 3, "medium" };
                }
                else if ((pressedKey.Key == ConsoleKey.NumPad3) || (pressedKey.Key == ConsoleKey.D3))
                {
                    return new object[] { 100, 60, 7, "hard" };
                }
                else if ((pressedKey.Key == ConsoleKey.NumPad4) || (pressedKey.Key == ConsoleKey.D4))
                {
                    return new object[] { 30, 100, 10, "INSANE" };
                }
                else if ((pressedKey.Key == ConsoleKey.E))
                {
                    Environment.Exit(0);
                }
            }
        }
    }

    private static void GameOver(string difficulty, rockValues[] rocks, int gameScore)
    {
        for (int l = 0; l < rocks.Length; l++) //write the rocks in dark gray
        {
            Console.SetCursorPosition(rocks[l].X, rocks[l].Y);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(rocks[l].rockSymbol);
        }

        text = "GAME OVER!";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) - 1);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);

        if (gameScore > highScore)
        {
            highScore = gameScore;
            text = "Your score is " + gameScore + " on " + difficulty + " difficulty (new high score!).";
        }
        else
        {
            text = "Your score is " + gameScore + " on " + difficulty + " difficulty.";
        }
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) + 1);
        Console.Write(text);

        text = "Press Enter to return to menu";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight - 1);
        Console.Write(text);

        Console.SetCursorPosition(0, Console.BufferHeight - 1);
        Console.ReadLine();
        Main(); //I know this can be a very bad idea (calling Main() recursively),
        //but I haven't found a better simple way to restart the game
    }

    static void Pause()
    {
        Console.ForegroundColor = (ConsoleColor)Randomizer.Next(10, 16); //choose a random text color

        text = "PAUSED";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2);
        Console.WriteLine(text);

        text = "Press Enter to continue...";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, (Console.BufferHeight / 2) + 1);
        Console.WriteLine(text);

        Console.ReadLine();
    }

    static void WriteDwarf(int dwarfX, bool hitByRock = false, bool hitByBonus = false)
    {
        if (hitByRock)
        {
            Console.SetCursorPosition(dwarfX, Console.BufferHeight - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(new string('#', dwarf.Length));
        }
        else if (hitByBonus)
        {
            Console.SetCursorPosition(dwarfX, Console.BufferHeight - 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(new string('+', dwarf.Length));
        }
        else
        {
            Console.SetCursorPosition(dwarfX, Console.BufferHeight - 1);
            Console.ForegroundColor = dwarfColor;
            Console.Write(dwarf);
        }
    }
}