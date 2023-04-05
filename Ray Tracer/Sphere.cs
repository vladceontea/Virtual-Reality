using System;

namespace rt
{
    public class Sphere : Geometry
    {
        private Vector Center { get; set; }
        private double Radius { get; set; }

        public Sphere(Vector center, double radius, Material material, Color color) : base(material, color)
        {
            Center = center;
            Radius = radius;
        }

        public Vector getCenter()
        {
            return this.Center;
        }

        public override Intersection GetIntersection(Line line, double minDist, double maxDist)
        {
            // ADD CODE HERE: Calculate the intersection between the given line and this sphere
            // (p - c)^2 = r^2
            // p = x0 + t * dx
            // a * t^2 + b * t + c = 0

            var originCenter = line.X0 - this.Center;
            var a = line.Dx * line.Dx;
            var b = 2 * (line.Dx * originCenter);
            var c = originCenter * originCenter - this.Radius * this.Radius;

            var delta = b * b - 4 * c * a;

            if (delta < 0)
            {
                return new Intersection();
            }
            else
            {
                var t = (-b - Math.Sqrt(delta))/ (2*a);

                if (t < 0)
                {
                    t = (-b + Math.Sqrt(delta)) / (2*a);
                }

                if (t >= minDist && t <= maxDist)
                {
                    return new Intersection(true, true, this, line, t);
                }
                return new Intersection();
            }
        }

        public override Vector Normal(Vector v)
        {
            var n = v - Center;
            n.Normalize();
            return n;
        }
    }
}