using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BossBattleCourseWork
{
    public class Polygon: Shape
    {
        public List<Triangle> Triangles { get; } = new List<Triangle>();
        public Polygon(Vector2 position,Color colour, List<Vector2> points) : base(position, colour)
        {
            for (int i = 2; i < points.Count; i++)
            {
                Triangles.Add(new Triangle(points[i - 2],Color.Red, points[i - 1], points[i]));
            }
        }
        public override bool IsInside(Vector2 position)
        {

            for (int i = 0; i < Triangles.Count; i++)
            {
                if (Triangles[i].IsInside(position)) return true;
            }

            return false;
        }
    }
}
