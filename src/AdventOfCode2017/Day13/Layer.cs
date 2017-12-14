namespace AdventOfCode2017.Day13
{
    public class Layer
    {
        private int movementMod = 1;

        public int Id { get; }
        public int Depth { get; }
        public int Severity => Id * Depth;
        public bool HasScanner { get; }
        public int ScannerPosition { get; private set; }

        public Layer(int id, int depth, bool hasScanner)
        {
            Id = id;
            Depth = depth;
            HasScanner = hasScanner;
            ScannerPosition = HasScanner ? 0 : -1;
        }

        public bool IsSafe(int delay)
        {
            return !HasScanner || (delay + Id) % ((Depth - 1) * 2) != 0;
        }

        public void Step()
        {
            if(HasScanner)
            {
                ScannerPosition += movementMod;

                if(ScannerPosition == 0 || ScannerPosition == Depth - 1)
                {
                    movementMod = -movementMod;
                }
            }
        }

        public static Layer Parse(string input)
        {
            var split = input.SplitOn(':', ' ');
            var id = int.Parse(split[0]);
            var depth = int.Parse(split[1]);
            return new Layer(id, depth, true);
        }
    }
}