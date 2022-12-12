namespace AdventOfCode2022;

internal static class Day12
{
    internal static void Execute()
    {
        // https://adventofcode.com/2022/day/12
        var gridLines = File.ReadAllLines("inputs/Day12.txt");

        var xMax = gridLines[0].Length;
        var yMax = gridLines.Length;
        var grid = new char[yMax][];

        for (var y = 0; y < yMax; y++)
            grid[y] = gridLines[y].ToCharArray();

        (int x, int y) startPos = new();
        (int x, int y) goalPos = new();

        for (var y = 0; y < yMax; y++)
        {
            for (var x = 0; x < xMax; x++)
            {
                if (grid[y][x] == 'S')
                    startPos = (x, y);

                if (grid[y][x] == 'E')
                    goalPos = (x, y);
            }
        }

        var path = FindPath(grid, startPos, goalPos);
        Console.WriteLine($"Part 1 - The minimum number of moves to reach the goal is {path.Count - 1}\n");

        DrawGrid(grid, path, startPos);

        List<(int x, int y)> shortestPath = new();
        (int x, int y) startPoint = new();

        for (var y = 0; y < yMax; y++)
        {
            for (var x = 0; x < xMax; x++)
            {
                if (grid[y][x] == 'a')
                {
                    var checkPath = FindPath(grid, (x, y), goalPos);

                    var shortestPathcount = (shortestPath.Count == 0 ? int.MaxValue : shortestPath.Count);
                    if (checkPath.Count > 0 && checkPath.Count < shortestPathcount)
                    {
                        shortestPath = checkPath;
                        startPoint = (x, y);
                    }
                }
            }
        }

        Console.WriteLine($"Part 2 - The fewest steps from the lowest elevation is {shortestPath.Count - 1}, starting at ({startPoint.x},{startPoint.y})\n");

        DrawGrid(grid, shortestPath, startPoint);
    }

    private static List<(int x, int y)> FindPath(char[][] grid, (int x, int y) startPos, (int x, int y) goalPos)
    {
        var currentPos = (startPos.x, startPos.y);
        List<(int x, int y)> openSet = new()
        {
            new() { x = currentPos.x, y = currentPos.y }
        };

        Dictionary<((int x, int y) To, (int x, int y) From), int> cameFrom = new();
        Dictionary<(int x, int y), int> gScore = new();
        gScore.Add(currentPos, 0);

        Dictionary<(int x, int y), int> fScore = new();
        fScore.Add(currentPos, DistanceScore(currentPos, goalPos));

        while (openSet.Count > 0)
        {
            foreach (var fVal in fScore.OrderBy(f => f.Value))
            {
                if (openSet.Any(o => o.x == fVal.Key.x && o.y == fVal.Key.y))
                {
                    currentPos = fVal.Key;
                    break;
                }
            }

            if (currentPos.x == goalPos.x && currentPos.y == goalPos.y)
            {
                List<(int x, int y)> finalPath = new();

                finalPath.Add(currentPos);

                var buildPath = true;
                while (buildPath)
                {
                    currentPos = cameFrom
                        .OrderBy(c => c.Value)
                        .FirstOrDefault(c =>
                            c.Key.To.x == currentPos.x
                            && c.Key.To.y == currentPos.y)
                        .Key
                        .From;

                    if (currentPos.x == startPos.x && currentPos.y == startPos.y)
                        buildPath = false;

                    finalPath.Add(currentPos);
                }

                return finalPath;
            }

            openSet.Remove(currentPos);

            var currentElevation = grid[currentPos.y][currentPos.x] == 'S' ? 'a' : grid[currentPos.y][currentPos.x];

            var left = currentPos.x > 0 && currentElevation - grid[currentPos.y][currentPos.x - 1] >= -1;
            var right = currentPos.x < grid[0].Length - 1 && currentElevation - grid[currentPos.y][currentPos.x + 1] >= -1;
            var up = currentPos.y > 0 && currentElevation - grid[currentPos.y - 1][currentPos.x] >= -1;
            var down = currentPos.y < grid.Length - 1 && currentElevation - grid[currentPos.y + 1][currentPos.x] >= -1;

            if (left)
            {
                var movePos = (currentPos.x - 1, currentPos.y);
                ProcessMove(movePos, currentPos, goalPos, cameFrom, gScore, fScore, openSet);
            }

            if (right)
            {
                var movePos = (currentPos.x + 1, currentPos.y);
                ProcessMove(movePos, currentPos, goalPos, cameFrom, gScore, fScore, openSet);
            }

            if (up)
            {
                var movePos = (currentPos.x, currentPos.y - 1);
                ProcessMove(movePos, currentPos, goalPos, cameFrom, gScore, fScore, openSet);
            }

            if (down)
            {
                var movePos = (currentPos.x, currentPos.y + 1);
                ProcessMove(movePos, currentPos, goalPos, cameFrom, gScore, fScore, openSet);
            }
        }

        return new List<(int, int)>();
    }

    private static void ProcessMove(
        (int x, int y) newPosition,
        (int x, int y) currentPosition,
        (int x, int y) goalPosition,
        Dictionary<((int x, int y) To, (int x, int y) From), int> cameFrom,
        Dictionary<(int x, int y), int> gScore,
        Dictionary<(int x, int y), int> fScore,
        List<(int x, int y)> openSet)
    {
        var tentativeScore = gScore[currentPosition] + 1;
        if (tentativeScore < DistanceScore(currentPosition, goalPosition))
        {
            if (cameFrom.ContainsKey((newPosition, currentPosition)))
                cameFrom[(newPosition, currentPosition)] = Math.Min(cameFrom[(newPosition, currentPosition)], tentativeScore);
            else
                cameFrom.Add((newPosition, currentPosition), tentativeScore);

            if (!gScore.ContainsKey(newPosition))
            {
                gScore.Add(newPosition, tentativeScore);
                fScore.Add(newPosition, tentativeScore + DistanceScore(newPosition, goalPosition));

                if (!openSet.Contains(newPosition))
                    openSet.Add(newPosition);
            }
        }
    }

    private static int DistanceScore((int x, int y) currentPos, (int x, int y) goalPos) => goalPos.x - currentPos.x + goalPos.y - currentPos.y + 350;

    private static void DrawGrid(char[][] grid, List<(int x, int y)> visitedPositions, (int x, int y) startPosition)
    {
        var xMax = grid[0].Length;
        var yMax = grid.Length;

        for (var y = 0; y < yMax; y++)
        {
            for (var x = 0; x < xMax; x++)
            {
                if (grid[y][x] == 'S')
                    grid[y][x] = 'a';

                if (visitedPositions.Any(v => v.x == x & v.y == y))
                    Console.ForegroundColor = ConsoleColor.DarkBlue;

                if (x == startPosition.x && y == startPosition.y)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    grid[y][x] = 'S';
                }

                if (grid[y][x] == 'E')
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.Write(grid[y][x]);

                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Write("\n");
        }
    }
}
