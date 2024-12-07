using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2024.Solutions
{
    public class Day07 : ISolution
    {
        public void RunPart1(string input)
        {
            string[] lines = HelperFunctions.SplitLines(input);

            ulong total = 0;

            foreach (string line in lines)
            {
                total += (ulong)CheckIfValid(line);
            }
            Console.WriteLine(total);
        }

        private static int CheckIfValid(string s)
        {
            string[] parts = s.Split(':');

            ulong answer = ulong.Parse(parts[0]);

            ulong[] numbers = parts[1].TrimStart(' ').Split(' ').Select(ulong.Parse).ToArray();

            HashSet<ulong> results = [numbers[0]];
            for (int i = 1; i < numbers.Length; i++)
            {
                ulong currentNumber = numbers[i];

                HashSet<ulong> newIntermediateResults = [];
                foreach (ulong number in results)
                {
                    if (currentNumber <= ulong.MaxValue / number)
                        newIntermediateResults.Add(number * currentNumber);

                    if (currentNumber <= ulong.MaxValue - number)
                        newIntermediateResults.Add(number + currentNumber);

                    results = newIntermediateResults;
                }
            }

            return results.Contains(answer) ? (int)answer : 0;
        }

        public void RunPart2(string input)
        {
        }
    }
}
