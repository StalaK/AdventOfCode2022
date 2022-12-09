namespace AdventOfCode2022;

internal static class Day9
{
    internal static void Execute()
    {
        // https://adventofcode.com/2022/day/9
        var movements = File.ReadAllLines("inputs/Day9.txt");
        var head = (x: 0, y: 0);
        var tail = (x: 0, y: 0);

        var longRope = new List<(int x, int y)>();
        for (int i = 0; i < 10; i++)
            longRope.Add((0, 0));

        var uniqueTailPos = new List<(int x, int y)> { tail };
        var uniqueLongRopeTailPos = new List<(int x, int y)> { longRope[^1] };

        foreach (var movement in movements)
        {
            var move = movement.Split(' ');
            var (direction, distance) = (move[0], int.Parse(move[1]));

            for (var i = 0; i < distance; i++)
            {
                var longRopeHead = longRope[0];

                switch (direction)
                {
                    case "R":
                        head.x++;
                        longRopeHead.x++;
                        break;

                    case "L":
                        head.x--;
                        longRopeHead.x--;
                        break;

                    case "U":
                        head.y++;
                        longRopeHead.y++;
                        break;

                    case "D":
                        head.y--;
                        longRopeHead.y--;
                        break;
                }

                longRope[0] = longRopeHead;

                if (!AreTouching(head, tail))
                {
                    tail = MoveTail(head, tail);

                    if (!uniqueTailPos.Any(t => t.x == tail.x && t.y == tail.y))
                        uniqueTailPos.Add(tail);
                }

                for (var j = 1; j < longRope.Count; j++)
                {
                    if (!AreTouching(longRope[j], longRope[j - 1]))
                        longRope[j] = MoveTail(longRope[j - 1], longRope[j]);
                }

                if (!uniqueLongRopeTailPos.Any(t => t.x == longRope[^1].x && t.y == longRope[^1].y))
                    uniqueLongRopeTailPos.Add(longRope[^1]);
            }
        }

        Console.WriteLine($"Part 1 - The total number of unique tail positions is {uniqueTailPos.Count}");
        Console.WriteLine($"Part 2 - The total number of unique tail positions of the long rope is {uniqueLongRopeTailPos.Count}");
    }

    private static bool AreTouching((int x, int y) head, (int x, int y) tail) =>
        Math.Abs(head.x - tail.x) <= 1 && Math.Abs(head.y - tail.y) <= 1;

    private static (int x, int y) MoveTail((int x, int y) head, (int x, int y) tail)
    {
        var xDiff = head.x - tail.x;
        var yDiff = head.y - tail.y;

        if (Math.Abs(xDiff) != 0)
            tail.x = xDiff > 0 ? tail.x + 1 : tail.x - 1;

        if (Math.Abs(yDiff) != 0)
            tail.y = yDiff > 0 ? tail.y + 1 : tail.y - 1;

        return tail;
    }
}
