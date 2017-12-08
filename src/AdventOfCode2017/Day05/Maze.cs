using System.Collections.Generic;

namespace AdventOfCode2017.Day05
{
    public class Maze
    {
        public IList<int> Nodes { get; }

        public Maze(IList<int> nodes)
        {
            Nodes = nodes;
        }

        public int CalculateStepsToExit()
        {
            var steps = 0;
            var index = 0;
            do
            {
                index = index + Nodes[index]++;
                ++steps;
            } while (index >= 0 && index < Nodes.Count);

            return steps;
        }
    }
}
