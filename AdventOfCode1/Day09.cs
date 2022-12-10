namespace AdventOfCode;

internal class Day09 : IDay
{
    private readonly List<string> Lines = Input.Day09.Split("\r\n").ToList();
    private readonly HashSet<string> TailLocations = new();
    private (int X, int Y) HeadPosition = (0, 0);
    private (int X, int Y)[] Segments = new (int X, int Y)[9];
    private (int X, int Y) TailPosition = (0, 0);

    public string Puzzle1()
    {
        Lines.ForEach(m => Move(m.Split(" ")));
        return TailLocations.Count.ToString();
    }

    public string Puzzle2()
    {
        TailLocations.Clear();
        Lines.ForEach(m => Move(m.Split(" "), true));
        return TailLocations.Count.ToString();
    }

    private void Move(string[] action, bool useKnots = false)
    {
        for (int i = 0; i < int.Parse(action[1]); i++)
        {
            switch (action[0])
            {
                case "L":
                    HeadPosition.X--;
                    break;

                case "R":
                    HeadPosition.X++;
                    break;

                case "U":
                    HeadPosition.Y++;
                    break;

                case "D":
                    HeadPosition.Y--;
                    break;
            }

            if (useKnots)
            {
                var lastPosition = HeadPosition;
                for (int segment = 0; segment < Segments.Length - 1; segment++)
                {
                    Segments[segment] = MoveSegment(lastPosition, Segments[segment]);
                    lastPosition = Segments[segment];
                }
                TailPosition = MoveSegment(lastPosition, TailPosition);
            }
            else
                TailPosition = MoveSegment(HeadPosition, TailPosition);

            if (!TailLocations.Contains(TailPosition.ToString()))
                TailLocations.Add(TailPosition.ToString());
        }
    }

    private (int, int) MoveSegment((int X, int Y) leadSegment, (int X, int Y) trailingSegment)
    {
        if (Math.Abs(leadSegment.X - trailingSegment.X) > 1 || Math.Abs(leadSegment.Y - trailingSegment.Y) > 1)
        {
            if (leadSegment.X > trailingSegment.X)
                trailingSegment.X++;
            else if (leadSegment.X < trailingSegment.X)
                trailingSegment.X--;

            if (leadSegment.Y > trailingSegment.Y)
                trailingSegment.Y++;
            else if (leadSegment.Y < trailingSegment.Y)
                trailingSegment.Y--;
        }

        return trailingSegment;
    }
}