using System;

namespace Frogger
{
    static class Level
    {
        private static void DrawGrass()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine(" ".PadRight(Console.WindowWidth - 1));

            Console.ResetColor();
        }

        //Draws the levels.
        //heightBottom is used to draw the grass under the road, currently under the frog. 
        //TODO: Need to get the frog on the grass, probably has something to do with the Engine :) 
        //TODO2: When a key is pressed the bottom left corner changes to the key's background color and not green, neet to fix that.
        public static void DrawLevel(int numberOfLanes, bool inputHeight = false)
        {
            if (!inputHeight)
            {
                Console.WindowHeight = Converter.LanesToHeight(numberOfLanes);
            }
            else
            {
                Console.WindowHeight = numberOfLanes;
            }

            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;

            ConsoleRenderer renderer = new ConsoleRenderer(Console.WindowHeight - 2, Console.WindowWidth - 1); 
            //windowheight - 2 and windowwidth - 1 make a buffering zone for the renderer                                     
            //otherwise we get a terrible performance and I really can't really be bothered
            //to see why

            KeyboardInterface keyInterface = new KeyboardInterface();
            Engine engine = new Engine(renderer, keyInterface);

            Frog frog = new Frog(new Coordinates(Console.WindowHeight - 3, (Console.WindowWidth - 2) / 2));
            engine.AddObject(frog);

            for (int row = 0; row < Console.WindowHeight - 2; row += 2) //adding some road surface markings
            {
                for (int col = 0; col < Console.WindowWidth; col += 2)
                {
                    RoadMarkings markings = new RoadMarkings(new Coordinates(row, col));
                    engine.AddObject(markings);
                }
            }

            for (int row = Console.WindowHeight - 5; row > 0; row -= 2) //adding some cars/trucks
            {
                int speed = RandomGenerators.Speed();

                for (int col = 0; col < Console.WindowWidth - 1; col += 8)
                {
                    Vehicle vehicle = new Vehicle(new Coordinates(row, col), speed, RandomGenerators.TruckLength());
                    engine.AddObject(vehicle);
                }
            }

            engine.AddObject(new ScoreBonus(RandomGenerators.RandomPosition())); //adding a score bonus, which will be automatically regenerated on pickup

            keyInterface.OnDownPressed += (sender, eventInfo) =>
            {
                engine.MoveFrogDown();  
            };
            keyInterface.OnUpPressed += (sender, eventInfo) =>
            {
                engine.MoveFrogUp();
            };
            keyInterface.OnLeftPressed += (sender, eventInfo) =>
            {
                engine.MoveFrogLeft();
            };
            keyInterface.OnRightPressed += (sender, eventInfo) =>
            {
                engine.MoveFrogRight();
            };
            keyInterface.OnPausePressed += (sender, eventInfo) =>
            {
                engine.PauseScreen();
            };
            keyInterface.OnExitPressed += (sender, eventInfo) =>
            {
                Environment.Exit(0);
            };

            //Initiliazing the game field
            DrawGrass();
            engine.Run();
        }
    }
}
