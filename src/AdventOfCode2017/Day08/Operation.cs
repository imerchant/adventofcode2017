using System;
using System.Collections.Generic;

namespace AdventOfCode2017.Day08
{
    public class Operation : IOperation
    {
        public static readonly IDictionary<string, Func<int, int, int>> Operations = new Dictionary<string, Func<int, int, int>>(StringComparer.OrdinalIgnoreCase)
        {
            { "inc", (one, two) => one + two },
            { "dec", (one, two) => one - two }
        };

        public int Value { get; }
        public Func<int, int, int> Op { get; }

        public Operation(int value, string operationKey)
        {
            Value = value;
            Op = Operations[operationKey];
        }

        public int OperateOn(int otherValue)
        {
            return Op(otherValue, Value);
        }
    }
}

