using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace LSP
{
    public abstract class Shape { }

    public class Rectangle : Shape
    {
        public double Width { get; set; }

        public double Height { get; set; }
    }

    public class Square : Rectangle { }

    public class SquareRectangle
    {
        [Theory]
        [MemberData(nameof(GetTestShapes))]
        public void RectangleTest(Rectangle r)
        {
            r.Width = 10;
            r.Height = 20;
            Assert.Equal(10, r.Width);
            Assert.Equal(20, r.Height);
        }

        public static IEnumerable<object[]> GetTestShapes()
        {
            yield return new object[] { new Rectangle() };
            yield return new object[] { new Square() };
        }
    }
}
