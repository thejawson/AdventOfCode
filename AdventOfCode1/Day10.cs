using System.Linq;
using System.Text;

namespace AdventOfCode;
class Day10 : IDay
{
    private IEnumerable<string[]> Commands = Input.Split("\r\n").Select(x => x.Split(' '));
    private int Value = 1;
    private int Cycles = 0;
    private Dictionary<int, int> CommandHistory = new();

    public string Puzzle1()
    {
        CommandHistory[0] = Value;
        foreach (var command in Commands)
        {
            switch (command[0])
            {
                case "noop":
                    Cycles++;
                    if (!CommandHistory.ContainsKey(Cycles))
                    {
                        CommandHistory[Cycles] = CommandHistory[Cycles - 1];
                    }
                    break;
                case "addx":
                    if (!CommandHistory.ContainsKey(Cycles + 1))
                    {
                        CommandHistory[Cycles + 1] = Value;
                    }
                    CommandHistory[Cycles + 2] = Value;

                    Value += int.Parse(command[1]);
                    Cycles += 2;
                    CommandHistory[Cycles + 1] = Value;

                    break;
            }
        }
        return $"{(CommandHistory[20] * 20)
            + (CommandHistory[60] * 60)
            + (CommandHistory[100] * 100)
            + (CommandHistory[140] * 140)
            + (CommandHistory[180] * 180)
            + (CommandHistory[220] * 220)}";
    }

    public string Puzzle2()
    {
        var output = new StringBuilder();
        output.AppendLine();
        int offset = 0;
        for (int pixel = 1; pixel < CommandHistory.Count(); pixel++)
        {
            int pixelPosition = pixel - offset;
            if (CommandHistory[pixel] <= pixelPosition && CommandHistory[pixel] + 2 >= pixelPosition)
                output.Append("#");
            else
                output.Append(".");
            if (pixel > 0 && pixel % 40 == 0)
            {
                output.AppendLine();
                offset += 40;
            }
        }
        return output.ToString();
    }

    const string Input = @"noop
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
