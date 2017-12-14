namespace AdventOfCode2017.Day13
{
    public class Layer
    {
        public int Id { get; }
        public int Depth { get; }
        public int Severity => Id * Depth;

        public Layer(int id, int depth)
        {
            Id = id;
            Depth = depth;
        }

        public bool IsSafe(int delay)
        {
            return (delay + Id) % ((Depth - 1) * 2) != 0;
        }

        public static Layer Parse(string input)
        {
            var split = input.SplitOn(':', ' ');
            var id = int.Parse(split[0]);
            var depth = int.Parse(split[1]);
            return new Layer(id, depth);
        }
    }
}