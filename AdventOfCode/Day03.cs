using System.Linq;

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
        char[][] enginePlan = new char[_inputs.Length][];
        var i = 0;
        foreach (var input in _inputs)
        {
            enginePlan[i] = input.ToCharArray();
            i++;
        }
        i = 0;
        foreach (var input in enginePlan)
        {
            for (var j = 0; j < input.Length; j++)
            {
                var num = "";
                if (char.IsDigit(input[j]))
                {
                    var start = j == 0 ? 0 : j - 1;
                    num += input[j];
                    j++;
                    while (j < input.Length && char.IsDigit(input[j]))
                    {
                        num += input[j];
                        j++;
                    }
                    var end = j < input.Length - 1 ? j : input.Length - 1;

                    if (CheckValid(start, end, i, enginePlan))
                    {
                        sum += int.Parse(num);
                    }
                }
            }
            i++;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + sum);
    }

    public bool CheckValid(int start, int end, int row, char[][] plan)
    {
        int checkRow;
        if (row > 0)
        {
            checkRow = row - 1;
            for (var j = start; j <= end; j++)
            {
                if (plan[checkRow][j] != '.' && !char.IsDigit(plan[checkRow][j])) return true;
            }
        }
        checkRow = row;
        if (plan[checkRow][start] != '.' && !char.IsDigit(plan[checkRow][start])) return true;
        if (plan[checkRow][end] != '.' && !char.IsDigit(plan[checkRow][end])) return true;
        if (row < plan.Length)
        {
            checkRow = row + 1;
            for (var j = start; j <= end; j++)
            {
                if (plan[checkRow][j] != '.' && !char.IsDigit(plan[checkRow][j])) return true;
            }
        }
        return false;
    }

    public bool CheckValidGear(int x, int y, char[][] plan)
    {
        int count = 0;
        if(char.IsDigit(plan[x-1][y-1]))
        {
            count++;
            if(char.IsDigit(plan[x-1][y]) && !char.IsDigit(plan[x-1][y])) count++;
        }
        return count > 1;
    }

    public override ValueTask<string> Solve_2()
    {
        int sum = 0;
        char[][] enginePlan = new char[_inputs.Length][];
        var i = 0;
        foreach (var input in _inputs)
        {
            enginePlan[i] = input.ToCharArray();
            i++;
        }
        i = 0;
        foreach (var input in enginePlan)
        {
            for (var j = 0; j < input.Length; j++)
            {
                var num = "";
                if (char.IsDigit(input[j]))
                {
                    var start = j == 0 ? 0 : j - 1;
                    num += input[j];
                    j++;
                    while (j < input.Length && char.IsDigit(input[j]))
                    {
                        num += input[j];
                        j++;
                    }
                    var end = j < input.Length - 1 ? j : input.Length - 1;

                    if (CheckValid(start, end, i, enginePlan))
                    {
                        sum += int.Parse(num);
                    }
                }
            }
            i++;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + sum);
    }


}
