using System.Text;

namespace AdventOfCode2022;

internal static partial class Execute
{
    internal static void Day5()
    {
        // https://adventofcode.com/2022/day/5
        var moves = File.ReadAllLines("inputs/Day5.txt")
            .Select(x => ParseMove(x))
            .ToList();

        List<Stack<char>> stacks = new();
        SetStartPosition(stacks);

        foreach (var move in moves)
        {
            for (var i = 0; i < move.amount; i++)
            {
                var block = stacks[move.start].Pop();
                stacks[move.end].Push(block);
            }
        }

        var part1Output = new StringBuilder("Part 1 - The blocks at the top of each stack are : ");

        foreach (var stack in stacks)
            part1Output.Append(stack.Pop());

        stacks = new();
        SetStartPosition(stacks);

        foreach (var move in moves)
        {
            List<char> blocks = new();
            for (var i = 0; i < move.amount; i++)
                blocks.Add(stacks[move.start].Pop());

            blocks.Reverse();

            foreach (var block in blocks)
                stacks[move.end].Push(block);
        }

        var part2Output = new StringBuilder("Part 2 - The blocks at the top of each stack are : ");

        foreach (var stack in stacks)
            part2Output.Append(stack.Pop());

        Console.WriteLine(part1Output);
        Console.WriteLine(part2Output);
    }

    internal static void SetStartPosition(List<Stack<char>> stacks)
    {
        var startPoint = File.ReadAllLines("inputs/Day5_Start.txt")
            .Select(x =>
                x.Replace("    ", " . ")
                .Replace("[", "")
                .Replace("]", "")
                .Replace(" ", ""))
            .ToList();

        for (var i = startPoint.Count() - 2; i >= 0; i--)
        {
            var blocks = startPoint[i].ToCharArray();
            for (var j = 0; j < blocks.Length; j++)
            {
                if (blocks[j] != '.')
                {
                    if (stacks.Count <= j)
                        stacks.Add(new Stack<char>());

                    stacks[j].Push(blocks[j]);
                }
            }
        }
    }

    internal static (int amount, int start, int end) ParseMove(string move)
    {
        var values = move.Split(' ')
            .Where(x => int.TryParse(x, out _))
            .Select(x => int.Parse(x))
            .ToList();

        return (values[0], values[1] - 1, values[2] - 1);
    }
}
