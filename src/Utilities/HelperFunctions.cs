using System;

namespace AdventOfCode2024.Utilities
{
    public static class HelperFunctions
    {
        public static string[] SplitLines(string input)
        {
            return input.Split(['\n'], StringSplitOptions.RemoveEmptyEntries);
        }
    }
}