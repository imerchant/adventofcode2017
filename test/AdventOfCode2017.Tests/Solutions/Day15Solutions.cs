using System.Numerics;
using AdventOfCode2017.Day15;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day15Solutions : TestBase
    {
        public Day15Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact(Skip = "This takes 17+ seconds on my laptop.")]
        public void Puzzle1_CountLow16Matches()
        {
            var generatorA = new Generator(Input.Day15.SeedA, Input.Day15.FactorA);
            var generatorB = new Generator(Input.Day15.SeedB, Input.Day15.FactorB);

            var matches = 0;

            for (var k = 0; k < 40_000_000; ++k)
            {
                if (generatorA.Next().Low16() == generatorB.Next().Low16())
                    matches++;
            }

            matches.Should().Be(567);
        }

        [Fact(Skip = "This takes 10+ seconds on my laptop.")]
        public void Puzzle2_CountLow16WithGeneratorsScrutinizingTheirReturns()
        {
            var generatorA = new Generator(Input.Day15.SeedA, Input.Day15.FactorA, Input.Day15.ScrutinyA);
            var generatorB = new Generator(Input.Day15.SeedB, Input.Day15.FactorB, Input.Day15.ScrutinyB);

            var matches = 0;

            for (var k = 0; k < 5_000_000; ++k)
            {
                if (generatorA.NextWithScrutiny().Low16() == generatorB.NextWithScrutiny().Low16())
                    matches++;
            }

            matches.Should().Be(323);
        }

        private static class Puzzle1Example
        {
            public const int SeedA = 65;
            public const int SeedB = 8921;
        }

        [Fact(Skip = "This takes 17+ seconds on my laptop.")]
        public void Puzzle1_ExamplePasses()
        {
            var generatorA = new Generator(Puzzle1Example.SeedA, Input.Day15.FactorA);
            var generatorB = new Generator(Puzzle1Example.SeedB, Input.Day15.FactorB);

            var matches = 0;

            for (var k = 0; k < 40_000_000; ++k)
            {
                if (generatorA.Next().Low16() == generatorB.Next().Low16())
                    matches++;
            }

            matches.Should().Be(588);
        }

        [Fact(Skip = "This takes 10+ seconds on my laptop.")]
        public void Puzzle2_ExamplePasses()
        {
            var generatorA = new Generator(Puzzle1Example.SeedA, Input.Day15.FactorA, Input.Day15.ScrutinyA);
            var generatorB = new Generator(Puzzle1Example.SeedB, Input.Day15.FactorB, Input.Day15.ScrutinyB);

            var matches = 0;

            for (var k = 0; k < 5_000_000; ++k)
            {
                if (generatorA.NextWithScrutiny().Low16() == generatorB.NextWithScrutiny().Low16())
                    matches++;
            }

            matches.Should().Be(309);
        }

        [Fact]
        public void GeneratorA_CalculatesCorrectly()
        {
            var generatorA = new Generator(Puzzle1Example.SeedA, Input.Day15.FactorA);

            generatorA.Next().Should().Be(1092455);
            generatorA.Next().Should().Be(1181022009);
            generatorA.Next().Should().Be(245556042);
            generatorA.Next().Should().Be(1744312007);
            generatorA.Next().Should().Be(1352636452);
        }

        [Fact]
        public void GeneratorB_CalculatesCorrectly()
        {
            var generatorB = new Generator(Puzzle1Example.SeedB, Input.Day15.FactorB);

            generatorB.Next().Should().Be(430625591);
            generatorB.Next().Should().Be(1233683848);
            generatorB.Next().Should().Be(1431495498);
            generatorB.Next().Should().Be(137874439);
            generatorB.Next().Should().Be(285222916);
        }

        [Fact]
        public void FindLast16Bits_AndCompare()
        {
            BigInteger aResult = 245556042;
            BigInteger bResult = 1431495498;

            var a16 = aResult.Low16();
            var b16 = bResult.Low16();

            a16.Should().Be(b16);
        }
    }
}
