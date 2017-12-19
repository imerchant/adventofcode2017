using System.Collections.Generic;
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

        [Fact]
        public void Puzzle2_CountDiscreteGroups()
        {
            var grid = new Grid(Input.Day14);

            grid.CountGroups().Should().Be(1180);
        }

        private const string PuzzleExample = "flqrgnkx";

        [Fact]
        public void Puzzle1_ExamplePasses()
        {
            var grid = new Grid(PuzzleExample);

            grid.CountLitCells().Should().Be(8108);
        }

        [Fact]
        public void Puzzle2_ExamplePasses()
        {
            var grid = new Grid(PuzzleExample);

            grid.CountGroups().Should().Be(1242);
        }

        private static readonly List<string> FourGroupSample = new List<string>
        {
            "010",
            "101",
            "010"
        };

        [Fact]
        public void Up_FindsCorrectLocationAndValue()
        {
            var grid = new Grid(FourGroupSample);

            var centerUp = grid.Up((1, 1));
            centerUp.value.Should().Be('1');
            centerUp.x.Should().Be(0);
            centerUp.y.Should().Be(1);

            var tlUp = grid.Up((0,0));
            tlUp.value.Should().BeNull();
            tlUp.x.Should().Be(-1);
            tlUp.y.Should().Be(-1);

            var blUp = grid.Up((2,0));
            blUp.value.Should().Be('1');
            blUp.x.Should().Be(1);
            blUp.y.Should().Be(0);
        }

        private static readonly List<string> OneGroupSample = new List<string>
        {
            "010",
            "111",
            "010"
        };

        [Fact]
        public void FindGroup_FindsSingleGroup()
        {
            var grid = new Grid(OneGroupSample);

            var group = grid.FindGroup((0, 1));

            group.Should().HaveCount(5);
        }

        private static IEnumerable<object[]> GroupSamples()
        {
            yield return new object[] { FourGroupSample, 4 };
            yield return new object[] { OneGroupSample, 1 };
        }

        [Theory]
        [MemberData(nameof(GroupSamples))]
        public void CountGroups_CountsGroupsCorrectly(List<string> rows, int expectedGroups)
        {
            var grid = new Grid(rows);

            grid.CountGroups().Should().Be(expectedGroups);
        }
    }
}
