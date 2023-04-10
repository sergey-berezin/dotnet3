using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Geometry
{
    public abstract class Shape
    {
        public abstract double GetSquare();
    }
    public class Rectangle : Shape
    {
        private double xmin, ymin, xmax, ymax;

        public override double GetSquare() => (xmax - xmin) * (ymax - ymin);
    }

    public class Circle : Shape
    {
        private double x, y, radius;

        public override double GetSquare() => Math.PI * radius * radius;
    }
}

namespace Rendering
{
    public abstract class Shape
    {
        public abstract void Render(Graphics g);
    }

    public class Rectangle : Shape
    {
        private Geometry.Rectangle rectangle;

        public override void Render(Graphics _)
        {
            Console.WriteLine("Render rectangle");
        }
    }

    public class Circle : Shape
    {
        private Geometry.Circle circle;

        public override void Render(Graphics g)
        {
            Console.WriteLine("Render circle");
        }
    }

    public class Polygon : Shape
    {
        public override void Render(Graphics g)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Rectangle());
            shapes.Add(new Circle());

            foreach (var s in shapes)
                s.Render(null);
        }
    }
}
