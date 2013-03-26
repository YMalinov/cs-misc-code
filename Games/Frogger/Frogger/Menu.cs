using System;
using System.IO;
using System.Threading;

namespace Frogger
{
    static class Menu
    {
        private static string text = ""; //a rather labourious way to center the text in the console

        public static void WriteMenu(int sleepTimeForLogo = 2000)
        {
            //Shows the logo
            Console.Title = "JustFrogger";
            WriteLogo();
            Thread.Sleep(sleepTimeForLogo);
            
            //Clears the logo and goes to the game.
            Console.Clear();
            Console.WindowWidth = 80;
            Console.WindowHeight = 20;

            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            
            //write rest of stuff, ex. authors
            //probably a good idea to include a legend of some sort for all of the objects

            text = "Frogger";
            Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, 5);
            Console.Write(text);

            text = "Press 1 for Easy mode (5 lanes)";
            Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2 - 2);
            Console.Write(text);

            text = "Press 2 for Medium mode (13 lanes)";
            Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2 - 1);
            Console.Write(text);

            text = "Press 3 for Hard mode (23 lanes)";
            Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2);
            Console.Write(text);

            text = "Press C to enter a custom number of lanes";
            Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2 + 1);
            Console.Write(text);

            text = "Press E to exit";
            Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2 + 3);
            Console.Write(text);

            int level = 0;
            bool continueLoop = true;
            while (continueLoop)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true); //Console.ReadKey(true) won't output the pressed key
                    switch (pressedKey.Key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            {
                                level = 1;
                                continueLoop = false;
                                break;
                            }
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            {
                                level = 2;
                                continueLoop = false;
                                break;
                            }
                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            {
                                level = 3;
                                continueLoop = false;
                                break;
                            }
                        case ConsoleKey.C:
                            {
                                CustomLevelScreen();
                                continueLoop = false;
                                break;
                            }
                        case ConsoleKey.E:
                            {
                                Environment.Exit(0);
                                break; //a rudimentary, unreachable break. Visual Studio won't compile without it :)
                            }
                    }
                }
            }

            InitializeGame(level);
        }

        private static void CustomLevelScreen()
        {
            Console.Clear();

            text = "Enter how many lanes should the game have: ";
            Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2);
            Console.Write(text);

            try
            {
                int lanes = Converter.LanesToHeight(int.Parse(Console.ReadLine()));

                if (1 >= lanes || lanes >= Console.LargestWindowHeight)
                {
                    throw new InvalidNumberOfLanesException();
                }

                Level.DrawLevel(lanes, true);
            }
            catch (InvalidNumberOfLanesException)
            {
                Console.WriteLine(InvalidNumberOfLanesException.ErrorMessage);
                Console.WriteLine("Press Enter to try again.");
                Console.ReadLine();
            }
        }

        private static void InitializeGame(int level)
        {
            Console.Clear();
            switch (level)
            {
                case 1:
                    {
                        Level.DrawLevel(5); //DrawLevel(number of desired lanes)
                        break;
                    }
                case 2:
                    {
                        Level.DrawLevel(13);
                        break;
                    }
                case 3:
                    {
                        Level.DrawLevel(23);
                        break;
                    }
            }
        }

        public static void WriteLogo()
        {
            Console.CursorVisible = false;

            try
            {
                string logoAddress = "";

                if (File.Exists("../../logo.txt"))
                {
                    logoAddress = "../../logo.txt";
                }
                else if (File.Exists("logo.txt"))
                {
                    logoAddress = "logo.txt";
                }
                else
                {
                    throw new FileNotFoundException();
                }

                using (StreamReader reader = new StreamReader(logoAddress))
                {
                    string currentLine = reader.ReadLine();
                    while (currentLine != null)
                    {
                        Console.WindowHeight = 23;
                        Console.WindowWidth = currentLine.Length;

                        Console.BufferHeight = Console.WindowHeight;
                        Console.BufferWidth = Console.WindowWidth;

                        Console.Write(currentLine);
                        currentLine = reader.ReadLine();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Logo not found! Continuing to the menu...");
            }
        }
    }
}