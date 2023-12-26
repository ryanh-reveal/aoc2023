namespace aoc2023 {
    public class Day2 : BaseCalculator 
    {

        public Day2()
        {
        }
        public Day2(int day, int part): this()
        {
        }

        public override void Run()
        {
            int sum = calcSum(_contents, _part);
            Console.WriteLine($"Day{_day}, Part{_part}: {sum}");
        }

        private int calcSum(string data, int part)
        {
            bool findMin = part == 2 ? true: false;
            return findMin ? getMinPower(data) : getValidGameIndexes(data);
        }

        private int getValidGameIndexes(string data)
        {
            var lines = data.Trim().Split("\n", StringSplitOptions.TrimEntries);
            int currentGame = 1;
            int runningSum = 0;

            foreach(var line in lines)
            {
                // System.Console.WriteLine($"Line: {line}");
                bool validGame = checkGameFeasability(line);
                if (validGame)
                {
                    runningSum += currentGame;
                    // System.Console.WriteLine($"======> Added game:{currentGame}, Running sum: {runningSum}");
                }
                currentGame++;
            }

            return runningSum;
        }

        private int getMinPower(string data)
        {
            var lines = data.Trim().Split("\n", StringSplitOptions.TrimEntries);
            int runningSum = 0;
            foreach(var line in lines)
            {
                var minCubeCount = getGameMinCubeCount(line);
                int gamePower = calculateGamePower(minCubeCount);
                runningSum += gamePower;
            }

            return runningSum;
        }

        private int calculateGamePower(Dictionary<string, int> minCubeCount)
        {
            int gamePower = 1;
            minCubeCount.Values.ToList().ForEach(x => gamePower *= x);
            return gamePower;
        }

        private Dictionary<string, int> getGameMinCubeCount(string line)
        {
            Dictionary<string, int> cubeTracker = new Dictionary<string, int>(){
                    {"red", 0},
                    {"green", 0},
                    {"blue", 0}
                };
            var rounds = parseRounds(line);
            foreach(var round in rounds)
            {
                var hands = parseHands(round);
                foreach(var hand in hands)
                {
                    var handData = hand.Split(" ");
                    var color = handData[1];
                    var count = handData[0];
                    var parsedCount = Int32.Parse(count);
                    if (parsedCount > cubeTracker[color])
                    {
                        cubeTracker[color] = parsedCount;
                    }
                }
            }
            return cubeTracker;
        }

        private bool checkGameFeasability(string line)
        {
            bool validGame = false;
            int roundCount = 0;

            var games = parseRounds(line);

            foreach (var game in games)
            {
                // System.Console.WriteLine($"Game: {game}");
                var validRound = isValidRound(game);
                if (validRound)
                {
                    if (roundCount == 0)
                    {
                        validGame = true;
                    }
                    else if (validGame == false)
                    {
                        continue;
                    }
                    else {
                        validGame = true;
                    }
                }
                else
                {
                    validGame = false;
                }
                roundCount++;
            }
            return validGame;
        }

        private bool isValidRound(string round)
        {
            bool isValid = false;
            int colorCheck = 0;
            Dictionary<string, int> rgbLimits = new Dictionary<string, int>(){
                {"red", 12},
                {"green", 13},
                {"blue", 14}
            };

            Dictionary<string, int> rgbTracker = new Dictionary<string, int>(){
                {"red", 0},
                {"green", 0},
                {"blue", 0}
            };
            string[] colors = ["red", "green", "blue"];
            // split on ,
            var hands = parseHands(round);
            foreach(var hand in hands)
            {
                var roundData = hand.Split(" ");
                var color = roundData[1];
                var count = roundData[0];
                var parsedCount = Int32.Parse(count);
                rgbTracker[color] += parsedCount;
            }

            // sum up each r, g, b and check if it exceeds rgb limit
            foreach(var color in colors)
            {
                // check r, g, b, if all are valid then return true
                // System.Console.WriteLine($"Color: {color}, Count: {rgbTracker[color]}, Limit: {rgbLimits[color]}");
                if (rgbTracker[color] <= rgbLimits[color])
                {
                    if (colorCheck == 0)
                    {
                        isValid = true;
                    }
                    else if (isValid == false)
                    {
                        continue;
                    }
                    else {
                        isValid = true;
                    }
                }
                else {
                    isValid = false;
                }
                colorCheck++;
            }
            return isValid;
        }

        private string[] parseRounds(string line)
        {
            var gameStats = line.Trim().Split(":", StringSplitOptions.TrimEntries)[1];
            var games = gameStats.Trim().Split(";", StringSplitOptions.TrimEntries);
            return games;
        }

        private string[] parseHands(string round)
        {
            return round.Trim().Split(",", StringSplitOptions.TrimEntries);
        }
    }
}