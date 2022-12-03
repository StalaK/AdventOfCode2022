using AdventOfCode2022;

char input = '0';

while (input != 'x')
{
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.Write("Day 1");

    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.Write("\tDay 2");

    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write("\tDay 3");

    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("\nEnter the day to run or x to exit: ");
    input = Console.ReadKey().KeyChar;

    Console.Clear();

    switch (input)
    {
        case '1':
            Day1.Run();
            break;

        case '2':
            Day2.Run();
            break;

        case '3':
            Day3.Run();
            break;

        case 'x':
            break;

        default:
            Console.WriteLine("Invalid input");
            break;
    }

    if (input != 'x')
    {
        Console.WriteLine("\nPress any key to continue");
        Console.ReadKey();
        Console.Clear();
    }
}
