using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string _input;
    private readonly string[] _inputs;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
        _inputs = _input.Split("\r\n");
    }

    public override ValueTask<string> Solve_1()
    {
        int sum = 0;
        char [][] enginePlan = new char[_inputs.Length][];
        var i = 0;
        foreach (var input in _inputs)
        {
            enginePlan[i] = input.ToCharArray();
            i++;
        }
        i = 0;
        foreach (var input in enginePlan)
        {
            for(var j = 0; j < enginePlan.Length; j++)
            {
                var num = "";
                if (char.IsDigit(input[j]))
                {
                    var start = j;
                    num += input[j];
                    j++;
                    while (char.IsDigit(input[j]))
                    {
                        num += input[j];
                        j++;
                    }
                    var end = j - 1;

                }
            }
            i++;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + sum);
    }

    public bool CheckValid(int start, int end, char[][] plan)
    {
        return false;
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
