using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Year2022;

internal class Day15 : IDay
{
    private IEnumerable<Sensor> Sensors = Input.Day15.Split("\r\n").Select(m => new Sensor(m.Split(':'))).ToArray();


    public string Puzzle1()
    {
        int count = 0;
        int y = 2000000;//2000000;//10 for test data
        int startingX = Sensors.Min(m => m.Position.x - m.Distance);
        int endingX = Sensors.Max(m => m.Position.x + m.Distance);

        for (int x = startingX; x < endingX; x++)
        {
            if (!Sensors.Any(m => (m.Beacon.x == x && m.Beacon.y == y)))
                if (Sensors.Any(m => m.IsCloser((x, y))))
                    count++;
        }
        return count.ToString();
    }

    public string Puzzle2()
    {
        //x 2638485
        // y 2650264
        foreach (var (x, y) in Sensors.SelectMany(m => m.Edges()))
        {
            if (!Sensors.Any(m => m.IsCloser((x, y))))
                return ((new BigInteger(x) * new BigInteger(4000000)) + new BigInteger(y)).ToString();
        }

        return "Not Found";
    }

    struct Sensor
    {
        public (int x, int y) Position;
        public (int x, int y) Beacon;
        public int Distance;

        public Sensor(string[] data)
        {
            Position = ParseLocation(data[0]);
            Beacon = ParseLocation(data[1]);
            Distance = Math.Abs(Position.x - Beacon.x) + Math.Abs(Position.y - Beacon.y);
        }

        private (int x, int y) ParseLocation(string data)
        {
            var position = data.Split(", y=");
            int x = int.Parse(position[0].Split('=').Last());
            int y = int.Parse(position[1]);
            return (x, y);

        }
        public IEnumerable<(int x, int y)> Edges()
        {
            const int maxX = 4000000;// 4000000;// 20;
            int x = Position.x - Distance - 1;
            int y = Position.y;
            int y2 = y;
            if (x > 0 && x < maxX)
            {
                if (y > 0 && y < maxX)
                    yield return (x, y);
            }
            for (int n = 0; n <= Distance * 2 + 1; n++)
            {
                x++;
                if (n > Distance / 2)
                {
                    y++;
                    y2--;
                }
                else
                {
                    y--;
                    y2++;
                }

                if (x > 0 && x < maxX)
                {
                    if (y > 0 && y < maxX)
                        yield return (x, y);
                    if (y2 > 0 && y2 < maxX)
                        yield return (x, y2);
                }
            }
        }

        public bool IsCloser((int x, int y) position)
        {
            return Distance >= Math.Abs(Position.x - position.x) + Math.Abs(Position.y - position.y);
        }
    }
}