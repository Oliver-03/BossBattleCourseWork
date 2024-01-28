using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossBattleCourseWork
{
    public class PursueState : State
    {
        private Player _player;
        private Graph _graph;
        private List<Rectangle> _map;
        private Vector2 PatrolPoint;
        public PursueState(Player player, Graph graph, List<Rectangle> map, Vector2 patrolPoint, GameTime gameTime)
        {
            _player = player;
            _graph = graph;
            _map = map;
            PatrolPoint = patrolPoint;
        }

        public override void Enter(Agent agent)
        {
            agent.Colour = Color.Red;
        }

        public override void Execute(Agent agent, GameTime gameTime)
        {
            List<Vector2> path = AStarPathfinding(agent.Position, _player.Position, _graph);

            if (path.Count > 1)
            {
                Vector2 nextWaypoint = path[1];
                float distanceToWaypoint = Vector2.Distance(agent.Position, nextWaypoint);
                float distanceThreshold = 00.0f;  // Adjust this threshold as needed

                if (distanceToWaypoint <= distanceThreshold)
                {
                    // Move to the next waypoint
                    path.RemoveAt(0);
                }

                Vector2 desiredVelocity = Vector2.Normalize(nextWaypoint - agent.Position) * agent.Speed;
                agent.Velocity = desiredVelocity;

                // Update agent's position based on velocity
                agent.Position += agent.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                agent.Velocity = Vector2.Zero;
            }
            if (agent.Role == 2 && Vector2.Distance(PatrolPoint, agent.Position) > 250)
            { 
                agent.StateMachine.RevertState();
            }
        }


        public override void Exit(Agent agent)
        {
            List<Vector2> PatrolPath = AStarPathfinding(agent.Position, PatrolPoint, _graph);

            if (PatrolPath.Count > 1)
            {
                Vector2 nextWaypoint = PatrolPath[1];
                float distanceToWaypoint = Vector2.Distance(agent.Position, nextWaypoint);
                float distanceThreshold = 10.0f;  // Adjust this threshold as needed

                if (distanceToWaypoint <= distanceThreshold)
                {
                    // Move to the next waypoint
                    PatrolPath.RemoveAt(0);
                }

                if (PatrolPath.Count > 0)
                {
                    Vector2 desiredVelocity = Vector2.Normalize(PatrolPath[0] - agent.Position) * agent.Speed;
                    agent.Velocity = desiredVelocity;

                    // Update agent's position based on velocity
                    agent.Position += agent.Velocity * 0.1f;
                }
            }
            else
            {
                agent.Velocity = Vector2.Zero;
            }
            Debug.WriteLine("Im leaving now");
        }


        private int GetClosestNode(Vector2 position, Graph graph)
        {
            float minDistance = float.MaxValue;
            int closestNodeID = -1;

            foreach (var node in graph.Nodes)
            {
                float distance = Vector2.Distance(position, node.Position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestNodeID = node.ID;
                }
            }
            return closestNodeID;
        }

        private List<Vector2> AStarPathfinding(Vector2 start, Vector2 goal, Graph graph)
        {
            int startNodeID = GetClosestNode(start, graph);
            int goalNodeID = GetClosestNode(goal, graph);

            // Check if the agent is already at the goal
            if (startNodeID == goalNodeID)
            {
                return new List<Vector2> { goal };
            }

            AStarSearch astar = new AStarSearch(graph, startNodeID, goalNodeID);

            while (!astar.IsFinished)
            {
                astar.Update(0.1f);
            }

            return astar.ShortestPath.Select(edge => graph.GetNode(edge.To).Position).ToList();
        }
    }
}


