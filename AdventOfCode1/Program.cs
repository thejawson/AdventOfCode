using AdventOfCode;
using System.Diagnostics;

var startTime = Stopwatch.GetTimestamp();

List<IDay> days = new()
{
    //new Day1AI(),
    new Day01(),
    new Day02(),
    new Day03(),
    new Day04(),
    new Day05(),
    new Day06(),
    new Day07(),
    new Day08(),
    new Day09(),
    new Day10(),
    new Day11(),
    new Day12(),
};

for (int i = 0; i < days.Count; i++)
    Console.WriteLine($"Day {i + 1}\n    Results 1 {days[i].Puzzle1()}\n    Results 2 {days[i].Puzzle2()}");

Console.WriteLine($"Run time: {Stopwatch.GetElapsedTime(startTime)}");

//Day 1
//    Results 1 70116
//    Results 2 206582
//Day 2
//    Results 1 15691
//    Results 2 12989
//Day 3
//    Results 1 8039
//    Results 2 2510
//Day 4
//    Results 1 424
//    Results 2 804
//Day 5
//    Results 1 WSFTMRHPP
//    Results 2 FRSFFMWDC
//Day 6
//    Results 1 1156
//    Results 2 2790
//Day 7
//    Results 1 1449447
//    Results 2 8679207
//Day 8
//    Results 1 1870
//    Results 2 517440
//Day 9
//    Results 1 5883
//    Results 2 2472
//Day 10
//    Results 1 17180
//    Results 2
//▌▌▌  ▌▌▌▌ ▌  ▌ ▌▌▌  ▌▌▌  ▌    ▌  ▌ ▌▌▌
//▌  ▌ ▌    ▌  ▌ ▌  ▌ ▌  ▌ ▌    ▌  ▌ ▌  ▌
//▌  ▌ ▌▌▌  ▌▌▌▌ ▌  ▌ ▌  ▌ ▌    ▌  ▌ ▌▌▌
//▌▌▌  ▌    ▌  ▌ ▌▌▌  ▌▌▌  ▌    ▌  ▌ ▌  ▌▌
//▌ ▌  ▌    ▌  ▌ ▌    ▌ ▌  ▌    ▌  ▌ ▌  ▌▌
//▌  ▌ ▌▌▌▌ ▌  ▌ ▌    ▌  ▌ ▌▌▌▌  ▌▌  ▌▌▌
//Day 11
//    Results 1 98908
//    Results 2 25935263541
//Day 12
//    Results 1 339
//    Results 2 332