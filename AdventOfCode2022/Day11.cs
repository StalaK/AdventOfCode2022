using System;

namespace AdventOfCode2022;

internal static class Day11
{
    internal static void Execute()
    {
        // https://adventofcode.com/2022/day/11
        var monkeys = GetStartPosition();
        const int PART_1_ROUND_COUNT = 20;

        for (var i = 0; i < PART_1_ROUND_COUNT; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Count > 0)
                {
                    if (!monkey.Items.TryDequeue(out var item))
                        continue;

                    var worryLevel = monkey.WorryOperation(item);
                    monkey.InspectionCount++;

                    worryLevel /= 3;
                    var recipient = monkey.WorryCheck(worryLevel) ? monkey.TrueRecipient : monkey.FalseRecipient;
                    monkeys[recipient].Items.Enqueue(worryLevel);
                }
            }
        }
        
        var part1MonkeyBusiness = monkeys.OrderByDescending(m => m.InspectionCount).Take(2).Aggregate(1, (acc, m) => acc * m.InspectionCount);
        Console.WriteLine($"Part 1 - The level of monkey business after {PART_1_ROUND_COUNT} rounds from the top 2 active monkeys is {part1MonkeyBusiness} ");

        monkeys = GetStartPosition();
        const int PART_2_ROUND_COUNT = 10000;

        // Product of all worryCheck mod values
        var checkProduct = 11 * 5 * 19 * 13 * 7 * 17 * 2 * 3;

        for (var i = 0; i < PART_2_ROUND_COUNT; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Count > 0)
                {
                    if (!monkey.Items.TryDequeue(out var item))
                        continue;

                    var worryLevel = monkey.WorryOperation(item);
                    monkey.InspectionCount++;

                    var recipient = monkey.WorryCheck(worryLevel) ? monkey.TrueRecipient : monkey.FalseRecipient;
                    monkeys[recipient].Items.Enqueue(worryLevel % checkProduct);
                }
            }
        }

        var part2MonkeyBusiness = monkeys.OrderByDescending(m => m.InspectionCount).Take(2).Aggregate(1UL, (acc, m) => (ulong)acc * (ulong)m.InspectionCount);
        Console.WriteLine($"Part 2 - The level of monkey business after {PART_2_ROUND_COUNT} rounds from the top 2 active monkeys is {part2MonkeyBusiness} ");
    }

    private static List<Monkey> GetStartPosition() => new List<Monkey>
    {
        new Monkey
        {
            Items = new(new long[]{ 83, 88, 96, 79, 86, 88, 70 }),
            WorryOperation = (worryLevel) => worryLevel * 5,
            WorryCheck = (worryLevel) => (worryLevel % 11) == 0,
            TrueRecipient = 2,
            FalseRecipient = 3
        },
        new Monkey
        {
            Items = new(new long[]{ 59, 63, 98, 85, 68, 72 }),
            WorryOperation = (worryLevel) => worryLevel * 11,
            WorryCheck = (worryLevel) => (worryLevel % 5) == 0,
            TrueRecipient = 4,
            FalseRecipient = 0
        },
        new Monkey
        {
            Items = new(new long[]{ 90, 79, 97, 52, 90, 94, 71, 70 }),
            WorryOperation = (worryLevel) => worryLevel + 2,
            WorryCheck = (worryLevel) => (worryLevel % 19) == 0,
            TrueRecipient = 5,
            FalseRecipient = 6
        },
        new Monkey
        {
            Items = new(new long[]{ 97, 55, 62 }),
            WorryOperation = (worryLevel) => worryLevel + 5,
            WorryCheck = (worryLevel) => (worryLevel % 13) == 0,
            TrueRecipient = 2,
            FalseRecipient = 6
        },
        new Monkey
        {
            Items = new(new long[]{ 74, 54, 94, 76 }),
            WorryOperation = (worryLevel) => worryLevel * worryLevel,
            WorryCheck = (worryLevel) => (worryLevel % 7) == 0,
            TrueRecipient = 0,
            FalseRecipient = 3
        },
        new Monkey
        {
            Items = new(new long[]{ 58 }),
            WorryOperation = (worryLevel) => worryLevel + 4,
            WorryCheck = (worryLevel) => (worryLevel % 17) == 0,
            TrueRecipient = 7,
            FalseRecipient = 1
        },
        new Monkey
        {
            Items = new(new long[]{ 66, 63 }),
            WorryOperation = (worryLevel) => worryLevel + 6,
            WorryCheck = (worryLevel) => (worryLevel % 2) == 0,
            TrueRecipient = 7,
            FalseRecipient = 5
        },
        new Monkey
        {
            Items = new(new long[]{ 56, 56, 90, 96, 68 }),
            WorryOperation = (worryLevel) => worryLevel + 7,
            WorryCheck = (worryLevel) => (worryLevel % 3) == 0,
            TrueRecipient = 4,
            FalseRecipient = 1
        },
    };

    private class Monkey
    {
        public required Queue<long> Items { get; set; }

        public required Func<long, long> WorryOperation { get; set; }

        public required Func<long, bool> WorryCheck { get; set; }

        public int TrueRecipient { get; set; }

        public int FalseRecipient { get; set; }

        public int InspectionCount { get; set; } = 0;
    }
}

