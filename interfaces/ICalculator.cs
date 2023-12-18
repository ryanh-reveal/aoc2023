namespace aoc2023 {
    public interface ICalculator {
        public string contents { get; set; }
        public int part { get; set; }
        void Run();
    }
}