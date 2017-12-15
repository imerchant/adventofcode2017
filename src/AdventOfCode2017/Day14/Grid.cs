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

        public List<string> Rows { get; }

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
            return Rows.Sum(row => row.Count(c => c == '1'));
        }

        public int CountGroups()
        {
            var groups = 0;
            var groupedCells = new HashSet<(int x, int y)>();

            for (var x = 0; x < Rows.Count; ++x)
            {
                for (var y = 0; y < Rows[x].Length; ++y)
                {
                    
                }
            }
        }

        private string GenerateHash(string key)
        {
            var hashGenerator = new NumberHashGenerator();
            return hashGenerator.CalculateKnotHash(Input.Day10ParseAsAscii(key));
        }
    }
}