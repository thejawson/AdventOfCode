using System.Linq;
using System.Numerics;

namespace AdventOfCode;

internal class Day11 : IDay
{
    private static int CommonFactor = 1;
    private readonly List<Item> Items = new List<Item>();
    private readonly Dictionary<int, int> Inspections = new Dictionary<int, int>();
    private readonly List<Monkey> Monkeys = new();

    public Day11() => ResetData();

    public string Puzzle1() => RunInspections(20, true);

    public string Puzzle2()
    {
        ResetData();
        return RunInspections(10000, false);
    }

    private void ResetData()
    {
        Items.Clear();
        Inspections.Clear();
        CommonFactor = 1;
        Monkeys.Clear();
        Monkeys.AddRange(Input.Day11.Split("\r\n\r\n").Select(m => new Monkey(m, Items)).ToList());
        foreach (var monkey in Monkeys)
        {
            Inspections.Add(monkey.Id, 0);
            CommonFactor *= monkey.DivisibleBy;
        }
    }

    private string RunInspections(int run, bool divideBy3)
    {
        for (int i = 0; i < run; i++)
            foreach (var monkey in Monkeys)
                foreach (var item in Items.Where(m => m.Monkey == monkey.Id))
                {
                    Inspections[monkey.Id]++;
                    monkey.Inspect(item, divideBy3);
                }

        var monkeyBusiness = Inspections.Select(m => m.Value).OrderDescending().ToArray();
        return $"{new BigInteger(monkeyBusiness[0]) * new BigInteger(monkeyBusiness[1])}";
    }

    private class Item
    {
        public int Monkey;
        public long Worry;

        public Item(int monkey, long worry)
        {
            Worry = worry;
            Monkey = monkey;
        }
    }

    private class Monkey
    {
        public readonly int DivisibleBy;
        public readonly int Id;
        private readonly int Monkey1;
        private readonly int Monkey2;
        private readonly int OperationNumber;
        private readonly char Operator;

        public Monkey(string input, List<Item> items)
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
                    items.Add(new Item(Id, item));
            else
                items.Add(new Item(Id, int.Parse(itemList)));
        }

        public void Inspect(Item item, bool lowerWorry)
        {
            //using -1 for when it does the action to the currenty Worry
            var number = OperationNumber == -1 ? item.Worry : OperationNumber;
            switch (Operator)
            {
                case '*':
                    item.Worry *= number;
                    break;

                case '+':
                    item.Worry += number;
                    break;
            }

            if (lowerWorry)
                item.Worry /= 3;

            item.Worry = item.Worry % CommonFactor;

            item.Monkey = item.Worry % DivisibleBy == 0 ? Monkey1 : item.Monkey = Monkey2;
        }
    }
}