namespace aoc2023 
{
    public class Day3 : BaseCalculator
    {

        public Day3()
        {
        }

        public Day3(int day, int part): this()
        {
            _contents = "";
        }

        public override void Run()
        {
            int sum = 0;
            Console.WriteLine($"Day{_day}, Part{_part}: {sum}");
        }

    }
}