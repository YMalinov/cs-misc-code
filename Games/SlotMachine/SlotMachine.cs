using System;

class SlotMachine
{
    static void Main()
    {
        //some console settings
        Console.BufferHeight = Console.WindowHeight = 40;
        Console.BufferWidth = Console.WindowWidth = 110;
        Console.CursorVisible = false;

        //used to center text in the console
        string text = "";

        text = "SLOT MACHINE";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, 0);
        Console.Write(text);

        text = "Press 1 for easy difficulty";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 - 2);
        Console.Write(text);

        text = "Press 2 for medium difficulty";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 - 1);
        Console.Write(text);

        text = "Press 3 for hard difficulty";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2);
        Console.Write(text);

        text = "Press 4 for INSANE difficulty";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 + 1);
        Console.Write(text);

        text = "Press E to exit";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 + 3);
        Console.Write(text);

        int difficulty = 0;

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                if (pressedKey.Key == ConsoleKey.D1 || pressedKey.Key == ConsoleKey.NumPad1)
                {
                    difficulty = 3;
                    break;
                }
                else if (pressedKey.Key == ConsoleKey.D2 || pressedKey.Key == ConsoleKey.NumPad2)
                {
                    difficulty = 5;
                    break;
                }
                else if (pressedKey.Key == ConsoleKey.D3 || pressedKey.Key == ConsoleKey.NumPad3)
                {

                    difficulty = 10;
                    break;
                }
                else if (pressedKey.Key == ConsoleKey.D4 || pressedKey.Key == ConsoleKey.NumPad4)
                {
                    difficulty = 12;
                    break;
                }
                else if (pressedKey.Key == ConsoleKey.E)
                {
                    return;
                }
            }
        }

        string difficultyText = "";

        //the difficulty is determined by the characters in availableChars[]
        char[] availableChars = new char[difficulty];
        if (difficulty == 3)
        {
            availableChars = new char[3] { '^', '#', '%' };
            difficultyText = "easy";
        }
        else if (difficulty == 5)
        {
            availableChars = new char[5] { '?', '!', '&', '+', '=' };
            difficultyText = "medium";
        }
        else if (difficulty == 10)
        {
            availableChars = new char[10] { '?', '!', '&', '+', '=', '~', '$', '^', '#', '%' };
            difficultyText = "hard";
        }
        else if (difficulty == 12)
        {
            availableChars = new char[12] { '?', '!', '&', '+', '=', '~', '$', '^', '#', '%', '@', '\\' };
            difficultyText = "INSANE";
        }

        //initializing the three lines
        char[] firstSlot = new char[Console.BufferHeight];
        char[] secondSlot = new char[Console.BufferHeight];
        char[] thirdSlot = new char[Console.BufferHeight];

        //Y coordinates for every slot line
        int lineY = 0;

        //stops the cycle on keypress
        bool stopLoop = false;

        //scoring system
        int gameScore = 0;

        //subtracts score if the player doesn't stop the slot machine for a while
        int subtractScore = 0;

        //used for generating new characters for the three slots and the jackpot
        Random Randomizer = new Random();

        //generates the initial value for the jackpot
        char jackpot = availableChars[Randomizer.Next(0, availableChars.Length)];

        //counters
        int jackpotCnt = 0;
        int threeCnt = 0;
        int twoCnt = 0;

        //clears the console to prepare it for the next stage
        Console.Clear();

        //text in the upper left corner
        text = "Playing on " + difficultyText + " difficulty.";
        Console.SetCursorPosition(0, 0);
        Console.Write(text);

        text = "Press Enter to stop or E to exit...";
        Console.SetCursorPosition(0, 1);
        Console.Write(text);

        while (true)
        {
            //generate random values for the first slot
            for (int i = 0; i < firstSlot.Length; i++)
            {
                firstSlot[i] = availableChars[Randomizer.Next(0, availableChars.Length)];
            }

            //generate random values for the second slot
            for (int i = 0; i < secondSlot.Length; i++)
            {
                secondSlot[i] = availableChars[Randomizer.Next(0, availableChars.Length)];
            }

            //generate random values for the third slot
            for (int i = 0; i < thirdSlot.Length; i++)
            {
                thirdSlot[i] = availableChars[Randomizer.Next(0, availableChars.Length)];
            }

            //resetting the lineY, so we don't get an invalid console.setcursorpos error
            if (lineY == Console.BufferHeight)
            {
                lineY = 0;
            }
            else
            {
                //if lineY doesn't equal to zero, write the entire arrays vertically
                for (int i = 0; i < Console.BufferHeight; i++)
                {
                    if (lineY == Console.BufferHeight / 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.SetCursorPosition(Console.BufferWidth / 2 - 3, lineY);
                    Console.Write(firstSlot[i]);

                    Console.SetCursorPosition(Console.BufferWidth / 2, lineY);
                    Console.Write(secondSlot[i]);

                    Console.SetCursorPosition(Console.BufferWidth / 2 + 3, lineY);
                    Console.Write(thirdSlot[i]);

                    lineY++;
                }
            }

            if (Console.KeyAvailable || stopLoop)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                if (pressedKey.Key == ConsoleKey.Enter || stopLoop)
                {
                    stopLoop = true;
                    if (lineY == Console.BufferHeight / 2 || stopLoop)
                    {
                        //checks whether there are three of the same chars in the middle line
                        if ((firstSlot[Console.BufferHeight / 2] == jackpot) && 
                            (secondSlot[Console.BufferHeight / 2] == jackpot) && 
                            (thirdSlot[Console.BufferHeight / 2] == jackpot))
                        {
                            text = "      JACKPOT!      ";
                            Console.SetCursorPosition(Console.BufferWidth / 2 + (text.Length - 1), Console.BufferHeight / 2);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(text);
                            Console.ReadLine();
                            jackpot = availableChars[Randomizer.Next(0, availableChars.Length)];
                            stopLoop = false;
                            gameScore += 50;
                            jackpotCnt++;
                        }
                        else if ((firstSlot[Console.BufferHeight / 2] == secondSlot[Console.BufferHeight / 2]) && 
                                 (secondSlot[Console.BufferHeight / 2] == thirdSlot[Console.BufferHeight / 2]))
                        {
                            text = " Three of the same! ";
                            Console.SetCursorPosition(Console.BufferWidth / 2 + (text.Length - 1), Console.BufferHeight / 2);
                            Console.WriteLine(text);
                            Console.ReadLine();
                            stopLoop = false;
                            gameScore += 35;
                            threeCnt++;
                        }
                        //checks whether there are two of the same chars in the middle line
                        else if ((firstSlot[Console.BufferHeight / 2] == secondSlot[Console.BufferHeight / 2]) || 
                                 (firstSlot[Console.BufferHeight / 2] == thirdSlot[Console.BufferHeight / 2]) || 
                                 (secondSlot[Console.BufferHeight / 2] == thirdSlot[Console.BufferHeight / 2]))
                        {
                            text = "  Two of the same!  ";
                            Console.SetCursorPosition(Console.BufferWidth / 2 + (text.Length - 1), Console.BufferHeight / 2);
                            Console.WriteLine(text);
                            Console.ReadLine();
                            stopLoop = false;
                            gameScore += 25;
                            twoCnt++;
                        }
                        else
                        {
                            text = "     No matches.    ";
                            Console.SetCursorPosition(Console.BufferWidth / 2 + (text.Length - 1), Console.BufferHeight / 2);
                            Console.WriteLine(text);
                            Console.ReadLine();
                            stopLoop = false;
                            gameScore -= 15;
                        }
                        text = "                    ";
                        Console.SetCursorPosition(Console.BufferWidth / 2 + (text.Length - 1), Console.BufferHeight / 2);
                        Console.WriteLine(text);
                    }
                }
                else if (pressedKey.Key == ConsoleKey.E)
                {
                    Console.Clear();
                    text = "GAME OVER! Your score is: " + gameScore + ".";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2);
                    Console.Write(text);
                    Console.ReadLine();
                    return;
                }
            }

            //shows the score in the upper right corner
            text = "                   Score: " + gameScore;
            Console.SetCursorPosition(Console.BufferWidth - text.Length, 0);
            if (gameScore < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(text);

            //shows the current jackpot in the bottom left corner
            text = "Current jackpot: " + jackpot + " " + jackpot + " " + jackpot;
            Console.SetCursorPosition(0, Console.BufferHeight - 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);

            //shows playtime statistics
            text = "JACKPOTS:          " + jackpotCnt;
            Console.SetCursorPosition(0, Console.BufferHeight / 2 - 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);

            text = "Three of the same: " + threeCnt;
            Console.SetCursorPosition(0, Console.BufferHeight / 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);

            text = "Two of the same:   " + twoCnt;
            Console.SetCursorPosition(0, Console.BufferHeight / 2 + 2);
            Console.Write(text);

            //subtracts 1 from the score on every difficulty*15th iteration 
            //of the cycle. Won't work with INSANE mode, as it's hard enough
            if (subtractScore % (difficulty * 15) == 0 && difficulty != 12)
            {
                gameScore--;
            }
            subtractScore += 1;
        }
    }
}