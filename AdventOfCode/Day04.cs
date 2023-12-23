namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string _input;
    private readonly string[] _inputs;
    private static readonly char[] separator = new char[] { ' ' };

    public Day04()
    {
        _input = File.ReadAllText(InputFilePath);
        _inputs = _input.Split("\r\n");
    }

    public override ValueTask<string> Solve_1()
    {
        int sum = 0;
        string[] scratchCards = _inputs;
        foreach (var line in scratchCards)
        {
            int winnings = 0;
            var card = line.Split(':')[1];
            var nums = card.Split('|');
            var winningNums = nums[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            var ownedNums = nums[1].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var num in ownedNums)
            {
                if (winningNums.Contains(num))
                {
                    winnings = winnings == 0 ? 1 : winnings * 2;
                }
            }
            sum += winnings;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + sum);
    }



    public override ValueTask<string> Solve_2()
    {
        int total = 0;
        string[] scratchCards = _inputs;
        total += scratchCards.Length;
        var counts = Enumerable.Repeat(1, total).ToArray();

        foreach (var line in scratchCards)
        {
            int count = 0;
            var splitLine = line.Split(':');
            var cardNum = int.Parse(splitLine[0].Split(separator, StringSplitOptions.RemoveEmptyEntries)[1]) - 1;
            var card = splitLine[1];
            var nums = card.Split('|');
            var winningNums = nums[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            var ownedNums = nums[1].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var num in ownedNums)
            {
                if (winningNums.Contains(num)) count++;
            }
            for (int i = cardNum + 1; i <= cardNum + count; i++)
            {
                counts[i] += counts[cardNum];
            }
            //sum += count;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + counts.Sum());
    }


}
