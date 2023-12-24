namespace AdventOfCode;

public class Day09 : BaseDay
{
    private readonly string _input;
    private readonly string[] _inputs;
    private static readonly char[] separator = [' '];

    public Day09()
    {
        _input = File.ReadAllText(InputFilePath);
        _inputs = _input.Split("\r\n");
    }

    public override ValueTask<string> Solve_1()
    {
        var total = 0;
        foreach (var input in _inputs)
        {
            List<List<int>> values = [input.Split(separator).Select(x => int.Parse(x)).ToList()];
            var currentList = values.First();
            var first = true;
            var prev = 0;
            while (true)
            {
                List<int> differences = [];
                foreach (var i in currentList)
                {
                    if (first)
                    {
                        prev = i;
                        first = false;
                        continue;
                    }
                    differences.Add(i - prev);
                    prev = i;
                }
                values.Add(differences);
                if(!values.Last().Any(x => x != 0)) break;
                first = true;
                currentList = values.Last();
            }
            values.Reverse();
            first = true;
            prev = 0;
            foreach(var l in values)
            {
                if (first)
                {
                    prev = l.Last();
                    first = false;
                    continue;
                }
                prev += l.Last();
            }
            total += prev;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + total);
    }



    public override ValueTask<string> Solve_2()
    {
        var total = 0;
        foreach (var input in _inputs)
        {
            List<List<int>> values = [input.Split(separator).Select(x => int.Parse(x)).ToList()];
            var currentList = values.First();
            var first = true;
            var prev = 0;
            while (true)
            {
                List<int> differences = [];
                foreach (var i in currentList)
                {
                    if (first)
                    {
                        prev = i;
                        first = false;
                        continue;
                    }
                    differences.Add(i - prev);
                    prev = i;
                }
                values.Add(differences);
                if(!values.Last().Any(x => x != 0)) break;
                first = true;
                currentList = values.Last();
            }
            values.Reverse();
            first = true;
            prev = 0;
            foreach(var l in values)
            {
                if (first)
                {
                    prev = l.First();
                    first = false;
                    continue;
                }
                prev = l.First() - prev;
            }
            total += prev;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + total);
    }

}
