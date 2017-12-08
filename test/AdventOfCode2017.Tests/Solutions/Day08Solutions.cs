using System.Linq;
using AdventOfCode2017.Day08;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day08Solutions : TestBase
    {
        public Day08Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1And2_FindHighestValueAtEnd_AndHighestValueEncountered_AfterEntireSetRuns()
        {
            var instructions = Input.Day08Parse(Input.Day08);
            var instructionSet = new InstructionSet(instructions);

            foreach (var instruction in instructionSet.Instructions)
            {
                instructionSet.RunInstruction(instruction);
            }

            var maxValue = instructionSet.Registers.Values.Max(x => x.Value);

            maxValue.Should().Be(4832);
            instructionSet.HighestValueEncountered.Should().Be(5443);
        }

        private const string Puzzle1Example =
@"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";

        [Fact]
        public void Puzzle1And2_ExamplesPass()
        {
            var instructions = Input.Day08Parse(Puzzle1Example);
            var instructionSet = new InstructionSet(instructions);

            foreach (var instruction in instructionSet.Instructions)
            {
                instructionSet.RunInstruction(instruction);
            }

            var maxValue = instructionSet.Registers.Values.Max(x => x.Value);

            maxValue.Should().Be(1);
            instructionSet.HighestValueEncountered.Should().Be(10);
        }

        [Fact]
        public void Puzzle1_InstructionThatDoesNotPassComparison_DoesNotModifyTarget()
        {
            const string instruction = "b inc 5 if a > 1";

            var instructionSet = new InstructionSet(new[] { instruction });

            instructionSet.RunInstruction(instructionSet.Instructions[0]);

            instructionSet.Registers["b"].Value.Should().Be(0);
        }
    }
}