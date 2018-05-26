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
            Particles = input.Select(Particle.Parse).ToList();
        }

        public void Tick(bool destroyOnCollision = false)
        {
            foreach (var particle in Particles)
            {
                particle.Tick();
            }

            if (!destroyOnCollision)
            {
                return;
            }

            // could also set them as destroyed, avoid the extra allocations and removal
            foreach (var particle in new List<Particle>(Particles))
            {
                var toRemove = Particles.Where(x => particle.Equals(x)).ToList();
                if (toRemove.Count < 2)
                    continue;

                foreach (var remove in toRemove)
                {
                    Particles.Remove(remove);
                }
            }
        }
    }
}