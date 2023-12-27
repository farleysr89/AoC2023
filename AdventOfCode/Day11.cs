using Spectre.Console.Rendering;
using System.Net.WebSockets;

namespace AdventOfCode;

public class Day11 : BaseDay
{
    private readonly string _input;
    private readonly string[] _inputs;
    private static readonly char[] separator = [' '];

    public Day11()
    {
        _input = File.ReadAllText(InputFilePath);
        _inputs = _input.Split("\r\n");
    }

    public override ValueTask<string> Solve_1()
    {
        var total = 0;
        List<List<char>> map = [];
        char[][] preMap = new char[_inputs.Length][];

        var count = 0;
        foreach (var input in _inputs)
        {
            preMap[count] = input.ToCharArray();
            count++;
        }
        List<int> emptyCols = [];
        for (int i = 0; i < preMap[0].Length; i++)
        {
            var empty = true;
            for (int j = 0; j < preMap.Length; j++)
            {
                if (preMap[j][i] != '.') { empty = false; break; }
            }
            if (empty) emptyCols.Add(i);
        }

        foreach (var line in _inputs)
        {
            var empty = true;
            var index = 0;
            map.Add([]);
            foreach (var c in line)
            {
                map.Last().Add(c);
                if (c != '.') empty = false;
                if (emptyCols.Contains(index)) map.Last().Add('.');
                index++;
            }
            if (empty) map.Add(map.Last());
        }
        List<(int, int)> galaxies = [];
        count = 0;

        char[][] postMap = new char[map.Count][];
        foreach (var line in map)
        {
            postMap[count] = [.. line];
            count++;
        }
        for (int i = 0; i < postMap.Length; i++)
        {
            for (int j = 0; j < postMap[i].Length; j++)
            {
                if (postMap[i][j] == '#') galaxies.Add((i, j));
            }
        }
        (int, int)[] galaxiesArray = galaxies.ToArray();
        //count = 0;
        for (int i = 0; i < galaxiesArray.Length; i++)
        {
            for (int j = i + 1; j < galaxiesArray.Length; j++)
            {
                total += Math.Abs(galaxiesArray[i].Item1 - galaxiesArray[j].Item1) + Math.Abs(galaxiesArray[i].Item2 - galaxiesArray[j].Item2);
                //count++;
            }
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + total);
    }

    public override ValueTask<string> Solve_2()
    {
        ulong total = 0;
        List<List<char>> map = [];
        char[][] preMap = new char[_inputs.Length][];

        var count = 0;
        foreach (var input in _inputs)
        {
            preMap[count] = input.ToCharArray();
            count++;
        }
        List<int> emptyCols = [];
        List<int> emptyRows = [];
        for (int i = 0; i < preMap[0].Length; i++)
        {
            var empty = true;
            for (int j = 0; j < preMap.Length; j++)
            {
                if (preMap[j][i] != '.') { empty = false; break; }
            }
            if (empty) emptyCols.Add(i);
        }
        var index = 0;
        foreach (var line in _inputs)
        {
            var empty = true;
            foreach (var c in line)
            {
                if (c != '.')
                {
                    empty = false; break;
                }
            }
            if (empty) emptyRows.Add(index);
            index++;

        }

        //foreach(var line in _inputs)
        //{
        //    var empty = true;
        //    var index = 0;
        //    map.Add([]);
        //    foreach(var c in line)
        //    {
        //        map.Last().Add(c);
        //        if(c != '.') empty = false;
        //        if(emptyCols.Contains(index)) map.Last().Add('.');
        //        index++;
        //    }
        //    if(empty) map.Add(map.Last());
        //}
        List<(int, int)> galaxies = [];
        count = 0;

        //char[][] postMap = new char[map.Count][];
        //foreach(var line in map)
        //{
        //    postMap[count] = [.. line];
        //    count++;
        //}
        for (int i = 0; i < preMap.Length; i++)
        {
            for (int j = 0; j < preMap[i].Length; j++)
            {
                if (preMap[i][j] == '#') galaxies.Add((i, j));
            }
        }
        (int, int)[] galaxiesArray = [.. galaxies];
        for (int i = 0; i < galaxiesArray.Length; i++)
        {
            galaxiesArray[i].Item1 += emptyRows.Where(x => x < galaxiesArray[i].Item1).Count() * 999999;
            galaxiesArray[i].Item2 += emptyCols.Where(x => x < galaxiesArray[i].Item2).Count() * 999999;
        }
        //count = 0;
        for (int i = 0; i < galaxiesArray.Length; i++)
        {
            for (int j = i + 1; j < galaxiesArray.Length; j++)
            {
                total += (ulong)(Math.Abs(galaxiesArray[i].Item1 - galaxiesArray[j].Item1) + Math.Abs(galaxiesArray[i].Item2 - galaxiesArray[j].Item2));
                //count++;
            }
        }
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + total);
    }

}
