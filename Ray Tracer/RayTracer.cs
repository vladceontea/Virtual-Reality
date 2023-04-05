using System;
using System.Runtime.InteropServices;

namespace rt
{
    class RayTracer
    {
        private Geometry[] geometries;
        private Light[] lights;

        public RayTracer(Geometry[] geometries, Light[] lights)
        {
            this.geometries = geometries;
            this.lights = lights;
        }

        private double ImageToViewPlane(int n, int imgSize, double viewPlaneSize)
        {
            var u = n * viewPlaneSize / imgSize;
            u -= viewPlaneSize / 2;
            return u;
        }

        private Intersection FindFirstIntersection(Line ray, double minDist, double maxDist)
        {
            var intersection = new Intersection();

            foreach (var geometry in geometries)
            {
                var intr = geometry.GetIntersection(ray, minDist, maxDist);

                if (!intr.Valid || !intr.Visible) continue;

                if (!intersection.Valid || !intersection.Visible)
                {
                    intersection = intr;
                }
                else if (intr.T < intersection.T)
                {
                    intersection = intr;
                }
            }

            return intersection;
        }

        private bool IsLit(Vector point, Light light)
        {
            // ADD CODE HERE: Detect whether the given point has a clear line of sight to the given light
            Line lightRay = new Line(light.Position, point);

            var intersection = FindFirstIntersection(lightRay, 0.0, (light.Position - point).Length()-0.0001);

            //I added this value (0.0001) because I had issues with "shadow acne" and I found out this can help (and it did)

            return !intersection.Visible;
        }

        public void Render(Camera camera, int width, int height, string filename)
        {
            var image = new Image(width, height);

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    // ADD CODE HERE: Implement pixel color calculation
                    var color = new Color();
                    var x1 = camera.Position + camera.Direction*camera.ViewPlaneDistance
                        + camera.Up * ImageToViewPlane(j, height, camera.ViewPlaneHeight)
                        + (camera.Up^camera.Direction) * ImageToViewPlane(i, width, camera.ViewPlaneWidth);

                    var x0 = camera.Position;

                    var line = new Line(x0, x1);

                    var intersection = FindFirstIntersection(line, camera.FrontPlaneDistance, camera.BackPlaneDistance);
                    if (intersection.Geometry == null)
                    {
                        image.SetPixel(i, j, color);
                    }
                    else
                    {
                        Material material = intersection.Geometry.Material;
                        foreach (var light in lights)
                        {
                            var lightColor = material.Ambient * light.Ambient;

                            if (IsLit(intersection.Position, light)){

                                var t = (light.Position - intersection.Position).Normalize();
                                var n = (intersection.Position - ((Sphere)intersection.Geometry).getCenter()).Normalize();

                                if (n * t > 0)
                                {
                                    lightColor += material.Diffuse * light.Diffuse * (n * t);
                                }

                                var e = (camera.Position - intersection.Position).Normalize();
                                var r = n * (n * t) * 2 - t;

                                if (e * r > 0)
                                {
                                    lightColor += material.Specular * light.Specular * Math.Pow(e * r, material.Shininess);
                                }

                                lightColor *= light.Intensity;

                            }
                            color += lightColor;
                        }
                        
                        image.SetPixel(i, j, color);
                    }

                }
            }

            image.Store(filename);
        }
    }
}