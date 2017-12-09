using AdventOfCode2017.Day09;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day09Solutions : TestBase
    {
        public Day09Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_SumOfGroupValues()
        {
            var groupCounter = new GroupCounter(Input.Day09);

            var (_, score) = groupCounter.CountGroupsAndScore();

            score.Should().Be(-1);
        }

        [Fact]
        public void PuzzleInput_EscapedQuotesAreInterpretedCorrectly()
        {
            const string input = Input.Day09;

            input[21].Should().Be('"');
            input[22].Should().Be('i');
            input[23].Should().Be('!');
        }

        [Theory]
        [InlineData("{}", 1, 1)]
        [InlineData("{{{}}}", 3, 6)]
        [InlineData("{{},{}}", 3, 5)]
        [InlineData("{{{},{},{{}}}}", 6, 16)]
        [InlineData("{<{},{},{{}}>}", 1, 1)]
        [InlineData("{<a>,<a>,<a>,<a>}", 1, 1)]
        [InlineData("{{<a>},{<a>},{<a>},{<a>}}", 5, 9)]
        [InlineData("{{<!!>},{<!!>},{<!!>},{<!!>}}", 5, 9)]
        [InlineData("{{<!>},{<!>},{<!>},{<a>}}", 2, 3)]
        public void Puzzle1Example_GroupsAreCountedCorrectly(string input, int expectedGroupCount, int expectedScoreSum)
        {
            var groupCounter = new GroupCounter(input);

            var (groups, score) = groupCounter.CountGroupsAndScore();

            groups.Should().Be(expectedGroupCount);
            score.Should().Be(expectedScoreSum);
        }
    }
}
