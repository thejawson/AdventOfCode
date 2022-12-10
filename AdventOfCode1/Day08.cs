namespace AdventOfCode;

internal class Day08 : IDay
{
    private readonly int Height = 0;
    private readonly int[][] ParseList = Input.Day08.Split("\r\n").Select(m => m.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
    private readonly int Width = 0;

    public Day08()
    {
        Width = ParseList.Length;
        Height = ParseList[0].Length;
    }

    public string Puzzle1()
    {
        int visibleTrees = 0;
        for (int i = 0; i < Width; i++)
            for (int n = 0; n < Height; n++)
                if (CheckPosition(i, n))
                    visibleTrees++;

        return visibleTrees.ToString();
    }

    public string Puzzle2()
    {
        int max = 0;
        for (int i = 0; i < Width; i++)
            for (int n = 0; n < Height; n++)
            {
                int height = ParseList[i][n];
                int value = ColumnCount(i, n, height) * RowCount(i, n, height);
                if (value > max)
                    max = value;
            }

        return max.ToString();
    }

    private bool CheckColumn(int xPosition, int treeHeight, int startPosition, int endPosition)
    {
        for (int i = startPosition; i < endPosition; i++)
            if (ParseList[xPosition][i] >= treeHeight)
                return false;

        return true;
    }

    private bool CheckPosition(int xPosition, int yPosition)
    {
        if (xPosition == 0 || yPosition == 0 || xPosition == Width - 1 || yPosition == Height - 1)
            return true;
        int treeHeight = ParseList[xPosition][yPosition];

        return CheckColumn(xPosition, treeHeight, 0, yPosition) ||
            CheckColumn(xPosition, treeHeight, yPosition + 1, Height) ||
            CheckRow(yPosition, treeHeight, 0, xPosition) ||
            CheckRow(yPosition, treeHeight, xPosition + 1, Height);
    }

    private bool CheckRow(int yPosition, int treeHeight, int startPosition, int endPosition)
    {
        for (int i = startPosition; i < endPosition; i++)
            if (ParseList[i][yPosition] >= treeHeight)
                return false;

        return true;
    }

    private int ColumnCount(int xPosition, int yPosition, int treeHeight)
    {
        int above = 0;
        if (yPosition > 0)
            for (int i = yPosition - 1; i >= 0; i--)
            {
                above++;
                if (ParseList[xPosition][i] >= treeHeight)
                    break;
            }

        int below = 0;
        if (yPosition < Width)
            for (int i = yPosition + 1; i < Width; i++)
            {
                below++;
                if (ParseList[xPosition][i] >= treeHeight)
                    break;
            }

        return above * below;
    }

    private int RowCount(int xPosition, int yPosition, int treeHeight)
    {
        int left = 0;
        if (xPosition > 0)
            for (int i = xPosition - 1; i >= 0; i--)
            {
                left++;
                if (ParseList[i][yPosition] >= treeHeight)
                    break;
            }
        int right = 0;
        if (xPosition < Width)
            for (int i = xPosition + 1; i < Width; i++)
            {
                right++;
                if (ParseList[i][yPosition] >= treeHeight)
                    break;
            }

        return right * left;
    }
}