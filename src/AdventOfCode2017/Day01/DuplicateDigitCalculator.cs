namespace AdventOfCode2017.Day01
{
    public static class DuplicateDigitCalculator
    {
        public static int SumDuplicateDigits(string input)
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
    }
}