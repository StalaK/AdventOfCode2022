namespace AdventOfCode2022;

internal static class Day3
{
    internal static void Execute()
    {
        // https://adventofcode.com/2022/day/3
        var backpacks = File.ReadAllLines("inputs/Day3.txt");
        var totalPriority = 0;

        foreach (var backpack in backpacks)
        {
            if (string.IsNullOrEmpty(backpack))
                continue;

            var compartments = backpack.Chunk(backpack.Length / 2).ToList();
            var duplicateItem = compartments[0].Intersect(compartments[1]).First();

            totalPriority += GetItemPriority(duplicateItem);
        }

        Console.WriteLine($"Part 1 - The total priority of all items in both compartments of all backpacks is {totalPriority}");

        var groupPriority = 0;

        for (var i = 0; i < backpacks.Length; i += 3)
        {
            var item = backpacks[i]
                .Intersect(backpacks[i + 1]
                .Intersect(backpacks[i + 2]))
                .First();

            groupPriority += GetItemPriority(item);
        }

        Console.WriteLine($"Part 2 - The total priority of all groups badges is {groupPriority}");
    }

    private static int GetItemPriority(char item) => item >= 'a' ? item - 96 : item - 38;
}
