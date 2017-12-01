namespace AdventOfCode2017.Day01
{
    public static class DuplicateDigitCalculator
    {
        public static int SumDuplicateNeighborDigits(string input)
        {
            var sum = input[0] == input[input.Length - 1]
                ? input[0] - '0'
                : 0;

            for(var k = 0; k < input.Length - 1; ++k)
            {
                if(input[k] == input[k+1])
                    sum += input[k] - '0';
            }

            return sum;
        }

        public static int SumDuplicateTravelDigits(string input)
        {
            var sum = 0;

            for(var k = 0; k < input.Length; ++k)
            {
                if(input[k] == input[IndexHalfwayAround(k, input.Length)])
                    sum += input[k] - '0';
            }

            return sum;
        }

        private static int IndexHalfwayAround(int index, int length)
        {
            var half = length / 2;
            return (index + half) % length;
        }
    }
}