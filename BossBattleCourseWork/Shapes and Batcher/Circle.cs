using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BossBattleCourseWork
{
    public class Circle : Shape
    {
        public float Radius { get; set; }
        public Circle(Vector2 position, Color colour, float radius) : base(position, colour)
        {
            Radius = radius;
        }

        public override bool IsInside(Vector2 point)
        {
            return (point - Position).LengthSquared() < Radius * Radius;
        }
        public void SetPosition(Vector2 position)
        {
            Position = position;
        }
    }
}
