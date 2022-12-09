namespace AdventOfCode2022;

internal static class Day8
{
    internal static void Execute()
    {
        // https://adventofcode.com/2022/day/8
        var gridLines = File.ReadAllLines("inputs/Day8.txt");

        var xMax = gridLines[0].Length;
        var yMax = gridLines.Length;
        var grid = new int[yMax][];

        for (var y = 0; y < yMax; y++)
        {
            grid[y] = gridLines[y]
                .ToCharArray()
                .Select(x => int.Parse(x.ToString()))
                .ToArray();
        }

        var visibleTrees = 0;
        var scenicScore = 0;

        for (var y = 0; y < yMax; y++)
        {
            for (var x = 0; x < xMax; x++)
            {
                var currentTreeSize = grid[y][x];
                var yLine = grid.Select(g => g[x]).ToArray();
                var takeX = x + 1;
                var takeY = y + 1;

                var leftView = grid[y][..takeX];
                var rightView = grid[y][x..];
                var upView = yLine[..takeY];
                var downView = yLine[y..];

                if ((leftView.Max() == currentTreeSize && leftView.Count(x => x == currentTreeSize) == 1)
                    || (rightView.Max() == currentTreeSize && rightView.Count(x => x == currentTreeSize) == 1)
                    || (upView.Max() == currentTreeSize && upView.Count(x => x == currentTreeSize) == 1)
                    || (downView.Max() == currentTreeSize && downView.Count(x => x == currentTreeSize) == 1))
                    visibleTrees++;

                int leftScore = 0;
                for (var i = leftView.Length - 2; i >= 0; i--)
                {
                    leftScore++;
                    if (leftView[i] >= currentTreeSize)
                        break;
                }

                int rightScore = 0;
                for (var i = 1; i < rightView.Length; i++)
                {
                    rightScore++;
                    if (rightView[i] >= currentTreeSize)
                        break;
                }

                int upScore = 0;
                for (var i = upView.Length - 2; i >= 0; i--)
                {
                    upScore++;
                    if (upView[i] >= currentTreeSize)
                        break;
                }

                int downScore = 0;
                for (var i = 1; i < downView.Length; i++)
                {
                    downScore++;
                    if (downView[i] >= currentTreeSize)
                        break;
                }

                scenicScore = Math.Max(scenicScore, leftScore * rightScore * upScore * downScore);
            }
        }

        Console.WriteLine($"Part 1 - There are {visibleTrees} visible trees");
        Console.WriteLine($"Part 2 - The highest scenic score is {scenicScore}");
    }
}
