using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode;

internal class Day12 : IDay
{
    private const int EndChar = (int)'E' - 97;
    private const int StartChar = (int)'S' - 97;
    private readonly int[][] DistanceMap;
    private readonly int[][] Map = Input.Day12.Split("\r\n").Select(x => x.ToCharArray().Select(c => (int)c - 97).ToArray()).ToArray();
    private (int x, int y) CurrentPosition;
    private (int x, int y) EndingPosition;
    private int Height;
    private (int x, int y) StartingPosition;
    private int Width;

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
                        Map[x][y] = (int)'a' - 97;
                        break;

                    case EndChar:
                        EndingPosition = (x, y);
                        Map[x][y] = (int)'z' - 97;
                        DistanceMap[x][y] = 0;
                        break;

                    default:
                        break;
                }
            }
        }
        CurrentPosition = EndingPosition;
    }

    public string Puzzle1()
    {
        Queue<(int x, int y)> PositionToCheck = new();
        ValidMoves(CurrentPosition).ToList().ForEach(m => PositionToCheck.Enqueue(m));

        while (PositionToCheck.TryDequeue(out var position))
        {
            var points = ValidMoves(position).ToList();
            if (points.Count == 0)
            {
                Console.WriteLine("DeadEnd");
            }
            foreach (var point in points)
            {
                if (!PositionToCheck.Contains(point))
                {
                    PositionToCheck.Enqueue(point);
                }
            }
        }
        PrintMap();

        return DistanceMap[StartingPosition.x][StartingPosition.y].ToString();
    }

    public string Puzzle2()
    {
        return "";
    }

    private void PrintMap((int x, int y) position)
    {
        for (int x = position.x - 1; x < position.x + 2; x++)
        {
            for (int y = position.y - 1; y < position.y + 2; y++)
                Console.Write($"{(char)(Map[x][y] + 97)} ({DistanceMap[x][y]}) ");
            Console.WriteLine();
        }
    }

    private void PrintMap()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
                Console.Write($"{Map[x][y].ToString("D2")}{(char)(Map[x][y] + 97)} ({DistanceMap[x][y].ToString("D4")}) ");
            Console.WriteLine();
        }
    }

    private IEnumerable<(int, int)> ValidMoves((int x, int y) position)
    {
        if (position == StartingPosition)
        {
            Console.WriteLine("found");
        }
        else
        {
            //PrintMap(position);
            int height = Map[position.x][position.y];
            int distance = DistanceMap[position.x][position.y];
            Console.WriteLine($"{height} {distance}");

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
                            //Console.WriteLine($"{position} => {test}");
                            yield return test;
                        }
        }
    }
}