using System.Dynamic;

namespace aoc2023 
{
    public class Day3 : BaseCalculator
    {

        public Day3()
        {
        }

        public Day3(int day, int part): base(day, part)
        {
        }

        public override void Run()
        {
            int sum = getSolution(_contents, _part);
            Console.WriteLine($"Day{_day}, Part{_part}: {sum}");
        }

        private int getSolution(string data, int part)
        {
            return part == 1 ? partOne(data) : partTwo(data);
        }

        private int partOne(string data)
        {
            int runningSum = 0;
            int lineCounter = 1;
            var lines = data.Split("\n", StringSplitOptions.TrimEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                var previousLine = i == 0 ? "" : lines[i-1];
                var currentLine = lines[i];
                var nextLine = i < lines.Length - 1 ? lines[i+1] : "";

                List<(int, int)> numberIndexes = getNumberIndexes(currentLine);
                foreach(var index in numberIndexes)
                {
                    System.Console.WriteLine($"Found number: {getNumberFromIndexes(index, currentLine)}");
                }

                List<int> previousSymbolIndexes = getSymbolIndexes(previousLine);
                List<int> currentSymbolIndexes = getSymbolIndexes(currentLine);
                List<int> nextSymbolIndexes = getSymbolIndexes(nextLine);

                foreach(var number in numberIndexes)
                {
                    if (numberHasAdjacentSymbols(number, currentSymbolIndexes) 
                    || numberHasAdjacentSymbols(number, previousSymbolIndexes) 
                    || numberHasAdjacentSymbols(number, nextSymbolIndexes))
                    {
                        // System.Console.WriteLine($"Found number: {getNumberFromIndexes(number, currentLine)}");
                        runningSum += getNumberFromIndexes(number, currentLine);
                    }
                    else
                    {

                        System.Console.WriteLine($"Did not find number: {getNumberFromIndexes(number, currentLine)}, Line: {lineCounter}");
                    }
                    
                }
                lineCounter++;

            }

            return runningSum;
        }

        private int getNumberFromIndexes((int, int) number, string currentLine)
        {
            string numberString = currentLine.Substring(number.Item1, number.Item2 - number.Item1 + 1);
            return int.Parse(numberString);
        }

        private bool numberHasAdjacentSymbols((int, int) number, List<int> symbolIndexes)
        {
            bool startSymbol = symbolIndexes.Contains(number.Item1 - 1) || symbolIndexes.Contains(number.Item1) || symbolIndexes.Contains(number.Item1 + 1);
            bool endSymbol = symbolIndexes.Contains(number.Item2 + 1) || symbolIndexes.Contains(number.Item2) || symbolIndexes.Contains(number.Item2 - 1);
            return startSymbol || endSymbol;
        }

        // returns first and last index
        private List<(int, int)> getNumberIndexes(string line)
        {
            var numbers = new List<(int, int)>();
            bool isNumber = false;
            int numberHolder = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsNumber(line[i]))
                {
                    if (isNumber is false)
                    {
                        numberHolder = i;
                        isNumber = true;   
                    }
                }
                else 
                {
                    if (isNumber is true)
                    {
                        numbers.Add((numberHolder, i - 1));
                        isNumber = false;
                    }
                }
            }
            if (isNumber is true)
            {
                numbers.Add((numberHolder, line.Length - 1));
            }
            return numbers;
        }

        private List<int> getSymbolIndexes(string line)
        {
            List<int> symbolIndexes = new List<int>();
            for (int i = 0; i < line.Length; i++)
            {
                if (!line[i].Equals('.') && !char.IsNumber(line[i]))
                {
                    symbolIndexes.Add(i);
                }
            }
            return symbolIndexes;
        }

        private int partTwo(string data)
        {
            int runningSum = 0;
            int lineCounter = 1;
            var lines = data.Split("\n", StringSplitOptions.TrimEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                var previousLine = i == 0 ? "" : lines[i-1];
                var currentLine = lines[i];
                var nextLine = i < lines.Length - 1 ? lines[i+1] : "";

                List<int> gearIndexes = getIndex('*',currentLine);
                foreach(var index in gearIndexes)
                {
                    var numberIndexes = getAdjacentNumbers(index, previousLine, currentLine, nextLine);
                    var gearRatio = calculateGearRatio(numberIndexes);
                    // System.Console.WriteLine($"Gear ratio: {gearRatio}");
                    runningSum += gearRatio;
                }
            }
            return runningSum;
        }

        private int calculateGearRatio(List<int> numbers)
        {
            int multiplier = 0;
            foreach(var number in numbers)
            {
                if (multiplier != 0)
                {
                     multiplier *= number;
                }
                else multiplier = number;
            }
            return multiplier;
        }

        private List<int> getAdjacentNumbers(int index, string previousLine, string currentLine, string nextLine)
        {
            var adjacentNumbers = new List<int>();
            List<string> lines = new List<string>(){previousLine, currentLine, nextLine};

            foreach(var line in lines)
            {
                var numberIndexes = getNumberIndexes(line);
                foreach(var number in numberIndexes)
                {
                    if (numberIsAdjacent(index, number))
                    {
                        adjacentNumbers.Add(getNumberFromIndexes(number, line));
                    }
                }
            }

            if (adjacentNumbers.Count == 2)
            {
                // adjacentNumbers.ForEach(x => System.Console.WriteLine($"Adjacent number: {x}"));
                return adjacentNumbers;
            }
            return [];
        }

        private bool numberIsAdjacent(int gearIndex, (int, int) numberIndex)
        {
            bool leftBounds = gearIndex >= numberIndex.Item1 - 1;
            bool rightBounds = gearIndex <= numberIndex.Item2 + 1;
            return leftBounds && rightBounds;
        }

        private List<int> getIndex(char search, string currentLine)
        {
            var indexes = new List<int>();
            for (int i = 0; i < currentLine.Length; i++)
            {
                if (currentLine[i].Equals(search))
                {
                    indexes.Add(i);
                }
            }
            return indexes;
        }
    }
}