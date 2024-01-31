using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public float Speed = 40;
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

        public void Update(GameTime gameTime, List<Agent> otherAgents)
        {
            StateMachine.Update(gameTime);
            CheckCollisions(otherAgents);
        }
        private void CheckCollisions(List<Agent> otherAgents)
        {
            foreach (Agent otherAgent in otherAgents)
            {
                // Skip checking collisions with itself
                if (otherAgent == this)
                    continue;

                // Check if there is a collision
                if (CheckCollision(otherAgent))
                {
                    // Handle the collision (e.g., adjust positions or velocities)
                    HandleCollision(otherAgent);
                }
            }
        }

        private bool CheckCollision(Agent otherAgent)
        {
            float distance = Vector2.Distance(Position, otherAgent.Position);
            float combinedRadius = Radius + otherAgent.Radius;

            return distance < combinedRadius;
        }

        private void HandleCollision(Agent otherAgent)
        {
            Vector2 direction = Vector2.Normalize(otherAgent.Position - Position);

            float overlap = Radius + otherAgent.Radius - Vector2.Distance(Position, otherAgent.Position);

            Position -= 0.5f * overlap * direction;
            otherAgent.Position += 0.5f * overlap * direction;

            Velocity = -Velocity;
            otherAgent.Velocity = -otherAgent.Velocity;
        }
    }
}