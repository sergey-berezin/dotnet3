using System.Collections.Immutable;
using System.Xml.Schema;

namespace Particles
{
    public class ParticleSystem
    {
        private ParticleSystem(ImmutableArray<Particle> particles)
        {
            Particles = particles;
        }

        public ParticleSystem(int n) 
        {
            Particles = Enumerable.Range(0, n).Select(_ => Particle.Random()).ToImmutableArray();
        }
        public ImmutableArray<Particle> Particles { get; init; }


        public ParticleSystem Animate(double dt) =>
            new ParticleSystem(Particles.Select(p => p.Move(dt)).ToImmutableArray());


    }
}