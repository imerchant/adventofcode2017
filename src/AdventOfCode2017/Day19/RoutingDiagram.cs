using System.Linq;

namespace AdventOfCode2017.Day19
{
    public class RoutingDiagram
    {
        public char[][] Grid { get; }
        public (int x, int y) StartingPoint { get; }

        public RoutingDiagram(char[][] grid)
        {
            Grid = grid;
            StartingPoint = (0, Grid[0].AsString().IndexOf('|'));
        }

        public override string ToString()
        {
            return Grid.Select(x => new string(x)).JoinStrings("\n");
        }
    }
}
