using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BossBattleCourseWork
{
    public abstract class Shape
    {
        public Vector2 Position { get; set; }
        public Color Colour { get; set; }

        protected Shape(Vector2 position, Color colour)
        {
            Position = position;
            Colour = colour;
        }
        public abstract bool IsInside(Vector2 point);
    }
}
