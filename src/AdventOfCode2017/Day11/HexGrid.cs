using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day11
{
    public class HexGrid
    {
        public HexCoord Current { get; private set; }

        public int FurthestDistance { get; private set; }

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
            Current = directions.Aggregate(Current, Step);

            HexCoord Step(HexCoord current, Direction direction)
            {
                var next = current.Mutate(direction);
                var distance = next.DistanceTo(HexCoord.Zero);

                if(distance > FurthestDistance)
                    FurthestDistance = distance;

                return next;
            }
        }
    }
}
