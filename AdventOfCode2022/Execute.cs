using System.Reflection;

namespace AdventOfCode2022;

internal static partial class Execute
{
    internal static void Day(char day) => typeof(Execute)
        .GetMethod($"Day{day}", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy)?
        .Invoke(null, null);
}
