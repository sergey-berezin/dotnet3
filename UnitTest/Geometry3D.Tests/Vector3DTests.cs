using System;
using Xunit;

namespace Geometry3D.Tests
{
    public class Vector3DTests
    {
        [Fact]
        public void Normalize()
        {
            var v = new Geometry3D.Vector3D(6, 8, 0);
            var unit = v.Normalize();
            Assert.Equal(0.6, unit.X, 10);
            Assert.Equal(0.8, unit.Y, 10);
            Assert.Equal(0, unit.Z, 10);
        }

        [Fact]
        public void NormalizeZero()
        {
            var v = Geometry3D.Vector3D.Zero;
            Assert.Throws<ArgumentException>(() => v.Normalize());
        }
    }
}
