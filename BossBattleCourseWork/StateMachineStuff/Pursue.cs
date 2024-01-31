using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossBattleCourseWork.StateMachineStuff
{
    public class Pursue : State
    {
        private Agent _agent;
        private Player _player;  // The player to pursue
        private List<Edge> _path;  // The path to follow
        private Graph _graph;
        private GameTime _gameTime;
        private Vector2 PatrolPoint;

        public Pursue(Agent agent, Player player, Graph graph, GameTime gameTime, Vector2 patrolPoint)
        {
            _agent = agent;
            _player = player;
            _path = new List<Edge>();
            _graph = graph;
            _gameTime = gameTime;
            PatrolPoint = patrolPoint;
        }

        public override void Enter(Agent agent)
        {
            
        }

        public override void Execute(Agent agent, GameTime gameTime)
        {
            _path = CalculatePath();
            // Check if the path is not empty
            if (_path.Count > 0)
            {
                MoveAlongPath();
                
            }
            else
            {
                // No valid path, revert to a default state or handle accordingly
                agent.Velocity = Vector2.Zero;
            }
        }

        public override void Exit(Agent agent)
        {
            // Clean up or perform any necessary actions upon exiting the pursue state
        }

        // Method to calculate the path using A* algorithm
        private List<Edge> CalculatePath()
        {
            // Get the closest node to the agent's position
            int startNodeId = GetClosestNode(_agent.Position);

            // Get the closest node to the target's position
            int targetNodeId = GetClosestNode(_player.Position);

            // Use A* algorithm to calculate the path
            AStarSearch aStarSearch = new AStarSearch(_graph, startNodeId, targetNodeId);

            // Update the A* algorithm over time until it's finished or a certain time limit
            float maxExecutionTime = 5f; // Adjust the time limit as needed
            float elapsedTime = 0f;

            while (!aStarSearch.IsFinished && elapsedTime < maxExecutionTime)
            {
                aStarSearch.Update((float)_gameTime.ElapsedGameTime.TotalSeconds);
                elapsedTime += (float)_gameTime.ElapsedGameTime.TotalSeconds;
            }
            // Return the calculated path
            return aStarSearch.ShortestPath;
        }

        // Method to move along the calculated path
        private void MoveAlongPath()
        {
            Debug.WriteLine("hi");
            // Example: Move towards the next node in the path
            Vector2 targetPosition = _graph.GetNode(_path[0].To).Position;

            // Calculate the direction towards the next node
            Vector2 direction = Vector2.Normalize(targetPosition - _agent.Position);

            // Update the agent's velocity and position
            _agent.Velocity = direction * _agent.Speed;
            _agent.Position += _agent.Velocity * (float)_gameTime.ElapsedGameTime.TotalSeconds;

            float distanceThreshold = _agent.Speed * (float)_gameTime.ElapsedGameTime.TotalSeconds;
            if (_path.Count > 0 && Vector2.Distance(_agent.Position, targetPosition) < distanceThreshold)
            {
                // Remove the current node from the path
                _path.RemoveAt(0);
            }
        }
        private int GetClosestNode(Vector2 position)
        {
            int closestNodeId = -1;
            float closestDistance = float.MaxValue;

            foreach (Node node in _graph.Nodes)
            {
                float distance = Vector2.Distance(position, node.Position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestNodeId = node.ID;
                }
            }

            return closestNodeId;
        }
    }

}
