namespace AdventOfCode;

internal class Day03 : IDay
{
    private readonly string[] BagList = Input.Day03.Split("\r\n");

    public string Puzzle1() => BagList.Sum(Priority).ToString();

    public string Puzzle2()
    {
        int priority = 0;
        for (int i = 0; i < BagList.Length / 3; i++)
            priority += Priority2(BagList.Skip(i * 3).Take(3).ToArray());

        return priority.ToString();
    }

    private int Priority(string bag)
    {
        var compartment1 = bag.Take(bag.Length / 2);
        var compartment2 = bag.TakeLast(bag.Length / 2);
        foreach (var item in compartment1)
            if (compartment2.Contains(item))
                return PriorityScore(item);
        return 0;
    }

    private int Priority2(string[] bags)
    {
        foreach (var item in bags[0])
            if (bags[1].Contains(item) && bags[2].Contains(item))
                return PriorityScore(item);
        return 0;
    }

    private int PriorityScore(char item)
    {
        var ascii = (int)item;
        return ascii < 97 ? ascii - 38 : ascii - 96;
    }
}