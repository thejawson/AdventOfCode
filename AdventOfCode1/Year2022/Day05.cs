namespace AdventOfCode.Year2022;

internal class Day05 : IDay
{
    private readonly IEnumerable<int[]> Instructions = Input.Day05Instructions.Split("\r\n")
        .Select(n => n.Split(',')
        .Select(m => int.TryParse(m, out int i) ? i - 1 : 0).ToArray());

    private readonly List<string[]> Stacks = Input.Day05Stacks.Split("\r\n").Select(m => m.Split(',')).ToList();

    public string Puzzle1()
    {
        foreach (var instruction in Instructions)
        {
            var toMove = Stacks[instruction[1]].Take(instruction[0] + 1).Reverse();
            Stacks[instruction[1]] = Stacks[instruction[1]].Skip(instruction[0] + 1).ToArray();
            Stacks[instruction[2]] = toMove.Concat(Stacks[instruction[2]]).ToArray();
        }

        return String.Join("", Stacks.Select(m => m.First()));
    }

    public string Puzzle2()
    {
        foreach (var instruction in Instructions)
        {
            var toMove = Stacks[instruction[1]].Take(instruction[0] + 1);
            Stacks[instruction[1]] = Stacks[instruction[1]].Skip(instruction[0] + 1).ToArray();
            Stacks[instruction[2]] = toMove.Concat(Stacks[instruction[2]]).ToArray();
        }

        return String.Join("", Stacks.Select(m => m.First()));
    }
}