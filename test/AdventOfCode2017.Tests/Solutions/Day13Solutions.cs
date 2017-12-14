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

            trench.WalkMoverToEnd();

            trench.Severity.Should().Be(788);
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

        [Fact]
        public void Puzzle1_ExamplePasses()
        {
            var input = Input.Day13Parse(PuzzleExample);
            var trench = new Trench(input);

            trench.WalkMoverToEnd();

            trench.Severity.Should().Be(24);
        }

        [Fact]
        public void Trench_CreatesLayersNotExplicitlyInInput()
        {
            var input = Input.Day13Parse(PuzzleExample);
            var trench = new Trench(input);

            trench.Layers.Should().HaveCount(7);
            trench.Layers[2].HasScanner.Should().BeFalse();
            trench.Layers[6].Severity.Should().Be(24);
        }

        [Fact]
        public void Layer_Step_NeverMakesScannerMoveOutOfRange()
        {
            var layer = new Layer(0, 5, true);

            for (var k = 0; k < 100; ++k)
            {
                layer.ScannerPosition.Should().BeInRange(0, layer.Depth - 1);
                layer.Step();
                layer.ScannerPosition.Should().BeInRange(0, layer.Depth - 1);
            }
        }
    }
}