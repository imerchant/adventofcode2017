using AdventOfCode2017.Day01;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day01Solutions : TestBase
    {
        private readonly DuplicateDigitCalculator _calc;

        public Day01Solutions(ITestOutputHelper output) : base(output)
        {
            _calc = new DuplicateDigitCalculator();
        }

        [Fact]
        public void Puzzle1_CalculatesDuplicateDigitSum()
        {
            var input = Input.Day01;

            var sum = _calc.SumDuplicateNeighborDigits(input);

            sum.Should().Be(1203);
        }

        [Fact]
        public void Puzzle2_CalculatesDigitSumForHalfwayAround()
        {
            var input = Input.Day01;

            var sum = _calc.SumDuplicateTravelDigits(input);

            sum.Should().Be(1146);
        }

        [Theory]
        [InlineData("1122", 3)]
        [InlineData("1111", 4)]
        [InlineData("1234", 0)]
        [InlineData("91212129", 9)]
        public void Puzzle1_ExamplesPass(string input, int expectedSum)
        {
            _calc.SumDuplicateNeighborDigits(input).Should().Be(expectedSum);
        }

        [Theory]
        [InlineData("1212", 6)]
        [InlineData("1221", 0)]
        [InlineData("123425", 4)]
        [InlineData("123123", 12)]
        [InlineData("12131415", 4)]
        public void Puzzle2_ExamplesPass(string input, int expectedSum)
        {
            _calc.SumDuplicateTravelDigits(input).Should().Be(expectedSum);
        }
    }
}