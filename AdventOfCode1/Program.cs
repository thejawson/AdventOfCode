using AdventOfCode;
using System.Diagnostics;

Stopwatch stopWatch = Stopwatch.StartNew();

List<IDay> days = new()
{
    //new Day1AI(),
    new Day1(),
    new Day2(),
    new Day3(),
    new Day4(),
    new Day5(),
    new Day6(),
    new Day7(),
    new Day8(),
    new Day9(),

};

for (int i = 0; i < days.Count; i++)
{
    Console.WriteLine($"Day {i + 1}\n    Results 1 {days[i].Puzzle1()}\n    Results 2 {days[i].Puzzle2()}");
}

stopWatch.Stop();
Console.WriteLine($"Run time: {stopWatch.Elapsed}");