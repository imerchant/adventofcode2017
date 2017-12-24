using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Inputs
{
    public static partial class Input
    {
        public static IList<int> Day06Parse(string input) => input.SplitOn('\t', ' ').Select(int.Parse).ToList();

        public const string Day06 = @"2	8	8	5	4	2	3	1	5	5	1	2	15	13	5	14";
    }
}
