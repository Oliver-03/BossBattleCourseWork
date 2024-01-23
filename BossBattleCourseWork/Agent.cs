using System;
using System.Collections.Generic;
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
        public float Speed = 45;

        public Agent(Vector2 position, Vector2 velocity, float radius, Color colour)
        {
            Position = position;
            Velocity = velocity;
            Radius = radius;
            Colour = colour;
            StateMachine = new StateMachine(this);
        }
        
        public void Update(GameTime gameTime)
        {
            
            StateMachine.Update(gameTime);
        }
    }
}
