using System.Collections.Generic;
using AdventOfCode2017.Day25;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day25Solutions : TestBase
    {
        public Day25Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_ImranIsTuringComplete()
        {
            var states = new List<State>
            {
                new State('A', (1, 1, 'B'), (0, -1, 'B')),
                new State('B', (1, -1, 'C'), (0, 1, 'E')),
                new State('C', (1, 1, 'E'), (0, -1, 'D')),
                new State('D', (1, -1, 'A'), (1, -1, 'A')),
                new State('E', (0, 1, 'A'), (0, 1, 'F')),
                new State('F', (1, 1, 'E'), (1, 1, 'A'))
            };

            var machine = new Machine('A', states);

            for (var k = 0; k < 12861455; ++k)
            {
                machine.Tick();
            }

            machine.Checksum.Should().Be(3578);
        }

        [Fact]
        public void Puzzle1_ExamplePasses()
        {
            var a = new State('A', (1, 1, 'B'), (0, -1, 'B'));
            var b = new State('B', (1, -1, 'A'), (1, 1, 'A'));

            var machine = new Machine('A', new[] { a, b });

            for (var k = 0; k < 6; ++k)
            {
                machine.Tick();
            }

            machine.Checksum.Should().Be(3);
        }
    }
}