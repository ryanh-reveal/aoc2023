namespace aoc2023 {
    public static class Solution {

        public static void Run(string day, string part)
        {
            int dayInt = int.Parse(day);
            int partInt = int.Parse(part);
            ICalculator calculator = CalculatorFactory.CreateCalculator(dayInt, partInt);
            calculator.Run();
        }
        
    }
}