using System;
using System.Threading;

class CarGame //an experiment, based on my "Falling Rocks" code, only here I don't use Console.Clear() on every iteration of the while (true) cycle.
{
    struct cars
    {
        public int X;
        public int Y;
        public char symbol;
        public ConsoleColor color;
        public cars(int X, int Y, char symbol, ConsoleColor color)
        {
            this.X = X;
            this.Y = Y;
            this.symbol = symbol;
            this.color = color;
        }
    }

    static void Main()
    {
        //console settings
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;
        Console.CursorVisible = false;
        
        //used for centering the text
        string text;

        text = "CAR GAME";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, 0);
        Console.WriteLine(text);

        text = "Press 1 for seven lanes (easiest)";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 - 2);
        Console.WriteLine(text);

        text = "Press 2 for five lanes (harder)";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 - 1);
        Console.WriteLine(text);

        text = "Press 3 for three lanes (hardest)";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2);
        Console.WriteLine(text);

        text = "Press E to exit";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 + 1);
        Console.WriteLine(text);

        text = "The game will pause on Spacebar";
        Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 + 3);
        Console.WriteLine(text);

        string lanes;
        string difficulty;
        int leftmostLane;
        int rightmostLane;

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                if ((pressedKey.Key == ConsoleKey.NumPad1) || (pressedKey.Key == ConsoleKey.D1))
                {
                    lanes = "I       I";
                    leftmostLane = Console.BufferWidth / 2 - 3;
                    rightmostLane = leftmostLane + (lanes.Length - 2);
                    difficulty = "seven lanes";
                    break;
                }
                else if ((pressedKey.Key == ConsoleKey.NumPad2) || (pressedKey.Key == ConsoleKey.D2))
                {
                    lanes = "I     I";
                    leftmostLane = Console.BufferWidth / 2 - 2;
                    rightmostLane = leftmostLane + (lanes.Length - 2);
                    difficulty = "five lanes";
                    break;
                }
                else if ((pressedKey.Key == ConsoleKey.NumPad3) || (pressedKey.Key == ConsoleKey.D3))
                {
                    lanes = "I   I";
                    leftmostLane = Console.BufferWidth / 2 - 1;
                    rightmostLane = leftmostLane + (lanes.Length - 2);
                    difficulty = "three lanes";
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

        //how many other (lethal) cars will there be
        int carsCount = Console.BufferHeight / 2 + 2;

        //lower acceleration means faster gameplay
        int harder = 1;
        int sleepTime = 200;

        //gameplay stuff
        int lives = 5;
        int maxLives = lives;
        int gameScore = 0;
        bool carCrash = false;

        //the density variable decides how sparse/abundant will the other cars be (buggy, change at your own risk)
        int density = 2;

        //initiliazing the player's car and adding some info about it
        cars playerCar = new cars();
        playerCar.symbol = '#';
        playerCar.color = ConsoleColor.Yellow;
        playerCar.X = Console.BufferWidth / 2;
        playerCar.Y = Console.BufferHeight - 1;

        //initializing the other cars array and setting some initial random values
        cars[] otherCars = new cars[carsCount];
        for (int i = 0, j = 0; i < carsCount; i++, j -= density)
        {
            otherCars[i].X = Randomizer.Next(leftmostLane, rightmostLane);
            otherCars[i].Y = j;
            otherCars[i].symbol = '&';
        }

        //initializing the bonus life bonus
        cars bonusLife = new cars();
        bonusLife.color = ConsoleColor.Green;
        bonusLife.symbol = '+';

        //initializing the slower speed bonus
        cars bonusSpeed = new cars();
        bonusSpeed.color = ConsoleColor.Red;
        bonusSpeed.symbol = '*';

        //initializing the clear screen bonus (bonusClear.color is missing, because it'll generate a new color on every iteration of the cycle)
        cars bonusClear = new cars();
        bonusClear.symbol = '@';

        Console.Clear();

        while (true)
        {
            //writing the road
            for (int i = 0; i < Console.BufferHeight; i++)
            {
                Console.SetCursorPosition(Console.BufferWidth / 2 - lanes.Length / 2, i);
                Console.Write(lanes);
            }

            //writing the other cars
            for (int i = 0, j = 0; i < otherCars.Length; i++)
            {
                if (otherCars[i].Y == Console.BufferHeight)
                {
                    j -= density;
                    otherCars[i].Y = j;
                    otherCars[i].X = Randomizer.Next(leftmostLane, rightmostLane);
                }
                else if (otherCars[i].Y >= 0)
                {
                    Console.SetCursorPosition(otherCars[i].X, otherCars[i].Y);
                    Console.Write(otherCars[i].symbol);
                    otherCars[i].Y++;
                }
                else if (otherCars[i].Y < 0)
                {
                    otherCars[i].Y++;
                }
            }

            //choose when to give the clear screen bonus
            if (Randomizer.Next(0, 200) == 23 && bonusClear.Y <= 0)
            {
                bonusClear.Y = 1;
                bonusClear.X = Randomizer.Next(leftmostLane, rightmostLane);
            }
            else if (bonusClear.Y == Console.BufferHeight)
            {
                bonusClear.Y = -1;
            }
            else if (bonusClear.Y > 0)
            {
                Console.SetCursorPosition(bonusClear.X, bonusClear.Y);
                Console.ForegroundColor = (ConsoleColor)Randomizer.Next(10, 16);
                Console.Write(bonusClear.symbol);
                bonusClear.Y++;
            }

            //choose when to give the +1 life bonus
            if (Randomizer.Next(0, 150) == 23 && bonusLife.Y <= 0)
            {
                bonusLife.Y = 1;
                bonusLife.X = Randomizer.Next(leftmostLane, rightmostLane);
            }
            else if (bonusLife.Y == Console.BufferHeight)
            {
                bonusLife.Y = -1;
            }
            else if (bonusLife.Y > 0)
            {
                Console.SetCursorPosition(bonusLife.X, bonusLife.Y);
                Console.ForegroundColor = bonusLife.color;
                Console.Write(bonusLife.symbol);
                bonusLife.Y++;
            }

            //choose when to give the slower game bonus
            if (Randomizer.Next(0, 100) == 23 && bonusSpeed.Y <= 0)
            {
                bonusSpeed.Y = 1;
                bonusSpeed.X = Randomizer.Next(leftmostLane, rightmostLane);
            }
            else if (bonusSpeed.Y == Console.BufferHeight)
            {
                bonusSpeed.Y = -1;
            }
            else if (bonusSpeed.Y > 0)
            {
                Console.SetCursorPosition(bonusSpeed.X, bonusSpeed.Y);
                Console.ForegroundColor = bonusSpeed.color;
                Console.Write(bonusSpeed.symbol);
                bonusSpeed.Y++;
            }

            //writing the player's car
            Console.SetCursorPosition(playerCar.X, playerCar.Y);
            if (carCrash)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("@");
                carCrash = false;
            }
            else
            {
                Console.ForegroundColor = playerCar.color;
                Console.Write(playerCar.symbol);
            }
            Console.ForegroundColor = ConsoleColor.White;

            //fixing the buggy road
            Console.SetCursorPosition(rightmostLane, Console.BufferHeight - 1);
            Console.Write("I");

            //moving the car
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    Console.SetCursorPosition(playerCar.X, playerCar.Y);
                    Console.Write(" ");
                    playerCar.X++;
                }
                else if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    Console.SetCursorPosition(playerCar.X, playerCar.Y);
                    Console.Write(" ");
                    playerCar.X--;
                }
                else if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(playerCar.X, playerCar.Y);
                    Console.Write(" ");
                    playerCar.Y--;
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(playerCar.X, playerCar.Y);
                    Console.Write(" ");
                    playerCar.Y++;
                }
                else if (pressedKey.Key == ConsoleKey.Spacebar)
                {
                    text = "PAUSED, press Enter to continue.";
                    Console.SetCursorPosition(Console.BufferWidth - text.Length, Console.BufferHeight / 2);
                    Console.Write(text);
                    Console.ReadLine();
                    text = "                                ";
                    Console.SetCursorPosition(Console.BufferWidth - text.Length, Console.BufferHeight / 2);
                    Console.Write(text);
                }

                //making sure the player's car won't go off the road
                if (playerCar.X == leftmostLane - 1)
                {
                    playerCar.X++;
                }
                else if (playerCar.X == rightmostLane)
                {
                    playerCar.X--;
                }

                if (playerCar.Y == Console.BufferHeight)
                {
                    playerCar.Y--;
                }
                else if (playerCar.Y == -1)
                {
                    playerCar.Y++;
                }
            }

            //checking for collisions
            for (int i = 0; i < otherCars.Length; i++)
            {
                if (otherCars[i].Y == playerCar.Y && otherCars[i].X == playerCar.X)
                {
                    carCrash = true;
                    lives--;
                }
            }
            if (bonusSpeed.X == playerCar.X && bonusSpeed.Y == playerCar.Y)
            {
                sleepTime += 20;
                gameScore += 5;
                bonusSpeed.Y = -1;
            }
            if (bonusLife.X == playerCar.X && bonusLife.Y == playerCar.Y)
            {
                lives++;
                gameScore += 5;
                bonusLife.Y = -1;
            }
            if (bonusClear.X == playerCar.X && bonusClear.Y == playerCar.Y)
            {
                for (int i = 0, j = 0; i < carsCount; i++, j -= density)
                {
                    otherCars[i].Y = j;
                }
                bonusClear.Y = -1;
                gameScore += 5;
            }

            //ending the game
            if (lives == 0)
            {
                Console.Clear();

                text = "GAME OVER!";
                Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 - 2);
                Console.Write(text);

                text = "Score: " + gameScore;
                Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 - 1);
                Console.Write(text);

                text = "Maximum lives: " + maxLives;
                Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2);
                Console.Write(text);

                text = "Playing with " + difficulty + ".";
                Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight / 2 + 1);
                Console.Write(text);

                text = "Press Enter to continue...";
                Console.SetCursorPosition(Console.BufferWidth / 2 - text.Length / 2, Console.BufferHeight - 1);
                Console.Write(text);

                Console.ReadLine();
                return;
            }

            //writing the remaining lives
            Console.SetCursorPosition(0, 0);
            Console.Write("Lives: ");
            if (lives == 1)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write(lives);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.Write(lives);
            }

            //counter with the maximum lives
            if (maxLives < lives)
            {
                maxLives = lives;
            }

            //writing the game score
            text = "Score: " + gameScore;
            Console.SetCursorPosition(Console.BufferWidth - text.Length, 0);
            Console.Write(text);

            //writing the game speed
            Console.SetCursorPosition(0, Console.BufferHeight - 1);
            Console.Write("Speed: " + sleepTime + " (lower means faster)");

            //making the game harder (faster)
            if (harder % 10 == 0)
            {
                sleepTime--;
                gameScore += 1;
            }
            harder++;
            Thread.Sleep(sleepTime);
        }
    }
}