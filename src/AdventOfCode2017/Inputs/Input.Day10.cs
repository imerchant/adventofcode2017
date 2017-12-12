using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Inputs
{
    public static partial class Input
    {
        public static IList<int> Day10Parse(string input) =>
            input.SplitOn(',')
                .Select(int.Parse)
                .ToList();

        public static IList<int> Day10ParseAsAscii(string input) =>
            input.Trim()
                .Select(Convert.ToInt32)
                .Concat(Day10Addendum)
                .ToList();

        public const string Day10 = @"102,255,99,252,200,24,219,57,103,2,226,254,1,0,69,216";
        public static readonly IList<int> Day10Addendum = new List<int> { 17, 31, 73, 47, 23 };
    }
}