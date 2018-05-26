using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace AdventOfCode2017.Day20
{
    public class ParticleCollection
    {
        public IList<Particle> Particles { get; }
        public Particle ClosestParticle => Particles.MinBy(x => x.Distance);

        public ParticleCollection(IList<string> input)
        {
            Particles = input.Select((info, index) => Particle.Parse(index, info)).ToList();
        }

        public void Tick()
        {
            foreach(var particle in Particles)
            {
                particle.Tick();
            }
        }
    }
}