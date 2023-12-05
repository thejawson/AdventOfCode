namespace AdventOfCode.Year2022;

internal class Day04 : IDay
{
    private readonly IEnumerable<string[]> ElfRange = Input.Day04.Split("\r\n").Select(m => m.Split(',')).ToList();

    public string Puzzle1() => ElfRange.Count(m => IsDuplicate(m)).ToString();

    public string Puzzle2() => ElfRange.Count(m => IsOverlap(m)).ToString();

    private static bool IsBetween(int between, int start, int end) => between >= start && between <= end;

    private bool IsDuplicate(string[] assignment)
    {
        ParseElfs(assignment, out var elf1, out var elf2);
        return (IsBetween(elf1[0], elf2[0], elf2[1]) && IsBetween(elf1[1], elf2[0], elf2[1]))
            || (IsBetween(elf2[0], elf1[0], elf1[1]) && IsBetween(elf2[1], elf1[0], elf1[1]));
    }

    private bool IsOverlap(string[] assignment)
    {
        ParseElfs(assignment, out var elf1, out var elf2);
        return IsBetween(elf1[0], elf2[0], elf2[1]) || IsBetween(elf1[1], elf2[0], elf2[1])
            || IsBetween(elf2[0], elf1[0], elf1[1]) || IsBetween(elf2[1], elf1[0], elf1[1]);
    }

    private static void ParseElfs(string[] assignment, out int[] elf1, out int[] elf2)
    {
        elf1 = assignment[0].Split("-").Select(m => int.TryParse(m, out int results) ? results : 0).ToArray();
        elf2 = assignment[1].Split("-").Select(m => int.TryParse(m, out int results) ? results : 0).ToArray();
    }
}