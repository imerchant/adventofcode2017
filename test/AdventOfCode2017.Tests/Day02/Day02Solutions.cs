using System.Linq;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Day02
{
    public class Day02Solutions : TestBase
    {
        public Day02Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_GenerateChecksumFromLowHighDifferencePerRow()
        {
            var rows = Input.Day02Parse(Input.Day02);

            rows.Sum(x => x.Difference).Should().Be(47136);
        }

        [Fact]
        public void Puzzle2_GenerateSumOfDivisibleResults()
        {
            var rows = Input.Day02Parse(Input.Day02);

            rows.Sum(x => x.DivisibleResult).Should().Be(250);
        }

        const string Puzzle1Example =
@"5 1 9 5
7 5 3
2 4 6 8";

        [Fact]
        public void Puzzle1_ExamplePasses()
        {
             var rows = Input.Day02Parse(Puzzle1Example);

             rows.Sum(x => x.Difference).Should().Be(18);
        }

        const string Puzzle2Example =
@"5 9 2 8
9 4 7 3
3 8 6 5";

        [Fact]
        public void Puzzle2_ExamplePasses()
        {
            var rows = Input.Day02Parse(Puzzle2Example);

            var rowResults = rows.Select(x => x.DivisibleResult);

            rowResults.Sum().Should().Be(9);
        }
    }
}