using AdventOfCode2017.Day12;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day12Solutions : TestBase
    {
        public Day12Solutions(ITestOutputHelper output) : base(output)
        {
        }

        const string Puzzle1Example =
@"0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5";

        [Fact]
        public void Puzzle1_Example_ParsesPrograms()
        {
            var input = Input.Day12Parse(Puzzle1Example);
            var programSet = new ProgramSet(input);

            programSet.Programs.Should().HaveCount(input.Count);
            programSet.Programs[0].Peers.Should().ContainSingle(x => x == 2);
            programSet.Programs[4].Peers.Should().BeEquivalentTo(2, 3, 6);
        }
    }
}