using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdventOfCode2024.Solutions
{
    public class Day04 : ISolution
    {
        public void RunPart1(string input)
        {
            string[] lines = HelperFunctions.SplitLines(input);
            List<(int, int)> xPositions = GetCharPos(lines, 'X');
            Console.WriteLine(CheckStraight(xPositions, lines) + CheckDiagonal(xPositions, lines));
        }

        private static List<(int, int)> GetCharPos(string[] lines, char character)
        {
            return lines
                .SelectMany((line, i) => line.Select((c, j) => (i, j, c)))
                .Where(pos => pos.c == character)
                .Select(pos => (pos.i, pos.j))
                .ToList();
        }

        private static int CheckStraight(List<(int, int)> xPositions, string[] lines)
        {
            int total = 0;
            int width = lines[0].Length;
            int height = lines.Length;

            foreach ((int x, int y) in xPositions)
            {
                if (x + 3 < width && lines[x + 1][y] == 'M' && lines[x + 2][y] == 'A' && lines[x + 3][y] == 'S') total++;
                if (x - 3 >= 0 && lines[x - 1][y] == 'M' && lines[x - 2][y] == 'A' && lines[x - 3][y] == 'S') total++;
                if (y + 3 < height && lines[x][y + 1] == 'M' && lines[x][y + 2] == 'A' && lines[x][y + 3] == 'S') total++;
                if (y - 3 >= 0 && lines[x][y - 1] == 'M' && lines[x][y - 2] == 'A' && lines[x][y - 3] == 'S') total++;
            }

            return total;
        }

        private static int CheckDiagonal(List<(int, int)> xPositions, string[] lines)
        {
            int total = 0;
            int width = lines[0].Length;
            int height = lines.Length;

            foreach ((int x, int y) in xPositions)
            {
                if (x + 3 < width && y + 3 < height && lines[x + 1][y + 1] == 'M' && lines[x + 2][y + 2] == 'A' && lines[x + 3][y + 3] == 'S') total++;
                if (x + 3 < width && y - 3 >= 0 && lines[x + 1][y - 1] == 'M' && lines[x + 2][y - 2] == 'A' && lines[x + 3][y - 3] == 'S') total++;
                if (x - 3 >= 0 && y + 3 < height && lines[x - 1][y + 1] == 'M' && lines[x - 2][y + 2] == 'A' && lines[x - 3][y + 3] == 'S') total++;
                if (x - 3 >= 0 && y - 3 >= 0 && lines[x - 1][y - 1] == 'M' && lines[x - 2][y - 2] == 'A' && lines[x - 3][y - 3] == 'S') total++;

            }

            return total;
        }

        public void RunPart2(string input)
        {
            string[] lines = HelperFunctions.SplitLines(input);
            List<(int, int)> aPositions = GetCharPos(lines, 'A');
            Console.WriteLine(aPositions.Sum(pos => IsXmas(pos, lines) ? 1 : 0));
        }


        public bool IsXmas((int, int) coordinate, string[] lines)
        {
            (int x, int y) = coordinate;

            if (x - 1 < 0 || y - 1 < 0 || x + 1 >= lines[0].Length || y + 1 >= lines.Length) 
                return false;


            char topLeft = lines[x - 1][y - 1];
            char bottomRight = lines[x + 1][y + 1];
            char topRight = lines[x - 1][y + 1];
            char bottomLeft = lines[x + 1][y - 1];


            return ((topLeft == 'M' && bottomRight == 'S') || (topLeft == 'S' && bottomRight == 'M')) 
                   && ((topRight == 'M' && bottomLeft == 'S') || (topRight == 'S' && bottomLeft == 'M'));
        }
    }
}
