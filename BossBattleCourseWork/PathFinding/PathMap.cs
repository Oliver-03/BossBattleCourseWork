using BossBattleCourseWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Drawing;

namespace BossBattleCourseWork
{
    public class PathMap
    {
        public Graph _graph;
        public PathMap(Graph graph) 
        {
            _graph = graph;
        }
        public void NodeCreator(List<Rectangle> Map)
        {
            for (int column = 50; column < 1500; column += 50)
            {
                for (int row = 50; row < 1000; row += 50)
                {
                    _graph.AddNode(new Vector2(column, row));
                }
            }
            List<int> nodesToRemove = new List<int>();

            foreach (Node node in _graph.Nodes)
            {
                foreach (Rectangle rectangle in Map)
                {
                    if (rectangle.IsInside(node.Position))
                    {
                        nodesToRemove.Add(node.ID);
                        break;
                    }
                }
            }
            foreach (Node node in _graph.Nodes)
            {
                foreach (Rectangle rectangle in Map)
                {
                    if (node.Position.X >= rectangle.Position.X - rectangle.Width / 2 &&
                        node.Position.X <= rectangle.Position.X + rectangle.Width / 2 &&
                        node.Position.Y >= rectangle.Position.Y - rectangle.Height / 2 &&
                        node.Position.Y <= rectangle.Position.Y + rectangle.Height / 2)
                    {
                        nodesToRemove.Add(node.ID);
                        break;
                    }
                }
            }
            foreach (int nodeId in nodesToRemove)
            {
                _graph.RemoveNode(nodeId);
            }
        }

        public void CreateEdges()
        {
            foreach (Node node in _graph.Nodes)
            {
                // Get neighboring nodes
                List<Node> neighbors = GetNeighbors(node);

                // Add edges to neighboring nodes
                foreach (Node neighbor in neighbors)
                {
                    _graph.AddEdge(node.ID, neighbor.ID);
                }
            }
        }

        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            int[] dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] dy = { -1, -1, -1, 0, 0, 1, 1, 1 };

            for (int i = 0; i < dx.Length; i++)
            {
                int neighborX = (int)node.Position.X / 50 + dx[i];
                int neighborY = (int)node.Position.Y / 50 + dy[i];

                Node neighbor = _graph.Nodes.FirstOrDefault(n =>
                    (int)n.Position.X / 50 == neighborX && (int)n.Position.Y / 50 == neighborY);

                if (neighbor != null)
                {
                    neighbors.Add(neighbor);
                }
            }

            return neighbors;
        }
    }
}
