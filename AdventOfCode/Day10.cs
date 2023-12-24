namespace AdventOfCode;

public class Day10 : BaseDay
{
    private readonly string _input;
    private readonly string[] _inputs;
    private static readonly char[] separator = [' '];

    public Day10()
    {
        _input = File.ReadAllText(InputFilePath);
        _inputs = _input.Split("\r\n");
    }

    public override ValueTask<string> Solve_1()
    {
        var total = 0;
        var map = new char[_inputs.Length][];
        var count = 0;
        foreach (var input in _inputs)
        {
            map[count] = input.ToCharArray();
            count++;
        }
        (int, int) start = (-1, -1);
        var y = 0;
        foreach (var l in map)
        {
            int x = 0;
            foreach (var c in l)
            {
                if (c == 'S')
                {
                    start = (x, y);
                    break;
                }
                x++;
            }
            if (start != (-1, -1)) break;
            y++;
        }
        var steps = new int[map.Length][];
        for (int i = 0; i < map.Length; i++)
        {
            steps[i] = new int[map[i].Length];
        }
        for (int i = 0; i < steps.Length; i++)
        {
            for (int j = 0; j < steps[i].Length; j++)
            {
                steps[i][j] = int.MaxValue;
            }
        }
        steps[start.Item2][start.Item1] = 0;
        if (TryRight(start.Item1, start.Item2, map))
        {
            if (TryUp(start.Item1, start.Item2, map))
                map[start.Item2][start.Item1] = 'L';
            else if (TryDown(start.Item1, start.Item2, map))
                map[start.Item2][start.Item1] = 'F';
        }
        else if (TryLeft(start.Item1, start.Item2, map))
        {
            if (TryUp(start.Item1, start.Item2, map))
                map[start.Item2][start.Item1] = 'J';
            else if (TryDown(start.Item1, start.Item2, map))
                map[start.Item2][start.Item1] = '7';
        }
        (int, int) next = (-1, -1);
        (int, int) prev = start;
        char symbol = map[start.Item2][start.Item1];
        char prevSymbol;
        char dir = '.';
        if (symbol == 'F') { next = (start.Item1 + 1, start.Item2); dir = 'R'; }
        if (symbol == 'L') { next = (start.Item1 + 1, start.Item2); dir = 'R'; }
        if (symbol == 'J') { next = (start.Item1 - 1, start.Item2); dir = 'L'; }
        if (symbol == '7') { next = (start.Item1 - 1, start.Item2); dir = 'L'; }
        count = 0;
        while (next != start)
        {
            count++;
            prevSymbol = symbol;
            symbol = map[next.Item2][next.Item1];
            steps[next.Item2][next.Item1] = Math.Min(count, steps[next.Item2][next.Item1]);
            prev = next;
            if (symbol == '.')
            {
                Console.WriteLine("Something Broke!"); break;
            }
            else if (symbol == '-')
            {
                if (dir == 'R')
                {
                    next = (++next.Item1, next.Item2);
                }
                else if (dir == 'L')
                {
                    next = (--next.Item1, next.Item2);
                }
                else
                {
                    Console.WriteLine("Something Broke!");
                    break;
                }
            }
            else if (symbol == '|')
            {
                if (dir == 'U')
                {
                    next = (next.Item1, --next.Item2);
                }
                else if (dir == 'D')
                {
                    next = (next.Item1, ++next.Item2);
                }
                else
                {
                    Console.WriteLine("Something Broke!");
                    break;
                }
            }
            else if (symbol == 'F')
            {
                if (dir == 'U')
                {
                    next = (++next.Item1, next.Item2);
                    dir = 'R';
                }
                else if (dir == 'L')
                {
                    next = (next.Item1, ++next.Item2);
                    dir = 'D';
                }
                else
                {
                    Console.WriteLine("Something Broke!");
                    break;
                }
            }
            else if (symbol == 'J')
            {
                if (dir == 'R')
                {
                    next = (next.Item1, --next.Item2);
                    dir = 'U';
                }
                else if (dir == 'D')
                {
                    next = (--next.Item1, next.Item2);
                    dir = 'L';
                }
                else
                {
                    Console.WriteLine("Something Broke!");
                    break;
                }
            }
            else if (symbol == 'L')
            {
                if (dir == 'D')
                {
                    next = (++next.Item1, next.Item2);
                    dir = 'R';
                }
                else if (dir == 'L')
                {
                    next = (next.Item1, --next.Item2);
                    dir = 'U';
                }
                else
                {
                    Console.WriteLine("Something Broke!");
                    break;
                }
            }
            else if (symbol == '7')
            {
                if (dir == 'R')
                {
                    next = (next.Item1, ++next.Item2);
                    dir = 'D';
                }
                else if (dir == 'U')
                {
                    next = (--next.Item1, next.Item2);
                    dir = 'L';
                }
                else
                {
                    Console.WriteLine("Something Broke!");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Something Broke!");
                break;
            }
        }
        symbol = map[start.Item2][start.Item1];        
        dir = '.';
        if (symbol == 'F') { next = (start.Item1, start.Item2 + 1); dir = 'D'; }
        if (symbol == 'L') { next = (start.Item1, start.Item2 - 1); dir = 'U'; }
        if (symbol == 'J') { next = (start.Item1, start.Item2 + 1); dir = 'U'; }
        if (symbol == '7') { next = (start.Item1, start.Item2 - 1); dir = 'D'; }
        count = 0;
        while (next != start)
        {
            count++;
            prevSymbol = symbol;
            symbol = map[next.Item2][next.Item1];
            steps[next.Item2][next.Item1] = Math.Min(count, steps[next.Item2][next.Item1]);
            prev = next;
            if (symbol == '.')
            {
                Console.WriteLine("Something Broke!"); break;
            }
            else if (symbol == '-')
            {
                if (dir == 'R')
                {
                    next = (++next.Item1, next.Item2);
                }
                else if (dir == 'L')
                {
                    next = (--next.Item1, next.Item2);
                }
                else
                {
                    Console.WriteLine("Something Broke!");
                    break;
                }
            }
            else if (symbol == '|')
            {
                if (dir == 'U')
                {
                    next = (next.Item1, --next.Item2);
                }
                else if (dir == 'D')
                {
                    next = (next.Item1, ++next.Item2);
                }
                else
                {
                    Console.WriteLine("Something Broke!");
                    break;
                }
            }
            else if (symbol == 'F')
            {
                if (dir == 'U')
                {
                    next = (++next.Item1, next.Item2);
                    dir = 'R';
                }
                else if (dir == 'L')
                {
                    next = (next.Item1, ++next.Item2);
                    dir = 'D';
                }
                else
                {
                    Console.WriteLine("Something Broke!");
                    break;
                }
            }
            else if (symbol == 'J')
            {
                if (dir == 'R')
                {
                    next = (next.Item1, --next.Item2);
                    dir = 'U';
                }
                else if (dir == 'D')
                {
                    next = (--next.Item1, next.Item2);
                    dir = 'L';
                }
                else
                {
                    Console.WriteLine("Something Broke!");
                    break;
                }
            }
            else if (symbol == 'L')
            {
                if (dir == 'D')
                {
                    next = (++next.Item1, next.Item2);
                    dir = 'R';
                }
                else if (dir == 'L')
                {
                    next = (next.Item1, --next.Item2);
                    dir = 'U';
                }
                else
                {
                    Console.WriteLine("Something Broke!");
                    break;
                }
            }
            else if (symbol == '7')
            {
                if (dir == 'R')
                {
                    next = (next.Item1, ++next.Item2);
                    dir = 'D';
                }
                else if (dir == 'U')
                {
                    next = (--next.Item1, next.Item2);
                    dir = 'L';
                }
                else
                {
                    Console.WriteLine("Something Broke!");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Something Broke!");
                break;
            }
        }
        foreach(var l in steps)
        {
            foreach(var s in l)
            {
                if(s != int.MaxValue && s > total) total = s;
            }
        }
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + total);
    }

    public bool TryRight(int x, int y, char[][] map)
    {
        x++;
        if (x >= map[y].Length) return false;
        if (map[y][x] != '.')
            return true;
        return false;
    }

    public bool TryLeft(int x, int y, char[][] map)
    {
        x--;
        if (x < 0) return false;
        if (map[y][x] != '.')
            return true;
        return false;
    }

    public bool TryDown(int x, int y, char[][] map)
    {
        y++;
        if (y >= map.Length) return false;
        if (map[y][x] != '.')
            return true;
        return false;
    }

    public bool TryUp(int x, int y, char[][] map)
    {
        y--;
        if (y < 0) return false;
        if (map[y][x] != '.')
            return true;
        return false;
    }

    public override ValueTask<string> Solve_2()
    {
        var total = 0;

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + total);
    }

}
