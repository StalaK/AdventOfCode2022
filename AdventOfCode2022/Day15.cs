using System.Text.RegularExpressions;

namespace AdventOfCode2022;
internal static class Day15
{
    internal static void Execute()
    {
        // https://adventofcode.com/2022/day/15
        var sensorReadings = File.ReadAllLines("inputs/Day15.txt");

        var digits = new Regex(@"-?\d+");
        var freeSpaces = new Dictionary<int, List<(int min, int max)>>();
        const int CHECK_ROW = 2000000;
        const int TUNING_FREQUENCY = 4000000;

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
                if ((sensor.y + i >= 0 && sensor.y + i <= TUNING_FREQUENCY)
                    || (sensor.y - i >= 0 && sensor.y - i <= TUNING_FREQUENCY))
                {
                    var freeSpace = distance - i;
                    var min = Math.Min(sensor.x - freeSpace, sensor.x + freeSpace);
                    var max = Math.Max(sensor.x - freeSpace, sensor.x + freeSpace);
                    var newRange = (min, max);

                    if (freeSpaces.ContainsKey(sensor.y + i))
                        freeSpaces[sensor.y + i] = ConsolidateRanges(freeSpaces[sensor.y + i], newRange);
                    else
                        freeSpaces.Add(sensor.y + i, new() { newRange });

                    if (freeSpaces.ContainsKey(sensor.y - i))
                        freeSpaces[sensor.y - i] = ConsolidateRanges(freeSpaces[sensor.y - i], newRange);
                    else
                        freeSpaces.Add(sensor.y - i, new() { newRange });
                }
            }
        }

        var freeSpaceCount = freeSpaces[CHECK_ROW].Sum(r => r.max - r.min);
        Console.WriteLine($"Part 1 - In row {CHECK_ROW} there are {freeSpaceCount} where a beacon cannot be present");
        long tuningFrequency = 0;

        for (int i = 0; i <= TUNING_FREQUENCY; i++)
        {
            var beacon = GetAvailable(freeSpaces[i], (0, TUNING_FREQUENCY));

            if (beacon is not null)
                tuningFrequency = (long)beacon * TUNING_FREQUENCY + i;
        }

        Console.WriteLine($"Part 2 - The tuning frequency of the disress beacon is {tuningFrequency}");
    }

    private static int? GetAvailable(List<(int min, int max)> baseRanges, (int min, int max) searchRange)
    {
        var ranges = baseRanges.Where(b =>
            (b.min >= searchRange.min && b.max <= searchRange.max)
            || (b.min < searchRange.min && b.max < searchRange.max)
            || (b.min > searchRange.min && b.max > searchRange.max));

        foreach (var range in ranges)
        {
            searchRange.min = Math.Max(searchRange.min, range.min);
            searchRange.max = Math.Min(searchRange.max, range.max);
        }

        return (Math.Abs(searchRange.max - searchRange.min) == 2)
            ? Math.Min(searchRange.min, searchRange.max) + 1
            : null;
    }

    private static List<(int min, int max)> ConsolidateRanges(List<(int min, int max)> baseRanges, (int min, int max) newRange)
    {
        List<(int min, int max)> consolidatedRanges = new();
        var rangeHandled = false;

        if (
            baseRanges.Any(r =>
                newRange.min >= r.min && newRange.min <= r.max && newRange.max >= r.max)
            && baseRanges.Any(r =>
                newRange.max >= r.min && newRange.max <= r.max && newRange.min <= r.min))
        {
            var min = Math.Min(
                newRange.min,
                baseRanges
                    .Where(r =>
                        newRange.min >= r.min
                        && newRange.min <= r.max
                        && newRange.max >= r.max)
                    .Min(r => r.min));

            var max = Math.Max(
                newRange.max,
                baseRanges
                    .Where(r =>
                        newRange.max >= r.min
                        && newRange.max <= r.max
                        && newRange.min <= r.min)
                    .Max(r => r.max));

            consolidatedRanges.Add((min, max));
            rangeHandled = true;
        }

        if (!rangeHandled && baseRanges.Any(r => newRange.min >= r.min
            && newRange.min <= r.max
            && newRange.max > r.max))
        {
            consolidatedRanges.Add(
                (baseRanges
                    .Where(r =>
                        newRange.min >= r.min
                        && newRange.min <= r.max
                        && newRange.max > r.max)
                    .Min(r => r.min),
                newRange.max));

            rangeHandled = true;
        }

        if (!rangeHandled && baseRanges.Any(r => newRange.max >= r.min
            && newRange.max <= r.max
            && newRange.min < r.min))
        {
            consolidatedRanges.Add(
                (newRange.min,
                baseRanges
                    .Where(r =>
                        newRange.max >= r.min
                        && newRange.max <= r.max
                        && newRange.min < r.min)
                    .Max(r => r.max)));

            rangeHandled = true;
        }

        if (baseRanges.Any(r => newRange.min >= r.min && newRange.max <= r.max))
            rangeHandled = true;

        if (!rangeHandled)
            consolidatedRanges.Add(newRange);

        consolidatedRanges.AddRange(
            baseRanges.Where(r =>
                (newRange.min >= r.min && newRange.max <= r.max)
                || newRange.min > r.max
                || newRange.max < r.min));

        return consolidatedRanges.Any() ? consolidatedRanges : baseRanges;
    }
}
