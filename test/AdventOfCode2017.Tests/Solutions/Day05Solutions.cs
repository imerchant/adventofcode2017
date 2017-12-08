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

        [Fact]
        public void Puzzle1_FindStepsToExitMazeWithComplexValueCalculation()
        {
            var nodes = Input.Day05Parse(Input.Day05);

            var maze = new Maze(nodes);

            maze.CalculateStepsToExitWithComplexValueCalc().Should().Be(27763113);
        }

        private const string PuzzleExample =
@"0
3
0
1
-3";

        [Fact]
        public void Puzzle1_ExamplePasses()
        {
            var maze = new Maze(Input.Day05Parse(PuzzleExample));

            maze.CalculateStepsToExit().Should().Be(5);
        }

        [Fact]
        public void Puzzle2_ExamplePasses()
        {
            var maze = new Maze(Input.Day05Parse(PuzzleExample));

            maze.CalculateStepsToExitWithComplexValueCalc().Should().Be(10);
        }
    }
}
