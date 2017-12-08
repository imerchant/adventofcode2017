using AdventOfCode2017.Day05;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day05Solutions : TestBase
    {
        public Day05Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_FindStepsToExitMaze()
        {
            var nodes = Input.Day05Parse(Input.Day05);

            var maze = new Maze(nodes);

            maze.CalculateStepsToExit().Should().Be(388611);
        }

        private const string Puzzle1Example =
@"0
3
0
1
-3";

        [Fact]
        public void Puzzle1_ExamplePasses()
        {
            var maze = new Maze(Input.Day05Parse(Puzzle1Example));

            maze.CalculateStepsToExit().Should().Be(5);
        }
    }
}
