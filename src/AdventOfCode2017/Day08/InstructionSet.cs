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
            Registers = new Dictionary<string, Register>();
            HighestValueEncountered = 0;
        }

        public void RunInstruction(Instruction instruction)
        {
            var testRegister = GetRegister(instruction.TestRegister);
            var targetRegister = GetRegister(instruction.TargetRegister);

            if (instruction.TestComparison.CompareTo(testRegister.Value))
            {
                targetRegister.Value = instruction.TargetOperation.OperateOn(targetRegister.Value);
                if (targetRegister.Value > HighestValueEncountered)
                    HighestValueEncountered = targetRegister.Value;
            }
            
            Register GetRegister(string id)
            {
                if (Registers.TryGetValue(id, out var register))
                    return register;

                register = new Register(id);
                Registers[id] = register;
                return register;
            }
        }
    }
}

