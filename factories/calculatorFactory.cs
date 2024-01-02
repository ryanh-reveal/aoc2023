namespace aoc2023 {
    public static class CalculatorFactory {
        public static ICalculator? CreateCalculator(int day, int part)
        {
            return day switch
            {
                1 => new Day1(day, part),
                2 => new Day2(day, part),
                3 => new Day3(day, part),
                4 => new Day4(day, part),
                5 => new Day5(day, part),
                _ => null,
            };
        }
    }
}