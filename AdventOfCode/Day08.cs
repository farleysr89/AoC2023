using System.Net.Mail;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace AdventOfCode;

public class Day08 : BaseDay
{
    private readonly string _input;
    private readonly string[] _inputs;
    private static readonly char[] separator = [' '];

    public Day08()
    {
        _input = File.ReadAllText(InputFilePath);
        _inputs = _input.Split("\r\n");
    }

    public override ValueTask<string> Solve_1()
    {
        var directions = _inputs[0];
        Dictionary<string, (string, string)> nodes = new Dictionary<string, (string, string)>();
        foreach (var input in _inputs[2..])
        {
            var line = input.Split(" = ");
            line[1] = line[1][1..(line[1].Length - 1)];
            nodes[line[0]] = (line[1].Split(", ")[0], line[1].Split(", ")[1]);
        }
        int counter = 0;
        int index = 0;
        var node = nodes["AAA"];
        var nodeKey = "AAA";
        while (true)
        {
            counter++;
            if (directions[index] == 'L') nodeKey = node.Item1;
            else if (directions[index] == 'R') nodeKey = node.Item2;
            else Console.WriteLine("Seomthing Broke!");
            node = nodes[nodeKey];
            if (nodeKey == "ZZZ") break;
            index++;
            if (index > directions.Length - 1) index = 0;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + counter);
    }



    public override ValueTask<string> Solve_2()
    {
        var directions = _inputs[0];
        Dictionary<string, (string, string)> nodes = new Dictionary<string, (string, string)>();
        List<(string, ((string, string), int))> ghosts = new List<(string, ((string, string), int))>();
        var num = 0;
        foreach (var input in _inputs[2..])
        {
            var line = input.Split(" = ");
            line[1] = line[1][1..(line[1].Length - 1)];
            nodes[line[0]] = (line[1].Split(", ")[0], line[1].Split(", ")[1]);
            if (line[0].EndsWith('A'))
                ghosts.Add((line[0], (nodes[line[0]],num++)));
        }

        int counter = 0;
        int index = 0;
        //var node = nodes["AAA"];
        //var nodeKey = "AAA";
        while (ghosts.Any(g => !g.Item1.EndsWith('Z')))
        {
            List<(string, ((string, string),int))> newGhosts = new List<(string, ((string, string),int))>();
            counter++;
            //if(counter == 6) Console.WriteLine("HERE");
            foreach (var ghost in ghosts)
            {
                if (directions[index] == 'L') newGhosts.Add((ghost.Item2.Item1.Item1,((nodes[ghost.Item2.Item1.Item1],ghost.Item2.Item2))));
                else if (directions[index] == 'R') newGhosts.Add((ghost.Item2.Item1.Item2,(nodes[ghost.Item2.Item1.Item2], ghost.Item2.Item2)));
                else Console.WriteLine("Something Broke!");
            }
            ghosts = newGhosts;
            foreach(var ghost in ghosts)
            {
                if(ghost.Item1.EndsWith('Z')) Console.WriteLine("Ghost " + ghost.Item2.Item2 + " found exit on move " + counter);
            }
            //node = nodes[nodeKey];
            //if (nodeKey == "ZZZ") break;
            index++;
            if (index > directions.Length - 1) index = 0;
        }


        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + counter);
    }

}
