using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace AdventOfCode2024.Solutions
{
    public class Day02 : ISolution
    {
        public void RunPart1(string input)
        {
            string[] lines = HelperFunctions.SplitLines(input);
            Console.WriteLine(lines
                .Select(line => line.Split(' ').Select(int.Parse).ToList())
                .Sum(ParseLevel));
        }

        private static int ParseLevel(List<int> levels)
        {
            bool isAscending = levels[0] < levels[1];

            bool isValid = levels
                .Take(levels.Count - 1)
                .Select((val, i) => levels[i + 1] - val)
                .All(difference => isAscending && difference is > 0 and <= 3
                                    || (!isAscending && difference is < 0 and >= -3));

            return isValid ? 1 : 0;
        }

        public void RunPart2(string input)
        {
            string[] lines = HelperFunctions.SplitLines(input);
            Console.WriteLine(lines
                .Select(line => line.Split(' ').Select(int.Parse).ToList())
                .Sum(ParseLevelWithDampener));
        }

        private static int ParseLevelWithDampener(List<int> levels)
        {
            if (ParseLevel(levels) == 1)
            {
                return 1;
            }

            for (int i = 0; i < levels.Count; i++)
            {
                List<int> newLevels = [.. levels];
                newLevels.RemoveAt(i);

                bool isValid = ParseLevel(newLevels) == 1;

                if (isValid) return 1;
            }

            return 0;
        }
    }
}