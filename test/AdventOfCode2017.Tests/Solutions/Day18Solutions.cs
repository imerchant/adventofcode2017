using System;
using AdventOfCode2017.Day18;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day18Solutions : TestBase
    {
        public Day18Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_FindFirstRecoveredValue()
        {
            var soundChip = new SoundChip();
            var instructions = Input.Day18Parse(Input.Day18);

            Action run = () => soundChip.FollowInstructions(instructions);

            run.ShouldThrow<Exception>().WithMessage("recovered: 8600");
        }

        [Fact]
        public void Puzzle2_CountSendsBySecondProgram()
        {
            var duet = new Duet(Input.Day18Parse(Input.Day18));

            duet.CountMessagesSentBySecond().Should().Be(7239);
        }

        private const string Puzzle2Example =
@"snd 1
snd 2
snd p
rcv a
rcv b
rcv c
rcv d";

        [Fact]
        public void Puzzle2_ExamplePasses()
        {
            var duet = new Duet(Input.Day18Parse(Puzzle2Example));

            duet.CountMessagesSentBySecond().Should().Be(3);
        }

        private const string Puzzle1Example =
@"set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2";

        [Fact]
        public void Puzzle1_ExamplePasses()
        {
            var soundChip = new SoundChip();
            var instructions = Input.Day18Parse(Puzzle1Example);

            Action run = () => soundChip.FollowInstructions(instructions);

            run.ShouldThrow<Exception>().WithMessage("recovered: 4");
        }
    }
}
