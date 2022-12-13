using AdventOfCode2022;

string input;

do
{
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.Write("Day 1");

    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.Write("\tDay 2");

    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write("\tDay 3");

    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.Write("\tDay 4");

    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.Write("\tDay 5");

    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.Write("\nDay 6");

    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.Write("\tDay 7");

    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.Write("\tDay 8");

    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write("\tDay 9");

    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.Write("\tDay 10");

    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.Write("\nDay 11");

    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.Write("\tDay 12");

    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.Write("\tDay 13");

    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("\nEnter the day to run or x to exit: ");
    input = Console.ReadLine() ?? string.Empty;

    Console.Clear();

    Execute.Day(input);

    if (input != "x")
    {
        Console.WriteLine("\nPress any key to continue");
        Console.ReadKey();
        Console.Clear();
    }

} while (input != "x");
