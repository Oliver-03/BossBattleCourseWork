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

        public PursueState(Player player, Graph graph)
        {
            _player = player;
            _graph = graph;  
        }

        public override void Enter(Agent agent)
        {
            
        }

        public override void Execute(Agent agent, GameTime gameTime)
        {
            Vector2 direction = _player.Position - agent.Position;
            direction.Normalize();

            List<Vector2> path = AStarPathfinding(agent.Position, _player.Position, _graph);

            if (path.Count > 0)
            {
                Vector2 nextWaypoint = path[0];
                Vector2 desiredVelocity = Vector2.Normalize(nextWaypoint - agent.Position) * agent.Speed;
                agent.Velocity = desiredVelocity;
                Debug.WriteLine(desiredVelocity);
            }
            else
            {
                agent.Velocity = Vector2.Zero;
            }
        }

        public override void Exit(Agent agent)
        {

        }

        private List<Vector2> AStarPathfinding(Vector2 start, Vector2 goal, Graph graph)
        {
            AStarSearch astar = new AStarSearch(graph, GetClosestNode(start, graph), GetClosestNode(goal, graph));

            while (!astar.IsFinished)
            {
                astar.Update(0.1f); 
            }

            return astar.ShortestPath.Select(edge => graph.GetNode(edge.To).Position).ToList();
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
    }
}


