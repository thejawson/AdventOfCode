namespace AdventOfCode;

internal class Day02 : IDay
{
    private readonly string[] GameList = Input.Day02.Split("\r\n");

    public string Puzzle1() => GameList.Sum(m => Score(m)).ToString();

    public string Puzzle2() => GameList.Sum(m => Score2(m)).ToString();

    private int Score(ReadOnlySpan<char> thrown)
    {
        return (thrown[0], thrown[2])
            switch
        {
            ('A', 'X') => 4,
            ('B', 'X') => 1,
            ('C', 'X') => 7,
            ('A', 'Y') => 8,
            ('B', 'Y') => 5,
            ('C', 'Y') => 2,
            ('A', 'Z') => 3,
            ('B', 'Z') => 9,
            ('C', 'Z') => 6,
            _ => 0,
        };
    }

    private int Score2(ReadOnlySpan<char> thrown)
    {
        return (thrown[0], thrown[2])
            switch
        {
            ('A', 'X') => 3,
            ('B', 'X') => 1,
            ('C', 'X') => 2,
            ('A', 'Y') => 4,
            ('B', 'Y') => 5,
            ('C', 'Y') => 6,
            ('A', 'Z') => 8,
            ('B', 'Z') => 9,
            ('C', 'Z') => 7,
            _ => 0,
        };
    }
}