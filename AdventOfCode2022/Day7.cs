namespace AdventOfCode2022;

internal static class Day7
{
    internal static void Execute()
    {
        // https://adventofcode.com/2022/day/6
        var consoleText = File.ReadAllLines("inputs/Day7.txt");
        Stack<string> directoryPath = new();
        List<(string path, int totalSize)> directorySizes = new();

        foreach (var line in consoleText)
        {
            var words = line.Split(' ');
            if (words[0] == "$")
            {
                if (words[1] == "cd")
                {
                    switch (words[2])
                    {
                        case "/":
                            directoryPath = new();
                            directoryPath.Push("/");
                            break;

                        case "..":
                            directoryPath.Pop();
                            break;

                        default:
                            directoryPath.Push(words[2]);
                            break;
                    }
                }

                continue;
            }

            if (words[0] == "dir")
                continue;

            var directoryFolders = directoryPath.Reverse().ToArray();

            for (var i = 0; i < directoryFolders.Length; i++)
            {
                var currentDirectory = string.Join("/", directoryFolders[..^i]);

                if (directorySizes.Any(d => d.path == currentDirectory))
                {
                    directorySizes = directorySizes
                        .Select(d =>
                        {
                            if (d.path == currentDirectory)
                                d.totalSize += int.Parse(words[0]);

                            return d;
                        })
                        .ToList();
                }
                else
                {
                    directorySizes.Add((currentDirectory, int.Parse(words[0])));
                }
            }
        }

        var totalSmallDirectorySize = directorySizes.Where(d => d.totalSize <= 100000).Sum(d => d.totalSize);
        Console.WriteLine($"Part 1 - The total size of all small directories is {totalSmallDirectorySize}");

        const int FILESYSTEM_SIZE = 70000000;
        const int SPACE_NEEDED = 30000000;
        var unusedSpace = FILESYSTEM_SIZE - directorySizes.First(d => d.path == "/").totalSize;
        var amountToBeDeleted = SPACE_NEEDED - unusedSpace;

        var sizeOfDirectoryToDelete = directorySizes.Where(d => d.totalSize >= amountToBeDeleted).Min(d => d.totalSize);
        Console.WriteLine($"Part 2 - The total size of the smallest directory to delete is {sizeOfDirectoryToDelete}");
    }
}
