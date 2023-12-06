namespace AdventOfCode.Year2023
{
    internal class Day05 : IDay
    {
        private static readonly bool UseTestDate = false;
        private readonly List<(int[], int[])> cards;
        public Day05()
        {
            var input = UseTestDate
                ? Input.Day05Test.Split("\n\n").ToList()
                : Input.Day05.Split("\n\n").ToList();
        }

        public string Puzzle1()
        {
            int sum = 0;
            foreach (var card in cards)
                sum += (int)Math.Pow(2, (card.Item1.Count(m => card.Item2.Any(n => m == n)) - 1));

            return sum.ToString();
        }

        public string Puzzle2()
        {
            long sum = 0;
            Dictionary<int, int> copies = new();
            for (int i = 0; i < cards.Count; i++)
                copies[i] = 1;
            for (int i = 0; i < cards.Count; i++)
            {
                int count = cards[i].Item1.Count(m => cards[i].Item2.Any(n => m == n));
                sum += copies[i];
                for (int j = count + i; j > i; j--)
                    copies[j] += copies[i];
            }
            return sum.ToString();
        }

        private static (int[], int[]) ParseCards(string input)
        {
            var set = input.Replace("  ", " ").Split(": ")[1].Split(" | ");
            return (set[0].Split(' ').Select(m => int.Parse(m)).ToArray(), set[1].Split(' ').Select(m => int.Parse(m)).ToArray());
        }
    }
}