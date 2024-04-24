using System.Security.Cryptography;

namespace Particles
{
    public record struct Particle(double X, double Y, double Vx, double Vy)
    {
        private static readonly Random random = new Random();

        public static Particle Random() => 
            new Particle(random.NextDouble(), random.NextDouble(), 0.1 - 0.2 * random.NextDouble(), 0.1 - 0.2 * random.NextDouble());

        public Particle Move(double dt)
        {
            double x = X + Vx * dt;
            double y = Y + Vy * dt;

            if (x < 0)
            {
                x = -x;
                Vx = -Vx;
            }
            else if (x > 1)
            {
                x = 2 - x;
                Vx = -Vx;
            }

            if (y < 0)
            {
                y = -y;
                Vy = -Vy;
            }
            else if (y > 1)
            {
                y = 2 - y;
                Vy = -Vy;
            }

            return new Particle(x, y, Vx, Vy);
        }
    }
}