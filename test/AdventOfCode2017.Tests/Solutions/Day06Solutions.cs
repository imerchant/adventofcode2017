using AdventOfCode2017.Day06;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day06Solutions : TestBase
    {
        public Day06Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_FindStepsToIdentifyCycle()
        {
            var starting = Input.Day06Parse(Input.Day06);
            var banks = new MemoryBanks(starting);

            banks.LocateFirstDuplicateOrganization().Should().Be(3156);
        }

        [Theory]
        [InlineData("0 2 7 0", 5)]
        public void Puzzle1_ExamplePasses(string startingArrangement, int expectedSteps)
        {
            var starting = Input.Day06Parse(startingArrangement);
            var banks = new MemoryBanks(starting);

            banks.LocateFirstDuplicateOrganization().Should().Be(expectedSteps);
        }

        [Theory]
        [InlineData("0 2 7 0", "2 4 1 2")]
        public void MemoryBanksRedistributeCorrectly(string startingArrangement, string expectedArrangement)
        {
            var starting = Input.Day06Parse(startingArrangement);
            var banks = new MemoryBanks(starting);

            banks.Reorganize();

            banks.ToString().Should().Be(expectedArrangement);
        }
    }
}
