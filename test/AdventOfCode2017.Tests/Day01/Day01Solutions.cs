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

        var sum = DuplicateDigitCalculator.SumDuplicateNeighborDigits(input);

        sum.Should().Be(1203);
    }

    [Fact]
    public void Puzzle2_CalculatesDigitSumForHalfwayAround()
    {
        var input = Input.Day01;

        var sum = DuplicateDigitCalculator.SumDuplicateTravelDigits(input);

        sum.Should().Be(1146);
    }

    [Theory]
    [InlineData("1122", 3)]
    [InlineData("1111", 4)]
    [InlineData("1234", 0)]
    [InlineData("91212129", 9)]
    public void Puzzle1_ExamplesPass(string input, int expectedSum)
    {
        DuplicateDigitCalculator.SumDuplicateNeighborDigits(input).Should().Be(expectedSum);
    }

    [Theory]
    [InlineData("1212", 6)]
    [InlineData("1221", 0)]
    [InlineData("123425", 4)]
    [InlineData("123123", 12)]
    [InlineData("12131415", 4)]
    public void Puzzle2_ExamplesPass(string input, int expectedSum)
    {
        DuplicateDigitCalculator.SumDuplicateTravelDigits(input).Should().Be(expectedSum);
    }
}