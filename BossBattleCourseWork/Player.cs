using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;

namespace BossBattleCourseWork
{
    public class Player
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Radius { get; set; }
        public float Speed = 50;
        public Input Input = new Input()
        {
            Left = Keys.A , Right = Keys.D, Down = Keys.S, Up = Keys.W
        };

        public float velocityX { get; set; }
        public float velocityY { get; set; }

        public Player(Vector2 position, Vector2 velocity, float radius)
        {
            Position = position;
            Velocity = velocity;
            Radius = radius;
        }


        public void Update(float time, List<Rectangle> rectangles)
        {
            Move();

            foreach (var rectangle in rectangles)
            {
                if (Velocity.X > 0 && IsTouchingLeft(rectangle) || Velocity.X < 0 && IsTouchingRight(rectangle))
                {
                    velocityX = -velocityX;
                }
                if (Velocity.Y > 0 && IsTouchingTop(rectangle) || Velocity.Y < 0 && IsTouchingBottom(rectangle))
                {
                    velocityY = -velocityY;
                }
            }
            Velocity = new Vector2(velocityX, velocityY);
            Position += Velocity * time;
            }

        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                velocityX = -Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                velocityX = Speed;
            }
            else
            {
                velocityX = 0; // No horizontal movement
            }
            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                velocityY = Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                velocityY = -Speed;
            }
            else
            {
                velocityY = 0; // No vertical movement
            }
        }

        //Collisions stuff
        private bool IsTouchingLeft(Rectangle rectangle)
        {
            return Position.X - Radius < rectangle.Position.X + rectangle.Width / 2 &&
                   Position.X + Radius > rectangle.Position.X - rectangle.Width / 2 &&
                   Position.Y < rectangle.Position.Y + rectangle.Height / 2 &&
                   Position.Y > rectangle.Position.Y - rectangle.Height / 2;
        }

        private bool IsTouchingRight(Rectangle rectangle)
        {
            return Position.X + Radius > rectangle.Position.X - rectangle.Width / 2 &&
                   Position.X - Radius < rectangle.Position.X + rectangle.Width / 2 &&
                   Position.Y < rectangle.Position.Y + rectangle.Height / 2 &&
                   Position.Y > rectangle.Position.Y - rectangle.Height / 2;
        }

        private bool IsTouchingTop(Rectangle rectangle)
        {
            return Position.Y - Radius < rectangle.Position.Y + rectangle.Height / 2 &&
                   Position.Y + Radius > rectangle.Position.Y - rectangle.Height / 2 &&
                   Position.X < rectangle.Position.X + rectangle.Width / 2 &&
                   Position.X > rectangle.Position.X - rectangle.Width / 2;
        }

        private bool IsTouchingBottom(Rectangle rectangle)
        {
            return Position.Y + Radius > rectangle.Position.Y - rectangle.Height / 2 &&
                   Position.Y - Radius < rectangle.Position.Y + rectangle.Height / 2 &&
                   Position.X < rectangle.Position.X + rectangle.Width / 2 &&
                   Position.X > rectangle.Position.X - rectangle.Width / 2;
        }

    }
}

        
