using AdventOfCode.Year2022;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode.Year2023
{
    internal class Day02 : IDay
    {
        public string Puzzle1() => Input.Day02.Split("\n")
                .Select((m, i) => IsPossible(m) ? i + 1 : 0)
                .Sum().ToString();

        public string Puzzle2() => Input.Day02.Split("\n")
                .Select((m, i) => RequiredStones(m))
                .Sum().ToString();

        private static bool IsPossible(string input)
        {
            int red = 0;
            int green = 0;
            int blue = 0;

            foreach (var pull in input.Split(": ")[1].Split("; "))
            {
                foreach (var stone in pull.Split(", "))
                {
                    var stones = stone.Split(" ");
                    ParseStones(ref red, ref green, ref blue, stones);
                }
            }
            return red <= 12 && green <= 13 && blue <= 14;
        }

        private static void ParseStones(ref int red, ref int green, ref int blue, string[] stones)
        {
            switch (stones[1])
            {
                case "blue":
                    blue = int.Max(blue, int.Parse(stones[0]));
                    break;

                case "red":
                    red = int.Max(red, int.Parse(stones[0]));
                    break;

                case "green":
                    green = int.Max(green, int.Parse(stones[0]));
                    break;
            }
        }

        private static int RequiredStones(string input)
        {
            int red = 0;
            int green = 0;
            int blue = 0;

            foreach (var pull in input.Split(": ")[1].Split("; "))
            {
                foreach (var stone in pull.Split(", "))
                {
                    var stones = stone.Split(" ");
                    ParseStones(ref red, ref green, ref blue, stones);
                }
            }
            return red * green * blue;
        }
    }
}