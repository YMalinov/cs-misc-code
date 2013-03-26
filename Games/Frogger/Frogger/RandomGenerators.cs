using System;

namespace Frogger
{
    static class RandomGenerators
    {
        private static Random randomizer = new Random();

        public static int Speed()
        {
            int temp = randomizer.Next(0, 2);
            if (temp % 2 == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public static int TruckLength()
        {
            return randomizer.Next(1, 5);
        }

        public static Coordinates RandomPosition()
        {
            int randomLane = Converter.LanesToHeight(randomizer.Next(0, Converter.HeightToLanes(Console.WindowHeight))) - 3;
            int randomCol = randomizer.Next(0, Console.WindowWidth - 1);

            return new Coordinates(randomLane, randomCol);
        }

        public static StaticObject StaticObjectOnRandomPosition()
        {
            int objectNumber = randomizer.Next(0, 3);

            switch (objectNumber)
            {
                case 0:
                    {
                        return new Hole(RandomPosition());
                    }
                case 1:
                    {
                        return new OneUpBonus(RandomPosition());
                    }
                case 2:
                    {
                        return new SlowerTrafficBonus(RandomPosition());
                    }
            }

            return null; //this code will never be executed and yet VS won't compile without it :)
        }
    }
}
