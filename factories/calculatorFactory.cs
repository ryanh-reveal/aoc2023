namespace aoc2023 {
    public static class CalculatorFactory {
        public static ICalculator CreateCalculator(int day, int part)
        {
            switch (day) {
                case 1:
                    return new Day1(part);
                default:
                    return null;
            }
        }
    }
}