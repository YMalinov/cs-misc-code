using System;
using System.Collections.Generic;

class PerfectNumbers //this program generates Perfect Numbers
{
    static void Main()
    {
        Console.WriteLine("PERFECT NUMBER GENERATOR");
        uint number = 0;
        while (true)
        {
            List<uint> numberMultiples = new List<uint>();
            uint sumofMultiples = 0;

            DateTime prevDateTime = DateTime.Now;
            for (uint i = number / 2; i >= 1; i--)
            {
                if (number % i == 0)
                {
                    numberMultiples.Add(i);
                }
            }

            foreach (uint multiple in numberMultiples)
            {
                sumofMultiples += multiple;
            }

            if (sumofMultiples == number)
            {
                TimeSpan calcTime = DateTime.Now - prevDateTime;
                Console.WriteLine(number + " Calculated in: " + calcTime);
            }
            number += 2;
        }
    }
}