using System;
namespace AdventOfCode2022;

internal static partial class Execute
{
    internal static void Day4()
    {
        // https://adventofcode.com/2022/day/3
        var cleaningZones = File.ReadAllLines("inputs/Day4.txt");
        var containedCount = 0;
        var overlapCount = 0;

        foreach(var zone in cleaningZones)
        {
            var elves = zone.Split(',');
            var elf1 = elves[0].Split('-').Select(x => int.Parse(x)).ToList();
            var elf2 = elves[1].Split('-').Select(x => int.Parse(x)).ToList();

            if ((elf1[0] >= elf2[0] && elf1[1] <= elf2[1])
                || (elf2[0] >= elf1[0] && elf2[1] <= elf1[1]))
                containedCount++;

            if ((elf2[0] >= elf1[0] && elf2[0] <= elf1[1])
                || (elf2[1] >= elf1[0] && elf2[1] <= elf1[1])
                || (elf1[0] >= elf2[0] && elf1[0] <= elf2[1])
                || (elf1[1] >= elf2[0] && elf1[1] <= elf2[1]))
                overlapCount++;
        }

        Console.WriteLine($"Part 1 - There are {containedCount} cleaning assignments fully contained in the other");
        Console.WriteLine($"Part 2 - There are {overlapCount} cleaning assignments which overlap with the other");
    }
}