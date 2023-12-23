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

    public List<(int, int)> CheckValidGear(int x, int y, char[][] plan)
    {
        List<(int, int)> indices = [];
        if (char.IsDigit(plan[y - 1][x - 1]))
        {
            indices.Add((y - 1, x - 1));
            if (!char.IsDigit(plan[y - 1][x]) && char.IsDigit(plan[y - 1][x + 1])) indices.Add((y - 1, x + 1));
        }
        else if (char.IsDigit(plan[y - 1][x]))
        {
            indices.Add((y - 1, x));
        }
        else if (char.IsDigit(plan[y - 1][x + 1]))
        {
            indices.Add((y - 1, x + 1));
        }
        if (char.IsDigit(plan[y][x - 1]))
        {
            indices.Add((y, x - 1));
        }
        if (char.IsDigit(plan[y][x + 1]))
        {
            indices.Add((y, x + 1));
        }
        if (char.IsDigit(plan[y + 1][x - 1]))
        {
            indices.Add((y + 1, x - 1));
            if (!char.IsDigit(plan[y + 1][x]) && char.IsDigit(plan[y + 1][x + 1])) indices.Add((y + 1, x + 1));
        }
        else if (char.IsDigit(plan[y + 1][x]))
        {
            indices.Add((y + 1, x));
        }
        else if (char.IsDigit(plan[y + 1][x + 1]))
        {
            indices.Add((y + 1, x + 1));
        }
        return indices.Count > 1 ? indices : null;
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
                if (input[j] == '*')
                {
                    var indicies = CheckValidGear(j, i, enginePlan);
                    if (indicies == null) continue;
                    if (indicies.Count > 2) Console.WriteLine("Something Broke!");
                    if (indicies.Count < 2) continue;
                    var product = 1;
                    foreach (var index in indicies)
                    {
                        var num = "";
                        var x = index.Item2;
                        var y = index.Item1;
                        num += enginePlan[y][x];
                        for (int xx = x - 1; xx >= 0; xx--)
                        {
                            if (char.IsDigit(enginePlan[y][xx])) num = enginePlan[y][xx] + num;
                            else break;
                        }
                        for (int xx = x + 1; xx < enginePlan[y].Length; xx++)
                        {
                            if (char.IsDigit(enginePlan[y][xx])) num += enginePlan[y][xx];
                            else break;
                        }
                        product *= int.Parse(num);
                    }
                    sum += product;
                    //var start = j == 0 ? 0 : j - 1;
                    //num += input[j];
                    //j++;
                    //while (j < input.Length && char.IsDigit(input[j]))
                    //{
                    //    num += input[j];
                    //    j++;
                    //}
                    //var end = j < input.Length - 1 ? j : input.Length - 1;

                    //if (CheckValid(start, end, i, enginePlan))
                    //{
                    //    sum += int.Parse(num);
                    //}
                }
            }
            i++;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + sum);
    }


}
