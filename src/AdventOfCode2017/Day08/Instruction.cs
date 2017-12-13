using System;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Day08
{
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
}
