namespace AdventOfCode2017.Day22
{
    public class Direction
    {
        public string Name { get; }
        public int MutateX { get; }
        public int MutateY { get; }
        public Direction Left { get; set; }
        public Direction Right { get; set; }

        public Direction(string name, int x, int y, Direction left = null, Direction right = null)
        {
            Name = name;
            MutateX = x;
            MutateY = y;
            Left = left;
            Right = right;
        }
    }
}