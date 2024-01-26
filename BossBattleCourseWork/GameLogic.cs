using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossBattleCourseWork
{
    public class GameLogic
    {
        private Agent _agent;
        private Player _player;
        private List<Rectangle> _rectangles;

        public GameLogic(Agent agent, Player player, List<Rectangle> rectangles)
        {
            _agent = agent;
            _player = player;
            _rectangles = rectangles;
        }

        public void Update(GameTime gameTime, List<Rectangle> rectangles)
        {
            // Update agent and player positions
            _agent.Update(gameTime);
            _player.Update(gameTime.ElapsedGameTime.Seconds, rectangles);

            // Check for collision
            if (CheckCollision(_agent, _player))
            {
                // Collision detected, handle player death
                PlayerDeath();
            }
        }

        private bool CheckCollision(Agent agent, Player player)
        {
            // Calculate the distance between agent and player centers
            float distance = Vector2.Distance(agent.Position, player.Position);

            // Check if the distance is less than the sum of their radii
            return distance < agent.Radius + player.Radius;
        }

        private void PlayerDeath()
        {
            _player.IsDead = true;
        }
    }

}
