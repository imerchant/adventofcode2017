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
            return DepthFirst(new List<Bridge>());
        }

        private IEnumerable<Bridge> DepthFirst(List<Bridge> bridges)
        {
            var bridge = bridges.LastOrDefault()?.Components ?? new [] { (0, 0) };
            var current = bridge.Last().B;
            foreach (var next in Components[current])
            {
                if (!bridge.Contains((current, next)) && !bridge.Contains((next, current)))
                {
                    bridges.Add(new Bridge(bridge.Append((current, next))));
                    DepthFirst(bridges);
                }
            }
            return bridges;
        }
    }
}