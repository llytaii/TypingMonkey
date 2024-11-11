using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

public class Program
{
    public class OutputChar
    {
        public char Char { get; set; }
        public bool IsPartOfWord { get; set; }

        public OutputChar(char c)
        {
            Char = c;
            IsPartOfWord = false;
        }
    }

    public static void Main(string[] args)
    {
        int nextCharDelay = 300;
        if (args.Any() && int.TryParse(args[0], out int val))
            nextCharDelay = val;

        // Load the word list into a HashSet for efficient lookups
        var wordSet = new HashSet<string>(File.ReadLines("words2.txt").Select(s => s.Trim()));
        int maxWordLength = wordSet.Max(word => word.Length);

        List<OutputChar> buffer = new List<OutputChar>();

        Random random = new Random();
        const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        while (buffer.Count < maxWordLength)
        {
            char newChar = alphabet[random.Next(alphabet.Length)];
            buffer.Add(new OutputChar(newChar));
        }

        while (true)
        {
            // Detect words in the buffer
            for (int start = 0; start < buffer.Count; start++)
            {
                int maxLength = Math.Min(maxWordLength, buffer.Count - start);
                for (int length = 1; length <= maxLength; length++)
                {
                    string substring = new string(buffer.Skip(start).Take(length).Select(c => c.Char).ToArray());
                    if (wordSet.Contains(substring))
                    {
                        for (int k = start; k < start + length; k++)
                        {
                            buffer[k].IsPartOfWord = true;
                        }
                    }
                }
            }

            // Output the first character with appropriate color
            OutputChar outputChar = buffer[0];

            if (outputChar.IsPartOfWord)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ResetColor();

            Console.Write(outputChar.Char);
            Console.ResetColor();

            // Remove the first character from the buffer
            buffer.RemoveAt(0);

            // Add a new random character to the buffer
            char newChar = alphabet[random.Next(alphabet.Length)];
            buffer.Add(new OutputChar(newChar));

            Thread.Sleep(nextCharDelay);
        }
    }
}
