using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2017.Day10;
using AdventOfCode2017.Inputs;

namespace AdventOfCode2017.Day14
{
    public class Grid
    {
        private static readonly Dictionary<char, string> _hexBinaryMap = new Dictionary<char, string>
        {
            { '0', "0000" },
            { '1', "0001" },
            { '2', "0010" },
            { '3', "0011" },
            { '4', "0100" },
            { '5', "0101" },
            { '6', "0110" },
            { '7', "0111" },
            { '8', "1000" },
            { '9', "1001" },
            { 'a', "1010" },
            { 'b', "1011" },
            { 'c', "1100" },
            { 'd', "1101" },
            { 'e', "1110" },
            { 'f', "1111" }
        };

        private const char One = '1';

        public List<string> Rows { get; }

        internal Grid(List<string> rows)
        {
            Rows = rows;
        }

        public Grid(string keyString)
        {
            Rows = Enumerable.Range(0, 128)
                .Select(index =>
                    GenerateHash($"{keyString}-{index}")
                        .SelectMany(x => _hexBinaryMap[x])
                        .AsString()
                ).ToList();
        }

        public int CountLitCells()
        {
            return Rows.Sum(row => row.Count(c => c == One));
        }

        public int CountGroups()
        {
            var groups = 0;
            var groupedCells = new HashSet<(int x, int y)>();

            for (var x = 0; x < Rows.Count; ++x)
            {
                for (var y = 0; y < Rows[x].Length; ++y)
                {
                    if(Rows[x][y] != One || groupedCells.Contains((x, y)))
                        continue;

                    var group = FindGroup((x, y));
                    groups++;
                    foreach (var cell in group)
                        if(!groupedCells.Add(cell))
                            throw new Exception("that shouldn't happen");
                }
            }

            return groups;
        }

        internal HashSet<(int x, int y)> FindGroup((int x, int y) point)
        {
            var seen = new HashSet<(int x, int y)>();
            var stack = new Stack<(int x, int y)>();
            stack.Push(point);

            do
            {
                var thisPoint = stack.Pop();

                if (!seen.Add(thisPoint))
                    continue;

                var up = Up(thisPoint);
                if (up.value == One)
                    stack.Push((up.x, up.y));

                var down = Down(thisPoint);
                if (down.value == One)
                    stack.Push((down.x, down.y));

                var right = Right(thisPoint);
                if (right.value == One)
                    stack.Push((right.x, right.y));

                var left = Left(thisPoint);
                if (left.value == One)
                    stack.Push((left.x, left.y));
            }
            while (stack.HasAny());

            return seen;
        }

        internal (int x, int y, char? value) Up((int x, int y) point)
        {
            if (point.x == 0)
                return (-1, -1, null);

            var newX = point.x - 1;
            var newY = point.y;

            return (newX, newY, Rows[newX][newY]);
        }

        internal (int x, int y, char? value) Down((int x, int y) point)
        {
            if (point.x == Rows.Count - 1)
                return (-1, -1, null);

            var newX = point.x + 1;
            var newY = point.y;

            return (newX, newY, Rows[newX][newY]);
        }

        internal (int x, int y, char? value) Right((int x, int y) point)
        {
            if (point.y == Rows[point.x].Length - 1)
                return (-1, -1, null);

            var newX = point.x;
            var newY = point.y + 1;

            return (newX, newY, Rows[newX][newY]);
        }

        internal (int x, int y, char? value) Left((int x, int y) point)
        {
            if (point.y == 0)
                return (-1, -1, null);

            var newX = point.x;
            var newY = point.y - 1;

            return (newX, newY, Rows[newX][newY]);
        }

        private string GenerateHash(string key)
        {
            var hashGenerator = new NumberHashGenerator();
            return hashGenerator.CalculateKnotHash(Input.Day10ParseAsAscii(key));
        }
    }
}
