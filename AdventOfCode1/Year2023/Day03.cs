namespace AdventOfCode.Year2023
{
    internal class Day03 : IDay
    {
        private static readonly bool UseTestDate = false;
        private readonly char[][] map;
        private readonly Dictionary<(int, int), long> NumberList = new();

        public Day03()
        {
            map = UseTestDate
                ? Input.Day03Test.Split("\n").Select(x => x.ToCharArray()).ToArray()
                : Input.Day03.Split("\n").Select(x => x.ToCharArray()).ToArray();
        }

        public string Puzzle1()
        {
            bool isPart = false;
            bool isLabeled = false;
            string number = string.Empty;
            int sum = 0;
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (char.IsDigit(map[i][j]))
                    {
                        number += map[i][j];
                        isPart = true;
                        isLabeled = isLabeled || CheckLabel(i, j);
                    }
                    else
                    {
                        if (int.TryParse(number, out int num))
                        {
                            if (isLabeled && isPart)
                                sum += num;
                            for (int pos = 1; pos <= number.Length; pos++)
                                if (!NumberList.ContainsKey((i, j - pos)))
                                    NumberList.Add((i, j - pos), num);
                        }
                        number = string.Empty;
                        isPart = false;
                        isLabeled = false;
                    }
                }
                var j2 = map[i].Length - 1;
                if (int.TryParse(number, out int num1))
                    for (int pos = 0; pos < number.Length; pos++)
                        if (!NumberList.ContainsKey((i, j2 - pos)))
                            NumberList.Add((i, j2 - pos), num1);
                number = string.Empty;
                isPart = false;
                isLabeled = false;
            }
            return sum.ToString();
        }

        public string Puzzle2()
        {
            long sum = 0;
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                    if (map[i][j] == '*')
                    {
                        long gearMultiplier = 1;
                        var foundCount = 0;
                        if (!CheckPosition(i - 1, j, ref gearMultiplier, ref foundCount))
                        {
                            CheckPosition(i - 1, j - 1, ref gearMultiplier, ref foundCount);
                            CheckPosition(i - 1, j + 1, ref gearMultiplier, ref foundCount);
                        }

                        if (!CheckPosition(i + 1, j, ref gearMultiplier, ref foundCount))
                        {
                            CheckPosition(i + 1, j - 1, ref gearMultiplier, ref foundCount);
                            CheckPosition(i + 1, j + 1, ref gearMultiplier, ref foundCount);
                        }

                        CheckPosition(i, j - 1, ref gearMultiplier, ref foundCount);
                        CheckPosition(i, j + 1, ref gearMultiplier, ref foundCount);

                        if (foundCount == 2)
                        {
                            sum += gearMultiplier;
                        }
                    }
            }
            return sum.ToString();
        }

        private bool CheckLabel(int i, int j) => IsLabel(i - 1, j - 1) || IsLabel(i - 1, j) || IsLabel(i - 1, j + 1) || IsLabel(i, j - 1) || IsLabel(i, j + 1)
                || IsLabel(i + 1, j - 1) || IsLabel(i + 1, j) || IsLabel(i + 1, j + 1);

        private bool CheckPosition(int i, int j, ref long gearMultiplier, ref int foundCount)
        {
            if (foundCount < 3 && NumberList.ContainsKey((i, j)))
            {
                gearMultiplier *= NumberList[(i, j)];
                foundCount++;
                return true;
            }
            return false;
        }

        private bool IsLabel(int i, int j)
        {
            if (i > 0 && i < map.Length && j > 0 && j < map[i].Length)
                return !(map[i][j] == '.' || char.IsDigit(map[i][j]));
            return false;
        }
    }
}