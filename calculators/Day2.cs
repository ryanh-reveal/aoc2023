namespace aoc2023 {
    public class Day2(int passedPart) : ICalculator
    {
        private string _contents;
        private int _part;
        public string contents { get => _contents; set => _contents = File.ReadAllText("/Users/ryanheitmann/repos/personal/aoc2023/data/day2.txt"); }
        public int part { get => _part; set => _part = passedPart; }

        public void Run()
        {
            int sum = calcSum(_contents);
            Console.WriteLine($"Day2, Part{_part}:");
        }

        private int calcSum(string data)
        {
            
            bool checkWords = _part == 2 ? true: false;
            // var lines = data.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var gameData = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                        Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                        Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                        Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                        Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
            var lines = gameData.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            // solution = 8
            int currentGame = 1;
            int runningSum = 0;

            foreach(var line in lines)
            {
                bool validGame = checkGameFeasability(line);
                if (validGame)
                {
                    runningSum += currentGame;
                }
                currentGame++;
            }

            return runningSum;
        }

        private bool checkGameFeasability(string line)
        {
            bool validGame = false;
            int roundCount = 0;

            // chop Game X
            var gameStats = line.Split(":")[1];
            var games = gameStats.Split(";");

            foreach (var game in games)
            {
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
                roundCount++;
            }
            return validGame;
        }

        private bool isValidRound(string round)
        {
            bool isValid = false;
            int[] rgbLimits = [12, 13, 14];

            Dictionary<string, int> rgbTracker = new Dictionary<string, int>(){
                {"red", 0},
                {"green", 0},
                {"blue", 0}
            };
            // split on ,
            // split on space, grab digits in [0] and grab first char in [1]
            // scan and add numbers to a dictionary or something
            // sum up each r, g, b and check if it exceeds rgb limit

            return isValid;
        }

    }
}