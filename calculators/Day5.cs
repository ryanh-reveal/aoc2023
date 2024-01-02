
namespace aoc2023
{
    public class Day5 : BaseCalculator
    {
        public string TestData = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";
        public Day5(): base()
        {
        }

        public Day5(int day, int part): base(day, part)
        {
        }

        public override void Run()
        {

            long sum = getSolution(_part, _contents);
            Console.WriteLine($"Day{_day}, Part{_part}: {sum}");
        }

        private long getSolution(int part, string data)
        {
            long sum = part == 1 ? partOne(data) : partTwo(data);
            return sum;
        }

        private long partOne(string data)
        {
            List<long> seeds = getSeeds(data);
            List<string> mapOrder = ["seed-to-soil", "soil-to-fertilizer", "fertilizer-to-water", "water-to-light", "light-to-temperature", "temperature-to-humidity", "humidity-to-location"];
            Dictionary<string, string> maps = getMaps(data, mapOrder);

            long lowestLocation = 0;
            foreach(var seed in seeds)
            {
                long location = getMapValueFromOrder(seed, mapOrder, maps);
                lowestLocation = lowestLocation == 0 ? lowestLocation = location : lowestLocation = Math.Min(lowestLocation, location);
            }
            return lowestLocation;
        }

        private long getMapValueFromOrder(long seed, List<string> mapOrder, Dictionary<string, string> maps)
        {
            long sourceValue = seed;
            foreach(var map in mapOrder)
            {
                string mapData = maps[map];
                List<long[]> mapDataArray = convertMapDataToArray(mapData);
                long mapValue = getMapValue(sourceValue, mapDataArray);
                sourceValue = mapValue;
            }
            return sourceValue;
        }

        private long getMapValue(long source, List<long[]> mapDataArray)
        {
            long target = 0;
            foreach(var data in mapDataArray)
            {
                (long start, long end) = calculateRange(data, true);
                if (source >= start && source <= end)
                {
                    long offset = source - start;
                    (long start, long end) destinationRange = calculateRange(data, false);
                    target =  destinationRange.start + offset;
                }
            }
            return target == 0 ? source : target;
        }

        private (long, long) calculateRange(long[] data, bool getSource)
        {
            (long, long) range;
            if(getSource)
            {
                range.Item1 = data[1];
                range.Item2 = data[1] + data[2];
            }
            else
            {
                range.Item1 = data[0];
                range.Item2 = data[0] + data[2];
            }
            return range;
        }

        private List<long[]> convertMapDataToArray(string mapData)
        {
            List<long[]> mapDataArray = new List<long[]>();
            foreach(var line in mapData.Split("\n", StringSplitOptions.RemoveEmptyEntries))
            {
                long[] lineArray = [.. convertNumberStringToArray(line)];
                mapDataArray.Add(lineArray);
            }
            return mapDataArray;
        }

        private Dictionary<string, string> getMaps(string data, List<string> mapOrder)
        {
            Dictionary<string, string> maps = new Dictionary<string, string>();
            foreach(var map in mapOrder)
            {
                string mapData = getMapData(data, map);
                maps.Add(map, mapData);
            }
            return maps;
        }

        private List<long> getSeeds(string contents)
        {
            string seeds = contents.Split("\n")[0].Split(":")[1].Trim();
            return convertNumberStringToArray(seeds);
        }

        private string getMapData(string contents, string mapName)
        {
            string mapData = contents.Split($"{mapName} map:")[1].Split("\n\n")[0];
            return mapData;
        }

        private List<long> convertNumberStringToArray(string seeds)
        {
            return seeds.Split(" ", StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToList();
        }

        private int partTwo(string data)
        {
            throw new NotImplementedException();
        }
    }
}