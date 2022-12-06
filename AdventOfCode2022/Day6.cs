namespace AdventOfCode2022;

internal static class Day6
{
    internal static void Execute()
    {
        // https://adventofcode.com/2022/day/6
        var signal = File.ReadAllLines("inputs/Day6.txt")
            .First()
            .ToCharArray();

        int packetMarkerPosition = 0;

        for (var i = 0; i < signal.Length - 4; i++)
        {
            var currentPacketMarkerCheck = signal[i..(i + 4)];
            var startFound = true;

            foreach (var c in currentPacketMarkerCheck)
            {
                if (currentPacketMarkerCheck.Count(m => m == c) > 1)
                {
                    startFound = false;
                    break;
                }
            }

            if (startFound)
            {
                packetMarkerPosition = i + 4;
                Console.WriteLine($"Part 1 - The first start-of-packet marker is detected at {packetMarkerPosition}");
                break;
            }
        }

        for (var i = packetMarkerPosition; i < signal.Length - 14; i++)
        {
            var currentMessageMarkerCheck = signal[i..(i + 14)];
            var startFound = true;

            foreach (var c in currentMessageMarkerCheck)
            {
                if (currentMessageMarkerCheck.Count(m => m == c) > 1)
                {
                    startFound = false;
                    break;
                }
            }

            if (startFound)
            {
                Console.WriteLine($"Part 2 - The first start-of-message marker is detected at {i + 14}");
                break;
            }
        }
    }
}