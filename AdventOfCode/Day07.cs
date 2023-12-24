namespace AdventOfCode;

public class Day07 : BaseDay
{
    private readonly string _input;
    private readonly string[] _inputs;
    private static readonly char[] separator = [' '];

    public Day07()
    {
        _input = File.ReadAllText(InputFilePath);
        _inputs = _input.Split("\r\n");
    }

    public override ValueTask<string> Solve_1()
    {
        var total = 0;
        var hands = new List<Hand>();
        foreach (var hand in _inputs)
        {
            hands.Add(new Hand(hand.Split(separator)[0], int.Parse(hand.Split(separator)[1])));
        }
        var sortedHands = new List<Hand>();
        sortedHands.AddRange(hands.Where(h => h.FiveKind()).OrderDescending());
        sortedHands.AddRange(hands.Where(h => h.FourKind()).OrderDescending());
        sortedHands.AddRange(hands.Where(h => h.FullHouse()).OrderDescending());
        sortedHands.AddRange(hands.Where(h => h.ThreeKind()).OrderDescending());
        sortedHands.AddRange(hands.Where(h => h.TwoPair()).OrderDescending());
        sortedHands.AddRange(hands.Where(h => h.Pair()).OrderDescending());
        sortedHands.AddRange(hands.Where(h => h.HighCard()).OrderDescending());

        sortedHands.Reverse();
        var count = 1;
        foreach(var hand in sortedHands)
        {
            total += count * hand.bid;
            count++;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is " + total);
    }



    public override ValueTask<string> Solve_2()
    {
        long total = 0;
        var hands = new List<JokerHand>();
        foreach (var hand in _inputs)
        {
            hands.Add(new JokerHand(hand.Split(separator)[0], int.Parse(hand.Split(separator)[1])));
        }
        var sortedHands = new List<JokerHand>();
        sortedHands.AddRange(hands.Where(h => h.FiveKind()).OrderDescending());
        hands.RemoveAll(h => h.FiveKind());
        sortedHands.AddRange(hands.Where(h => h.FourKind()).OrderDescending());
        hands.RemoveAll(h => h.FourKind());
        sortedHands.AddRange(hands.Where(h => h.FullHouse()).OrderDescending());
        hands.RemoveAll(h => h.FullHouse());
        sortedHands.AddRange(hands.Where(h => h.ThreeKind()).OrderDescending());
        hands.RemoveAll(h => h.ThreeKind());
        sortedHands.AddRange(hands.Where(h => h.TwoPair()).OrderDescending());
        hands.RemoveAll(h => h.TwoPair());
        sortedHands.AddRange(hands.Where(h => h.Pair()).OrderDescending());
        hands.RemoveAll(h => h.Pair());
        sortedHands.AddRange(hands.Where(h => h.HighCard()).OrderDescending());
        hands.RemoveAll(h => h.HighCard());

        sortedHands.Reverse();
        var count = 1;
        foreach(var hand in sortedHands)
        {
            total += count * hand.bid;
            count++;
        }

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is " + total);
    }

    public class Hand(string cards, int bid) : IComparable<Hand>
    {
        public string cards = cards;
        public int bid = bid;

        public bool FiveKind()
        {
            return cards.Distinct().Count() == 1;
        }

        public bool FourKind()
        {
            var distincts = cards.Distinct().ToArray();
            return !FiveKind() && distincts.Count() == 2
                && (cards.Where(c => c == distincts[0]).Count() == 4
                || cards.Where(c => c == distincts[1]).Count() == 4);
        }
        public bool FullHouse()
        {
            var distincts = cards.Distinct().ToArray();
            return !FourKind() && distincts.Length == 2
                && ((cards.Where(c => c == distincts[0]).Count() == 3
                && cards.Where(c => c == distincts[1]).Count() == 2)
                || (cards.Where(c => c == distincts[0]).Count() == 2
                && cards.Where(c => c == distincts[1]).Count() == 3));
        }
        public bool ThreeKind()
        {
            var distincts = cards.Distinct().ToArray();
            return !FullHouse() && distincts.Length == 3
                && (cards.Where(c => c == distincts[0]).Count() == 3
                || cards.Where(c => c == distincts[1]).Count() == 3
                || (cards.Where(c => c == distincts[2]).Count() == 3));
        }
        public bool TwoPair()
        {
            var distincts = cards.Distinct().ToArray();
            return !ThreeKind() && distincts.Length == 3;
        }
        public bool Pair()
        {
            var distincts = cards.Distinct().ToArray();
            return !TwoPair() && distincts.Length == 4;
        }
        public bool HighCard()
        {
            var distincts = cards.Distinct().ToArray();
            return distincts.Length == 5;
        }

        public int CompareTo(Hand other)
        {
            for(int i = 0; i < 5; i++)
            {
                int returnValue = CompareCard(this.cards[i], other.cards[i]);
                if(returnValue != 0) return returnValue;
            }
            return 0;
        }

        public int CompareCard(char a, char b)
        {
            if (a == b) return 0;
            if (a == 'A') return 1;
            if (b == 'A') return -1;
            if (a == 'K') return 1;
            if (b == 'K') return -1;
            if (a == 'Q') return 1;
            if (b == 'Q') return -1;
            if (a == 'J') return 1;
            if (b == 'J') return -1;
            if (a == 'T') return 1;
            if (b == 'T') return -1;
            if (a == '9') return 1;
            if (b == '9') return -1;
            if (a == '8') return 1;
            if (b == '8') return -1;
            if (a == '7') return 1;
            if (b == '7') return -1;
            if (a == '6') return 1;
            if (b == '6') return -1;
            if (a == '5') return 1;
            if (b == '5') return -1;
            if (a == '4') return 1;
            if (b == '4') return -1;
            if (a == '3') return 1;
            if (b == '3') return -1;
            if (a == '2') return 1;
            if (b == '2') return -1;
            return 0;
        }
    }

       public class JokerHand(string cards, int bid) : IComparable<JokerHand>
    {
        public string cards = cards;
        public int bid = bid;

        public bool FiveKind()
        {
            var newCards = cards.Replace("J", string.Empty);
            return newCards ==  "" || newCards.Distinct().Count() == 1;
        }

        public bool FourKind()
        {
            var newCards = cards.Replace("J", string.Empty);
            var distincts = newCards.Distinct().ToArray();
            return !FiveKind() && distincts.Length == 2
                && (newCards.Where(c => c == distincts[0]).Count() == 1
                || newCards.Where(c => c == distincts[1]).Count() == 1);
        }
        public bool FullHouse()
        {
            var newCards = cards.Replace("J", string.Empty);
            var distincts = newCards.Distinct().ToArray();
            return !FourKind() && distincts.Length == 2
                && ((newCards.Where(c => c == distincts[0]).Count() == 2
                || newCards.Where(c => c == distincts[1]).Count() == 2));
        }
        public bool ThreeKind()
        {
            if(FullHouse()) return false;
            var newCards = cards.Replace("J", string.Empty);
            var distincts = newCards.Distinct().ToArray();
            if(newCards.Length == 5) return distincts.Length == 3
                && (newCards.Where(c => c == distincts[0]).Count() == 3
                || newCards.Where(c => c == distincts[1]).Count() == 3
                || (newCards.Where(c => c == distincts[2]).Count() == 3));
             if(newCards.Length == 4) return distincts.Length == 3
                && (newCards.Where(c => c == distincts[0]).Count() == 2
                || newCards.Where(c => c == distincts[1]).Count() == 2
                || (newCards.Where(c => c == distincts[2]).Count() == 2));           
            return true;
        }
        public bool TwoPair()
        {
            var newCards = cards.Replace("J", string.Empty);
            var distincts = newCards.Distinct().ToArray();
            return !ThreeKind() && distincts.Length == 3 && newCards.Length > 3;
        }
        public bool Pair()
        {
            var newCards = cards.Replace("J", string.Empty);
            var distincts = newCards.Distinct().ToArray();
            return !TwoPair() && distincts.Length == 4;
        }
        public bool HighCard()
        {
            var distincts = cards.Distinct().ToArray();
            return distincts.Length == 5;
        }

        public int CompareTo(JokerHand other)
        {
            for(int i = 0; i < 5; i++)
            {
                int returnValue = CompareCard(this.cards[i], other.cards[i]);
                if(returnValue != 0) return returnValue;
            }
            return 0;
        }

        public int CompareCard(char a, char b)
        {
            if (a == b) return 0;
            if (a == 'A') return 1;
            if (b == 'A') return -1;
            if (a == 'K') return 1;
            if (b == 'K') return -1;
            if (a == 'Q') return 1;
            if (b == 'Q') return -1;
            if (a == 'T') return 1;
            if (b == 'T') return -1;
            if (a == '9') return 1;
            if (b == '9') return -1;
            if (a == '8') return 1;
            if (b == '8') return -1;
            if (a == '7') return 1;
            if (b == '7') return -1;
            if (a == '6') return 1;
            if (b == '6') return -1;
            if (a == '5') return 1;
            if (b == '5') return -1;
            if (a == '4') return 1;
            if (b == '4') return -1;
            if (a == '3') return 1;
            if (b == '3') return -1;
            if (a == '2') return 1;
            if (b == '2') return -1;
            if (a == 'J') return 1;
            if (b == 'J') return -1;
            return 0;
        }
    }
}
