using MathLib;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Xunit.Abstractions;

namespace MathTests
{
    public class ArgMaxMinTests
    {
        ITestOutputHelper output;

        public ArgMaxMinTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestArgMaxMin()
        {
            double[] a = { 1, 2, -3 };
            var result = Utils.ArgMaxMin(a);
            result.MinIndex.Should().Be(2);
            result.MaxIndex.Should().Be(1);

            output.WriteLine("Hello!");
        }

        [Fact]
        public void HandlesEmptyArray()
        {
            double[] a = { };
            Action code = () => Utils.ArgMaxMin(a);
            code.Should().Throw<ArgumentException>();
        }

        [Property(Arbitrary = new Type[] { typeof(NormalFloatGenerator) })]
        public Property FuzzingMaxMin() =>
            Prop.ForAll<double[]>(a =>
            {
                output.WriteLine("Test data:" + String.Join(",", a.Select(e => e.ToString())));
                if (a.Length == 0) return true;

                var r = Utils.ArgMaxMin(a);

                return
                    (a.All(x => x <= a[r.MaxIndex]) &&
                    a.All(x => x >= a[r.MinIndex]) &&
                    a.Any(x => x == a[r.MaxIndex]) &&
                    a.Any(x => x == a[r.MinIndex]));
            });

        public static class NormalFloatGenerator
        {
            public static Arbitrary<double> Float() =>
                Arb.Default.Float().Filter(f => !double.IsNaN(f));
        }

    }
}