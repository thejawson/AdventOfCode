namespace AdventOfCode.Year2023
{
    internal class Day05 : IDay
    {
        private static readonly bool UseTestDate = false;
        private readonly List<List<Transform>> transforms;
        private List<long> seeds;
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
            var seeds = input.First().Split(' ').Skip(1).Select(m => long.Parse(m)).ToList();
            transforms = input.Skip(1).Select(m => ParseTransform(m)).ToList();
        }

        private List<Transform> ParseTransform(string transform)
        {
            var rows = transform.Split("\r\n").Skip(1).Select(m => m.Split(' ').Select(n => long.Parse(n)).ToArray()).ToList();
            return rows.Select(m => new Transform() { Start = m[1], End = m[0], Offset = m[2]} ).ToList();
        }

        public string Puzzle1()
        {
            int sum = 0;
            long currentStep;
            foreach (var seed in seeds)
            {
                currentStep = seed;
                foreach (var transform in transforms)
                {
                    var step = transform.First(m => m.Start <= currentStep && m.End >= currentStep);
                    if (step != null)
                    {
                        currentStep += step.Offset;
                    }
                }
            }
                

            return sum.ToString();
        }

        public string Puzzle2()
        {
            long sum = 0;
          
            return sum.ToString();
        }

        private static (int[], int[]) ParseCards(string input)
        {
            var set = input.Replace("  ", " ").Split(": ")[1].Split(" | ");
            return (set[0].Split(' ').Select(m => int.Parse(m)).ToArray(), set[1].Split(' ').Select(m => int.Parse(m)).ToArray());
        }
    }
}