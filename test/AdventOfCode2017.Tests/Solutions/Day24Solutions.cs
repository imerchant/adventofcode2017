using System.Linq;
using AdventOfCode2017.Day24;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using MoreLinq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day24Solutions : TestBase
    {
        public Day24Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1And2_FindStrongestBridge_AndStrongestOfTheLongest()
        {
            var input = Input.Day24Parse(Input.Day24);
            var builder = new BridgeBuilder(input);

            var bridges = builder.BuildBridges().ToList();

            bridges.Max(x => x.Strength).Should().Be(2006);

            bridges.MaxBy(x => x.Components.Count).Max(x => x.Strength).Should().Be(1994);
        }

        private const string Puzzle1Example =
@"0/2
2/2
2/3
3/4
3/5
0/1
10/1
9/10";

        [Fact]
        public void BridgeBuilder_BuildsBridgesCorrectly()
        {
            var input = Input.Day24Parse(Puzzle1Example);
            var builder = new BridgeBuilder(input);

            var bridges = builder.BuildBridges().ToList();

            foreach (var bridge in bridges)
            {
                Output.WriteLine(bridge.ToString());
            }
            bridges.Should().HaveCount(11);
            bridges.Max(x => x.Strength).Should().Be(31);
        }

        [Fact]
        public void TestingDefaultDictionary()
        {
            var dict = new DefaultDictionary<int, string>(() => string.Empty);

            dict.TryGetValue(0, out var value);

            dict.Should().ContainKey(0);
        }
    }
}