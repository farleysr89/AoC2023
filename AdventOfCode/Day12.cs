using Spectre.Console.Rendering;
using System.Net.WebSockets;

namespace AdventOfCode;

public class Day12 : BaseDay
{
    private readonly string _input;
    private readonly string[] _inputs;
    private static readonly char[] separator = [' '];

    public Day12()
    {
        _input = File.ReadAllText(InputFilePath);
        _inputs = _input.Split("\r\n");
    }

    public override ValueTask<string> Solve_1()
    {
        var total = 0;
        foreach (var input in _inputs)
        {
            var splitInput = input.Split(separator);
            var record = splitInput[0];
            var damagedSprings = splitInput[1].Split(',').Select(x => int.Parse(x)).ToArray();
            var size = record.Length;
            var minLength = damagedSprings.Length - 1 + damagedSprings.Sum(x => x);
            if (size == minLength)
            {
                total += 1;
                continue;
            }

            var currentSize = minLength;
            total += CalculatePossibilities("", record, damagedSprings, currentSize);

        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + total);
    }

    public bool CheckValid(string record, string possible, int start = 0)
    {
        for (int i = start; i < possible.Length; i++)
        {
            if (record[i] != '?' && record[i] != possible[i]) return false;
        }
        return true;
    }

    public int CalculatePossibilities(string possible, string record, int[] groups, int currentSize)
    {
        if (groups.Length == 0) return CheckValid(record, possible.PadRight(record.Length, '.')) ? 1 : 0;
        var total = 0;
        var periods = "";
        while (currentSize <= record.Length)
        {
            var tmpPossible = possible + periods + string.Concat(Enumerable.Repeat("#", groups[0]));
            if (CheckValid(record, tmpPossible, possible.Length))
                total += CalculatePossibilities(tmpPossible.Length == record.Length ? tmpPossible : tmpPossible + '.', record, groups[1..], currentSize);
            periods += ".";
            currentSize++;
        }
        return total;
    }

        public ulong CalculatePossibilities2(string possible, string record, int[] groups, int currentSize)
    {
        if (groups.Length == 0) return (ulong)(CheckValid(record, possible.PadRight(record.Length, '.')) ? 1 : 0);
        ulong total = 0;
        var periods = "";
        while (currentSize <= record.Length)
        {
            var tmpPossible = possible + periods + string.Concat(Enumerable.Repeat("#", groups[0]));
            if (CheckValid(record, tmpPossible, possible.Length))
                total += CalculatePossibilities2(tmpPossible.Length == record.Length ? tmpPossible : tmpPossible + '.', record, groups[1..], currentSize);
            periods += ".";
            currentSize++;
        }
        return total;
    }

    public override ValueTask<string> Solve_2()
    {
        ulong total = 0;

        foreach (var input in _inputs)
        {
            var splitInput = input.Split(separator);
            var record = string.Concat(Enumerable.Repeat(splitInput[0], 5).Select(x => x + "?"));
            record = record[..^1];
            var damagedSprings = splitInput[1].Split(',').Select(x => int.Parse(x)).ToArray();
            damagedSprings = (Enumerable.Repeat(damagedSprings, 5)).SelectMany(x => x).ToArray();
            var size = record.Length;
            var minLength = damagedSprings.Length - 1 + damagedSprings.Sum(x => x);
            if (size == minLength)
            {
                total += 1;
                continue;
            }

            var currentSize = minLength;
            total += CalculatePossibilities2("", record, damagedSprings, currentSize);

        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + total);
    }

}
