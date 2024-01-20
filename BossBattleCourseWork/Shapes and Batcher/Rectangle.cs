using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BossBattleCourseWork
{
    public class Rectangle : Shape
    {
        public float Width { get; set; }
        public float Height { get; set; }
        public Rectangle(Vector2 position, Color colour, float width, float height) : base(position, colour)
        {
            Width = width;
            Height = height;
        }

        public override bool IsInside(Vector2 point)
        {
            float halfHeight = Height * 0.5f;
            float halfWidth = Width * 0.5f;
            Vector2 pointInRectangleSpace = point - Position;
            return Math.Abs(pointInRectangleSpace.X) < halfWidth && Math.Abs(pointInRectangleSpace.Y) < halfHeight;
        }
        public void SetPosition(Vector2 position)
        {
            Position = position;
        }
    }
}
