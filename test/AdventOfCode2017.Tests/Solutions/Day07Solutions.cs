using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2017.Day07;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using MoreLinq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day07Solutions : TestBase
    {
        public Day07Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_LocateBottomOfTower()
        {
            var input = Input.Day07Parse(Input.Day07);
            var tower = new ProgramTower(input);

            tower.Head.Id.Should().Be("fbgguv");

            PrintObj(tower.Programs["fbgguv"]);
        }

        private const string Puzzle1Example =
@"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)";

        [Fact]
        public void Puzzle1_ExamplePasses()
        {
            var input = Input.Day07Parse(Puzzle1Example);
            var tower = new ProgramTower(input);

            tower.FindLeastWeightedProgram().Id.Should().Be("tknk");
        }
    }
}
