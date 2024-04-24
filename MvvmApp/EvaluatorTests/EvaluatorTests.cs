using FluentAssertions;
using System.Linq;

namespace TestProject1
{
    public class EvaluatorTests
    {
        [Fact]
        public void BasicTest()
        {
            (double[] x, double[] y) = Evaluator.Evaluator.EvaluateSeries("x*x");

            x.SkipLast(1).Zip(x.Skip(1)).Should().OnlyContain(t =>  t.First < t.Second);
            x.Zip(y).Should().OnlyContain(t => Math.Abs(t.First * t.First - t.Second) < 1e-10);
        }
    }
}