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
        public void Puzzle1And2_SumOfGroupValues_AndCountOfGarbageCharacters()
        {
            var groupCounter = new GroupCounter(Input.Day09);

            var (_, score, garbageChars) = groupCounter.CountGroupsAndScore();

            score.Should().Be(16869);
            garbageChars.Should().Be(7284);
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

            var (groups, score, _) = groupCounter.CountGroupsAndScore();

            groups.Should().Be(expectedGroupCount);
            score.Should().Be(expectedScoreSum);
        }

        [Theory]
        [InlineData("<>", 0)]
        [InlineData("<random characters>", 17)]
        [InlineData("<<<<>", 3)]
        [InlineData("<{!>}>", 2)]
        [InlineData("<!!>", 0)]
        [InlineData("<!!!>>", 0)]
        [InlineData(@"<{o""i!a,<{i<a>", 10)]
        public void Puzzle2Examples_CountGarbageCharsCorrectly(string input, int expectedGarbageChars)
        {
            var groupCounter = new GroupCounter(input);

            var (_, _, garbageChars) = groupCounter.CountGroupsAndScore();

            garbageChars.Should().Be(expectedGarbageChars);
        }
    }
}
