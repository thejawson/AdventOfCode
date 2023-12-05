using System.Diagnostics;

namespace AdventOfCode.Year2023
{
    internal static class Year
    {
        public static void Run()
        {
            var startTime = Stopwatch.GetTimestamp();

            List<IDay> days = new()
            {
                new Day01(),
                new Day02(),
                new Day03(),
                new Day04(),
                //new Day05(),
                //new Day06(),
                //new Day07(),
                //new Day08(),
                //new Day09(),
                //new Day10(),
                //new Day11(),
                //new Day12(),
                //new Day13(),
                //new Day14(),
                //new Day15(),
                //new Day16(),
                //new Day17(),
                //new Day18(),
                //new Day19(),
            };

            for (int i = 0; i < days.Count; i++)
                Console.WriteLine($"Day {i + 1}\n    Results 1 {days[i].Puzzle1()}\n    Results 2 {days[i].Puzzle2()}");

            Console.WriteLine($"Run time: {Stopwatch.GetElapsedTime(startTime)}");

            //Day 1
            //    Results 1 54644
            //    Results 2 53348
            //Day 2
            //    Results 1 2716
            //    Results 2 72227
            //Day 3
            //    Results 1 506727
            //    Results 2 75220503
            //Day 4
            //    Results 1 20855
            //    Results 2 5489600
            //Run time: 00:00:00.0916863
        }
    }
}