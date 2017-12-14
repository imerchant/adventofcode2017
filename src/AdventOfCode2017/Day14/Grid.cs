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
            Rows = new List<string>();
            foreach (var k in Enumerable.Range(0, 128))
            {
                var rowKey = $"{keyString}-{k}";
                var binary = GenerateHash(rowKey)
                    .Select(x => _hexBinaryMap[x])
                    .JoinStrings();
                Rows.Add(binary);
            }
        }

        public int CountLitCells()
        {
            return Rows.Sum(row => row.Count(c => c == '1'));
        }

        private string GenerateHash(string key)
        {
            var hashGenerator = new NumberHashGenerator();
            return hashGenerator.CalculateKnotHash(Input.Day10ParseAsAscii(key));
        }
    }
}