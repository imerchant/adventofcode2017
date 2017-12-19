using System.Collections.Generic;
using System.Linq;
using AdventOfCode2017.Day16;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day16Solutions : TestBase
    {
        public Day16Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_FindOrderAfterDance()
        {
            var operations = Input.Day16Parse(Input.Day16).Select(Operation.Parse).ToList();
            var dancers = new Dancers();

            foreach (var operation in operations)
            {
                operation.Op(dancers);
            }

            dancers.Line.Should().Be("padheomkgjfnblic");
        }

        [Fact]
        public void Puzzle2_DanceABillionTimes()
        {
            const int oneBillion = 1_000_000_000;
            var operations = Input.Day16Parse(Input.Day16).Select(Operation.Parse).ToList();
            var dancers = new Dancers();
            var seenLines = new HashSet<string>();

            for (var k = 0; k < oneBillion; ++k)
            {
                dancers.Dance(operations);
                if (!seenLines.Add(dancers.Line))
                    break;
            }

            var listOfSeen = new List<string>(seenLines);

            var count = seenLines.Count;

            var rounds = oneBillion % count;

            listOfSeen[rounds - 1].Should().Be("bfcdeakhijmlgopn"); // -1 because of indexing at 0?
        }

        private const string Puzzle1ExampleDancers = "abcde";
        private const string Puzzle1ExampleInput = "s1,x3/4,pe/b";

        [Fact]
        public void Puzzle1_ExamplePasses()
        {
            var operations = Input.Day16Parse(Puzzle1ExampleInput).Select(Operation.Parse).ToList();
            var dancers = new Dancers(Puzzle1ExampleDancers);

            foreach (var operation in operations)
            {
                operation.Op(dancers);
            }

            dancers.Line.Should().Be("baedc");
        }

        [Theory]
        [InlineData(1, "eabcd")]
        [InlineData(3, "cdeab")]
        public void Spin_WorksAsExpected(int spinCount, string expectedResult)
        {
            var dancers = new Dancers(Puzzle1ExampleDancers);

            dancers.Spin(spinCount);

            dancers.Line.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(3, 4, "abced")]
        [InlineData(0, 1, "bacde")]
        public void Exchange_WorksAsExpected(int position1, int position2, string expectedResult)
        {
            var dancers = new Dancers(Puzzle1ExampleDancers);

            dancers.Exchange(position1, position2);

            dancers.Line.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData('b', 'c', "acbde")]
        [InlineData('a', 'e', "ebcda")]
        public void Partner_WorksAsExpected(char one, char two, string expectedResult)
        {
            var dancers = new Dancers(Puzzle1ExampleDancers);

            dancers.Partner(one, two);

            dancers.Line.Should().Be(expectedResult);
        }

        [Fact]
        public void Dancers_AreArrangedCorrectlyByDefault()
        {
            var dancers = new Dancers();

            dancers.Line.Should().Be("abcdefghijklmnop");
        }
    }
}