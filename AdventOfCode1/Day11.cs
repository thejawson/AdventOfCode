using System.Numerics;

namespace AdventOfCode;

internal class Day11 : IDay
{
    private long CommonFactor = 1;
    private readonly Dictionary<int, Monkey> Monkeys = new();

    public string Puzzle1() => RunInspections(20, true);

    public string Puzzle2() => RunInspections(10000, false);

    private void ResetData()
    {
        CommonFactor = 1;
        Monkeys.Clear();
        Input.Day11.Split("\r\n\r\n").Select(m => new Monkey(m)).ToList().ForEach(m => Monkeys[m.Id] = m);

        foreach (var monkey in Monkeys)
            CommonFactor *= monkey.Value.DivisibleBy;
    }

    private string RunInspections(int run, bool divideBy3)
    {
        ResetData();
        for (int i = 0; i < run; i++)
            foreach (int m in Monkeys.Keys)
                Monkeys[m].Inspect(divideBy3, CommonFactor, Monkeys);

        var monkeyBusiness = Monkeys.Select(m => m.Value.Inspections).OrderDescending().ToArray();
        return $"{new BigInteger(monkeyBusiness[0]) * new BigInteger(monkeyBusiness[1])}";
    }

    private class Monkey
    {
        public readonly int DivisibleBy;
        public readonly int Id;
        private readonly int Monkey1;
        private readonly int Monkey2;
        private readonly int OperationNumber;
        private readonly char Operator;
        public long Inspections = 0;
        public List<long> Items = new List<long>();

        public Monkey(string input)
        {
            var lines = input.Split("\r\n");
            Id = int.Parse(lines[0][7].ToString());
            Operator = lines[2][23];
            OperationNumber = int.TryParse(lines[2].Split(' ').Last(), out int i) ? i : -1;
            DivisibleBy = int.Parse(lines[3].Split(' ').Last());
            Monkey1 = int.Parse(lines[4].Split(' ').Last());
            Monkey2 = int.Parse(lines[5].Split(' ').Last());
            var itemList = lines[1].Split(": ")[1];
            if (itemList.Contains(", "))
                foreach (var item in itemList.Split(", ").Select(n => int.Parse(n)))
                    Items.Add(item);
            else
                Items.Add(int.Parse(itemList));
        }

        public void Inspect(bool lowerWorry, long factor, Dictionary<int, Monkey> monkeys)
        {
            var items = Items.ToArray();
            Inspections += items.Length;
            Items.Clear();
            foreach (var item in items)
            {
                long worry = item;
                //using -1 for when it does the action to the currenty Worry
                var number = OperationNumber == -1 ? worry : OperationNumber;
                switch (Operator)
                {
                    case '*':
                        worry *= number;
                        break;

                    case '+':
                        worry += number;
                        break;
                }
                if (lowerWorry)
                    worry /= 3;

                worry %= factor;
                monkeys[worry % DivisibleBy == 0 ? Monkey1 : Monkey2].Items.Add(worry);
            }
        }
    }
}