using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace AdventOfCode2024.Solutions
{
    public class Day03 : ISolution
    {
        public void RunPart1(string input)
        {
            string[] lines = HelperFunctions.SplitLines(input);
            int result = lines
                .Select(line => new Regex(@"mul\((\d+),(\d+)\)").Matches(line)
                    .Cast<Match>()
                    .Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value)))
                .Sum();
                
            Console.WriteLine(result);
        }

        public void RunPart2(string input) 
        {
            string[] lines = HelperFunctions.SplitLines(input);

            bool isEnabled = true;
            int result = 0;

            MatchCollection matches = new Regex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)").Matches(input);

            foreach (Match match in matches)
            {
                switch (match.Value)
                {
                    case "do()":
                        isEnabled = true;
                        break;
                    case "don't()":
                        isEnabled = false;
                        break;
                    default:
                    {
                        if (isEnabled)
                        {
                            result += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                        }
                        break;
                    }
                }
            }
                
            Console.WriteLine(result);
        }
    }

}
