using System.Text.RegularExpressions;

namespace AdventOfCode2022;

internal static class Day15
{
    internal static void Execute()
    {
        // https://adventofcode.com/2022/day/15
        var sensorReadings = File.ReadAllLines("inputs/Day15.txt");

        var digits = new Regex(@"\d+");
        var freeSpaces = new Dictionary<int, List<int>>();

        foreach (string reading in sensorReadings)
        {
            var words = reading.Split(' ');

            var sensor = (x: int.Parse(digits.Match(words[2]).Value),
                y: int.Parse(digits.Match(words[3]).Value));

            var beacon = (x: int.Parse(digits.Match(words[8]).Value),
                y: int.Parse(digits.Match(words[9]).Value));

            var distance = Math.Abs(sensor.x - beacon.x) + Math.Abs(sensor.y - beacon.y);

            for (var i = 0; i <= distance; i++)
            {
                var freeSpace = distance - i;
                var numberRange = new List<int>();
                numberRange.AddRange(Enumerable.Range(sensor.x - distance - i, ((distance - i) * 2) + 1));

                if (freeSpaces.ContainsKey(sensor.y + i))
                    freeSpaces[sensor.y + i].AddRange(numberRange);
                else
                    freeSpaces.Add(sensor.y + i, numberRange);

                if (freeSpaces.ContainsKey(sensor.y - i))
                    freeSpaces[sensor.y - i].AddRange(numberRange);
                else
                    freeSpaces.Add(sensor.y - i, numberRange);
            }
        }

        Console.WriteLine($"Ex in row 10 there are {freeSpaces[10].Distinct().Count()} where a beacon cannot be present");
    }
}
