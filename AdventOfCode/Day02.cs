using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string _input;
    private readonly string[] _inputs;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
        _inputs = _input.Split("\r\n");
    }

    public override ValueTask<string> Solve_1()
    {
        int sum = 0;
        foreach (var input in _inputs)
        {
            var gameNum = int.Parse(input[input.IndexOf(' ')..input.IndexOf(':')]);
            var draws = input[(input.IndexOf(": ") + 2)..].Split("; ");
            var impossible = false;
            foreach (var draw in draws)
            {
                var colors = draw.Split(", ");
                foreach(var color in colors)
                {
                    (int, string) splitColor = color.Split(' ') switch { var c => (int.Parse(c[0]), c[1])};
                    if(splitColor.Item2 == "red" & splitColor.Item1 > 12)
                    {
                        impossible = true;
                        break;
                    }
                    if(splitColor.Item2 == "green" & splitColor.Item1 > 13)
                    {
                        impossible = true;
                        break;
                    }
                    if(splitColor.Item2 == "blue" & splitColor.Item1 > 14)
                    {
                        impossible = true;
                        break;
                    }
                }
                if(impossible) break;
            }
            if(!impossible) sum += gameNum;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + sum);
    }

    public override ValueTask<string> Solve_2()
    {
        int sum = 0;
        foreach (var input in _inputs)
        {
            //var gameNum = int.Parse(input[input.IndexOf(' ')..input.IndexOf(':')]);
            var draws = input[(input.IndexOf(": ") + 2)..].Split("; ");
            //var impossible = false;
            int redMin = 0, blueMin = 0, greenMin = 0;
            foreach (var draw in draws)
            {
                var colors = draw.Split(", ");
                foreach(var color in colors)
                {
                    (int, string) splitColor = color.Split(' ') switch { var c => (int.Parse(c[0]), c[1])};
                    if(splitColor.Item2 == "red" & splitColor.Item1 > redMin)
                    {
                        redMin = splitColor.Item1;
                    }
                    if(splitColor.Item2 == "green" & splitColor.Item1 > greenMin)
                    {
                        greenMin = splitColor.Item1;
                    }
                    if(splitColor.Item2 == "blue" & splitColor.Item1 > blueMin)
                    {
                        blueMin = splitColor.Item1;
                    }
                }
                //if(impossible) break;
            }
            //if(!impossible) sum += gameNum;
            sum += (redMin * greenMin * blueMin);
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + sum);
    }


}
