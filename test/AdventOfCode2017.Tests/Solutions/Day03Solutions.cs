using System;
using System.Linq;
using AdventOfCode2017.Day03;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day03Solutions : TestBase
    {
        public Day03Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_ManhattenDistanceToTarget()
        {
            var grid = new Grid();

            var (coord, _) = grid.RunToIndex(Input.Day03);

            var distance = Math.Abs(coord.X) + Math.Abs(coord.Y);

            distance.Should().Be(552);
        }

        [Fact]
        public void Puzzle2_FindFirstValueLargerThanInput()
        {
            var grid = new Grid();

            var (coord, _, values) = grid.RunTo(targetValue: Input.Day03);

            values[(coord.X, coord.Y)].Should().Be(330785);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(12, 3)]
        [InlineData(23, 2)]
        [InlineData(1024, 31)]
        public void Puzzle1_ExamplesPass(int index, int expectedDistance)
        {
            var grid = new Grid();

            var (coord, _) = grid.RunToIndex(index);

            var distance = Math.Abs(coord.X) + Math.Abs(coord.Y);

            distance.Should().Be(expectedDistance);
        }

        [Theory]
        [InlineData(9, 1, -1)]
        [InlineData(10, 2, -1)]
        [InlineData(11, 2, 0)]
        [InlineData(12, 2, 1)]
        [InlineData(13, 2, 2)]
        [InlineData(14, 1, 2)]
        [InlineData(15, 0, 2)]
        public void Grid_CanRunToTarget_AndGetCoordCorrectly(int index, int expectedX, int expectedY)
        {
            var grid = new Grid();

            var (coord, coords, values) = grid.RunTo(targetIndex: index);

            PrintObj(coords.Select(c => $"({c.Index}: {c.X}, {c.Y}; {values.GetValueOrDefault((c.X, c.Y))}"));

            coord.X.Should().Be(expectedX);
            coord.Y.Should().Be(expectedY);
            coord.Index.Should().Be(index);
        }

        [Theory]
        [InlineData(23, 806)]
        public void Puzzle2_ExamplesPassByIndex(int index, int expectedValue)
        {
            var grid = new Grid();

            var (coord, _, values) = grid.RunTo(targetIndex: index);

            coord.Index.Should().Be(index);
            values[(coord.X, coord.Y)].Should().Be(expectedValue);
        }

        [Theory]
        [InlineData(800, 23)]
        [InlineData(700, 22)]
        [InlineData(2, 3)]
        [InlineData(100, 14)]
        public void Puzzle2_ExamplesPass_ByFindingFirstIndexOverValue(int targetValue, int expectedIndex)
        {
            var grid = new Grid();

            var (coord, _, values) = grid.RunTo(targetValue: targetValue);

            coord.Index.Should().Be(expectedIndex);
            values[(coord.X, coord.Y)].Should().BeGreaterOrEqualTo(targetValue);
        }
    }
}
