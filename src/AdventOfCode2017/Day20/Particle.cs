using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Day20
{

    public class Particle
    {
        public int Index { get; }
        public ParticlePosition X { get; }
        public ParticlePosition Y { get; }
        public ParticlePosition Z { get; }

        public long Distance => Math.Abs(X.Position) + Math.Abs(Y.Position) + Math.Abs(Z.Position);

        private Particle(int index, ParticlePosition x, ParticlePosition y, ParticlePosition z)
        {
            Index = index;
            X = x;
            Y = y;
            Z = z;
        }

        private static readonly Regex ParseRegex = new Regex("p=<(?'pos'.*?)>, v=<(?'vel'.*?)>, a=<(?'accel'.*?)>", RegexOptions.Compiled);
        public static Particle Parse(int index, string input)
        {
            var dataMatch = ParseRegex.Match(input);
            if(!dataMatch.Success)
            {
                throw new Exception($"Could not parse {input}");
            }

            var positions = dataMatch.Groups["pos"].Value.SplitOn(',').Select(long.Parse).ToList();
            var velocities = dataMatch.Groups["vel"].Value.SplitOn(',').Select(long.Parse).ToList();
            var accels = dataMatch.Groups["accel"].Value.SplitOn(',').Select(long.Parse).ToList();

            var x = new ParticlePosition(positions[0], velocities[0], accels[0]);
            var y = new ParticlePosition(positions[1], velocities[1], accels[1]);
            var z = new ParticlePosition(positions[2], velocities[2], accels[2]);

            return new Particle(index, x, y, z);
        }

        public void Tick()
        {
            X.Tick();
            Y.Tick();
            Z.Tick();
        }

        public override string ToString()
        {
            return $"[{Index}]: {Distance} ({X.Position}, {Y.Position}, {Z.Position})";
        }

        public class ParticlePosition
        {
            public long Position { get; private set; }
            public long Velocity { get; private set; }
            public long Acceleration { get; private set; }

            internal ParticlePosition(long position, long velocity, long acceleration)
            {
                Position = position;
                Velocity = velocity;
                Acceleration = acceleration;
            }

            internal void Tick()
            {
                Velocity += Acceleration;
                Position += Velocity;
            }
        }
    }
}