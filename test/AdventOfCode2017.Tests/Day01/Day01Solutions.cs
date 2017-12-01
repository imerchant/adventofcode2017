using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using AdventOfCode2017.Day01;
using AdventOfCode2017.Inputs;

public class Day01Solutions
{
    public ITestOutputHelper Output { get; }

    public Day01Solutions(ITestOutputHelper output)
    {
        Output = output;
    }

    [Fact]
    public void Puzzle1_CalculatesDuplicateDigitSum()
    {
        var input = Input.Day01;

        var sum = DuplicateDigitCalculator.SumDuplicateDigits(input);

        sum.Should().Be(1203);
    }

    [Theory]
    [InlineData("1122", 3)]
    [InlineData("1111", 4)]
    [InlineData("1234", 0)]
    [InlineData("91212129", 9)]
    public void Puzzle1_ExamplesPass(string input, int expectedSum)
    {
        DuplicateDigitCalculator.SumDuplicateDigits(input).Should().Be(expectedSum);
    }
}