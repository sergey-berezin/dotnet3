namespace MathLib
{
    public static class Equation
    {
        public static double[] Solve(double a, double b, double c)
        {
            double d = b * b - 4 * a * c;
            if (d > 0) return new double[] { (-b + Math.Sqrt(d)) / 2, (-b - Math.Sqrt(d)) / 2 };
            else if (d == 0) return new double[] { -b / 2 };
            else return new double[0];
        }
    }
}