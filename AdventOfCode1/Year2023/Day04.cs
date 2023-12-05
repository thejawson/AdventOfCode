
namespace AdventOfCode.Year2023
{
    internal class Day04 : IDay
    {
        private readonly List<(int[], int[])> cards;
        private readonly Dictionary<(int, int), long> NumberList = new();
        private static readonly bool UseTestDate = false;
        public Day04()
        {
            cards = UseTestDate
                ? Input.Day04Test.Split("\n").Select(x => ParseCards(x)).ToList()
                : Input.Day04.Split("\n").Select(x => ParseCards(x)).ToList();
        }

        private static (int[], int[]) ParseCards(string input)
        {
            var set = input.Replace("  ", " ").Split(": ")[1].Split(" | ");
            return (set[0].Split(' ').Select(m => int.Parse(m)).ToArray(), set[1].Split(' ').Select(m => int.Parse(m)).ToArray());
        }

        public string Puzzle1()
        {
            int sum = 0;
            foreach(var card in cards)
            {
                int count = card.Item1.Count(m => card.Item2.Any(n => m == n));
                sum += (int)Math.Pow(2, (count - 1));
            }
            
            return sum.ToString();
        }


        public string Puzzle2()
        {
            long sum = 0;
            Dictionary<int, int> copies = new Dictionary<int, int>();
            for (int i = 0; i < cards.Count; i++)
            {
                copies[i] = 1;
            }
            for (int i = 0; i < cards.Count; i++)
            {
                int count = cards[i].Item1.Count(m => cards[i].Item2.Any(n => m == n));
                sum += copies[i];
                for (int j = count + i; j > i; j--)
                {
                    copies[j] += copies[i];
                }
            }
            return sum.ToString();
        }

       
    }
}

