namespace aoc2023 {
    public static class CalculatorFactory {
        public static ICalculator? CreateCalculator(int day, int part)
        {
            switch (day) {
                case 1:
                    return new Day1(day, part);
                case 2:
                    return new Day2(day, part);
                case 3:
                    return new Day3(day, part);
                default:
                    return null;
            }
        }
    }
}