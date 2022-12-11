using System.Linq;

namespace AdventOfCode;

internal class Day11 : IDay
{
    private readonly List<Item> Items = new List<Item>();
    private readonly List<Monkey> Monkeys;
    private Dictionary<int, int> inspections = new Dictionary<int, int>();

    public Day11()
    {
        Monkeys = Input.Day11.Split("\r\n\r\n").Select(m => new Monkey(m, Items)).ToList();
        foreach (var monkey in Monkeys)
            inspections.Add(monkey.Id, 0);
    }

    public string Puzzle1()
    {
        for (int i = 0; i < 20; i++)
            foreach (var monkey in Monkeys)
                foreach (var item in Items.Where(m => m.Monkey == monkey.Id))
                {
                    inspections[monkey.Id]++;
                    monkey.Inspect(item);
                }
        var monkeyBusiness = inspections.Select(m => m.Value).OrderDescending().ToArray();
        return $"{monkeyBusiness[0] * monkeyBusiness[1]}";
    }

    public string Puzzle2()
    {
        return "";
    }

    private void PrintItems()
    {
        foreach (Item item in Items.OrderBy(m => m.Monkey).ThenBy(m => m.Worry))
            Console.WriteLine(item.ToString());
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

        public new string ToString() => $"{Monkey} {Worry}";
    }

    private class Monkey
    {
        public readonly int Id;

        private readonly int DivisibleBy;
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

        public void Inspect(Item item)
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

                default:
                    break;
            }
            item.Worry /= 3;

            if (item.Worry % DivisibleBy == 0)
                item.Monkey = Monkey1;
            else
                item.Monkey = Monkey2;
        }
    }
}