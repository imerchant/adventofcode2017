using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day11
{
    public class HexGrid
    {
        public HexCoord Current { get; private set; }

        public int DistanceToCenter => Current.DistanceTo(HexCoord.Zero);

        public HexGrid()
        {
            Current = new HexCoord(0, 0);
        }

        public void Travel(Direction direction)
        {
            Current = Current.Mutate(direction);
        }

        public void Travel(IEnumerable<Direction> directions)
        {
            Current = directions.Aggregate(Current, (cur, item) => cur.Mutate(item));
        }
    }
}
