using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BossBattleCourseWork
{
    public class Node
    {
        static int _nextID = 0;

        public Vector2 Position { get; set; }
        public int ID { get; private set; }

        public Node(Vector2 pPosition)
        {
            Position = pPosition;
            ID = _nextID;
            _nextID++;
        }
    }
}
