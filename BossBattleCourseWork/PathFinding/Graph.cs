using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace BossBattleCourseWork
{
    public class Graph
    {
        private List<Node> _nodes;
        private List<Edge> _edges;

        public Graph()
        {
            _nodes = new List<Node>();
            _edges = new List<Edge>();
        }

        public IEnumerable<Node> Nodes { get { return _nodes; } }
        public IEnumerable<Edge> Edges { get { return _edges; } }

        public int NodeCount { get { return _nodes.Count; } }

        public void AddNode(Vector2 pPosition)
        {
            _nodes.Add(new Node(pPosition));
        }

        public void AddEdge(int pFromID, int pToID)
        {
            bool from = false;
            bool to = false;
            foreach (Node node in _nodes)
            {
                if (node.ID == pFromID)
                {
                    from = true;
                }

                if (node.ID == pToID)
                {
                    to = true;
                }
            }
            if (from && to)
            {
                _edges.Add(new Edge(pFromID, pToID));
            }
        }

        public Node GetNode(int pID)
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                if (_nodes[i].ID == pID)
                {
                    return _nodes[i];
                }
            }
            return null;
        }

        public Edge GetEdge(int pID)
        {
            for (int i = 0; i < _edges.Count; i++)
            {
                if (_edges[i].ID == pID)
                {
                    return _edges[i];
                }
            }
            return null;
        }

        public float GetEdgeCost(int pID)
        {
            Edge edge = GetEdge(pID);

            if (edge == null)
            {
                return -1f;
            }

            return MathF.Round((GetNode(edge.From).Position - GetNode(edge.To).Position).Length() * 0.1f, 1);
        }

        public void RemoveNode(int pID)
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                if (_nodes[i].ID == pID)
                {

                    for (int j = _edges.Count - 1; j >= 0; j--)
                    {
                        if (_edges[j].From == pID)
                        {
                            _edges.RemoveAt(j);
                        }
                    }

                    for (int j = _edges.Count - 1; j >= 0; j--)
                    {
                        if (_edges[j].To == pID)
                        {
                            _edges.RemoveAt(j);
                        }
                    }

                    _nodes.RemoveAt(i);
                    return;
                }
            }
        }

        public void RemoveEdge(int pID)
        {
            for (int i = 0; i < _edges.Count; i++)
            {
                if (_edges[i].ID == pID)
                {
                    _edges.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
