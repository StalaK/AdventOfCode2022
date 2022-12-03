namespace AdventOfCode2022;

internal static partial class Execute
{
    internal static void Day1()
    {
        // https://adventofcode.com/2022/day/1
        var calorieEntries = File.ReadAllLines("inputs/Day1.txt");

        List<int> totalCaloriesPerElf = new();
        var elfTotal = 0;

        foreach (var calorieEntry in calorieEntries)
        {
            if (string.IsNullOrEmpty(calorieEntry))
            {
                totalCaloriesPerElf.Add(elfTotal);
                elfTotal = 0;
            }
            else
            {
                elfTotal += int.Parse(calorieEntry);
            }
        }

        Console.WriteLine($"Part 1: The elf with the most calories is carrying {totalCaloriesPerElf.Max()} calories");

        var topThreeTotal = totalCaloriesPerElf.OrderBy(x => x).TakeLast(3).Sum();

        Console.WriteLine($"\nPart 2: The top three elves carrying the most calories are carrying a total of {topThreeTotal}");
    }
}