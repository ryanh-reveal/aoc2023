namespace aoc2023
{
    public abstract class BaseCalculator : ICalculator
    {
        protected int _part;
        protected string _contents;
        protected int _day;

        public int part;
        public int day;

        public BaseCalculator()
        {
            _contents = "";
        }

        public BaseCalculator(int day, int part)
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

        public virtual void Run()
        {
            throw new NotImplementedException();
        }
    }
}