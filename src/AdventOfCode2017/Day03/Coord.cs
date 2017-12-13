using System;

namespace AdventOfCode2017.Day03
{
    public class Coord : IEquatable<Coord>
    {
        public int X { get; }
        public int Y { get; }
        public int Index { get; set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coord Modify(Mod mod)
        {
            return new Coord(X + mod.X, Y + mod.Y);
        }

        public bool Equals(Coord other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Coord) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }
}
