namespace AdventOfCode.Year2022;

internal class Day18 : IDay
{
    private IEnumerable<Coordinate> Coords = Input.Day18.Split("\r\n").Select(m => m.Split(",")).Select(m => new Coordinate(int.Parse(m[0]), int.Parse(m[1]), int.Parse(m[2])));

    public string Puzzle1() => Coords.SelectMany(m => ConnectedCoords(m)).Count(m => !Coords.Any(n => m == n)).ToString();

    private IEnumerable<Coordinate> ConnectedCoords(Coordinate coords)
    {
        return new Coordinate[]
        {
            new Coordinate(coords.X + 1, coords.Y, coords.Z),
            new Coordinate(coords.X-1, coords.Y, coords.Z),
            new Coordinate(coords.X, coords.Y+1, coords.Z),
            new Coordinate(coords.X, coords.Y-1, coords.Z),
            new Coordinate(coords.X, coords.Y, coords.Z+1),
            new Coordinate(coords.X, coords.Y, coords.Z - 1),
        };
    }

    public string Puzzle2()
    {
        return "";
    }
    private record Coordinate(int X, int Y, int Z);
}