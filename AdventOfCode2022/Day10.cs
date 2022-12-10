namespace AdventOfCode2022;

internal static class Day10
{
    internal static void Execute()
    {
        // https://adventofcode.com/2022/day/10
        var instructions = File.ReadAllLines("inputs/Day10.txt");

        var totalSignalStrength = 0;
        var x = 1;
        var cycleCount = 1;

        var cycleReports = new int[] { 20, 60, 100, 140, 180, 220 };

        var crt = new char[6][];

        for (var i = 0; i < crt.Length; i++)
            crt[i] = new string('.', 40).ToCharArray();

        foreach (var instructionLine in instructions)
        {
            var instruction = instructionLine.Split(' ');

            crt = PrintToCrt(crt, cycleCount, x);
            cycleCount++;

            if (cycleReports.Contains(cycleCount))
                totalSignalStrength += x * cycleCount;

            if (instruction.Length > 1)
            {
                crt = PrintToCrt(crt, cycleCount, x);
                cycleCount++;

                x += int.Parse(instruction[1]);

                if (cycleReports.Contains(cycleCount))
                    totalSignalStrength += x * cycleCount;
            }
        }

        Console.WriteLine($"Part 1 - The total signal strength across all intervals is {totalSignalStrength}");
        Console.WriteLine("Part 2 - The CRT displays:\n");
        foreach (var row in crt)
            Console.WriteLine(string.Join(' ', row));
    }

    internal static char[][] PrintToCrt(char[][] crt, int cycle, int x)
    {
        var crtY = (cycle - 1) / 40;
        var crtX = (cycle - 1) % 40;

        if (Math.Abs(crtX - x) <= 1)
            crt[crtY][crtX] = '#';

        return crt;
    }
}
