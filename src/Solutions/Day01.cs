using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2024.Solutions
{
    public class Day01 : ISolution
    {
        public void RunPart1(string input)
        {
            // First we get the input and parse it into two lists
            (List<int> l, List<int> r) = ParseInput(input);

            l.Sort();
            r.Sort();

            // For every index in the left list we calculate the difference in index between the two lists and sum them up to get the answer.
            Console.WriteLine(l.Select((t, i) => Math.Abs(r[i] - t)).Sum());
        }

        public void RunPart2(string input)
        {
            // Again, we get the input into two lists
            (List<int> l, List<int> r) = ParseInput(input);

            // The similarity score is the sum of every item of l times the amount of times it (t) is in r
            Console.WriteLine(l.Sum(t => t * r.Count(j => j == t)));
        }

        private static (List<int>, List<int>) ParseInput(string input)
        {
            // First we split the input into an array of strings
            string[] splitInput = HelperFunctions.SplitLines(input);
            List<int> l = [];
            List<int> r = [];
            foreach (string s in splitInput)
            {
                // Then for every line in the array we split it into two integers and add them to the lists
                string[] ss = s.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                l.Add(int.Parse(ss[0]));
                r.Add(int.Parse(ss[1]));
            }
            return (l, r);
        }
    }
}