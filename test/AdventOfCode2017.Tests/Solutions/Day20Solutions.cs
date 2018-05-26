using System;
using AdventOfCode2017.Day20;
using AdventOfCode2017.Inputs;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2017.Tests.Solutions
{
    public class Day20Solutions : TestBase
    {
        public Day20Solutions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Puzzle1_SeeWhatsClosestAfterSoManyTicks()
        {
            var input = Input.Day20Parse(Input.Day20);
            var collection = new ParticleCollection(input);

            for(var k = 0; k < 1000; ++k)
            {
                collection.Tick();
            }

            collection.ClosestParticle.Index.Should().Be(144);
        }

        [Fact]
        public void Puzzle1_InputParsesCorrectly()
        {
            var input = Input.Day20Parse(Input.Day20);

            var collection = new ParticleCollection(input);

            collection.Particles.Should().HaveCount(1000);
        }

        const string Example1Input = "p=< 3,0,0>, v=< 2,0,0>, a=<-1,0,0>";

        [Fact]
        public void ParticleParse_ParsesInputCorrectly()
        {
            var particle = Particle.Parse(0, Example1Input);

            particle.X.Position.Should().Be(3);
            particle.X.Velocity.Should().Be(2);
            particle.X.Acceleration.Should().Be(-1);
        }
    }
}