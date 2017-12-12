using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day11
{
    public struct HexCoord
    {
        public static readonly HexCoord Zero = new HexCoord(0, 0);

        public int X { get; }
        public int Y { get; }

        //keekerdc.com/2011/03/hexagon-grids-coordinate-systems-and-distance-calculations/
        public int Z => -(X + Y);

        public HexCoord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public HexCoord Mutate(Direction direction)
        {
            var mod = HexMod.Mutations[direction];
            return new HexCoord(X + mod.X, Y + mod.Y);
        }

        public int DistanceTo(HexCoord coord)
        {
            var xDelta = Math.Abs(coord.X - X);
            var yDelta = Math.Abs(coord.Y - Y);
            var zDelta = Math.Abs(coord.Z - Z);
            return new[] { xDelta, yDelta, zDelta }.Max();
        }
    }
}
