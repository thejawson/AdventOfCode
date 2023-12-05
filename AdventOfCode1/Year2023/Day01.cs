
using AdventOfCode.Year2022;

namespace AdventOfCode.Year2023
{
    internal class Day01 : IDay
    {
        public string Puzzle1() => Input.Day01.Split("\r\n")
                .Select(m => GetDigit(m.ToCharArray().Where(n => char.IsDigit(n)).ToArray()))
                .Sum().ToString();

        private int GetDigit(char[] numbers) => int.Parse($"{numbers.First()}{numbers.Last()}");

        public string Puzzle2()
        {
            return Input.Day01.Split("\r\n")
                .Select(m => ReplaceWords(m))
                .Select(m => GetDigit(m.ToCharArray().Where(n => char.IsDigit(n)).ToArray()))
                .Sum().ToString();

        }

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

