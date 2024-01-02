namespace aoc2023
{
    public class Day4 : BaseCalculator
    {
        public List<string> Lines { get; set; } = [];
        public Day4(): base()
        {
        }

        public Day4(int day, int part) : base(day, part)
        {
            Lines = _contents.Split("\n", StringSplitOptions.TrimEntries).ToList();
        }

        public override void Run()
        {
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
            var numberData = parseCardData(line);
            int wins = calculateWinningNumbers(numberData[1], numberData[0]);
            int points = calculatePointValue(wins);
            return points;
        }

        private List<string[]> parseCardData(string line)
        {
            List<string[]> dataSet = [];
            var rawNumberData = line.Split(':', StringSplitOptions.TrimEntries)[1];
            var numberData = rawNumberData.Split('|', StringSplitOptions.TrimEntries);
            dataSet.Add(numberData[0].Split(' ', StringSplitOptions.RemoveEmptyEntries));
            dataSet.Add(numberData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries));
            return dataSet;
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
            int sum = 0;
            for (int i = 0; i < Lines.Count; i++)
            {
                sum += countCopies(i);
            }
            return sum;
        }

        private int countCopies(int index)
        {
            int sum = 0;
            if (index > Lines.Count)
            {
                return 0;
            }
            
            string currentLine = Lines[index];
            sum += 1;

            var cardData = parseCardData(currentLine);
            int winningNumbers = calculateWinningNumbers(cardData[1], cardData[0]);

            List<int> winningCards = getWinningIndexes(index, winningNumbers);

            foreach(var card in winningCards)
            {
                sum += countCopies(card);
            }
            return sum;
        }

        private List<int> getWinningIndexes(int index, int winningNumbers)
        {
            List<int> winningIndexes = [];
            for (int i = 1; i < winningNumbers + 1; i++)
            {
                winningIndexes.Add(index + i);
            }
            return winningIndexes;
        }
    }
}