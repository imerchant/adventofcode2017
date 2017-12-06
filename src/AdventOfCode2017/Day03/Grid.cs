using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2017.Day03
{
    public class Grid
    {
        public HashSet<Coord> Coordinates { get; }
        private readonly Mod _startMod;

        public Grid()
        {
            Coordinates = new HashSet<Coord>();

            var fourth = new Mod(0, -1);
            var third = new Mod(-1, 0, next: fourth);
            var second = new Mod(0, 1, next: third);
            var first = new Mod(1, 0, previous: fourth, next: second);

            second.Previous = first;
            third.Previous = second;
            fourth.Previous = third;
            fourth.Next = first;

            _startMod = first;
        }

        public (int x, int y, HashSet<Coord> coords) RunTo(int target)
        {
            if (target < 1)
                throw new Exception("can't do that");

            var coord = new Coord(0, 0);
            var value = 1;

            Coordinates.Add(coord);

            var mod = _startMod;

            var count = 1;
            do
            {
                var nextCoord = coord.Modify(mod);
                if (Coordinates.Add(nextCoord))
                {
                    value++;
                    mod = mod.Next;
                    coord = nextCoord;
                    if (value == target)
                    {
                        return (nextCoord.X, nextCoord.Y, Coordinates);
                    }
                }
                else
                {
                    mod = mod.Previous;
                }
                count++;
            } while (count < 1000000);
            throw new Exception("went too long");
        }
    }

    public class Coord : IEquatable<Coord>
    {
        public int X { get; }
        public int Y { get; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coord Modify(Mod mod)
        {
            return new Coord(X + mod.X, Y + mod.Y);
        }

        public override string ToString() => $"({X}, {Y})";

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

    public class Mod
    {
        public int X { get; }
        public int Y { get; }
        public Mod Previous { get; set; }
        public Mod Next { get; set; }

        public Mod(int x, int y, Mod previous = null, Mod next = null)
        {
            X = x;
            Y = y;
            Previous = previous;
            Next = next;
        }
    }
}
