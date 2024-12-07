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
                total += CheckIfValid(line, GetSuccessorsPart1);
            }
            Console.WriteLine(total);
        }

        private static ulong CheckIfValid(string s, Func<ulong, ulong, ulong[]> getSuccessors)
        {
            string[] parts = s.Split(':');
            ulong answer = ulong.Parse(parts[0]);
            ulong[] numbers = parts[1].TrimStart(' ').Split(' ').Select(ulong.Parse).ToArray();

            Queue<(int index, ulong value)> queue = new();
            queue.Enqueue((index: 0, value: numbers[0]));

            while (queue.Count > 0)
            {
                (int i, ulong value) = queue.Dequeue();

                if (i == numbers.Length - 1)
                {
                    if (value == answer)
                    {
                        return answer;
                    }

                    continue;
                }

                getSuccessors(value, numbers[i + 1])
                    .ToList()
                    .ForEach(successor => queue.Enqueue((i + 1, successor)));
            }
            return 0;
        }

        private static ulong[] GetSuccessorsPart1(ulong value, ulong nextNumber)
        {
            return [value * nextNumber, value + nextNumber];
        }

        public void RunPart2(string input)
        {
            string[] lines = HelperFunctions.SplitLines(input);

            ulong total = 0;

            foreach (string line in lines)
            {
                total += CheckIfValid(line, GetSuccessorsPart2);
            }
            Console.WriteLine(total);
        }

        private static ulong[] GetSuccessorsPart2(ulong value, ulong nextNumber)
        {
            return [value * nextNumber, value + nextNumber, ulong.Parse(value.ToString() + nextNumber)];
        }
    }
}
