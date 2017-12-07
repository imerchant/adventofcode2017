using System;
using System.Collections.Generic;

namespace AdventOfCode2017.Day03
{
    public class Grid
    {
        public HashSet<Coord> Coordinates { get; }
        public IDictionary<(int x, int y), int> Values { get; }
        private readonly Mod _startMod;

        public Grid()
        {
            Coordinates = new HashSet<Coord>();
            Values = new Dictionary<(int x, int y), int>();

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

        public (Coord coord, HashSet<Coord> coords) RunToIndex(int target)
        {
            if (target < 1)
                throw new Exception("can't do that");

            var coord = new Coord(0, 0);
            var index = 1;
            coord.Index = index;

            Coordinates.Add(coord);

            var mod = _startMod;

            var count = 1;
            do
            {
                if (coord.Index == target)
                {
                    return (coord, Coordinates);
                }
                var nextCoord = coord.Modify(mod);
                if (Coordinates.Add(nextCoord))
                {
                    nextCoord.Index = ++index;
                    mod = mod.Next;
                    coord = nextCoord;
                }
                else
                {
                    mod = mod.Previous;
                }
                count++;
            } while (count < 1000000);
            throw new Exception("went too long");
        }

        public (Coord coord, HashSet<Coord> coords, IDictionary<(int x, int y), int> values) RunTo(int? targetIndex = null, int? targetValue = null)
        {
            if (targetValue < 1 || targetIndex < 1 || (!targetIndex.HasValue && !targetValue.HasValue))
                throw new Exception("can't do that");

            var coord = new Coord(0, 0);
            var index = 1;
            coord.Index = index;

            Coordinates.Add(coord);
            Values[(coord.X, coord.Y)] = 1;

            var mod = _startMod;

            var count = 1;
            do
            {
                if (Values[(coord.X, coord.Y)] >= targetValue || coord.Index == targetIndex)
                {
                    return (coord, Coordinates, Values);
                }
                var nextCoord = coord.Modify(mod);
                if (Coordinates.Add(nextCoord)) // could probably check for Value availability
                {
                    nextCoord.Index = ++index;
                    Values[(nextCoord.X, nextCoord.Y)] = GetValueFromNeighbors(nextCoord);
                    mod = mod.Next;
                    coord = nextCoord;
                }
                else
                {
                    mod = mod.Previous;
                }
                count++;
            } while (count < 1000000);
            throw new Exception("went too long");

            int GetValueFromNeighbors(Coord location)
            {
                var neighborMods = new List<Mod>
                {
                    new Mod(1, 0),
                    new Mod(1, 1),
                    new Mod(0, 1),
                    new Mod(-1, 1),
                    new Mod(-1, 0),
                    new Mod(-1, -1),
                    new Mod(0, -1),
                    new Mod(1, -1)
                };

                var sum = 0;
                foreach (var neighborMod in neighborMods)
                {
                    var neighbor = location.Modify(neighborMod);
                    if (Values.TryGetValue((neighbor.X, neighbor.Y), out var neighborValue))
                        sum += neighborValue;
                }
                return sum;
            }
        }
    }

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
