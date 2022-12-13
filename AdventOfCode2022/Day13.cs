using System.Text.Json.Nodes;
namespace AdventOfCode2022;

internal static class Day13
{
    internal static void Execute()
    {
        // https://adventofcode.com/2022/day/13
        var signals = File.ReadAllLines("inputs/Day13.txt");

        var orderedIndexSum = 0;

        for (var i = 0; i < signals.Length; i += 3)
        {
            var left = JsonNode.Parse(signals[i]);
            var right = JsonNode.Parse(signals[i + 1]);

            var ordered = Compare(left!, right!);

            if (ordered < 0)
                orderedIndexSum += (i / 3) + 1;
        }

        Console.WriteLine($"Part 1 - The sum of the indices of the ordered inputs is {orderedIndexSum}");

        var dividers = new List<string> { "[[2]]", "[[6]]" };
        var signalList = signals
            .Where(s => !string.IsNullOrEmpty(s))
            .Concat(dividers)
            .ToList();

        var nodeList = signalList.ConvertAll(s => JsonNode.Parse(s));
        nodeList.Sort(Compare!);

        signalList = nodeList.ConvertAll(s => s.ToJsonString());

        var decoderKey = (signalList.IndexOf(dividers[0]) + 1) * (signalList.IndexOf(dividers[1]) + 1);

        Console.WriteLine($"Part 2 - The decoder key for the distress signal is {decoderKey}");
    }

    // If the overall response is positive, it means the compared values on the right are smaller than the left
    // If the overall response is negative, it means the compared values on the left are smaller than the right
    // If the response is zero, check the length of the array. If the length of left array is negative then it's ordered
    private static int Compare(JsonNode leftNode, JsonNode rightNode)
    {
        if (leftNode is JsonValue && rightNode is JsonValue)
            return leftNode.GetValue<int>() - rightNode.GetValue<int>();

        var leftArray = leftNode is JsonArray leftJsonArray ? leftJsonArray : new JsonArray(leftNode.GetValue<int>());
        var rightArray = rightNode is JsonArray rightJsonArray ? rightJsonArray : new JsonArray(rightNode.GetValue<int>());

        foreach (var (leftItem, rightItem) in leftArray.Zip(rightArray))
        {
            var response = Compare(leftItem!, rightItem!);
            if (response != 0)
                return response;
        }

        return leftArray.Count - rightArray.Count;
    }
}
