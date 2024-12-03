using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2024.Solutions
{
    public class Day02 : ISolution
    {
        public void RunPart1(string input)
        {
            Console.WriteLine(HelperFunctions
                .SplitLines(input)
                .Select(line => line.Split(' ').Select(int.Parse).ToList())
                .Sum(ParseLevel));
        }

        private static int ParseLevel(List<int> levels)
        {
            bool isAscending = levels[0] < levels[1];

            return levels
                .Take(levels.Count - 1)
                .Select((val, i) => levels[i + 1] - val)
                .All(difference => isAscending && difference is > 0 and <= 3
                                   || (!isAscending && difference is < 0 and >= -3)) ? 1 : 0;
        }

        public void RunPart2(string input)
        {
            Console.WriteLine(HelperFunctions
                .SplitLines(input)
                .Select(line => line.Split(' ').Select(int.Parse).ToList())
                .Sum(ParseLevelWithDampener));
        }

        private static int ParseLevelWithDampener(List<int> levels)
        {
            if (ParseLevel(levels) == 1) return 1;

            return levels
                .Select((_, i) =>
                    levels
                        .Where((_, j) => j != i)
                        .ToList())
                .Any(smallerLevel => ParseLevel(smallerLevel) == 1) ? 1 : 0;
        }
    }
}