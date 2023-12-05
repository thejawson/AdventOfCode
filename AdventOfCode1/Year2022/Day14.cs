using System.Runtime.CompilerServices;

namespace AdventOfCode.Year2022;

internal class Day14 : IDay
{
    private IEnumerable<string[]> Data = Input.Day14.Split("\r\n")
        .Select(m => m.Split(" -> "));
    private readonly char[,] Map;
    private readonly List<Rock> Rocks = new();
    private static int XOffset = 0;
    private int Width = 0;
    private int Height = 0;
    private int Floor = 0;

    private const int spawn = 500;

    public Day14()
    {
        foreach(var points in Data)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                Rocks.Add(new Rock(points[i], points[i+1]));
            }
        }
        var maxPoint = (Rocks.Max(m => m.MaxX), Rocks.Max(m => m.MaxY));
        var minPoint = (Rocks.Min(m => m.MinX), Rocks.Min(m => m.MinY));
        XOffset = minPoint.Item1-2;
        Width = maxPoint.Item1 - minPoint.Item1 + 5;
        Height = maxPoint.Item2;
        Floor = Height + 1;
        Map = new char[Width, Floor+1];
        Rocks.ForEach(r => r.AddToMap(Map));
    }

    private void PrintMap()
    {
        for (int y = 0; y < Floor+1; y++)
        {
            for (int x = 0; x < Width; x++)
                Console.Write(Map[x, y] == 0 ? '.' : Map[x, y]);
            Console.WriteLine();
        }
        for (int x = 0; x < Width; x++)
            Console.Write('#');
        Console.WriteLine();

    }
    public string Puzzle1()
    {
        var count = 0;
        bool run = true;
        (int x, int y) source = (spawn - XOffset, 0);
        while (run)
        {
            (int x, int y) position = source;
            while (run && Map[position.x, position.y] == 0)
            {
                if(position.y >= Height)
                    run = false;
                else  if (Map[position.x, position.y + 1] == 0)
                    position.y++;
                else if(position.x == 0 || position.x + 1 >= Width)
                    run = false;
                else if (Map[position.x - 1, position.y + 1] == 0)
                {
                    position.y++;
                    position.x--;
                }
                else if (Map[position.x+1, position.y + 1] == 0)
                {
                    position.y++;
                    position.x++;
                }
                else
                {
                    Map[position.x, position.y] = 's';
                    count++;
                }
            }
        }
        return count.ToString();
    }

    public string Puzzle2()
    {
        for (int y = 0; y < Floor + 1; y++)
            for (int x = 0; x < Width; x++)
                if (Map[x, y] == 's')
                    Map[x, y] = (char)0;
        var count = 0;
        (int x, int y) source = (spawn - XOffset, 0);
        while (Map[source.x, source.y] == 0)
        {
            (int x, int y) position = source;
            while (Map[position.x, position.y] == 0)
            {
                if (position.y >= Floor)
                {
                    Map[position.x, position.y] = 's';
                    count++;
                }
                else if (Map[position.x, position.y + 1] == 0)
                    position.y++;
                else if (position.x == 0 || position.x + 1 >= Width)
                {
                    count += Floor - position.y + 1;
                    Map[position.x, position.y] = 's';
                }
                
                else if (Map[position.x - 1, position.y + 1] == 0)
                {
                    position.y++;
                    position.x--;
                }
                else if (Map[position.x + 1, position.y + 1] == 0)
                {
                    position.y++;
                    position.x++;
                }
                else
                {
                    Map[position.x, position.y] = 's';
                    count++;
                }
            }
        }
        return count.ToString();
    }

    private class Rock
    {
        private (int x, int y) FromPoint;
        private (int x, int y) ToPoint;
        public int MaxX
        {
            get
            {
                return FromPoint.x> ToPoint.x? FromPoint.x: ToPoint.x;
            }
        }
        public int MinX
        {
            get
            {
                return FromPoint.x > ToPoint.x ? ToPoint.x: FromPoint.x;
            }
        }
        public int MaxY
        {
            get
            {
                return FromPoint.y > ToPoint.y ? FromPoint.y : ToPoint.y;
            }
        }
        public int MinY
        {
            get
            {
                return FromPoint.y > ToPoint.y ? ToPoint.y : FromPoint.y;
            }
        }

        public void AddToMap(char[,] map)
        {
            if (FromPoint.x == ToPoint.x)
                for (int i = MinY; i < MaxY + 1; i++)
                    map[FromPoint.x - Day14.XOffset, i] = '#';
            else
                for (int i = MinX; i < MaxX + 1; i++)
                    map[i - Day14.XOffset, FromPoint.y] = '#';
        }

        public Rock(string point1, string point2)
        {
            var points = point1.Split(',').Select(p=> int.Parse(p)).ToArray();
            FromPoint = (points[0], points[1]);
            points = point2.Split(',').Select(p => int.Parse(p)).ToArray();
            ToPoint = (points[0], points[1]);
        }
    }

}