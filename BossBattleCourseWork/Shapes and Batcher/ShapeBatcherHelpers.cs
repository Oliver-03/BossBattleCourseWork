using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BossBattleCourseWork
{
    public static class ShapeBatcherHelpers
    {
        public static void Draw(this ShapeBatcher shapeBatcher, Shape shape)
        {
            switch (shape)
            {
                case Circle:
                    shapeBatcher.Draw(shape as Circle);
                    break;
                case Rectangle:
                    shapeBatcher.Draw(shape as Rectangle);
                    break;
                case Triangle triangle:
                    shapeBatcher.Draw(shape as Triangle);
                    break;
                case Polygon:
                    shapeBatcher.Draw(shape as Polygon);
                    break;
            }
        }
        public static void Draw(this ShapeBatcher shapeBatcher, Circle circle)
        {
            shapeBatcher.DrawCircle(circle.Position, circle.Radius, 16, 2, circle.Colour);
        }
        public static void Draw(this ShapeBatcher shapeBatcher, Rectangle rectangle)
        {
            float halfWidth = rectangle.Width * 0.5f;
            float halfHeight = rectangle.Height * 0.5f;
            Vector2[] corners = {
            new Vector2(rectangle.Position.X + halfWidth, rectangle.Position.Y - halfHeight),
            new Vector2(rectangle.Position.X - halfWidth, rectangle.Position.Y - halfHeight),
            new Vector2(rectangle.Position.X - halfWidth, rectangle.Position.Y + halfHeight),
            new Vector2(rectangle.Position.X + halfWidth, rectangle.Position.Y + halfHeight)};

            shapeBatcher.DrawLine(corners[0], corners[1], 2, rectangle.Colour);
            shapeBatcher.DrawLine(corners[1], corners[2], 2, rectangle.Colour);
            shapeBatcher.DrawLine(corners[2], corners[3], 2, rectangle.Colour);
            shapeBatcher.DrawLine(corners[3], corners[0], 2, rectangle.Colour);
        }
        public static void Draw(this ShapeBatcher shapeBatcher, Triangle triangle)
        {
            shapeBatcher.DrawLine(triangle.Position, triangle.Point2, 2, triangle.Colour);
            shapeBatcher.DrawLine(triangle.Point2, triangle.Point3, 2, triangle.Colour);
            shapeBatcher.DrawLine(triangle.Point3, triangle.Position, 2, triangle.Colour);
        }
        public static void Draw(this ShapeBatcher shapeBatcher, Polygon polygon)
        {
            foreach (Triangle triangle in polygon.Triangles)
            {
                shapeBatcher.DrawLine(triangle.Position + polygon.Position, triangle.Point2 + polygon.Position, 2, polygon.Colour);
                shapeBatcher.DrawLine(triangle.Point2 + polygon.Position, triangle.Point3 + polygon.Position, 2, polygon.Colour);
                shapeBatcher.DrawLine(triangle.Point3 + polygon.Position, triangle.Position + polygon.Position, 2, polygon.Colour);
            }
        }
        public static void Draw(this ShapeBatcher shapeBatcher, Player player, Color colour)
        {
            shapeBatcher.DrawCircle(player.Position, player.Radius, 20, 2, colour);
        }

        public static void Draw(this ShapeBatcher shapeBatcher, Agent agent, Color colour)
        {
            shapeBatcher.DrawCircle(agent.Position, agent.Radius, 8, 2, colour);
        }
    }
}
