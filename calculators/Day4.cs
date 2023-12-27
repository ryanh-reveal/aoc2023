

namespace aoc2023
{
    public class Day4 : BaseCalculator
    {
        public Day4()
        {
        }

        public Day4(int day, int part) : base(day, part)
        {
        }

        public override void Run()
        {
            string testData = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";
            int sum = calcSum(_contents, _part);
            Console.WriteLine($"Day{_day}, Part{_part}: {sum}");
        }

        private int calcSum(string contents, int part)
        {
            return part == 1 ? calcPartOne(contents) : calcPartTwo(contents);
        }

        private int calcPartOne(string data)
        {
            int runningSum = 0;
            var lines = data.Split("\n", StringSplitOptions.TrimEntries);

            foreach(var line in lines)
            {
                int points = calculateCardPoints(line);
                runningSum += points;
            }
            
            return runningSum;
        }

        private int calculateCardPoints(string line)
        {
            var rawNumberData = line.Split(':', StringSplitOptions.TrimEntries)[1];
            var numberData = rawNumberData.Split('|', StringSplitOptions.TrimEntries);
            var winningData = numberData[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var cardData = numberData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int wins = calculateWinningNumbers(cardData, winningData);
            int points = calculatePointValue(wins);
            return points;
        }

        private int calculateWinningNumbers(string[] cardData, string[] winningData)
        {
            int winCount = 0;
            HashSet<string> winningSet = new(winningData);
            foreach(var card in cardData)
            {
                if (winningSet.Contains(card))
                {
                    winCount++;
                }
            }
            return winCount;
            
        }
        private int calculatePointValue(int wins)
        {
            return (int)Math.Pow(2, wins - 1);
        }

        private int calcPartTwo(string data)
        {
            // similar to part 1, but we take x copies of the next cards, where x is wins of current card
            // need to do some recursion i think
            int runningSum = 0;
            var lines = data.Split("\n", StringSplitOptions.TrimEntries);

            foreach(var line in lines)
            {
                int points = calculateCardPoints(line);
                runningSum += points;
            }
            
            return runningSum;
            throw new NotImplementedException();
        }
    }
}