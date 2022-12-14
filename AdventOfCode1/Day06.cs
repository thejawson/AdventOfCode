namespace AdventOfCode;

internal class Day06 : IDay
{
    public string Puzzle1() => NonRepeatingWork(4);

    public string Puzzle2() => NonRepeatingWork(14);

    private static string NonRepeatingWork(int length)
    {
        for (int i = length; i < Input.Day06.Length; i++)
            if (Input.Day06.Take(new Range(i - length, i)).Distinct().Count() == length)
                return (i + 1).ToString();

        return "not found";
    }
}