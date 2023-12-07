using static AdventOfCode.Year2023.Day05;

namespace AdventOfCode.Year2023
{
    internal class Day05 : IDay
    {
        private static readonly bool UseTestDate = false;
        private readonly List<Transform>[] Transforms;
        private readonly long[] Seeds;
        
        internal class Transform
        {
            public long Start;
            public long End;
            public long Offset;
        }

        public Day05()
        {
            var input = UseTestDate
                ? Input.Day05Test.Split("\r\n\r\n").ToList()
                : Input.Day05.Split("\r\n\r\n").ToList();
            Seeds = input.First().Split(' ').Skip(1).Select(m => long.Parse(m)).ToArray();
            //Seeds = new long[] {82, 0};
            Transforms = input.Skip(1).Select(m => ParseTransform(m)).ToArray();
        }

        private static List<Transform> ParseTransform(string transform)
        {
            var rows = transform.Split("\r\n").Skip(1).Select(m => m.Split(' ').Select(n => long.Parse(n)).ToArray()).ToList();
            return rows.Select(m => new Transform() { Start = m[1], End = m[1] + m[2], Offset = m[0] - m[1]} ).ToList();
        }

        public string Puzzle1()
        {
            long min = long.MaxValue;
            long currentStep;
            foreach (var seed in Seeds)
            {
                currentStep = seed;
                foreach (var transform in Transforms)
                {
                    var step = transform.FirstOrDefault(m => m.Start <= currentStep && m.End >= currentStep);
                    if (step != null)
                    {
                        currentStep += step.Offset;
                    }
                }
                min = long.Min(min, currentStep);
            }

            return min.ToString();
        }

        public string Puzzle2()
        {
            List<(long, long)> currentRange = new();
            long min = long.MaxValue;
            for (long index = 0; index < Seeds.Length; index += 2)
            {
                currentRange.Clear();
                currentRange.Add((Seeds[index], Seeds[index] + Seeds[index + 1]));
                foreach (var transformSet in Transforms)
                {
                    List<(long, long)> nextRange = new();
                    foreach (var range in currentRange)
                    {
                        var transforms = transformSet.Where(m => range.Item1 >= m.Start && range.Item1 < m.End
                           || m.Start >= range.Item1 && m.Start < range.Item2).ToList();
                        if (transforms.Any())
                        {
                            if (transforms.All(m => m.Start > range.Item1))
                            {
                                nextRange.Add((range.Item1, transforms.Min(m => m.Start) - 1));
                            }
                            foreach (var transform in transforms)
                            {
                                nextRange.Add((long.Max(transform.Start, range.Item1) + transform.Offset, long.Min(transform.End - 1, range.Item2) + transform.Offset));
                            }
                            if (transforms.All(m => m.End <= range.Item2))
                            {
                                nextRange.Add((transforms.Max(m => m.End), range.Item2));
                            }
                        }
                        else
                        {
                            nextRange.Add(range);
                        }
                    }
                    
                    currentRange.Clear();
                    currentRange = nextRange.ToList();
                }
                min =long.Min(min, currentRange.Min(m => m.Item1));
            }

            return min.ToString();
        }
    }
}