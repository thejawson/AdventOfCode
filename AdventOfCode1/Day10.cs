using System.Text;

namespace AdventOfCode;
class Day10 : IDay
{
    private readonly IEnumerable<string[]> Commands = Input.Split("\r\n").Select(x => x.Split(' '));
    private int Cycles = 0;
    private readonly Dictionary<int, int> RegistryHistory = new() { { 0, 1 } };
    private const char LiteChar = (char)166;

    public Day10() => Commands.ToList().ForEach(m => AddToHistory(m));
    
    public string Puzzle1() => $"{(RegistryHistory[20] * 20) +
            (RegistryHistory[60] * 60) +
            (RegistryHistory[100] * 100) +
            (RegistryHistory[140] * 140) +
            (RegistryHistory[180] * 180) +
            (RegistryHistory[220] * 220)}";

    private void AddToHistory(string[] command)
    {
        switch (command[0])
        {
            case "noop":
                Cycles++;
                if (!RegistryHistory.ContainsKey(Cycles))
                    RegistryHistory[Cycles] = RegistryHistory[Cycles - 1];
                break;
            case "addx":
                if (!RegistryHistory.ContainsKey(Cycles + 1))
                    RegistryHistory[Cycles + 1] = RegistryHistory[Cycles];
                RegistryHistory[Cycles + 2] = RegistryHistory[Cycles + 1];
                RegistryHistory[Cycles + 3] = RegistryHistory[Cycles+2] + int.Parse(command[1]);
                Cycles += 2;
                break;
        }
    }

    public string Puzzle2()
    {
        var output = new StringBuilder();
        output.AppendLine();
        for (int pixel = 1; pixel < RegistryHistory.Count(); pixel++)
        {
            int pixelPosition = pixel % 40;
            if (RegistryHistory[pixel] <= pixelPosition && RegistryHistory[pixel] + 2 >= pixelPosition)
                output.Append(LiteChar);
            else
                output.Append(" ");
            if (pixelPosition == 0)
                output.AppendLine();
        }
        return output.ToString();
    }

    private const string Input = @"noop
noop
noop
addx 4
addx 3
addx 3
addx 3
noop
addx 2
addx 1
addx -7
addx 10
addx 1
addx 5
addx -3
addx -7
addx 13
addx 5
addx 2
addx 1
addx -30
addx -8
noop
addx 3
addx 2
addx 7
noop
addx -2
addx 5
addx 2
addx -7
addx 8
addx 2
addx 5
addx 2
addx -12
noop
addx 17
addx 3
addx -2
addx 2
noop
addx 3
addx -38
noop
addx 3
addx 4
noop
addx 5
noop
noop
noop
addx 1
addx 2
addx 5
addx 2
addx -3
addx 4
addx 2
noop
noop
addx 7
addx -30
addx 31
addx 4
noop
addx -24
addx -12
addx 1
addx 5
addx 5
noop
noop
noop
addx -12
addx 13
addx 4
noop
addx 23
addx -19
addx 1
addx 5
addx 12
addx -28
addx 19
noop
addx 3
addx 2
addx 5
addx -40
addx 4
addx 32
addx -31
noop
addx 13
addx -8
addx 5
addx 2
addx 5
noop
noop
noop
addx 2
addx -7
addx 8
addx -7
addx 14
addx 3
addx -2
addx 2
addx 5
addx -40
noop
noop
addx 3
addx 4
addx 1
noop
addx 2
addx 5
addx 2
addx 21
noop
addx -16
addx 3
noop
addx 2
noop
addx 1
noop
noop
addx 4
addx 5
noop
noop
noop
noop
noop
noop
noop";
}
