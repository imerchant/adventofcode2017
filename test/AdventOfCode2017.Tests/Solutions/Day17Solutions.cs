using System.Collections.Generic;
using AdventOfCode2017.Day17;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day17Solutions : TestBase
    {
        public Day17Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_FindValueAfter2017Insertions()
        {
            var spinlock = new Spinlock(Input.Day17);

            spinlock.ValueAfterInsertions(2017).Should().Be(180);
        }

        [Fact]
        public void Puzzle1_FindValue_ExamplePasses()
        {
            var spinlock = new Spinlock(3);

            spinlock.ValueAfterInsertions(2017).Should().Be(638);
        }

        private static readonly IList<string> ExpectedSpinlockOutput = new List<string>
        {
            "(0)",
            "0 (1)",
            "0 (2) 1",
            "0 2 (3) 1",
            "0 2 (4) 3 1",
            "0 (5) 2 4 3 1",
            "0 5 2 4 3 (6) 1",
            "0 5 (7) 2 4 3 6 1",
            "0 5 7 2 4 3 (8) 6 1",
            "0 (9) 5 7 2 4 3 8 6 1"
        };

        [Fact]
        public void Puzzle1_ExamplesPass()
        {
            var spinlock = new Spinlock(3);

            foreach (var expected in ExpectedSpinlockOutput)
            {
                spinlock.ToString().Should().Be(expected);
                spinlock.Step();
            }
        }

        [Fact]
        public void Spinlock_Step_WorksCorrectly()
        {
            var spinlock = new Spinlock(3);

            spinlock.Step();

            spinlock.ToString().Should().Be("0 (1)");
        }
    }
}
