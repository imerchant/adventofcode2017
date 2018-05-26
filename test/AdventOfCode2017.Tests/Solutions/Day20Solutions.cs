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
        public void Puzzle2_FindHowManyParticlesSurvivedAllTheCollisions()
        {
            var input = Input.Day20Parse(Input.Day20);
            var collection = new ParticleCollection(input);

            for(var k = 0; k < 39; ++k)
            {
                collection.Tick(destroyOnCollision: true);
            }

            collection.Particles.Should().HaveCount(477);
        }

        public const string Example2 =
@"p=<-6,0,0>, v=< 3,0,0>, a=< 0,0,0>
p=<-4,0,0>, v=< 2,0,0>, a=< 0,0,0>
p=<-2,0,0>, v=< 1,0,0>, a=< 0,0,0>
p=< 3,0,0>, v=<-1,0,0>, a=< 0,0,0>";

        [Fact]
        public void Puzzle2_ExampleCompletesCorrectly()
        {
            var input = Input.Day20Parse(Example2);
            var collection = new ParticleCollection(input);

            for(var k = 0; k < 3; ++k)
            {
                collection.Tick(true);
            }

            collection.Particles.Should().HaveCount(1).And.Contain(x => x.Index == 3);
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
            var particle = Particle.Parse(Example1Input, 0);

            particle.X.Position.Should().Be(3);
            particle.X.Velocity.Should().Be(2);
            particle.X.Acceleration.Should().Be(-1);
        }
    }
}