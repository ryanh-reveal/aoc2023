namespace aoc2023 {
    public static class Solution {

        public static void Run(string day, string part)
        {
            try
            {
                int dayInt = int.Parse(day);
                int partInt = int.Parse(part);
                ICalculator? calculator = CalculatorFactory.CreateCalculator(dayInt, partInt);
                if (calculator is null)
                {
                    Console.WriteLine("Invalid day or part");
                    return;
                }
                calculator.Run();
            }
            catch (System.Exception)
            {
                Console.WriteLine("Invalid day or part, must enter two numbers ie: '1 2'");
            }

        }
        
    }
}