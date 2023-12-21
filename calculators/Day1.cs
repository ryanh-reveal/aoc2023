namespace aoc2023
{
    public class Day1 : ICalculator
    {
        private readonly int _part;
        private readonly string _contents;
        private readonly int _day;

        public Day1()
        {
            _contents = "";
        }

        public Day1(int day, int part): this()
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
            Console.WriteLine($"Day1, Part{_part}: {sum}");
        }

        
        private int calcSum(string data, int part)
        {
            bool checkWords = part == 2 ? true : false;
            var lines = data.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            int runningSum = 0;
            int lineCounter = 1;

            foreach(var line in lines)
            {
                int left = matchLeft(line, checkWords);
                int right = matchRight(line, checkWords);
                int sum = left * 10 + right;
                runningSum += sum;   
                // System.Console.WriteLine($"Line {lineCounter}: {line}, Left: {left}, Right: {right}  => {sum}  ==> {runningSum}");
                lineCounter++;
            }

            return runningSum;
        }

        private int matchLeft(string line, bool checkWords)
        {
            bool found = false;
            int digit = 0;
            int counter = 0;
            while (!found)
            {
                if (checkWords)
                {
                    string subString = line[..counter];
                    string digitString = findFirstNumberString(subString);
                    if (!String.IsNullOrWhiteSpace(digitString))
                    {
                        digit = getDigitFromSpelling(digitString);
                        found = true;
                    }

                }
                if (digit == 0 && char.IsNumber(line[counter]))
                {
                    digit = (int)Char.GetNumericValue(line[counter]);
                    found = true;
                }

                
                counter++;
            }
            return digit;
        }

        private int matchRight(string line, bool checkWords)
        {

            bool found = false;
            int digit = 0;
            int counter = line.Length - 1;
            while (!found)
            {
                if (checkWords)
                {
                    var subString = line.Substring(counter, line.Length - counter);
                    var digitString = findFirstNumberString(subString);
                    if (!String.IsNullOrWhiteSpace(digitString))
                    {
                        digit = getDigitFromSpelling(digitString);
                        found = true;
                    }
                }
                if (digit == 0 && char.IsNumber(line[counter]))
                {
                    digit = (int)Char.GetNumericValue(line[counter]);
                    found = true;
                }


                counter--;
            }
            return digit;
        }

        private string findFirstNumberString(string line)
        {
            string[] wordBank = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
            
            var result = wordBank.Where(t => line.Contains(t)).FirstOrDefault();
            if (result != null)
            {
                return result;
            }
            return "";
        }

        private int getDigitFromSpelling(string digit)
        {
            Dictionary<string, int> digitMap = new Dictionary<string, int>()
            {
                {"one", 1},
                {"two", 2},
                {"three", 3},
                {"four", 4},
                {"five", 5},
                {"six", 6},
                {"seven", 7},
                {"eight", 8},
                {"nine", 9}
            };

            return digitMap[digit];
        }
    }
}