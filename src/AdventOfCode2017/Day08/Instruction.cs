using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
        }

        private Register GetRegister(string id)
        {
            if (Registers.TryGetValue(id, out var register))
                return register;

            register = new Register(id);
            Registers[id] = register;
            return register;
        }
    }

    public class Register
    {
        public string Id { get; }
        public int Value { get; set; }

        public Register(string id)
        {
            Id = id;
            Value = 0;
        }
    }

    public class Instruction
    {
        public static readonly Regex InstructionRegex = new Regex(@"(?'target'[a-z]+?) (?'targetOp'inc|dec) (?'targetDiff'-*\d+?) if (?'test'[a-z]+?) (?'testOp'<|>|<=|>=|==|!=) (?'testDiff'-*\d+)", RegexOptions.Compiled);

        public string TargetRegister { get; }
        public IOperation TargetOperation { get; }
        public string TestRegister { get; }
        public IComparison TestComparison { get; }

        public Instruction(string targetRegister, IOperation targetOperation, string testRegister, IComparison testComparison)
        {
            TargetRegister = targetRegister;
            TargetOperation = targetOperation;
            TestRegister = testRegister;
            TestComparison = testComparison;
        }

        public static Instruction Parse(string input)
        {
            var match = InstructionRegex.Match(input);
            if (!match.Success)
                throw new Exception($"could not parse \"{input}\"");

            var targetRegister = match.Groups["target"].Value;
            var targetOp = match.Groups["targetOp"].Value;
            var targetDifferential = int.Parse(match.Groups["targetDiff"].Value);
            var testRegister = match.Groups["test"].Value;
            var testOp = match.Groups["testOp"].Value;
            var testDifferential = int.Parse(match.Groups["testDiff"].Value);

            return new Instruction(targetRegister, new Operation(targetDifferential, targetOp), testRegister, new Comparison(testDifferential, testOp));
        }
    }

    public interface IOperation
    {
        int Value { get; }
        int OperateOn(int otherValue);
    }

    public class Operation : IOperation
    {
        public static readonly IDictionary<string, Func<int, int, int>> Operations = new Dictionary<string, Func<int, int, int>>(StringComparer.OrdinalIgnoreCase)
        {
            { "inc", (one, two) => one + two },
            { "dec", (one, two) => one - two }
        };

        public int Value { get; }
        public Func<int, int, int> Op { get; }

        public Operation(int value, string operationKey)
        {
            Value = value;
            Op = Operations[operationKey];
        }

        public int OperateOn(int otherValue)
        {
            return Op(otherValue, Value);
        }
    }

    public interface IComparison
    {
        int Value { get; }
        bool CompareTo(int otherValue);
    }

    public class Comparison : IComparison
    {
        public static readonly IDictionary<string, Func<int, int, bool>> Comparisons = new Dictionary<string, Func<int, int, bool>>
        {
            { "<", (one, two) => one < two },
            { ">", (one, two) => one > two },
            { "<=", (one, two) => one <= two },
            { ">=", (one, two) => one >= two },
            { "==", (one, two) => one == two },
            { "!=", (one, two) => one != two }
        };

        public int Value { get; }
        public Func<int, int, bool> ComparisonOp { get; }

        public Comparison(int value, string comparisonKey)
        {
            Value = value;
            ComparisonOp = Comparisons[comparisonKey];
        }

        public bool CompareTo(int otherValue)
        {
            return ComparisonOp(otherValue, Value);
        }
    }
}

