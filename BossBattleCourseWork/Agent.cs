using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BossBattleCourseWork
{
    public class Agent
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Color Colour { get; set; }
        public float Radius { get; set; }
        public StateMachine StateMachine { get; private set; }
        public float Speed = 30;
        public bool IsDead { get; set; }
        public int Role { get; set; }
        public Agent(Vector2 position, Vector2 velocity, float radius, Color colour, int role)
        {
            Position = position;
            Velocity = velocity;
            Radius = radius;
            Colour = colour;
            StateMachine = new StateMachine(this);
            Role = role;
            IsDead = false;
        }

        public void Update(GameTime gameTime)
        {

            StateMachine.Update(gameTime);
        }
    }
}