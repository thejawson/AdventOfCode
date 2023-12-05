namespace AdventOfCode.Year2022;

//Code created by https://chat.openai.com/chat
internal class Day01AI : IDay
{
    string IDay.Puzzle1()
    {
        // Create a dictionary to store the calorie counts for each elf
        Dictionary<string, int> elfCalories = new Dictionary<string, int>();

        // Read the input
        int elfIndex = 0;
        foreach (var line in Input.Day01.Split("\r\n"))
        {
            // Check if the line is empty
            if (line.Trim() == "")
            {
                // The line is empty, so increment the elf index to move to the next elf
                elfIndex++;
            }
            else
            {
                // The line is not empty, so add the calorie count to the total for the current elf
                if (!elfCalories.ContainsKey(elfIndex.ToString()))
                {
                    elfCalories[elfIndex.ToString()] = 0;
                }
                elfCalories[elfIndex.ToString()] += int.Parse(line);
            }
        }

        // Find the elf with the highest calorie count
        int maxCalories = 0;
        string maxElf = "";
        foreach (string elfName in elfCalories.Keys)
        {
            if (elfCalories[elfName] > maxCalories)
            {
                maxCalories = elfCalories[elfName];
                maxElf = elfName;
            }
        }

        // Print the elf with the highest calorie count
        return $"Elf {maxElf} has the most calories: {maxCalories}";
    }

    public string Puzzle2()
    {
        return "";
    }
}