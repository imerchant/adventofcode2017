using System.Collections.Generic;
using System.Linq;
using AdventOfCode2017.Day10;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day10Solutions : TestBase
    {
        public Day10Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_FindChecksumFromInputSetOfLengths()
        {
            var lengths = Input.Day10Parse(Input.Day10);
            var generator = new NumberHashGenerator();

            generator.TwistSet(lengths);

            generator.Checksum.Should().Be(5577);
        }

        [Fact]
        public void Puzzle2_InterpretingInputAsAscii_AndAddingAddendum_WorksCorrectly()
        {
            var expectedLengths = new List<int> { 49, 44, 50, 44, 51, 17, 31, 73, 47, 23 };
            var lengths = Input.Day10ParseAsAscii("1,2,3");

            lengths.Should().Equal(expectedLengths);
        }

        [Fact]
        public void Puzzle1_ExamplesPass()
        {
            var numbers = new List<int> { 0, 1, 2, 3, 4 };
            var lengths = new List<int> { 3, 4, 1, 5 };
            var expectedList = new List<int> { 3, 4, 2, 1, 0 };
            var generator = new NumberHashGenerator(numbers);

            generator.TwistSet(lengths);

            generator.Numbers.Should().Equal(expectedList);
            generator.CurrentIndex.Should().Be(4);
            generator.SkipSize.Should().Be(4);
            generator.Checksum.Should().Be(12);
        }

        [Fact]
        public void NumberHashGenerator_TwistSet_OperatesCorrectly()
        {
            var numbers = new List<int> { 0, 1, 2, 3, 4 };
            var generator = new NumberHashGenerator(numbers);

            var lengths = new List<int> { 1, 2 };

            generator.TwistSet(lengths);

            generator.Numbers.Should().Equal(new List<int> { 0, 2, 1, 3, 4 });
            generator.CurrentIndex.Should().Be(4);
            generator.SkipSize.Should().Be(2);
        }

        [Theory]
        [MemberData(nameof(TwistOperations))]
        public void NumberHashGenerator_Twist_OperatesCorrectly(int index, int length, List<int> expectedWholeList)
        {
            var numbers = new List<int> { 0, 1, 2, 3, 4 };
            var generator = new NumberHashGenerator(numbers);

            generator.Twist(index, length);

            generator.Numbers.Should().Equal(expectedWholeList);
        }

        [Theory]
        [MemberData(nameof(GetSublistOperations))]
        public void NumberHashGenerator_GetSublist_OperatesCorrectly(int index, int length, List<int> expectedSublist)
        {
            var numbers = new List<int> { 0, 1, 2, 3, 4 };
            var generator = new NumberHashGenerator(numbers);

            var sublist = generator.GetSublist(index, length).ToList();

            sublist.Should().Equal(expectedSublist);
        }

        [Theory]
        [MemberData(nameof(ReplaceSublistOperations))]
        public void NumberHashGenerator_ReplaceSublist_OperatesCorrectly(int index, List<int> replacements, List<int> expectedWholeList)
        {
            var numbers = new List<int> { 0, 1, 2, 3, 4 };
            var generator = new NumberHashGenerator(numbers);

            generator.ReplaceSublist(index, replacements);

            generator.Numbers.Should().Equal(expectedWholeList);
        }

        private static IEnumerable<object[]> GetSublistOperations()
        {
            yield return new object[] { 0, 2, new List<int> { 0, 1 } };
            yield return new object[] { 3, 4, new List<int> { 3, 4, 0, 1 } };
            yield return new object[] { 4, 5, new List<int> { 4, 0, 1, 2, 3 } };
            yield return new object[] { 1, 0, new List<int>() };
            yield return new object[] { 1, 1, new List<int> { 1 } };
        }

        private static IEnumerable<object[]> ReplaceSublistOperations()
        {
            yield return new object[] { 0, new List<int> { 1, 0 }, new List<int> { 1, 0, 2, 3, 4 } };
            yield return new object[] { 4, new List<int> { 0, 4 }, new List<int> { 4, 1, 2, 3, 0 } };
            yield return new object[] { 1, new List<int> { 0, 4, 3, 2, 1 }, new List<int> { 1, 0, 4, 3, 2 } };
            yield return new object[] { 1, new List<int>(), new List<int> { 0, 1, 2, 3, 4 } };
            yield return new object[] { 2, new List<int>{ 2 }, new List<int> { 0, 1, 2, 3, 4 } };
        }

        private static IEnumerable<object[]> TwistOperations()
        {
            yield return new object[] { 0, 2, new List<int> { 1, 0, 2, 3, 4 } };
            yield return new object[] { 4, 2, new List<int> { 4, 1, 2, 3, 0 } };
            yield return new object[] { 1, 5, new List<int> { 1, 0, 4, 3, 2 } };
            yield return new object[] { 1, 0, new List<int> { 0, 1, 2, 3, 4 } };
            yield return new object[] { 2, 1, new List<int> { 0, 1, 2, 3, 4 } };
        }
    }
}