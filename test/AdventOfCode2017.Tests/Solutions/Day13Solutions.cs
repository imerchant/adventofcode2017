using System.Linq;
using AdventOfCode2017.Day13;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day13Solutions : TestBase
    {
        public Day13Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_FindSeverityOfCrossing()
        {
            var input = Input.Day13Parse(Input.Day13);
            var trench = new Trench(input);

            trench.SeverityOfCrossing(0).Should().Be(788);
        }

        [Fact]
        public void Puzzle2_FindFirstSafeCrossing()
        {
            var input = Input.Day13Parse(Input.Day13);
            var trench = new Trench(input);

            trench.FindFirstSafeCrossing().Should().Be(3905748);
        }

        private const string PuzzleExample =
@"0: 3
1: 2
4: 4
6: 4";

        [Fact]
        public void Puzzle2_ExamplePasses()
        {
            var input = Input.Day13Parse(PuzzleExample);
            var trench = new Trench(input);

            trench.FindFirstSafeCrossing().Should().Be(10);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(6, 0)]
        [InlineData(0, 4)]
        public void Puzzle1_Example_LayerIsUnsafeAtDelay(int layer, int delay)
        {
            var input = Input.Day13Parse(PuzzleExample);
            var trench = new Trench(input);
            trench.Layers.First(x => x.Id == layer).IsSafe(delay).Should().BeFalse();
        }

        [Fact]
        public void Puzzle1_ExamplePasses()
        {
            var input = Input.Day13Parse(PuzzleExample);
            var trench = new Trench(input);

            trench.SeverityOfCrossing(0).Should().Be(24);
        }
    }
}