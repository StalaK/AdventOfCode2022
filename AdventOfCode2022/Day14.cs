namespace AdventOfCode2022;

internal static class Day14
{
    internal static void Execute()
    {
        // https://adventofcode.com/2022/day/14
        var rockPaths = File.ReadAllLines("inputs/Day14.txt");
        var startPoint = (500, 0);
        var sandPit = CreateSandpit(rockPaths, startPoint);

        var sandInTheAbyss = false;
        var sandCount = 0;

        do
        {
            (sandPit, sandInTheAbyss) = DropSand(sandPit, startPoint, startPoint);

            if (!sandInTheAbyss)
                sandCount++;
        } while (!sandInTheAbyss);

        Console.WriteLine($"Sand fell into the abyss after {sandCount} grains had landed");

        sandPit = CreateSandpit(rockPaths, startPoint);
        sandPit[sandPit.Length - 1] = sandPit[sandPit.Length - 1].Select(s => s = '#').ToArray();
        sandCount = 0;
        sandInTheAbyss = false;

        do
        {
            (sandPit, sandInTheAbyss) = DropSand(sandPit, startPoint, startPoint);

            if (!sandInTheAbyss)
                sandCount++;
        } while (!sandInTheAbyss);

        Console.WriteLine($"Sand filled to the top after {sandCount} grains had landed");
        DrawSandpit(sandPit);
    }

    private static (char[][] sandPit, bool sandInTheAbyss) DropSand(char[][] sandPit, (int x, int y) sandPosition, (int x, int y) origin)
    {
        if (sandPit[origin.y][origin.x] == 'o')
            return (sandPit, true);

        if (sandPosition.x >= sandPit[sandPosition.y].Length || sandPosition.y + 1 >= sandPit.Length)
            return (sandPit, true);

        if (sandPit[sandPosition.y + 1][sandPosition.x] == '.')
            return DropSand(sandPit, (sandPosition.x, sandPosition.y + 1), origin);

        if (sandPit[sandPosition.y + 1][sandPosition.x] != '.')
        {
            if (sandPit[sandPosition.y + 1][sandPosition.x - 1] == '.')
                return DropSand(sandPit, (sandPosition.x - 1, sandPosition.y + 1), origin);

            if (sandPit[sandPosition.y + 1][sandPosition.x + 1] == '.')
                return DropSand(sandPit, (sandPosition.x + 1, sandPosition.y + 1), origin);
        }

        sandPit[sandPosition.y][sandPosition.x] = 'o';

        return (sandPit, false);
    }

    private static char[][] CreateSandpit(string[] rockPaths, (int x, int y) sandAppears)
    {
        var yMax = 0;
        var xMax = 0;

        foreach (var rockPath in rockPaths)
        {
            foreach (var line in rockPath.Split(" -> "))
            {
                var coord = line.Split(',');

                xMax = int.Parse(coord[0]) > xMax ? int.Parse(coord[0]) + 200 : xMax;
                yMax = int.Parse(coord[1]) > yMax ? int.Parse(coord[1]) + 3 : yMax;
            }
        }

        var sandPit = new char[yMax][];
        xMax += 200;

        for (var y = 0; y < yMax; y++)
        {
            sandPit[y] = new char[xMax];
            for (var x = 0; x < xMax; x++)
                sandPit[y][x] = '.';
        }

        sandPit[sandAppears.y][sandAppears.x] = '+';

        foreach (var rockPath in rockPaths)
        {
            (int x, int y) previous = (-1, -1);

            foreach (var line in rockPath.Split(" -> "))
            {
                var coord = line
                    .Split(',')
                    .Select(l => int.Parse(l))
                    .ToArray();

                if (previous.x == -1 && previous.y == -1)
                    previous = (coord[0], coord[1]);

                if (previous.x - coord[0] != 0)
                {
                    var drawStart = Math.Min(previous.x, coord[0]);
                    var drawEnd = Math.Max(previous.x, coord[0]);

                    for (var x = drawStart; x <= drawEnd; x++)
                        sandPit[coord[1]][x] = '#';
                }
                else
                {
                    var drawStart = Math.Min(previous.y, coord[1]);
                    var drawEnd = Math.Max(previous.y, coord[1]);

                    for (var y = drawStart; y <= drawEnd; y++)
                        sandPit[y][coord[0]] = '#';
                }

                previous = (coord[0], coord[1]);
            }
        }

        return sandPit;
    }

    private static void DrawSandpit(char[][] sandPit)
    {
        for (var y = 0; y < sandPit.Length; y++)
        {
            for (var x = 400; x < sandPit[y].Length - 100; x++)
            {
                Console.ForegroundColor = ConsoleColor.Black;

                if (sandPit[y][x] == '#')
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                if (sandPit[y][x] == 'o')
                    Console.ForegroundColor = ConsoleColor.Yellow;

                if (sandPit[y][x] == '+')
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(sandPit[y][x]);
            }

            Console.Write("\n");
        }
    }
}
