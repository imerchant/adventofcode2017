using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Inputs
{
    public static partial class Input
    {
        public static IList<int> Day10Parse(string input) => input.SplitOn(',').Select(int.Parse).ToList();

        public const string Day10 = @"102,255,99,252,200,24,219,57,103,2,226,254,1,0,69,216";
    }
}