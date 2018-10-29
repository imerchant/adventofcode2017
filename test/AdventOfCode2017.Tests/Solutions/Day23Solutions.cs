using AdventOfCode2017.Day23;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day23Solutions : TestBase
    {
        public Day23Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_CountMulCommands()
        {
            var program = new Program(Input.Day23Parse(Input.Day23));

            program.Run();

            program.MulInvoked.Should().Be(3969);
        }
    }
}