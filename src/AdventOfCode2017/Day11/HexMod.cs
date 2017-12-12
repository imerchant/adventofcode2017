using System.Collections.Generic;

namespace AdventOfCode2017.Day11
{
    public struct HexMod
    {
        public static readonly IDictionary<Direction, HexMod> Mutations = new Dictionary<Direction, HexMod>
        {
            { Direction.N,  new HexMod( 0,  1) },
            { Direction.NE, new HexMod( 1,  0) },
            { Direction.SE, new HexMod( 1, -1) },
            { Direction.S,  new HexMod( 0, -1) },
            { Direction.SW, new HexMod(-1,  0) },
            { Direction.NW, new HexMod(-1,  1) }
        };

        public int X { get; }
        public int Y { get; }

        private HexMod(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
