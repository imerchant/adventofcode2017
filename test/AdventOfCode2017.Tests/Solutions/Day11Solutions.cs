using Xunit.Abstractions;
using Xunit;
using AdventOfCode2017.Inputs;
using AdventOfCode2017.Day11;
using FluentAssertions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day11Solutions : TestBase
    {
        public Day11Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1And2_FindDistanceFromCenter_AndFurthestDistanceSeen()
        {
            var directions = Input.Day11Parse(Input.Day11).ParseEnums<Direction>();
            var hexGrid = new HexGrid();

            hexGrid.Travel(directions);

            hexGrid.DistanceToCenter.Should().Be(808);
            hexGrid.FurthestDistance.Should().Be(1556);
        }

        [Theory]
        [InlineData("ne,ne,ne", 3)]
        [InlineData("ne,ne,sw,sw", 0)]
        [InlineData("ne,ne,s,s", 2)]
        [InlineData("se,sw,se,sw,sw", 3)]
        public void Puzzle1_ExamplesPass(string input, int expectedDistance)
        {
            var directions = Input.Day11Parse(input).ParseEnums<Direction>();
            var hexGrid = new HexGrid();

            hexGrid.Travel(directions);

            hexGrid.DistanceToCenter.Should().Be(expectedDistance);
        }
    }
}
