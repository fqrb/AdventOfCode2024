using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2024.Solutions
{
    public class Day05 : ISolution
    {
        public void RunPart1(string input)
        {
            (HashSet<(int, int)> pageOrderingRules, List<int[]> pageNumbersList) = GetInput(input);

            Console.WriteLine(pageNumbersList.Sum(pageNumbers => CheckPageNumbers(pageOrderingRules, pageNumbers)));
        }

        private static (HashSet<(int, int)> pageOrderingRules, List<int[]> pageNumbersList) GetInput(string input)
        {
            string[] lines = input.Split('\n');

            HashSet<(int, int)> pageOrderingRules = [];
            int index = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    index = i;
                    break;
                }
                
                int[] pageRule = line.Split('|').Select(int.Parse).ToArray();
                pageOrderingRules.Add((pageRule[0], pageRule[1]));
            }

            List<int[]> pageNumbersList = [];
            for (int i = index + 1; i < lines.Length; i++)
            {
                string line = lines[i];
                int[] pageNumbers = line.Split(',').Select(int.Parse).ToArray();
                pageNumbersList.Add(pageNumbers);
            }

            return (pageOrderingRules, pageNumbersList);
        }

        private static int CheckPageNumbers(HashSet<(int, int)> pageOrderingRules, int[] pageNumbers)
        {
            for (int i = 0; i < pageNumbers.Length; i++)
            {
                int pageNumber = pageNumbers[i];

                for (int j = i + 1; j < pageNumbers.Length; j++)
                {
                    if (pageOrderingRules.Contains((pageNumbers[j], pageNumbers[i])))
                    {
                        return 0;
                    }
                }
            }
            
            return pageNumbers[(pageNumbers.Length - 1) / 2];
        }

        public void RunPart2(string input)
        {
            (HashSet<(int, int)> pageOrderingRules, List<int[]> pageNumbersList) = GetInput(input);

            Console.WriteLine(pageNumbersList.Sum(pageNumbers => CheckIncorrectPageNumbers(pageOrderingRules, pageNumbers)));
        }

        private static int CheckIncorrectPageNumbers(HashSet<(int, int)> pageOrderingRules, int[] pageNumbers)
        {
            for (int i = 0; i < pageNumbers.Length; i++)
            {
                int pageNumber = pageNumbers[i];

                for (int j = i + 1; j < pageNumbers.Length; j++)
                {
                    if (pageOrderingRules.Contains((pageNumbers[j], pageNumbers[i])))
                    {
                        return ReorderAndGetMiddle(pageOrderingRules, pageNumbers);
                    }
                }
            }

            // Page numbers are in correct order so we return 0
            return 0;
        }

        private static int ReorderAndGetMiddle(HashSet<(int, int)> pageOrderingRules, int[] pageNumbers)
        {
            int[] sortedPageNumbers = pageNumbers
                .OrderBy(page => pageNumbers.Count(other => pageOrderingRules.Contains((page, other))))
                .ToArray();

            return sortedPageNumbers[(sortedPageNumbers.Length - 1) / 2];
        }
    }
}
