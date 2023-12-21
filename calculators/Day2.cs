namespace aoc2023 {
    public class Day2 : ICalculator 
    {
        private readonly int _part;
        private readonly string _contents;
        private readonly int _day;

        public Day2()
        {
            _contents = "";
        }
        public Day2(int day, int part): this()
        {
            _part = part;
            _day = day;
            _contents = getSolutionData();
        }

        private string getSolutionData()
        {
            var path = Directory.GetFiles(Directory.GetCurrentDirectory(), $"day{_day}.txt", SearchOption.AllDirectories).FirstOrDefault();
            return path != null ? File.ReadAllText(path) : "";
        }
        public void Run()
        {
            int sum = calcSum(_contents, _part);
            Console.WriteLine($"Day2, Part{_part}: {sum}");
        }

        private int calcSum(string data, int part)
        {
            bool findMin = part == 2 ? true: false;
            
            var gameData = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                        Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                        Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                        Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                        Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

                        /*
            In game 1, the game could have been played with as few as 4 red, 2 green, and 6 blue cubes. If any color had even one fewer cube, the game would have been impossible.
            Game 2 could have been played with a minimum of 1 red, 3 green, and 4 blue cubes.
            Game 3 must have been played with at least 20 red, 13 green, and 6 blue cubes.
            Game 4 required at least 14 red, 3 green, and 15 blue cubes.
            Game 5 needed no fewer than 6 red, 3 green, and 2 blue cubes in the bag.

            powers of games = 48, 12, 1560, 630, 36
            total power = 2286

                        */

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
            throw new NotImplementedException();
            // int runningSum = 0;
            // var lines = data.Trim().Split("\n", StringSplitOptions.TrimEntries);

            // foreach(var line in lines)
            // {

            //     bool validGame = checkGameFeasability(line);
            //     if (validGame)
            //     {
            //         runningSum += currentGame;

            //     }
            //     currentGame++;
            // }

            // return runningSum;
        }

        private bool checkGameFeasability(string line)
        {
            bool validGame = false;
            int roundCount = 0;

            // chop Game X
            var gameStats = line.Trim().Split(":", StringSplitOptions.TrimEntries)[1];
            var games = gameStats.Trim().Split(";", StringSplitOptions.TrimEntries);

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
            var data = round.Trim().Split(",", StringSplitOptions.TrimEntries);
            foreach(var item in data)
            {
                var itemData = item.Split(" ");
                var color = itemData[1];
                var count = itemData[0];
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
    }
}