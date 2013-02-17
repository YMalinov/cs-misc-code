using System;
using System.Collections.Generic;
using System.Threading;

class SimonGame
{
    static void Main()
    {
        //console settings
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.BufferWidth;
        Console.CursorVisible = false;

        //menu
        string text = "SIMON GAME";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, 0);
        Console.Write(text);

        text = "Press Enter to start the game from level 1";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2);
        Console.Write(text);

        text = "Press C to start from a different level";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 + 1);
        Console.Write(text);

        text = "Press E to exit";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight - 1);
        Console.Write(text);

        int level = -1;
        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                if ((pressedKey.Key == ConsoleKey.Enter))
                {
                    level = 0;
                    Console.Clear();
                    break;
                }
                else if (pressedKey.Key == ConsoleKey.C)
                {
                    while (level < 0)
                    {
                        Console.Clear();
                        text = "Enter a level (must be positive): ";
                        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2);
                        Console.Write(text);
                        level = int.Parse(Console.ReadLine());
                        level--;
                        Console.Clear();
                    }
                    break;
                }
                else if ((pressedKey.Key == ConsoleKey.E))
                {
                    Console.Clear();
                    return;
                }
            }
        }

        Random Randomizer = new Random();

        //initiating some gameplay variables
        int lives = 5;
        int gameScore = 0;
        
        //if set to true, there won't be a new member on the next iteration of the cycle
        bool dontAdd = false;

        List<int> currentCombination = new List<int>();
        List<int> inputCombination = new List<int>();

        //checking if a custom level has been chosen
        for (int i = 0; i < level; i++)
        {
            currentCombination.Add(Randomizer.Next(0, 4));
            if (i > 1)
            {
                while (currentCombination[i] == currentCombination[i - 1])
                {
                    currentCombination[i] = Randomizer.Next(0, 4);
                }
            }
        }

        //writing the buttons
        for (int l = 0; l < 4; l++)
        {
            Button(l, false);
        }

        for (int i = level; true; i++)
        {
            //writing the level
            text = "Level: " + (int)(i + 1);
            Console.SetCursorPosition(0, 0);
            Console.Write(text);

            //writing the score
            text = "Score: " + gameScore;
            Console.SetCursorPosition(Console.BufferWidth - text.Length, 0);
            Console.Write(text);

            //writing the remaining lives
            text = "Lives: " + lives;
            Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, 0);
            Console.Write(text);

            //checking if the player has entered the right combination
            if (!dontAdd)
            {
                currentCombination.Add(Randomizer.Next(0, 4));
            }
            else
            {
                dontAdd = false;
            }

            //checking if there are two consecutive buttons
            if (i > 0)
            {
                while (currentCombination[i] == currentCombination[i - 1])
                {
                    currentCombination[i] = Randomizer.Next(0, 4);
                }
            }

            //writing the randomly generated buttons
            for (int l = 0; l < currentCombination.Count; l++)
            {
                Button(currentCombination[l], true);
                Thread.Sleep(750);
                Button(currentCombination[l], false);
            }

            text = "Use A, S, D, F to choose the appropriate squares or R to try again... ";
            Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight - 2);
            Console.WriteLine(text);

            inputCombination.Clear();
            int tempCnt = 0;
            while (true)
            {
                text = "   Left: " + (int)(currentCombination.Count - tempCnt) + "   ";
                Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 + 5);
                Console.Write(text);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true); //console.readkey(true) doesn't echo the keyboard input on the console
                    switch (pressedKey.Key)
                    {
                        case ConsoleKey.A:
                        {
                            inputCombination.Add(0);
                            Button(0, true);
                            Thread.Sleep(300);
                            Button(0, false);
                            tempCnt++;
                            break;
                        }
                        case ConsoleKey.S:
                        {
                            inputCombination.Add(1);
                            Button(1, true);
                            Thread.Sleep(300);
                            Button(1, false);
                            tempCnt++;
                            break;
                        }
                        case ConsoleKey.D:
                        {
                            inputCombination.Add(2);
                            Button(2, true);
                            Thread.Sleep(300);
                            Button(2, false);
                            tempCnt++;
                            break;
                        }
                        case ConsoleKey.F:
                        {
                            inputCombination.Add(3);
                            Button(3, true);
                            Thread.Sleep(300);
                            Button(3, false);
                            tempCnt++;
                            break;
                        }
                        case ConsoleKey.R:
                        {
                            for (int l = 0; l < 4; l++)
                            {
                                Button(l, true);
                            }
                            Thread.Sleep(200);
                            for (int l = 0; l < 4; l++)
                            {
                                Button(l, false);
                            }
                            tempCnt = 0;
                            inputCombination.Clear();
                            break;
                        }
                        case ConsoleKey.E:
                        {
                            return;
                        }
                    }

                    if (tempCnt == currentCombination.Count)
                    {
                        break;
                    }
                }
            }

            Console.SetCursorPosition(0, Console.BufferHeight / 2 + 5);
            Console.Write("{0}", new string(' ', Console.BufferWidth - 1));

            for (int l = 0; l < currentCombination.Count; l++)
            {
                if (currentCombination[l] != inputCombination[l])
                {
                    lives--;

                    //game over
                    if (lives == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        text = "GAME OVER! Your score: " + gameScore;
                        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2);
                        Console.Write(text);
                        Console.ReadLine();
                        return;
                    }

                    Console.SetCursorPosition(0, Console.BufferHeight - 2);
                    Console.Write("{0}", new string(' ', Console.BufferWidth - 1));
                    text = "You've entered a wrong combination! Press Enter to try again...";
                    Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight - 2);
                    Console.Write(text);
                    Console.ReadLine();

                    i--;
                    dontAdd = true;
                    break;
                }
                
                if (l == currentCombination.Count - 1)
                {
                    gameScore += currentCombination.Count;
                }
            }
            Console.SetCursorPosition(0, Console.BufferHeight - 2);
            Console.Write("{0}", new string(' ', Console.BufferWidth - 1));

            Thread.Sleep(1000);
        }
    }

    static void Button(int i, bool chosen)
    {
        //i = which button to write, expected value between 0 and 3
        //chosen = false will write the button in white; chosen = true will write the button in green
        int left = 0;
        switch (i)
        {
            case 0:
            {
                left = Console.BufferWidth / 2 - 14;
                break;
            }
            case 1:
            {
                left = Console.BufferWidth / 2 - 6;
                break;
            }
            case 2:
            {
                left = Console.BufferWidth / 2 + 2;
                break;
            }
            case 3:
            {
                left = Console.BufferWidth / 2 + 10;
                break;
            }
            default:
            {
                Console.WriteLine("Wrong input");
                return;
            }
        }
        
        if (chosen)
        {
            switch (i)
            {
                case 0:
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                }
                case 1:
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                }
                case 2:
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                }
                case 3:
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                }
            }
        }

        for (int l = Console.BufferHeight / 2 - 2; l <= Console.BufferHeight / 2 + 2; l++)
        {
            Console.SetCursorPosition(left, l);
            Console.Write("{0}", new string('#', 5));
        }

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }
}