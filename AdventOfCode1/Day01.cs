namespace AdventOfCode;

internal class Day01 : IDay
{
    private IEnumerable<int> CaloryList = Input.Day01.Split("\r\n\r\n")
        .Select(m => m.Split("\r\n")
        .Select(m => int.TryParse(m, out int calories) ? calories : 0)
        .Sum());

    public string Puzzle1() => CaloryList.Max().ToString();

    public string Puzzle2() => CaloryList.OrderByDescending(m => m).Take(3).Sum().ToString();
}