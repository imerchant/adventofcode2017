using System.Linq;
using AdventOfCode2017.Day07;
using AdventOfCode2017.Inputs;
using FluentAssertions;
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

        [Fact]
        public void Puzzle2_FindOutOfBalance()
        {
            var input = Input.Day07Parse(Input.Day07);
            var tower = new ProgramTower(input);

            var oob = tower.Head.ChildOutOfBalance;

            while(oob.ChildOutOfBalance?.ChildOutOfBalance != null)
            {
                oob = oob.ChildOutOfBalance;
            }

            Output.WriteLine(oob.SubTowerWeight());

            /*
             * Relevant output:
    + jdxfsa:2553:(1869 // should be **1865** <- answer
        + lugwl:(228)
        + ckcjr:228:(50
            + owgsk:(89)
            + vdnphf:(89))
        + umutl:228:(36
            + grtrt:(96)
            + tjaqrw:(96)))
    + liwlcz:2548:(728
        + fhvyhk:364:(252
            + boqgjn:(56)
            + rzzezfx:(56))
        + qqnywdg:364:(160
            + lrjnzf:(68)
            + luhxcq:(68)
            + whgpim:(68))
        + ajxsrs:364:(296
            + vwslb:(34)
            + wmukud:(34))
        + utojr:364:(82
            + nuytv:(94)
            + frjtwx:(94)
            + guurpo:(94))
        + tvinpl:364:(220
            + iglmzsi:(72)
            + zujrs:(72)))
             */
        }

        private const string PuzzleExample =
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
            var input = Input.Day07Parse(PuzzleExample);
            var tower = new ProgramTower(input);

            tower.FindLeastWeightedProgram().Id.Should().Be("tknk");
        }

        [Fact]
        public void Puzzle2_FindOutOfBalanceInExample()
        {
            var input = Input.Day07Parse(PuzzleExample);
            var tower = new ProgramTower(input);

            var outOfBalance = tower.Programs.Values.Where(x => x.ChildOutOfBalance != null);

            PrintObj(outOfBalance.Select(x => $"{x.Id}:{x.SubTowerWeight()}"));
        }
    }
}
