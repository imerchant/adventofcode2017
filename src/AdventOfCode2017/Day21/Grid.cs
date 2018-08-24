using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day21
{
    public class Grid
    {
        public static List<string> Starting => new List<string>
        {
            ".#.",
            "..#",
            "###"
        };

        public List<string> Content { get; }
        public IDictionary<string, List<string>> Replacements { get; }

        public Grid(IList<string> input)
        {
            Content = Starting;
            Replacements = ParseReplacements(input);
        }

        private static IDictionary<string, List<string>> ParseReplacements(IList<string> input)
        {
            return input
                .Select(line => line.SplitOn("=>").TrimEach().ToList())
                .ToDictionary(
                    split => split[0],
                    split => split[1].SplitOn('/').ToList()
                );
        }
    }
}