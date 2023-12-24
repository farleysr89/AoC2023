namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string _input;
    private readonly string[] _inputs;
    private static readonly char[] separator = new char[] { ' ' };

    public Day05()
    {
        _input = File.ReadAllText(InputFilePath);
        _inputs = _input.Split("\r\n");
    }

    public override ValueTask<string> Solve_1()
    {
        ulong location = ulong.MaxValue;
        var seeds = _inputs[0].Split(':')[1].Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(s => ulong.Parse(s));
        var data = _inputs[2..];
        var maps = new List<(ulong, ulong, ulong)>[7];
        var mapCount = 0;
        var map = new List<(ulong, ulong, ulong)>();

        foreach (var line in data)
        {
            if (line == "")
            {
                maps[mapCount] = map;
                mapCount++;
                map = [];
                continue;
            }
            if (!char.IsDigit(line[0])) continue;
            var nums = line.Split(separator).Select(n => ulong.Parse(n)).ToArray();
            map.Add((nums[0], nums[1], nums[2]));
        }
        maps[mapCount] = map;
        foreach (var seed in seeds)
        {
            var value = seed;
            foreach (var m in maps)
            {
                foreach (var l in m)
                {
                    if (value >= l.Item2 && value < l.Item2 + l.Item3)
                    {
                        var temp = value - l.Item2;
                        value = l.Item1 + temp;
                        break;
                    }
                }
            }
            if (value < location) location = value;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + location);
    }



    public override ValueTask<string> Solve_2()
    {
        ulong location = ulong.MaxValue;
        var seedData = _inputs[0].Split(':')[1].Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(s => ulong.Parse(s)).ToArray();
        var seeds = new List<ulong>();
        for (int i = 0; i < seedData.Count(); i += 2)
        {
            for (ulong x = seedData[i]; x <= seedData[i] + seedData[i + 1]; x++)
            {
                seeds.Add(x);
            }
            //seeds.AddRange(Enumerable.Range(seedData[i], seedData[seedData[i + 1]]));
        }
        var data = _inputs[2..];
        var maps = new List<(ulong, ulong, ulong)>[7];
        var mapCount = 0;
        var map = new List<(ulong, ulong, ulong)>();

        foreach (var line in data)
        {
            if (line == "")
            {
                maps[mapCount] = map;
                mapCount++;
                map = [];
                continue;
            }
            if (!char.IsDigit(line[0])) continue;
            var nums = line.Split(separator).Select(n => ulong.Parse(n)).ToArray();
            map.Add((nums[0], nums[1], nums[2]));
        }
        maps[mapCount] = map;
        foreach (var seed in seeds)
        {
            var value = seed;
            foreach (var m in maps)
            {
                foreach (var l in m)
                {
                    if (value >= l.Item2 && value < l.Item2 + l.Item3)
                    {
                        var temp = value - l.Item2;
                        value = l.Item1 + temp;
                        break;
                    }
                }
            }
            if (value < location) location = value;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + location);
    }


}
