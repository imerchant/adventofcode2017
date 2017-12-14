using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day13
{
    public class Trench
    {
        public IDictionary<int, Layer> Layers { get; }
        public int MoverPosition { get; private set; }
        public int Severity { get; private set;}
        public bool MoverDone => MoverPosition >= Layers.Count;

        public Trench(IList<string> input)
        {
            MoverPosition = -1;
            Severity = 0;

            Layers = new Dictionary<int, Layer>();
            foreach (var line in input)
            {
                var layer = Layer.Parse(line);
                Layers[layer.Id] = layer;
            }

            var highestLayer = Layers.Keys.Max();
            for (var k = 0; k < highestLayer; ++k)
            {
                if(!Layers.TryGetValue(k, out var _))
                {
                    Layers[k] = new Layer(k, 0, false);
                }
            }
        }

        public int FindFirstSafeCrossing()
        {
            var delay = 0;
            while(Layers.Values.Any(x => !x.IsSafe(delay)))
                delay++;
            return delay;
        }

        public void WalkMoverToEnd()
        {
            while(!MoverDone)
            {
                Step();
            }
        }

        public void Step()
        {
            MoverPosition++;
            if(MoverDone)
                return;
            var newLocation = Layers[MoverPosition];
            if(newLocation.ScannerPosition == 0)
                Severity += newLocation.Severity;
            foreach (var layer in Layers)
            {
                layer.Value.Step();
            }
        }
    }
}
