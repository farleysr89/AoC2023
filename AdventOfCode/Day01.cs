namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string _input;
    private readonly string[] _inputs;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
        _inputs = _input.Split('\n');
    }

    public override ValueTask<string> Solve_1()
    {
        int sum = 0;
        foreach (var input in _inputs)
        {
            var temp = new string(input.Where(c => char.IsDigit(c)).ToArray());
            sum += int.Parse(temp[0].ToString() + temp[^1]);
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + sum);
    }

    public override ValueTask<string> Solve_2()
    {
        int sum = 0;
        foreach (var input in _inputs)
        {
            List<char> nums = [];
            //var i = 0;
            for(var i = 0; i < input.Length; i++) 
            {
                var c = input[i];
                if (char.IsDigit(c)) {nums.Add(c);}
                else
                {
                    var tmp = input[i..];
                    switch (tmp)
                    {
                        case { } when tmp.StartsWith("one"):
                            nums.Add('1');
                            //i+=2;
                            break;
                        case { } when tmp.StartsWith("two"):
                            nums.Add('2');
                            //i+=2;
                            break;
                        case { } when tmp.StartsWith("three"):
                            nums.Add('3');
                            //i+=4;
                            break;
                        case { } when tmp.StartsWith("four"):
                            nums.Add('4');
                            //i+=3;
                            break;
                        case { } when tmp.StartsWith("five"):
                            nums.Add('5');
                            //i+=3;
                            break;
                        case { } when tmp.StartsWith("six"):
                            nums.Add('6');
                            //i+=2;
                            break;
                        case { } when tmp.StartsWith("seven"):
                            nums.Add('7');
                            //i+=4;
                            break;
                        case { } when tmp.StartsWith("eight"):
                            nums.Add('8');
                            //i+=4;
                            break;
                        case { } when tmp.StartsWith("nine"):
                            nums.Add('9');
                            //i+=3;
                            break;
                        default:
                            break;
                    }
                }
                //i++;
            }
            //if(nums.Count > 2) 
            //    Console.WriteLine("Something Broke");
            sum += int.Parse(nums[0].ToString() + nums[^1]);
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + sum);
    }


}
