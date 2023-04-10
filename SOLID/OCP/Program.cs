using Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Geometry
{
    public interface IVisitor
    {
        void VisitRectangle(Rect r);
        void VisitCircle(Circle c);
    }

    public abstract class Shape
    {
        public abstract double GetSquare();

        public abstract void Accept(IVisitor v);
    }
    public class Rect : Shape
    {
        private double xmin, ymin, xmax, ymax;

        public override double GetSquare() => (xmax - xmin) * (ymax - ymin);

        public override void Accept(IVisitor v) => v.VisitRectangle(this);
    }

    public class Circle : Shape
    {
        private double x, y, radius;

        public override double GetSquare() => Math.PI * radius * radius;

        public override void Accept(IVisitor v) => v.VisitCircle(this);
    }
}

namespace Rendering
{
    using Geometry;

    public class ShapeRenderer : IVisitor
    {
        private readonly Graphics g;

        public ShapeRenderer(Graphics g) => this.g = g;

        public void VisitCircle(Circle c)
        {
            Console.WriteLine($"Rendering circle");

        }

        public void VisitRectangle(Rect r)
        {
            Console.WriteLine($"Rendering rectangle");
        }
    }

}

namespace Persistence
{
    using Geometry;

    public class ShapeWriter : IVisitor
    {
        private readonly Stream output;

        public ShapeWriter(Stream s) => output = s;
        public void VisitCircle(Circle c)
        {
            Console.WriteLine($"Writing circle");

        }

        public void VisitRectangle(Rect r)
        {
            Console.WriteLine($"Writing rectangle");
        }
    }
}

namespace App
{
    using Rendering;
    using Persistence;

    class Program
    {
        static void Main(string[] args)
        {
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Rect());
            shapes.Add(new Circle());

            ShapeRenderer sw = new ShapeRenderer(null);
            foreach (var s in shapes)
                s.Accept(sw);

            ShapeWriter e = new ShapeWriter(File.OpenWrite("1.bin"));
            foreach (var s in shapes)
                s.Accept(e);
        }
    }
}
