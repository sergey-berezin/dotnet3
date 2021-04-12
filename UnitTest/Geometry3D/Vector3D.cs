using System;

namespace Geometry3D
{
    public class Vector3D
    {
        public Vector3D(double x, double y, double z) 
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D Normalize()
        {
            double len = Math.Sqrt(X * X + Y * Y + Z * Z);
            if (len < 1e-10)
                throw new ArgumentException();
            return new Vector3D(X / len, Y / len, Z / len);
        }

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public static readonly Vector3D Zero = new Vector3D(0, 0, 0);
    }
}
