namespace AdventOfCode;

internal class Day19 : IDay
{
    private string[] Data = Input.Day19.Split("\r\n\r\n");
    private Dictionary<int, BluePrint> BluePrints = new();
    int[] MaxRobot = new int[] { 1, 1, 1, 100 };
    static int[] Multiplier = new int[] { 1, 2, 3, 4};

    public Day19()
    {
        for (int i = 0; i < Data.Count(); i++)
        {
            var robotData = Data[i].Split(" ");
            BluePrints[i] = new BluePrint(new[] {
                new Robot(new int[] {int.Parse(robotData[7]), 0, 0 }),
                new Robot(new int[] {int.Parse(robotData[14]), 0, 0 }),
                new Robot(new int[] {int.Parse(robotData[21]), int.Parse(robotData[24]), 0 }),
                new Robot(new int[] {int.Parse(robotData[31]), 0, int.Parse(robotData[34])})
                });
        }
    }

    public string Puzzle1()
    {
        int[] results = new int[BluePrints.Count()];

        for (int bpId = 0; bpId < BluePrints.Count; bpId++)
        {
            var bluePrint = BluePrints[bpId];
            Multiplier = new int[] { 1, 1, 100, 1000 };
            MaxRobot = new int[] { 1, 1, 1, 100 };
            for (int i = 0; i<3; i++)
            {
                for (int r = 0; r < 4; r++)
                {
                    Multiplier[i] += bluePrint.Robots[r].Cost[i];
                    if (MaxRobot[i] < bluePrint.Robots[r].Cost[i] )
                    {
                        MaxRobot[i] = bluePrint.Robots[r].Cost[i];
                    }
                }
            }
            
            var runsToCheck = new Dictionary<int, List<RunDetails>>();
            runsToCheck[24] = new()
            {
                new RunDetails(new[] { 1, 0, 0, 0 }, new[] { 0, 0, 0, 0 }, 24)
            };
            var runsCompleted = new List<RunDetails>();
            for (int time = 23; time > 0; time--)
            {
                Console.WriteLine($"time:{time}: {results.Max()} -- {runsToCheck[time + 1].Count()}");
                runsToCheck[time] = new();

                foreach (var run in runsToCheck[time+1].OrderByDescending(m => m.Score))
                {
                    //Console.WriteLine($"Ore    {run.OreCount[0]}, {run.OreCount[1]}, {run.OreCount[2]},{run.OreCount[3]}");
                    //Console.WriteLine($"Robots {run.MiningRobotCount[0]}, {run.MiningRobotCount[1]}, {run.MiningRobotCount[2]},{run.MiningRobotCount[3]}");
                    
                    bool[] canCreate = new bool[4] { false, false, false, false };
                    for (int r = bluePrint.Robots.Length - 1; r >= 0; r--)
                    {
                        if (MaxRobot[r] > run.MiningRobotCount[r])
                        {
                            if (CanCreate(bluePrint.Robots[r], run, r))
                            {

                                canCreate[r] = true;
                            }
                            else
                            {

                            }

                        }
                    }
                    
                    for (int n = 0; n < 4; n++)
                        run.OreCount[n] += run.MiningRobotCount[n];
                    
                    if (canCreate[3])
                    {
                        var nextTest = CreateRobot(3, bluePrint.Robots, run);
                        if(!runsToCheck[time].Contains(nextTest))
                            runsToCheck[time].Add(nextTest);
                    }
                    if (canCreate[2])
                    {
                        var nextTest = CreateRobot(2, bluePrint.Robots, run);
                        if (!runsToCheck[time].Contains(nextTest))
                            runsToCheck[time].Add(nextTest);
                    }
                    if (canCreate[1])
                    {
                        var nextTest = CreateRobot(1, bluePrint.Robots, run);
                        if (!runsToCheck[time].Contains(nextTest))
                            runsToCheck[time].Add(nextTest);
                    }
                    if (canCreate[0])
                    {
                        var nextTest = CreateRobot(0, bluePrint.Robots, run);
                        if (!runsToCheck[time].Contains(nextTest))
                            runsToCheck[time].Add(nextTest);
                    }
                    runsToCheck[time].Add(new RunDetails(run.MiningRobotCount, run.OreCount, run.RemainingTime - 1));

                    if (results[bpId] < run.OreCount[3])
                        results[bpId] = run.OreCount[3];
                }
                runsToCheck[time + 1].Clear();
            }
        }

        return results.Max().ToString();
    }

    RunDetails CreateRobot(int r, Robot[] robots, RunDetails run)
    {
        var newRun = new RunDetails(run.MiningRobotCount.ToArray(), run.OreCount.ToArray(), run.RemainingTime - 1);
        for (int i = 0; i < 3; i++)
            newRun.OreCount[i] -= robots[r].Cost[i];
        newRun.MiningRobotCount[r]++;
        return newRun;
    }

    bool CanCreate(Robot robot, RunDetails run, int r)
    {
        return MaxRobot[r] > run.MiningRobotCount[r] && robot.Cost[0] <= run.OreCount[0] && 
            robot.Cost[1] <= run.OreCount[1] && robot.Cost[2] <= run.OreCount[2];
    }

    public string Puzzle2()
    {
        return "";
    }

    record class RunDetails(int[] MiningRobotCount, int[] OreCount, int RemainingTime)
    {
        public int Score
        {
            get
            {
                var score = 0;
                
                for (int i = 0; i < 4; i++)
                {
                    score *= Multiplier[i] * OreCount[i];
                    score *= Multiplier[i] * MiningRobotCount[i] * RemainingTime;
                }
                return score;
            }
        }
    }
    record BluePrint(Robot[] Robots);
    record struct Robot(int[] Cost);
}