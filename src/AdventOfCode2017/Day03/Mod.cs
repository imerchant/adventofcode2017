namespace AdventOfCode2017.Day03
{
    public class Mod
    {
        public int X { get; }
        public int Y { get; }
        public Mod Previous { get; set; }
        public Mod Next { get; set; }

        public Mod(int x, int y, Mod previous = null, Mod next = null)
        {
            X = x;
            Y = y;
            Previous = previous;
            Next = next;
        }
    }
}
