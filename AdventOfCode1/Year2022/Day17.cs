using System.Runtime.InteropServices;

namespace AdventOfCode.Year2022;

internal class Day17 : IDay
{
    private List<(int X, int Y)> RockPosisiton = new List<(int, int)>();
    private const int Width = 7;
    private const int OffsetHeight = 4;

    private readonly Rock[] Rocks = new Rock[]
    {
        new Rock(new (int, int)[] { (0,0), (1,0), (2,0), (3,0) }, 4, 0),
        new Rock(new (int, int)[] { (0,1), (1,0), (1,1), (1,2), (2,1) }, 3, 2),
        new Rock(new (int, int)[] { (0,0), (1,0), (2,0), (2,1), (2,2) }, 3, 2),
        new Rock(new (int, int)[] { (0,0), (0,1), (0,2), (0,3) }, 1, 3),
        new Rock(new (int, int)[] { (0,0), (0,1), (1,0), (1,1) }, 2, 1),
    };

    public string Puzzle1() => StackRocks(2022);

    private string StackRocks(long runTimes)
    {
        ReadOnlySpan<char> jetDirection = Input.Day17.AsSpan();

        long rockCount = 0;
        int jetCount = 0;
        var yOffset = 4;
        long[] groupCount = new long[3]{ -1,-1,-1};
        long lastGroup = 0;
        long offsetCount = 0;
        long groupSize = 10000 * jetDirection.Length;
        while (rockCount < runTimes)
        {
            if (rockCount % groupSize == 0 && rockCount > 1)
            {
                int currentCount = RockPosisiton.Max(m => m.Y);
                groupCount[0] = groupCount[1];
                groupCount[1] = groupCount[2];
                groupCount[2] = currentCount - lastGroup;
                lastGroup = currentCount;
                if (groupCount[0] == groupCount[1] && groupCount[0] == groupCount[2])
                {
                    var numberOfDuplicates = runTimes - rockCount;
                    long duplicated = numberOfDuplicates / groupSize;
                    offsetCount += duplicated * groupCount[0];
                    rockCount = runTimes - (numberOfDuplicates % Convert.ToInt64(groupSize));
                    Console.WriteLine($"Duplication size {groupCount} found at {currentCount}");
                }
            }

            var rock = Rocks[rockCount % 5];
            var collide = false;
            var xOffset = 2;
            while (!collide)
            {
                jetCount %= jetDirection.Length;
                switch (jetDirection[jetCount % jetDirection.Length])
                {
                    case '<':
                        if (xOffset > 0 && !IsCollision(rock, xOffset - 1, yOffset))
                            xOffset--;
                        break;
                    case '>':
                        if (xOffset + rock.Width < Width && !IsCollision(rock, xOffset + 1, yOffset))
                            xOffset++;
                        break;
                }
                jetCount++;

                collide = IsCollision(rock, xOffset, yOffset - 1);
                if (!collide)
                    yOffset--;
                //PrintMap(yOffset, rock, xOffset);
                if (collide)
                {
                    foreach (var coord in rock.Shape)
                        RockPosisiton.Add((coord.X + xOffset, coord.Y + yOffset));
                    RockPosisiton = RockPosisiton.TakeLast(115).ToList();
                    yOffset = RockPosisiton.Max(m => m.Y) + OffsetHeight;
                }
            }
            rockCount++;
        }
        return (RockPosisiton.Max(m => m.Y) + offsetCount).ToString();
    }

    private bool IsCollision(Rock rock, int xOffset, int yOffset)
    {
        if (yOffset == 0)
            return true;
        if (rock.Shape.Any(m => RockPosisiton.Any(n => n.X == m.X + xOffset && n.Y == yOffset + m.Y)))
            return true;
        return false;
    }

    private void PrintMap(int yOffset, Rock rock, int xOffset)
    {
        for (int y = 2; y > -10; y--)
        {
            for (int x = 0; x < 7; x++)
            {
                var isBlock = rock.Shape.Any(m => m.X == x - xOffset && m.Y == y);
                var isGround = RockPosisiton.Any(m => m.X == x && m.Y == y + yOffset);

                if (y + yOffset == 0)
                    Console.Write("0");
                else if (isBlock && isGround)
                    Console.Write("X");
                else if (isBlock)
                    Console.Write("x");
                else if (isGround)
                    Console.Write("#");
                else
                    Console.Write(".");
            }
            Console.WriteLine();
            if (y + yOffset == 0)
                break;
        }
        Console.WriteLine();

    }

    public string Puzzle2()
    {
        return "too slow";
        //return StackRocks(1000000000000);
    }

    record struct Rock((int X, int Y)[] Shape, int Width, int Height);
}