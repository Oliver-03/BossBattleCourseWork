using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossBattleCourseWork
{
    public class GameLogic
    {
        private List<Agent> _agents;
        private Player _player;
        private List<Rectangle> _rectangles;
        private bool IsNightTime;

        public GameLogic(List<Agent> agents, Player player, List<Rectangle> rectangles)
        {
            _agents = agents;
            _player = player;
            _rectangles = rectangles;
        }

        public void Update(GameTime gameTime, List<Rectangle> rectangle)
        {
            if (NightTimeCheck(gameTime))
            {
                NightTime(_agents, _player);
            }

            // Update agent and player positions
            foreach (Agent agent in _agents)
            {
                agent.Update(gameTime);
            }

            _player.Update(gameTime.ElapsedGameTime.Seconds, rectangle);

            // Check for collision
            if (IsNightTime == false)
            {

                if (CheckCollision(_agents, _player))
                {
                    // Collision detected, handle player death
                    PlayerDeath();
                }
            }
            else if (CheckCollision(_agents, _player))
            {
                AgentDeath(_player);
            }

        }

        private bool CheckCollision(List<Agent> agents, Player player)
        {
            return agents.Any(agent => Vector2.Distance(agent.Position, player.Position) < agent.Radius + player.Radius);
        }

        private void PlayerDeath()
        {
            _player.IsDead = true;
        }
        private bool NightTimeCheck(GameTime gameTime)
        {
            // Calculate the total elapsed time in seconds
            double totalElapsedSeconds = gameTime.TotalGameTime.TotalSeconds;

            // Calculate the time within the 90-second cycle starting from the beginning of the game
            double cycleTime = totalElapsedSeconds % 90;

            // Nighttime lasts for the last 30 seconds of the cycle
            return cycleTime >= 60 && cycleTime < 90;
        }
        private void NightTime(List<Agent> agents, Player player)
        {
            IsNightTime = true;
            player.Speed = 100;
            Debug.WriteLine("nighttime");
        }
        private void AgentDeath(Player player)
        {
            // Find the agent that collided with the player during nighttime
            Agent collidingAgent = _agents.FirstOrDefault(agent => Vector2.Distance(agent.Position, player.Position) < agent.Radius + player.Radius);

            if (collidingAgent != null)
            {
                // Set the IsDead property to true for the colliding agent
                collidingAgent.IsDead = true;
            }
        }
    } 
}
