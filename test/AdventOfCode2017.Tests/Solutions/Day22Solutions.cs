using System;
using System.Collections.Generic;
using AdventOfCode2017.Day22;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day22Solutions : TestBase
    {
        public Day22Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_CountInfectionsCaused_OverTheCourseOf10000Ticks()
        {
            var input = Input.Day22Parse(Input.Day22);
            var carrier = new VirusCarrier(input);

            for (var k = 0; k < 10_000; ++k)
            {
                carrier.Tick();
            }

            carrier.InfectionsCaused.Should().Be(5339);
        }

        [Fact(Skip = "Adds a couple seconds to the test runs")]
        public void Puzzle2_CountInfectionsCaused_ByEvolvedBehavior_OverTheCourseOf10000000Ticks()
        {
            var input = Input.Day22Parse(Input.Day22);
            var carrier = new VirusCarrier(input);

            for (var k = 0; k < 10_000_000; ++k)
            {
                carrier.EvolvedTick();
            }

            carrier.InfectionsCaused.Should().Be(2512380);
        }

        [Theory]
        [InlineData(100, 26)]
        [InlineData(10_000_000, 2511944)]
        public void EvolvedTick_RecordsInfectionsCorrectly(int ticks, int expectedCausedInfections)
        {
            var input = Input.Day22Parse(PuzzleExample);
            var carrier = new VirusCarrier(input);

            for (var k = 0; k < ticks; ++k)
            {
                carrier.EvolvedTick();
            }

            carrier.InfectionsCaused.Should().Be(expectedCausedInfections);
        }

        [Fact]
        public void EvolvedTick_WeakensRatherThanInfects_InitialLocation()
        {
            var input = Input.Day22Parse(PuzzleExample);
            var carrier = new VirusCarrier(input);
            (int originalX, int originalY) = carrier.Location;

            carrier.EvolvedTick();

            carrier.VisitedNodes[(originalX, originalY)].Should().Be(NodeState.Weakened);
        }

        public const string PuzzleExample =
@"..#
#..
...";

        [Theory]
        [InlineData(7, 5)]
        [InlineData(70, 41)]
        [InlineData(10_000, 5587)]
        public void VirusCarrier_RecordsInfectionsCorrectly(int ticks, int expectedCausedInfections)
        {
            var input = Input.Day22Parse(PuzzleExample);
            var carrier = new VirusCarrier(input);

            for (var k = 0; k < ticks; ++k)
            {
                carrier.Tick();
            }

            carrier.InfectionsCaused.Should().Be(expectedCausedInfections);
        }

        [Fact]
        public void VirusCarrier_TurnsAndMovesCorrectly()
        {
            var input = Input.Day22Parse(PuzzleExample);
            var carrier = new VirusCarrier(input);

            carrier.Tick();

            carrier.Location.Should().Be((1, 0));
            carrier.Facing.Name.Should().Be("West");
        }

        [Fact]
        public void VirusCarrier_IdentifiesStartingPosition()
        {
            var input = Input.Day22Parse(PuzzleExample);

            var carrier = new VirusCarrier(input);

            carrier.Location.Should().Be((1, 1));
        }

        [Theory]
        [MemberData(nameof(CenterOfInputCases))]
        public void CanLocateCenterOfInput(string map, int expectedX, int expectedY)
        {
            var input = Input.Day22Parse(map);
            (int X, int Y) center = ((int)Math.Floor(input.Count / 2.0), (int)Math.Floor(input[0].Length / 2.0));

            center.X.Should().Be(expectedX);
            center.Y.Should().Be(expectedY);
        }

        public static IEnumerable<object[]> CenterOfInputCases()
        {
            yield return new object[] { Input.Day22, 12, 12 };
            yield return new object[] { PuzzleExample, 1, 1 };
        }
    }
}