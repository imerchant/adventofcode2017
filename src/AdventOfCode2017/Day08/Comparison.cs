using System;
using System.Collections.Generic;

namespace AdventOfCode2017.Day08
{
    public class Comparison : IComparison
    {
        public static readonly IDictionary<string, Func<int, int, bool>> Comparisons = new Dictionary<string, Func<int, int, bool>>
        {
            { "<", (one, two) => one < two },
            { ">", (one, two) => one > two },
            { "<=", (one, two) => one <= two },
            { ">=", (one, two) => one >= two },
            { "==", (one, two) => one == two },
            { "!=", (one, two) => one != two }
        };

        public int Value { get; }
        public Func<int, int, bool> ComparisonOp { get; }

        public Comparison(int value, string comparisonKey)
        {
            Value = value;
            ComparisonOp = Comparisons[comparisonKey];
        }

        public bool CompareTo(int otherValue)
        {
            return ComparisonOp(otherValue, Value);
        }
    }
}

