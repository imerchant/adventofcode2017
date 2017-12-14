using AdventOfCode2017.Day14;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day14Solutions : TestBase
    {
        public Day14Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_CountLitCells()
        {
            var grid = new Grid(Input.Day14);

            grid.CountLitCells().Should().Be(8148);
        }

        private const string PuzzleExample = "flqrgnkx";

        [Fact]
        public void Puzzle1_ExamplePasses()
        {
            var grid = new Grid(PuzzleExample);

            grid.CountLitCells().Should().Be(8108);
        }
    }
}