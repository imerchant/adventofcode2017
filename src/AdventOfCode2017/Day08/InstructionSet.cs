using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Day08
{
    public class InstructionSet
    {
        public IList<Instruction> Instructions { get; }
        public IDictionary<string, Register> Registers { get; }
        public int HighestValueEncountered { get; private set; }

        public InstructionSet(IEnumerable<string> instructions)
        {
            Instructions = instructions.Select(Instruction.Parse).ToList();
            Registers = new DefaultDictionary<string, Register>(id => new Register(id));
            HighestValueEncountered = 0;
        }

        public void RunInstruction(Instruction instruction)
        {
            var testRegister = Registers[instruction.TestRegister];
            var targetRegister = Registers[instruction.TargetRegister];

            if (instruction.TestComparison.CompareTo(testRegister.Value))
            {
                targetRegister.Value = instruction.TargetOperation.OperateOn(targetRegister.Value);
                if (targetRegister.Value > HighestValueEncountered)
                    HighestValueEncountered = targetRegister.Value;
            }
        }
    }
}

