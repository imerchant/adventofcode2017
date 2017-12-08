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

            var (stepsToCycle, _) = banks.LocateFirstDuplicateOrganization();
            stepsToCycle.Should().Be(3156);
        }

        [Fact]
        public void Puzzle2_FindStepsInCycle()
        {
            var starting = Input.Day06Parse(Input.Day06);
            var banks = new MemoryBanks(starting);

            var (_, stepsInCycle) = banks.LocateFirstDuplicateOrganization();
            stepsInCycle.Should().Be(1610);
        }

        [Theory]
        [InlineData("0 2 7 0", 5, 4)]
        public void Puzzle1And2_ExamplePasses(string startingArrangement, int expectedStepsToCycle, int expectedStepsInCycle)
        {
            var starting = Input.Day06Parse(startingArrangement);
            var banks = new MemoryBanks(starting);

            var (stepsToCycle, stepsInCycle) = banks.LocateFirstDuplicateOrganization();
            stepsToCycle.Should().Be(expectedStepsToCycle);
            stepsInCycle.Should().Be(expectedStepsInCycle);
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
