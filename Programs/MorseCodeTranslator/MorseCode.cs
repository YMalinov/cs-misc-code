using System;
using System.Collections.Generic;

class MorseCode
{
    static void Main()
    {
        Dictionary<char, string> morseCodeTable = new Dictionary<char, string>()
        {
            //letters
            {'a', ".="}, {'b', "=..."}, {'c', "=.=."}, {'d', "=.."}, {'e', "."}, 
            {'f', "..=."}, {'g', "==."}, {'h', "...."}, {'i', ".."}, {'j', ".==="},
            {'k', "=.="}, {'l', ".=.."}, {'m', "=="}, {'n', "=."}, {'o', "==="},
            {'p', ".==."}, {'q', "==.="}, {'r', ".=."}, {'s', "..."}, {'t', "="}, 
            {'u', "..="}, {'v', "...="}, {'w', ".=="}, {'x', "=..="}, {'y', "=.=="},
            {'z', "==.."},

            //letters (capitals):
            {'A', ".="}, {'B', "=..."}, {'C', "=.=."}, {'D', "=.."}, {'E', "."}, 
            {'F', "..=."}, {'G', "==."}, {'H', "...."}, {'I', ".."}, {'J', ".==="},
            {'K', "=.="}, {'L', ".=.."}, {'M', "=="}, {'N', "=."}, {'O', "==="},
            {'P', ".==."}, {'Q', "==.="}, {'R', ".=."}, {'S', "..."}, {'T', "="}, 
            {'U', "..="}, {'V', "...="}, {'W', ".=="}, {'X', "=..="}, {'Y', "=.=="},
            {'Z', "==.."},

            //numbers
            {'1', ".===="}, {'2', "..==="}, {'3', "...=="}, {'4', "....="}, {'5', "....."}, 
            {'6', "=...."}, {'7', "==..."}, {'8', "===.."}, {'9', "====."}, {'0', "====="},

            //special characters
            {'.', ".=.=.="}, {',', "==..=="}, {'?', "..==.."}, {'@', ".==.=."}, {'/', "=..=."},
            {'"', ".=..=."}, {'\'', ".====."}, {'=', "=...="},

            //space between words
            {' ', "  "}
        };

        string input = "";
        string text = "";
        while (true)
        {
            Console.Clear();

            text = "MORSE CODE TRANSLATOR";
            Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, 0);
            Console.Write(text);

            text = "Works with the following characters:";
            Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight - 3);
            Console.WriteLine(text);

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            foreach (char letter in morseCodeTable.Keys)
            {
                Console.Write(letter + " ");
            }

            bool unsupportedSymbols = false;

            Console.SetCursorPosition(0, 3);
            Console.Write("Enter text: ");
            input = Console.ReadLine();

            //check if all of the characters are accounted for
            for (int i = 0; i < input.Length; i++)
            {
                if (!morseCodeTable.ContainsKey(input[i]))
                {
                    Console.Write(input[i] + " ");
                    unsupportedSymbols = true;
                }
            }

            //continue the loop if there are any unsupported symbols, or break if every char exists in the table
            if (unsupportedSymbols)
            {
                Console.WriteLine("aren't supported symbols. Press Enter to try again...");
                Console.ReadLine();
            }
            else
            {
                break;
            }
        }

        Console.WriteLine();

        for (int i = 0; i < input.Length; i++)
        {
            Console.Write(morseCodeTable[input[i]] + " ");
        }

        Console.WriteLine("\n");
    }
}