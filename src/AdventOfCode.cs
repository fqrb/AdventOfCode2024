using System;
using System.IO;
using AdventOfCode2024.Solutions;

namespace AdventOfCode2024
{
    public static class AdventOfCode
    {
        private static void Main(string[] args)
        {
            // We get the current day from the system time
            int currentDay = DateTime.Now.Day;

            // If the user wants to run a different day than it currently is they can input this.
            Console.WriteLine($"Enter the day you want to run. Default is {currentDay}");
            string dayInput = Console.ReadLine();

            // We format the day to be 01 if it is 1, but 10 if it was 10.
            string day = string.IsNullOrWhiteSpace(dayInput) ? $"{currentDay:00}" : dayInput.PadLeft(2, '0');

            try
            {
                string className = $"AdventOfCode2024.Solutions.Day{day}";
                Type solutionType = Type.GetType(className);

                if (solutionType == null)
                {
                    Console.WriteLine($"Solution for day {day} not found. Please make sure it exists");
                    return;
                }

                ISolution solutionFile = (ISolution)Activator.CreateInstance(solutionType);

                string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\", $"src/Inputs/Day{day}.txt");
                if (!File.Exists(inputPath))
                {
                    Console.WriteLine($"Input for day {day} at {inputPath} not found. Please make sure it exists");
                    return;
                }

                string input = File.ReadAllText(inputPath);

                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                Console.WriteLine("Successfully loaded. Running Part 1");
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                Console.WriteLine();
                solutionFile.RunPart1(input);
                Console.WriteLine();
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                Console.WriteLine("Finished part 1. Running part 2:");
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                Console.WriteLine();
                solutionFile.RunPart2(input);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has occurred. {ex.Message}");
            }
        }
    }
}