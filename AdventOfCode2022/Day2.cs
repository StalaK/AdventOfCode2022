namespace AdventOfCode2022;

internal static class Day2
{
    internal static void Run()
    {
        // https://adventofcode.com/2022/day/2
        var overallStrategy = File.ReadAllLines("inputs/Day2.txt");
        var rpsTotalScore = 0;
        var ldwTotalScore = 0;

        foreach (var game in overallStrategy)
        {
            if (string.IsNullOrEmpty(game))
                continue;

            var shapes = game?.Split(' ');

            if (shapes?.Length != 2)
                continue;

            if (!Enum.TryParse(typeof(HandScore), shapes[1], out var handScore))
                continue;

            rpsTotalScore += (int)handScore + (int)GetResult(shapes);

            if (!Enum.TryParse(typeof(GameResult), shapes[1], out var gameResult))
                continue;

            ldwTotalScore += (int)gameResult + (int)GetHandScore(shapes);
        }

        Console.WriteLine($"Part 1: Your total score with the XYZ = RockPaperScissors strategy would be: {rpsTotalScore}");
        Console.WriteLine($"Part 2: Your total score with the XYZ = LoseDrawWin strategy would be: {ldwTotalScore}");
    }

    private static GameResult GetResult(string[] shapes) => (opp: shapes[0], you: shapes[1]) switch
    {
        { opp: "A", you: "X" } => GameResult.Draw,
        { opp: "B", you: "Y" } => GameResult.Draw,
        { opp: "C", you: "Z" } => GameResult.Draw,

        { opp: "A", you: "Y" } => GameResult.Win,
        { opp: "B", you: "Z" } => GameResult.Win,
        { opp: "C", you: "X" } => GameResult.Win,

        { opp: "A", you: "Z" } => GameResult.Lose,
        { opp: "B", you: "X" } => GameResult.Lose,
        { opp: "C", you: "Y" } => GameResult.Lose,

        _ => throw new NotImplementedException()
    };

    private static HandScore GetHandScore(string[] shapes) => (opp: shapes[0], outcome: shapes[1]) switch
    {
        { opp: "A", outcome: "X" } => HandScore.Z,
        { opp: "B", outcome: "X" } => HandScore.X,
        { opp: "C", outcome: "X" } => HandScore.Y,

        { opp: "A", outcome: "Y" } => HandScore.X,
        { opp: "B", outcome: "Y" } => HandScore.Y,
        { opp: "C", outcome: "Y" } => HandScore.Z,

        { opp: "A", outcome: "Z" } => HandScore.Y,
        { opp: "B", outcome: "Z" } => HandScore.Z,
        { opp: "C", outcome: "Z" } => HandScore.X,

        _ => throw new NotImplementedException()
    };

    internal enum GameResult
    {
        Lose = 0,
        Draw = 3,
        Win = 6,

        X = 0,
        Y = 3,
        Z = 6
    }

    internal enum HandScore
    {
        X = 1,
        Y = 2,
        Z = 3
    }
}
