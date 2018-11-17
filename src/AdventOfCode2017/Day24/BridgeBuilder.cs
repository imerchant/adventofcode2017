using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day24
{
    public class BridgeBuilder
    {
        public DefaultDictionary<int, HashSet<int>> Components { get; }

        public BridgeBuilder(IList<string> input)
        {
            Components = new DefaultDictionary<int, HashSet<int>>(input.Count * 2, () => new HashSet<int>());
            foreach (var line in input)
            {
                var ports = line.SplitOn('/').Select(int.Parse).ToList();
                Components[ports[0]].Add(ports[1]);
                Components[ports[1]].Add(ports[0]);
            }
        }

        public IEnumerable<Bridge> BuildBridges()
        {
            return Build(new List<(int A, int B)>(Components.Count) { (0, 0) }, Components);
        }

        private static IEnumerable<Bridge> Build(List<(int A, int B)> bridge, IDictionary<int, HashSet<int>> components)
        {
            var current = bridge.Last().B;
            foreach (var next in components[current])
            {
                if (!(bridge.Contains((current, next)) || bridge.Contains((next, current))))
                {
                    var newBridge = bridge.Append((current, next)).ToList();
                    yield return new Bridge(newBridge);
                    foreach (var otherNew in Build(newBridge, components))
                    {
                        yield return otherNew;
                    }
                }
            }
        }
    }
}