using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace SRP1
{
    public abstract class Shape
    {
        public abstract void Render(Graphics g);

        public abstract double GetSquare();
    }

    public class Rectangle : Shape
    {
        private double xmin, ymin, xmax, ymax;

        public override void Render(Graphics _)
        {
            Console.WriteLine("Render rectangle");
        }

        public override double GetSquare() => (xmax - xmin) * (ymax - ymin);
    }

    public class Polygon : Shape
    {
        public override double GetSquare()
        {
            throw new NotImplementedException();
        }

        public override void Render(Graphics g)
        {
            throw new NotImplementedException();
        }
    }

    public class Circle : Shape
    {
        private double x, y, radius;

        public override void Render(Graphics g)
        {
            Console.WriteLine("Render circle");
        }

        public override double GetSquare() => Math.PI * radius * radius;
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Rectangle());
            shapes.Add(new Circle());
            shapes.Add(new Polygon());

            foreach (var s in shapes)
                s.Render(null);
        }
    }
}
