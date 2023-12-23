using System.Net.Mail;
using System.Net.WebSockets;

namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string _input;
    private readonly string[] _inputs;
    private static readonly char[] separator = new char[] { ' ' };

    public Day06()
    {
        _input = File.ReadAllText(InputFilePath);
        _inputs = _input.Split("\r\n");
    }

    public override ValueTask<string> Solve_1()
    {
        var total = 1;
        var times = _inputs[0].Split(':')[1].Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(t => int.Parse(t)).ToArray();
        var records = _inputs[1].Split(':')[1].Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(r => int.Parse(r)).ToArray();

        for (int i = 0; i < times.Length; i++)
        {
            var count = 0;
            for (int j = 1; j < times[i]; j++)
            {
                if ((times[i] - j) * j > records[i]) count++;
            }
            total *= count;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + total);
    }



    public override ValueTask<string> Solve_2()
    {
        var time = long.Parse(_inputs[0].Split(':')[1].Split(separator, StringSplitOptions.RemoveEmptyEntries).Aggregate((t, tt) => t + tt));
        var record = long.Parse(_inputs[1].Split(':')[1].Split(separator, StringSplitOptions.RemoveEmptyEntries).Aggregate((r, rr) => r + rr));
        long first = 0, last = 0;
        for (long j = 1; j < time; j++)
        {
            if ((time - j) * j > record) { first = j; break; }
        }
        for (long j = time; j > 0; j--)
        {
            if ((time - j) * j > record) { last = j; break; }
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + (last - first + 1));
    }


}
