using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day13
{
    public class Trench
    {
        public IList<Layer> Layers { get; }

        public Trench(IList<string> input)
        {
            Layers = input.Select(Layer.Parse).ToList();
        }

        public int FindFirstSafeCrossing()
        {
            var delay = 0;
            while(Layers.Any(x => !x.IsSafe(delay)))
                delay++;
            return delay;
        }

        public int SeverityOfCrossing(int delay = 0)
        {
            return Layers.Where(x => !x.IsSafe(delay)).Sum(x => x.Severity);
        }
    }
}
