using System;

namespace Frogger
{
    class InvalidNumberOfLanesException : Exception
    {
        public static string ErrorMessage = 
            "You have entered an invalid number of lanes! Min: 2, Max: " + Converter.HeightToLanes(Console.LargestWindowHeight);

        public InvalidNumberOfLanesException()
            : base(ErrorMessage)
        {
        }

        public InvalidNumberOfLanesException(string message) 
            : base(message)
        {
        }
    }
}
