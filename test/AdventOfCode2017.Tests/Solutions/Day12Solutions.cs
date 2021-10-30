using System.Collections.Generic;
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

        [Fact]
        public void Puzzle1_CountProgramsThatCanReach0()
        {
            var input = Input.Day12Parse(Input.Day12);
            var programSet = new ProgramSet(input);

            programSet.ProgramsThatCanReach(0).Should().HaveCount(128);
        }

        [Fact]
        public void Puzzle2_CountGroups()
        {
            var input = Input.Day12Parse(Input.Day12);
            var programSet = new ProgramSet(input);

            programSet.CountGroups().Should().Be(209);
        }

        const string PuzzleExample =
@"0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5";

        [Fact]
        public void Puzzle2_ExamplePasses()
        {
            var input = Input.Day12Parse(PuzzleExample);
            var programSet = new ProgramSet(input);

            programSet.CountGroups().Should().Be(2);
        }

        [Fact]
        public void Puzzle1_ExamplePasses()
        {
            var input = Input.Day12Parse(PuzzleExample);
            var programSet = new ProgramSet(input);

            programSet.ProgramsThatCanReach(0).Should().HaveCount(6);
        }

        [Fact]
        public void Puzzle1_Example_ParsesPrograms()
        {
            var input = Input.Day12Parse(PuzzleExample);
            var programSet = new ProgramSet(input);

            programSet.Programs.Should().HaveCount(input.Count);
            programSet.Programs[0].Peers.Should().ContainSingle(x => x == 2);
            programSet.Programs[4].Peers.Should().BeEquivalentTo(new List<int> { 2, 3, 6});
        }
    }
}