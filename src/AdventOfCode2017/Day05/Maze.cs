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

        public int CalculateStepsToExitWithComplexValueCalc()
        {
            var steps = 0;
            var index = 0;
            do
            {
                var jumps = Nodes[index];
                Nodes[index] = ComplexValueCalc(Nodes[index]);
                index = index + jumps;
                ++steps;
            } while (index >= 0 && index < Nodes.Count);

            return steps;
        }

        private static int ComplexValueCalc(int value)
        {
            return value >= 3 ? value - 1 : value + 1;
        }
    }
}
