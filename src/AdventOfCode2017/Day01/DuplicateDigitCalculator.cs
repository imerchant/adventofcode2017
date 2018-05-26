namespace AdventOfCode2017.Day01
{
    public class DuplicateDigitCalculator
    {
        public int SumDuplicateNeighborDigits(string input)
        {
            var sum = input[0] == input[input.Length - 1]
                ? input[0] - '0'
                : 0;

            for (var k = 0; k < input.Length - 1; ++k)
            {
                if (input[k] == input[k+1])
                    sum += input[k] - '0';
            }

            return sum;
        }

        public int SumDuplicateTravelDigits(string input)
        {
            var sum = 0;

            for (var k = 0; k < input.Length; ++k)
            {
                if (input[k] == CharHalfwayAround(input, k))
                    sum += input[k] - '0';
            }

            return sum;

            char CharHalfwayAround(string source, int index)
            {
                var half = source.Length / 2;
                return source[(index + half) % source.Length];
            }
        }
    }
}