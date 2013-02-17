using System;
using System.Numerics;

class KaprekarConstant //this program can generates Kaprekar constants (look it up on wikipedia)
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        BigInteger number = BigInteger.Parse(Console.ReadLine());
        BigInteger origNumber = number;

        //this program uses an array to sort the digits in ascending and descending order
        char[] digitArray = new char[number.ToString().Length];
        BigInteger numberAscending = 0;
        BigInteger numberDescending = 0;
        BigInteger oldNumber = 0;
        int iterations = 0;

        while (true)
        {
            //add the digits to an array, which will later be sorted
            for (int i = 0; i < number.ToString().Length; i++)
            {
                digitArray[i] = number.ToString()[i];
            }

            //sorting the digits in ascending order and putting them in an int
            Array.Sort(digitArray);
            for (int i = 0; i < digitArray.Length; i++)
            {
                numberAscending = BigInteger.Parse(numberAscending.ToString() + (digitArray[i] - '0'));
            }

            //sorting the digits in descending order and putting them in an int
            Array.Reverse(digitArray);
            for (int i = 0; i < digitArray.Length; i++)
            {
                numberDescending = BigInteger.Parse(numberDescending.ToString() + (digitArray[i] - '0'));
            }

            //doing the actual math
            number = numberDescending - numberAscending;

            //if the number == 0, then the input was made up of one digit (1111, 2222, etc.), which is not allowed
            if (number == 0)
            {
                Console.WriteLine("Invalid input!");
                return;
            }
            //this will work, because a Kaprekar constant will remain the same even after the operation
            else if (oldNumber == number)
            {
                Console.WriteLine("Kaprekar's constant reached. From {0} to {1} in {2} steps.", origNumber, number, iterations);
                break;
            }

            Console.WriteLine("\n  {0}\n-\n  {1}\n-------\n  {2}\n\n------------------", numberDescending, numberAscending, number);

            iterations++;
            if (iterations % 100 == 0)
            {
                Console.WriteLine("{0} steps made and no constant was found.", iterations);
                Console.ReadLine();
            }

            oldNumber = number;

            //resetting the digitArray and sorted numbers
            numberAscending = numberDescending = 0;
            for (int i = 0; i < digitArray.Length; i++)
            {
                digitArray[i] = '0';
            }
        }
    }
}