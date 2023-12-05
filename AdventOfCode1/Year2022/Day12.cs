namespace AdventOfCode.Year2022;

internal class Day12 : IDay
{
    private const int EndChar = (int)'E';
    private const int LowestChar = (int)'a';
    private const int StartChar = (int)'S';
    private readonly int[][] DistanceMap;
    private readonly (int x, int y) EndingPosition;
    private readonly int Height;
    private readonly int[][] Map = Input.Day12.Split("\r\n").Select(x => x.ToCharArray().Select(c => (int)c).ToArray()).ToArray();
    private readonly (int x, int y) StartingPosition;
    private readonly int Width;

    public Day12()
    {
        Width = Map.Length;
        Height = Map[0].Length;
        DistanceMap = new int[Width][];
        var max = Width * Height;
        for (int x = 0; x < Width; x++)
        {
            DistanceMap[x] = new int[Height];
            for (int y = 0; y < Height; y++)
            {
                DistanceMap[x][y] = max;
                switch (Map[x][y])
                {
                    case StartChar:
                        StartingPosition = (x, y);
                        Map[x][y] = (int)'a';
                        break;

                    case EndChar:
                        EndingPosition = (x, y);
                        Map[x][y] = (int)'z';
                        DistanceMap[x][y] = 0;
                        break;

                    default:
                        break;
                }
            }
        }
    }

    public string Puzzle1()
    {
        Queue<(int x, int y)> PositionsToCheck = new();
        PositionsToCheck.Enqueue(EndingPosition);

        while (PositionsToCheck.TryDequeue(out var position))
            foreach (var point in ValidMoves(position).ToList())
                if (!PositionsToCheck.Contains(point))
                    PositionsToCheck.Enqueue(point);

        return DistanceMap[StartingPosition.x][StartingPosition.y].ToString();
    }

    public string Puzzle2()
    {
        int minDistance = int.MaxValue;
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                if (Map[x][y] == LowestChar)
                    if (minDistance > DistanceMap[x][y])
                        minDistance = DistanceMap[x][y];

        return minDistance.ToString();
    }

    private IEnumerable<(int, int)> ValidMoves((int x, int y) position)
    {
        int height = Map[position.x][position.y];
        int distance = DistanceMap[position.x][position.y];

        var positionsToTest = new (int x, int y)[4]
        {
            (position.x, position.y + 1),
            (position.x, position.y - 1),
            (position.x + 1, position.y),
            (position.x - 1, position.y),
        };
        foreach (var test in positionsToTest)
            if (test.x > -1 && test.x < Width && test.y > -1 && test.y < Height)
                if (Map[test.x][test.y] + 1 >= height)
                    if (DistanceMap[test.x][test.y] - distance > 1)
                    {
                        DistanceMap[test.x][test.y] = distance + 1;
                        yield return test;
                    }
    }
}