using System.Text;

namespace AdventOfCode.Year2022;

internal class Day10 : IDay
{
    private const char LiteChar = (char)166;
    private readonly IEnumerable<string[]> Commands = Input.Day10.Split("\r\n").Select(x => x.Split(' '));
    private readonly Dictionary<int, int> RegistryHistory = new() { { 0, 1 } };
    private int Cycles = 0;

    public Day10() => Commands.ToList().ForEach(m => AddToHistory(m));

    public string Puzzle1() => $"{(RegistryHistory[20] * 20) +
            (RegistryHistory[60] * 60) +
            (RegistryHistory[100] * 100) +
            (RegistryHistory[140] * 140) +
            (RegistryHistory[180] * 180) +
            (RegistryHistory[220] * 220)}";

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
                RegistryHistory[Cycles + 3] = RegistryHistory[Cycles + 2] + int.Parse(command[1]);
                Cycles += 2;
                break;
        }
    }
}