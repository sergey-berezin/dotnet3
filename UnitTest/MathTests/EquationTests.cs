using MathLib;

namespace MathTests
{
    public class EquationTests
    {
        [Fact]
        public void TwoRoots()
        {
            var r = Equation.Solve(-1, 0, 1);
            Assert.Equal(1, r[0]);
            Assert.Equal(-1, r[1]);
        }

        [Fact]
        public void NoRoots()
        {
            var r = Equation.Solve(1, 0, 1);
            Assert.Empty(r);
        }

        [Theory]
        [InlineData(1,0,-1,1,-1)]
        [InlineData(-1, 0, 4, 2, -2)]
        public void TwoRoots2(double a, double b, double c, double x1, double x2)
        {
            var r = Equation.Solve(a, b, c);
            Assert.Equal(x1, r[0]);
            Assert.Equal(x2, r[1]);
        }
    }
}