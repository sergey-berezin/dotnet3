using ArrayUtils;
using FluentAssertions.Equivalency;
using FsCheck;
using FsCheck.Xunit;
using System.Linq;

namespace ArrayTests
{
    public class MaxMinTests
    {
        [Fact]
        public void Correct_MinMax()
        {
            double[] a = { 1.1, 2, -3.5 };
            var result = Utils.ArgMaxMin(a);

            result.Min.Should().Be(2);
            result.Max.Should().Be(1);
        }

        [Fact]
        public void Handles_Empty_Array()
        {
            Action a = () => Utils.ArgMaxMin(new double[0]);
            a.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(new double[] { -2.5, 1, 0 })]
        [InlineData(new double[] { 0, -2.5, 1 })]
        public void Correct_Maxs(double[] a)
        {
            var result = Utils.ArgMaxMin(a);

            result.Max.Should().BeGreaterThanOrEqualTo(0);
            result.Max.Should().BeLessThan(a.Length);

            a.All(x => x <= a[result.Max]).Should().BeTrue();
        }

        [Property]
        public FsCheck.Property Correct_Mins() =>
            Prop.ForAll<double[]>(a =>
            {
                if (a is null || a.Length == 0) return;

                var result = Utils.ArgMaxMin(a);

                result.Min.Should().BeGreaterThanOrEqualTo(0);
                result.Min.Should().BeLessThan(a.Length);

                a.All(x => x >= a[result.Min]).Should().BeTrue();
            });
    }
}