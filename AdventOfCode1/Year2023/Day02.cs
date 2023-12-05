
using AdventOfCode.Year2022;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode.Year2023
{
    internal class Day02 : IDay
    {
        public string Puzzle1() => Input.Day02.Split("\r\n")
                .Select((m, i) => IsPossible(m) ? i + 1 : 0)
                .Sum().ToString();

        private bool IsPossible(string input)
        {
            int red = 0;
            int green = 0;
            int blue = 0;
            
            foreach (var pull in input.Split(": ")[1].Split("; "))
            {
                foreach (var stone in pull.Split(", "))
                {
                    var values = stone.Split(" ");
                    switch (values[1])
                    {
                        case "blue":
                            blue =int.Max(blue, int.Parse(values[0]));
                            break;
                        case "red":
                            red = int.Max(red, int.Parse(values[0]));
                            break;
                        case "green":
                            green =int.Max(green, int.Parse(values[0]));
                            break;
                    }
                }

            }
            return red <= 12 && green <= 13 && blue <= 14;
        }
        private int RequiredStones(string input)
        {
            int red = 0;
            int green = 0;
            int blue = 0;

            foreach (var pull in input.Split(": ")[1].Split("; "))
            {
                foreach (var stone in pull.Split(", "))
                {
                    var values = stone.Split(" ");
                    switch (values[1])
                    {
                        case "blue":
                            blue = int.Max(blue, int.Parse(values[0]));
                            break;
                        case "red":
                            red = int.Max(red, int.Parse(values[0]));
                            break;
                        case "green":
                            green = int.Max(green, int.Parse(values[0]));
                            break;
                    }
                }

            }
            return red * green * blue;
        }

        public string Puzzle2() => Input.Day02.Split("\r\n")
                .Select((m, i) => RequiredStones(m))
                .Sum().ToString();
        

        private string ReplaceWords(string calibrationInput)
        {
            var calibration = calibrationInput.ToLower();
            var wordToNumber = new Dictionary<string, string>
            {
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6" },
                { "seven", "7" },
                { "eight", "8" },
                { "nine", "9" }
            };
            int index = 0;
            while (index < calibration.Length)
            {
                foreach (var word in wordToNumber)
                {
                    if (calibration.Substring(index).StartsWith(word.Key))
                    {
                        calibration = calibration.Substring(0, index) +  word.Value + calibration.Substring(index + 1);
                        index++;

                    }
                }
                index++;
            }
            return calibration;
        }
    }
}

