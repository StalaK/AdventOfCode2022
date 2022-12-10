using System.Reflection;

namespace AdventOfCode2022;

internal static class Execute
{
    internal static void Day(string day) => Type.GetType($"AdventOfCode2022.Day{day}")?
        .GetMethod("Execute", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy)?
        .Invoke(null, null);
}
