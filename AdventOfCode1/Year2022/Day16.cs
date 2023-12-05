using System.Diagnostics;

namespace AdventOfCode.Year2022;

internal class Day16 : IDay
{
    private IEnumerable<string> Data = AdventOfCode.Input.Day16.Split("\r\n");
    private static Dictionary<string, Room> Rooms = new();

    private const int dept = 100000;

    public Day16()
    {
        foreach (var row in Data)
        {
            var room = new Room(row);
            Rooms[room.Name] = room;
        }
    }

    public string Puzzle1()
    {
        int minutes = 30;
        var pathsToCheck = new Dictionary<int, List<Path>>();
        pathsToCheck[minutes] = new List<Path>();
        var bestPath = new Path("AA");
        pathsToCheck[minutes].Add(bestPath);
        while (minutes > 0)
        {
            minutes--;
            pathsToCheck[minutes] = new List<Path>();
            foreach (var path in pathsToCheck[minutes + 1].OrderByDescending(m => m.Score).Take(dept))
            {
                if (bestPath.Score < path.Score)
                    bestPath = path;
                if (path.CanOpen())
                    pathsToCheck[minutes].Add(path.Open(minutes));
                foreach (var room in Rooms[path.Room].ConnectingRooms)
                    pathsToCheck[minutes].Add(path.Move(room));
            }
            pathsToCheck[minutes + 1].Clear();
        }
        return bestPath.Score.ToString();
    }

    public string Puzzle2()
    {
        int minutes = 26;
        var pathsToCheck = new Dictionary<int, List<Path>>();
        pathsToCheck[minutes] = new List<Path>();
        var bestPath = new Path("AA");
        pathsToCheck[minutes].Add(bestPath);
        while (minutes > 0)
        {
            minutes--;
            pathsToCheck[minutes] = new List<Path>();
            Queue<Path> queue = new Queue<Path>();
            foreach (var path in pathsToCheck[minutes + 1].OrderByDescending(m => m.Score).Take(dept))
            {
                if (bestPath.Score < path.Score)
                    bestPath = path;
                if (path.CanOpen())
                    queue.Enqueue(path.Open(minutes));
                foreach (var room in Rooms[path.Room].ConnectingRooms)
                    queue.Enqueue(path.Move(room));
            }
            pathsToCheck[minutes + 1].Clear();
            while (queue.TryDequeue(out var path))
            {
                if (path.CanElephanOpen())
                    pathsToCheck[minutes].Add(path.ElephantOpen(minutes));
                foreach (var room in Rooms[path.ElephantRoom].ConnectingRooms)
                    pathsToCheck[minutes].Add(path.ElephandMove(room));
            }
        }
        return bestPath.Score.ToString();
    }

    [DebuggerDisplay("{Room} {ElephantRoom}:{Score}")]
    private readonly struct Path
    {
        public readonly int Score = 0;
        public readonly string Room;
        public readonly string ElephantRoom;
        readonly string Opened = "";

        public Path(string room) : this(room, room, 0, "")
        {
        }

        public Path(string room, string elephantRoom, int score, string opened)
        {
            Room = room;
            ElephantRoom = elephantRoom;
            Score = score;
            Opened = opened;
        }

        public Path Open(int minutes) => new Path(Room, ElephantRoom, Score + (minutes * Rooms[Room].Rate), Opened + Room + minutes);
        public bool CanOpen() => Rooms[Room].Rate > 0 && !Opened.Contains(Room);
        public Path Move(string room) => new Path(room, ElephantRoom, Score, Opened);
        public Path ElephantOpen(int minutes) => new Path(Room, ElephantRoom, Score + (minutes * Rooms[ElephantRoom].Rate), Opened + ElephantRoom + minutes);
        public bool CanElephanOpen() => Rooms[ElephantRoom].Rate > 0 && !Opened.Contains(ElephantRoom);
        public Path ElephandMove(string room) => new Path(Room, room, Score, Opened);
    }

    private readonly struct Room
    {
        public readonly string Name;
        public readonly string[] ConnectingRooms;
        public readonly int Rate = 0;

        public Room(string input)
        {
            string[] data;
            if (input.Contains("valves"))
            {
                data = input.Split("; tunnels lead to valves ");
                ConnectingRooms = data[1].Split(", ");
            }
            else
            {
                data = input.Split("; tunnel leads to valve ");
                ConnectingRooms = new string[1] { data[1] };
            }
            Name = data[0].Substring(6, 2);
            Rate = int.Parse(data[0].Split("=")[1]);
        }
    }
}