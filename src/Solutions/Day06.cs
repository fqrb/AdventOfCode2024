using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.XPath;

namespace AdventOfCode2024.Solutions
{
    public class Day06 : ISolution
    {
        public void RunPart1(string input)
        {
            string[] lines = HelperFunctions.SplitLines(input);
            char[][] grid = lines.Select(s => s.ToCharArray()).ToArray();

            HashSet<(int, int)> visited = [];
            (int, int) currentPosition = GetGuardPosition(grid);
            Orientation currentOrientation = Orientation.North;

            while (true)
            {
                visited.Add(currentPosition);

                (int, int) newPosition = GetNewPosition(currentPosition, currentOrientation);

                if (!IsInBounds(newPosition, grid))
                    break;

                if (IsObstacle(newPosition, grid))
                {
                    currentOrientation = TurnRight(currentOrientation);
                }
                else
                {
                    currentPosition = newPosition;
                }
            }
            
            Console.WriteLine(visited.Count());
        }

        private enum Orientation
        {
            North,
            East,
            South,
            West
        }

        private static Orientation TurnRight(Orientation currentOrientation)
        {
            return currentOrientation switch
            {
                Orientation.North => Orientation.East,
                Orientation.East => Orientation.South,
                Orientation.South => Orientation.West,
                Orientation.West => Orientation.North,
                _ => throw new ArgumentOutOfRangeException(nameof(currentOrientation), currentOrientation, null)
            };
        }

        private static (int, int) GetGuardPosition(char[][] grid)
        {
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[0].Length; x++)
                {
                    char current = grid[y][x];

                    switch (current)
                    {
                        case '^':
                            return (x, y);
                    }
                }
            }
            return (-1, -1);
        }

        private static bool IsInBounds((int, int) currentPosition, char[][] grid)
        {
            int x = currentPosition.Item1;
            int y = currentPosition.Item2;
            return x >= 0 && y >= 0 && x < grid[0].Length && y < grid.Length;
        }

        private static bool IsObstacle((int, int) currentPosition, char[][] grid)
        {
            return grid[currentPosition.Item2][currentPosition.Item1] == '#';
        }

        private static (int, int) GetNewPosition((int, int) currentPosition, Orientation currentOrientation)
        {
            int x = currentPosition.Item1;
            int y = currentPosition.Item2;

            switch (currentOrientation)
            {
                case Orientation.North:
                    y--;
                    break;
                case Orientation.East:
                    x++;
                    break;
                case Orientation.South:
                    y++;
                    break;
                case Orientation.West:
                    x--;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentOrientation), currentOrientation, null);
            }

            return (x, y);
        }

        public void RunPart2(string input)
        {
            string[] lines = HelperFunctions.SplitLines(input);
            char[][] grid = lines.Select(s => s.ToCharArray()).ToArray();

            int loopsCreated = 0;

            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[0].Length; x++)
                {
                    HashSet<((int, int), Orientation)> visited = [];
                    (int, int) currentPosition = GetGuardPosition(grid);
                    Orientation currentOrientation = Orientation.North;

                    while (true)
                    {
                        // If the element is already in the hashset we have already been there, and thus we have created a loop.
                        if (!visited.Add((currentPosition, currentOrientation))) 
                        {
                            loopsCreated++;
                            break;
                        }

                        (int, int) newPosition = GetNewPosition(currentPosition, currentOrientation);

                        if (!IsInBounds(newPosition, grid))
                            break;

                        if (IsObstacle(newPosition, grid) || newPosition == (x, y))
                        {
                            currentOrientation = TurnRight(currentOrientation);
                        }
                        else
                        {
                            currentPosition = newPosition;
                        }
                    }
                }
            }
            
            Console.WriteLine(loopsCreated);
        }
    }
}
