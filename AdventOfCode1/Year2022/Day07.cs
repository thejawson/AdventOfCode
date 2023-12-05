namespace AdventOfCode.Year2022;

internal class Day07 : IDay
{
    private readonly Folder Root = new("root");

    private Folder Current;

    public Day07()
    {
        Current = Root;
        foreach (string line in Input.Day07.Split("\r\n"))
        {
            ReadOnlySpan<string> command = line.Split(' ');
            switch (command[0])
            {
                case "$":
                    ExecuteCommand(command);
                    break;

                case "dir":
                    break;

                default:
                    Current.Files.Add(int.TryParse(command[0], out int s) ? s : 0);
                    break;
            }
        }
    }

    public string Puzzle1() => Root.SmallFoldersSize().ToString();

    public string Puzzle2() => Root.FindFolderToRemove().ToString();

    private void ExecuteCommand(ReadOnlySpan<string> command)
    {
        switch (command[1])
        {
            case "ls":
                break;

            case "cd":
                if (command[2] == "..")
                    Current = Current?.Parent ?? Root;
                else
                {
                    var folder = new Folder(command[2], Current);
                    Current.Folders.Add(folder);
                    Current = folder;
                }
                break;
        }
    }

    private class Folder
    {
        public const int LargeFolderSize = 100000;
        public const int SpaceNeeded = 30000000;
        public const int TotalSpace = 70000000;
        public readonly List<int> Files = new();
        public readonly List<Folder> Folders = new();
        public Folder? Parent;
        private string _folderName = "";

        public Folder(string name, Folder parent)
        {
            Name = name;
            Parent = parent;
        }

        public Folder(string name) => Name = name;

        public string Name
        {
            get
            {
                if (Parent is null)
                    return "";
                return $"{Parent.Name}/{_folderName}";
            }
            set => _folderName = value;
        }

        public int Size => Files.Sum() + Folders.Sum(m => m.Size);

        public int FindFolderToRemove(int spaceNeeded = 0)
        {
            if (spaceNeeded == 0)
                spaceNeeded = SpaceNeeded - (TotalSpace - Size);

            if (Folders.Any(m => m.Size >= spaceNeeded))
                return Folders.Where(m => m.Size >= spaceNeeded).Select(m => m.FindFolderToRemove(spaceNeeded)).Min();
            else
                return Size;
        }

        public int SmallFoldersSize()
        {
            int totalSize = 0;
            foreach (Folder folder in Folders)
            {
                if (folder.Size <= LargeFolderSize)
                    totalSize += folder.Size;
                totalSize += folder.SmallFoldersSize();
            }

            return totalSize;
        }
    }
}